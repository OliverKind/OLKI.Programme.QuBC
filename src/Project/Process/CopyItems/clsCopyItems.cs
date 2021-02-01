/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
 * License:     LGPL
 * 
 * Desctiption:
 * Copy item to backup or to resotre
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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace OLKI.Programme.QuBC.src.Project.Process
{
    /// <summary>
    /// Provides tools to write logfiles
    /// </summary>
    internal partial class CopyItems
    {
        #region Constants
        /// <summary>
        /// Exception code for full disc
        /// </summary>
        public const int EXCEPTION_FULL_DISC = unchecked((int)0x80070070);
        #endregion

        #region Properties
        /// <summary>
        /// The application MainForm
        /// </summary>
        private readonly MainForm.MainForm _mainFoorm;

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
        public CopyItems(MainForm.MainForm mainFoorm)
        {
            this._mainFoorm = mainFoorm;
        }

        /// <summary>
        /// Copy items in backup mode
        /// </summary>
        /// <param name="worker">BackgroundWorker for copy</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        internal void Backup(BackgroundWorker worker, DoWorkEventArgs e, ProgressStore progressStore)
        {
            //Initial progress
            this._progress = progressStore;
            this._progress.TotalBytes.ActualValue = 0;
            this._progress.TotalDirectories.ActualValue = 0;
            this._progress.TotalFiles.ActualValue = 0;

            // Create main target directory
            ProcessException CreateTargetDirectoryException = new ProcessException();
            if (!this.CreateRootDirectory(this._project.Settings.ControleBackup.Directory.Path, worker, e)) return;

            // Copy content of selected directories
            foreach (KeyValuePair<string, Project.DirectoryScope> item in this._project.ToBackupDirectorys.OrderBy(o => o.Key))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return; }

                // Copy Recursive    item.Key => sourceDirectory    item.Value = scope
                if (this.CopyRecursive(CopyMode.Backup, item.Key, item.Value, worker, e, out Exception ex) == ProcessException.ExceptionLevel.Critical)
                {
                    e.Cancel = true;
                    worker.CancelAsync();
                    return;
                }
                worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy, new ProgressState(this._progress, true));
            }
            worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy, new ProgressState(this._progress, true));
        }

        /// <summary>
        /// Copy items in restore mode
        /// </summary>
        /// <param name="worker">BackgroundWorker for copy</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        internal void Restore(BackgroundWorker worker, DoWorkEventArgs e, ProgressStore progressStore)
        {
            if (worker is null) throw new ArgumentNullException(nameof(worker));
            if (e is null) throw new ArgumentNullException(nameof(e));
            if (progressStore is null) throw new ArgumentNullException(nameof(progressStore));
            //TODO: ADD CODE --> in future version to restore Backup
            throw new Exception("OLKI.Programme.QuBC.BackupProject.Process.CopyItems.Restore has no active code");
        }

        #region Create Root Directorys Items
        /// <summary>
        /// Create a new root directroy for creating backups
        /// </summary>
        /// <param name="targetDirectory">A string that specifice the target directory path to create</param>
        /// <param name="worker">BackgroundWorker for copy</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <returns>True if the directroy was created sucessfully</returns>
        private bool CreateRootDirectory(string targetDirectory, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(targetDirectory);
                return true;
            }
            catch (Exception ex)
            {
                ProcessException Exception = new ProcessException
                {
                    Description = Properties.Stringtable._0x001E,
                    Exception = ex,
                    Level = ProcessException.ExceptionLevel.Critical,
                    Source = "",
                    Target = targetDirectory
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, new ProgressState(this._progress, true));

                e.Cancel = true;
                worker.CancelAsync();
                return false;
            }
        }
        #endregion
        #endregion
    }
}