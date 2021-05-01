/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Count all items and bytes to write an backup or resture it
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

using OLKI.Programme.QuBC.MainForm.Usercontroles.uscTaskControle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace OLKI.Programme.QuBC.Project.Task
{
    /// <summary>
    /// Provides tools to write logfiles
    /// </summary>
    internal partial class CountItems
    {
        #region Properties
        /// <summary>
        /// The progresstore for counting items
        /// </summary>
        private ProgressStore _progress = null;

        /// <summary>
        /// The project for counting items
        /// </summary>
        private Project _project = null;
        /// <summary>
        /// Set the project for counting items
        /// </summary>
        internal Project Project
        {
            set
            {
                this._project = value;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise an new count item object
        /// </summary>
        public CountItems()
        {
        }

        /// <summary>
        /// Count items in backup mode
        /// </summary>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        internal void Backup(BackgroundWorker worker, DoWorkEventArgs e, ProgressStore progressStore)
        {
            // Initial Progress Store
            this._progress = progressStore;
            this._progress.DirectroyFiles.ActualValue = 0;
            this._progress.DirectroyFiles.MaxValue = this._project.ToBackupDirectorys.Count;
            this._progress.TotalDirectories.MaxValue = 0;
            this._progress.TotalFiles.MaxValue = 0;
            this._progress.TotalBytes.MaxValue = 0;

            worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));

            // Count content of selected directories
            foreach (KeyValuePair<string, Project.DirectoryScope> item in this._project.ToBackupDirectorys.OrderBy(o => o.Key))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; break; }

                //Report Directory
                this._progress.DirectroyFiles.ElemenName = item.Key;
                worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));

                // Search Recursive
                this.CountRecursive(item.Key, item.Value, worker, e);

                //Report Progress
                if (worker.CancellationPending) { e.Cancel = true; break; }
                this._progress.DirectroyFiles.ActualValue++;
                worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));
            }
            worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));
        }

        /// <summary>
        /// Count items in restore mode
        /// </summary>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        internal void Restore(BackgroundWorker worker, DoWorkEventArgs e, ProgressStore progressStore)
        {
            // Initial Progress Store
            this._progress = progressStore;
            this._progress.TotalDirectories.MaxValue = 0;
            this._progress.TotalFiles.MaxValue = 0;
            this._progress.TotalBytes.MaxValue = 0;

            worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));

            if (this._project.Settings.ControleRestore.Directory.CreateDriveDirectroy)
            {
                DirectoryInfo Source = new DirectoryInfo(this._project.Settings.ControleRestore.Directory.Path);
                foreach (DirectoryInfo DriveDirectory in Source.GetDirectories().OrderBy(o => o.Name))
                {
                    // Check for abbort
                    if (worker.CancellationPending) { e.Cancel = true; return; }

                    //Report Directory
                    this._progress.DirectroyFiles.ElemenName = DriveDirectory.FullName;
                    worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));

                    // Search Recursive
                    this.CountRecursive(DriveDirectory.FullName, Project.DirectoryScope.All, worker, e);

                    //Report Progress
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    this._progress.DirectroyFiles.ActualValue++;
                    worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));
                }
            }
            else
            {
                this.CountRecursive(this._project.Settings.ControleRestore.Directory.Path, Project.DirectoryScope.All, worker, e);
            }
            worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress, true));
        }
        #endregion
    }
}