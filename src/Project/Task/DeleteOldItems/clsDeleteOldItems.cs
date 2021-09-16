/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
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

using OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscTaskControle;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace OLKI.Programme.QuBC.src.Project.Task
{
    /// <summary>
    /// Provides tools to delete old items ins target directory
    /// </summary>
    internal partial class DeleteOldItems
    {
        #region Constants
        /// <summary>
        /// Dummy for a drive directory
        /// </summary>
        private const string DUMMY_DRIVE_PATH = @"\a\";
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
        /// <returns>True if Delete was sucessfull, False if an critical exception was thrown</returns>
        internal bool Backup(BackgroundWorker worker, DoWorkEventArgs e, ProgressStore progressStore)
        {
            //Initial progress
            this._progress = progressStore;

            DirectoryInfo Target = new DirectoryInfo(this._project.Settings.ControleBackup.Directory.Path);

            int PathStartIndex = OLKI.Toolbox.DirectoryAndFile.Path.Repair(Target.FullName + DUMMY_DRIVE_PATH).Length;
            foreach (DirectoryInfo DriveDirectory in Target.GetDirectories().OrderBy(o => o.Name))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return true; }

                this._progress.DirectroyFiles.ElemenName = DriveDirectory.FullName;
                worker.ReportProgress((int)TaskControle.TaskStep.DeleteOldItems_Busy, new ProgressState(this._progress, true));

                if (this.DeleteRecursive(DriveDirectory.Name, DriveDirectory, PathStartIndex, worker, e, out Exception ex) == TaskException.ExceptionLevel.Critical)
                {
                    e.Cancel = true;
                    worker.CancelAsync();
                    return false;
                }
            }
            worker.ReportProgress((int)TaskControle.TaskStep.DeleteOldItems_Busy, new ProgressState(this._progress, true));
            return true;
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
        private TaskException.ExceptionLevel DeleteRecursive(string driveName, DirectoryInfo targetDirectory, int pathStartIndex, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            exception = null;
            DirectoryInfo CheckDirectory;
            FileInfo CheckFile;
            string SourcePath = driveName + @":\";
            try
            {
                if (!new DirectoryInfo(SourcePath).Exists) return TaskException.ExceptionLevel.NoException;
                if (targetDirectory.FullName.Length >= pathStartIndex) SourcePath += targetDirectory.FullName.Substring(pathStartIndex);

                this._progress.DirectroyFiles.ElemenName = targetDirectory.FullName;
                worker.ReportProgress((int)TaskControle.TaskStep.DeleteOldItems_Busy, new ProgressState(this._progress, false));

                // Search for files in selected directory
                foreach (FileInfo FileItem in targetDirectory.GetFiles().OrderBy(o => o.Name))
                {
                    // Check for abbort
                    if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }

                    this._progress.FileBytes.ElemenName = FileItem.FullName;
                    worker.ReportProgress((int)TaskControle.TaskStep.DeleteOldItems_Busy, new ProgressState(this._progress, false));

                    CheckFile = new FileInfo(OLKI.Toolbox.DirectoryAndFile.Path.Repair(SourcePath + @"\" + FileItem.Name));
                    if (!CheckFile.Exists)
                    {
                        this.DeleteFile(FileItem, worker);
                    }
                }

                //Sub directories
                foreach (DirectoryInfo DirectoryItem in targetDirectory.GetDirectories().OrderBy(o => o.Name))
                {
                    // Check for abbort
                    if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }

                    // If dirextory exists deeper, sonst löschjem
                    CheckDirectory = new DirectoryInfo(OLKI.Toolbox.DirectoryAndFile.Path.Repair(SourcePath + @"\" + DirectoryItem.Name));
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
                if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }
                return TaskException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                this._progress.Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0020,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Critical,
                    Source = targetDirectory.FullName,
                    Target = ""
                };
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return TaskException.ExceptionLevel.Critical;
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
                File.SetAttributes(file.FullName, FileAttributes.Normal);
                file.Delete();
            }
            catch (Exception ex)
            {
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0020,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Slight,
                    Source = file.FullName,
                    Target = ""
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));
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
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0020,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Slight,
                    Source = directory.FullName,
                    Target = ""
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));
            }
        }
    }
    #endregion
}