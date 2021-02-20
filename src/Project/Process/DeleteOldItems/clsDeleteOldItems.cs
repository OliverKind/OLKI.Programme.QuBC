﻿/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
 * License:     LGPL
 * 
 * Desctiption:
 * Tools to delete old items ins target directory
 * 
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the LGPL General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * LGPL General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not check the GitHub-Repository.
 * 
 * */

using OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscProcControle;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace OLKI.Programme.QuBC.src.Project.Process
{
    /// <summary>
    /// Provides tools to delete old items ins target directory
    /// </summary>
    internal partial class DeleteOldItems
    {
        #region Constants
        /// <summary>
        /// Exception code for full disc
        /// </summary>
        public const int EXCEPTION_FULL_DISC = unchecked((int)0x80070070);
        #endregion

        #region Properties
        /// <summary>
        /// The progresstore for copy items
        /// </summary>
        private ProgressStore _progress = null;

        /// <summary>
        /// The project for copy items
        /// </summary>
        private Project _project = null;
        /// <summary>
        /// Set the project for copy items
        /// </summary>
        internal Project Project
        {
            set
            {
                this._project = value;
            }
        }
        #endregion

        #region Enums
        /// <summary>
        /// Mode how to run the copy process
        /// </summary>
        public enum CopyMode
        {
            Backup,
            Restore
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise an new copy item object
        /// </summary>
        public DeleteOldItems()
        {
        }

        /// <summary>
        /// Copy items in backup mode
        /// </summary>
        /// <param name="worker">BackgroundWorker for copy</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        internal void Backup(BackgroundWorker worker, DoWorkEventArgs e, ProgressStore progressStore)
        {
            string DummyDrivePath = @"\a\";

            //Initial progress
            this._progress = progressStore;

            DirectoryInfo Target = new DirectoryInfo(this._project.Settings.ControleBackup.Directory.Path);

            int PathStartIndex = Tools.CommonTools.DirectoryAndFile.Path.Repair(Target.FullName + DummyDrivePath).Length;
            foreach (DirectoryInfo DriveDirectory in Target.GetDirectories().OrderBy(o => o.Name))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return; }

                this._progress.DirectroyFiles.ElemenName = DriveDirectory.FullName;
                worker.ReportProgress((int)ProcControle.ProcessStep.DeleteOldItems_Busy, new ProgressState(this._progress, true));

                if (this.DeleteRecursive(DriveDirectory.Name, DriveDirectory, PathStartIndex, worker, e, out Exception ex) == ProcessException.ExceptionLevel.Critical)
                {
                    e.Cancel = true;
                    worker.CancelAsync();
                    return;
                }
            }
            worker.ReportProgress((int)ProcControle.ProcessStep.DeleteOldItems_Busy, new ProgressState(this._progress, true));
        }

        /// <summary>
        /// Delte recursive all items in directory an subdirectory they are not in backup Source directory
        /// </summary>
        /// <param name="driveName">the Name of the target drive</param>
        /// <param name="targetDirectory">Target directroy to check for delete items</param>
        /// <param name="pathStartIndex">Index in Path to Identify Drive</param>
        /// <param name="worker">BackgroundWorker for delte</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the delete process</returns>
        private ProcessException.ExceptionLevel DeleteRecursive(string driveName, DirectoryInfo targetDirectory, int pathStartIndex, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            exception = null;
            DirectoryInfo CheckDirectory;
            FileInfo CheckFile;
            string SourcePath = driveName + @":\";
            try
            {
                if (!new DirectoryInfo(SourcePath).Exists) return ProcessException.ExceptionLevel.NoException;
                if (targetDirectory.FullName.Length >= pathStartIndex) SourcePath += targetDirectory.FullName.Substring(pathStartIndex);

                this._progress.DirectroyFiles.ElemenName = targetDirectory.FullName;
                worker.ReportProgress((int)ProcControle.ProcessStep.DeleteOldItems_Busy, new ProgressState(this._progress, false));

                // Search for files in selected directory
                foreach (FileInfo FileItem in targetDirectory.GetFiles().OrderBy(o => o.Name))
                {
                    // Check for abbort
                    if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }

                    this._progress.FileBytes.ElemenName = FileItem.FullName;
                    worker.ReportProgress((int)ProcControle.ProcessStep.DeleteOldItems_Busy, new ProgressState(this._progress, false));

                    CheckFile = new FileInfo(OLKI.Tools.CommonTools.DirectoryAndFile.Path.Repair(SourcePath + @"\" + FileItem.Name));
                    if (!CheckFile.Exists)
                    {
                        this.DeleteFile(FileItem, worker);
                    }
                }

                //Sub directories
                foreach (DirectoryInfo DirectoryItem in targetDirectory.GetDirectories().OrderBy(o => o.Name))
                {
                    // Check for abbort
                    if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }

                    // If dirextory exists deeper, sonst löschjem
                    CheckDirectory = new DirectoryInfo(OLKI.Tools.CommonTools.DirectoryAndFile.Path.Repair(SourcePath + @"\" + DirectoryItem.Name));
                    if (!CheckDirectory.Exists)
                    {
                        this.DeleteDirectory(DirectoryItem, worker);
                    }
                    else
                    {
                        this.DeleteRecursive(driveName, DirectoryItem, pathStartIndex, worker, e, out exception);
                    }
                }

                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }
                return ProcessException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                this._progress.Exception = new ProcessException
                {
                    Description = Properties.Stringtable._0x0020,
                    Exception = ex,
                    Level = ProcessException.ExceptionLevel.Critical,
                    Source = targetDirectory.FullName,
                    Target = ""
                };
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, new ProgressState(this._progress, true));

                return ProcessException.ExceptionLevel.Critical;
            }
        }

        /// <summary>
        /// Delete the specified file
        /// </summary>
        /// <param name="file">File to delete</param>
        /// <param name="worker">BackgroundWorker for delte</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        private void DeleteFile(FileInfo file, BackgroundWorker worker)
        {
            try
            {
                file.Delete();
            }
            catch (Exception ex)
            {
                ProcessException Exception = new ProcessException
                {
                    Description = Properties.Stringtable._0x0020,
                    Exception = ex,
                    Level = ProcessException.ExceptionLevel.Slight,
                    Source = file.FullName,
                    Target = ""
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, new ProgressState(this._progress, true));
            }
        }

        /// <summary>
        /// Delete the specified directory
        /// </summary>
        /// <param name="directory">File to delete</param>
        /// <param name="worker">BackgroundWorker for delte</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        private void DeleteDirectory(DirectoryInfo directory, BackgroundWorker worker)
        {
            try
            {
                directory.Delete(true);
            }
            catch (Exception ex)
            {
                ProcessException Exception = new ProcessException
                {
                    Description = Properties.Stringtable._0x0020,
                    Exception = ex,
                    Level = ProcessException.ExceptionLevel.Slight,
                    Source = directory.FullName,
                    Target = ""
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, new ProgressState(this._progress, true));
            }
        }
    }
    #endregion
}