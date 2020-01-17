/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
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

using OLKI.Programme.QBC.MainForm.Usercontroles.uscProcControle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OLKI.Programme.QBC.BackupProject.Process
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
        /// Set the progresstore for counting items
        /// </summary>
        internal ProgressStore Progress
        {
            set
            {
                this._progress = value;
            }
        }

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
        internal void Backup(BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Initial Progress Store
            this._progress.DirectroyFiles.ActualValue = 0;
            this._progress.DirectroyFiles.MaxValue = this._project.ToBackupDirectorys.Count;
            this._progress.TotalDirectories.MaxValue = 0;
            this._progress.TotalFiles.MaxValue = 0;
            this._progress.TotalBytes.MaxValue = 0;

            worker.ReportProgress((int)ProcControle.ProcessStep.Count_Busy, ProcControle.FORCE_REPORTING_FLAG);

            // Count content of selected directories
            foreach (KeyValuePair<string, Project.DirectoryScope> item in this._project.ToBackupDirectorys.OrderBy(o => o.Key))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; break; }

                //Report Directory
                this._progress.DirectroyFiles.ElemenName = item.Key;
                worker.ReportProgress((int)ProcControle.ProcessStep.Count_Busy, ProcControle.FORCE_REPORTING_FLAG);

                // Search Recursive
                this.CountRecursive(item.Key, item.Value, worker, e);

                //Report Progress
                if (worker.CancellationPending) { e.Cancel = true; break; }
                this._progress.DirectroyFiles.ActualValue++;
                worker.ReportProgress((int)ProcControle.ProcessStep.Count_Busy, ProcControle.FORCE_REPORTING_FLAG);
            }
            worker.ReportProgress((int)ProcControle.ProcessStep.Count_Busy, MainForm.Usercontroles.uscProcControle.ProcControle.FORCE_REPORTING_FLAG);
        }

        /// <summary>
        /// Count items in restore mode
        /// </summary>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        internal void Restore(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (worker is null) throw new ArgumentNullException(nameof(worker));
            if (e is null) throw new ArgumentNullException(nameof(e));
            //TODO: ADD CODE --> in future version to restore Backup
            throw new Exception("OLKI.Programme.QBC.BackupProject.Process.CountItems.Restore has no active code");
        }
        #endregion
    }
}