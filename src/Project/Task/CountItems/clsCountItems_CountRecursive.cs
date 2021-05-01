/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Count recursive all items and bytes to write an backup or resture it
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
        #region Methodes
        #region Count CountRecursive
        /// <summary>
        /// Count recursive all elements in directory an subdirectory, sepending of the scope
        /// </summary>
        /// <param name="directory">A string that specifice the directory to count</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        private void CountRecursive(string directory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e)
        {
            this.CountRecursive(new DirectoryInfo(directory), scope, worker, e);
        }
        /// <summary>
        /// Count recursive all elements in directory an subdirectory, sepending of the scope
        /// </summary>
        /// <param name="directory">Directory to count</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        private void CountRecursive(DirectoryInfo directory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                //Check for existing directory
                if (!directory.Exists) return;

                // Report Progress
                this._progress.TotalDirectories.MaxValue++;
                this._progress.DirectroyFiles.ElemenName = directory.FullName;
                worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress));

                //Count Elements
                switch (scope)
                {
                    case Project.DirectoryScope.All:
                        this.CountRecursive_All(directory, scope, worker, e);

                        // Search for files in sub directorys
                        foreach (DirectoryInfo DirectoryItem in directory.GetDirectories().OrderBy(o => o.Name))
                        {
                            // Check for abbort
                            if (worker.CancellationPending) { e.Cancel = true; return; }
                            this.CountRecursive(DirectoryItem, scope, worker, e);
                        }
                        break;
                    case Project.DirectoryScope.Nothing:
                        this.CountRecursive_Nothing(directory, scope, worker, e);
                        break;
                    case Project.DirectoryScope.Selected:
                        this.CountRecursive_Selected(directory, scope, worker, e);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

#pragma warning disable IDE0060 // Remove unused parameter --> Unused parameters to have consistently function call
        /// <summary>
        /// Count all directroy, subdirectory and files
        /// </summary>
        /// <param name="directory">Directory to count</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        private void CountRecursive_All(DirectoryInfo directory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Search for files in selected directory
            foreach (FileInfo FileItem in directory.GetFiles().OrderBy(o => o.Name))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return; }

                //Count up Files an Bytes
                this._progress.TotalFiles.MaxValue++;
                this._progress.TotalBytes.MaxValue += FileItem.Length;

                //Report Progress
                worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress));
            }
        }

        /// <summary>
        /// Count no  directroy, subdirectory and files
        /// </summary>
        /// <param name="directory">Directory to count</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        private void CountRecursive_Nothing(DirectoryInfo directory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //Absolutly nothing to do
            return;
        }

        /// <summary>
        /// Count all selected files in directroy
        /// </summary>
        /// <param name="directory">Directory to count</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        private void CountRecursive_Selected(DirectoryInfo directory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //this._progress.TotalDirectories.MaxValue++;
            //Leafe if the key didn't exists or if no files are selected
            if (!this._project.ToBackupFiles.ContainsKey(directory.FullName) || this._project.ToBackupFiles[directory.FullName].Count == 0) return;

            // Search for files in selected directory
            foreach (System.IO.FileInfo FileItem in directory.GetFiles().OrderBy(o => o.Name))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return; }

                //Count up Files an Bytes if file is selected
                if (this._project.ToBackupFiles[directory.FullName].Contains(FileItem.FullName))
                {
                    this._progress.TotalFiles.MaxValue++;
                    this._progress.TotalBytes.MaxValue += FileItem.Length;
                }
                //Report Progress
                worker.ReportProgress((int)TaskControle.TaskStep.Count_Busy, new ProgressState(this._progress));
            }
        }
#pragma warning restore IDE0060 // Remove unused parameter
        #endregion
        #endregion
    }
}