/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * A class to send the progress state
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

namespace OLKI.Programme.QuBC.src.Project.Task
{
    /// <summary>
    /// A class to send the progress state
    /// </summary>
    public class ProgressState
    {
        #region Constants
        /// <summary>
        /// Defines if a progress report should been forced by default
        /// </summary>
        private const bool DEFAULT_FORCE_REPORT_PROGRRESS = false;
        #endregion

        #region Properties
        /// <summary>
        /// Get the stored progess data
        /// </summary>
        public ProgressStore ProgressStore { get; }

        /// <summary>
        /// Get or set if the progress report should been forced
        /// </summary>
        public bool ForceReportProgress { get; set; } = DEFAULT_FORCE_REPORT_PROGRRESS;
        #endregion

        #region Methodes
        /// <summary>
        /// Iniital a new ProgressState with ProgressStore
        /// </summary>
        /// <param name="progressStore">The prograss store</param>
        public ProgressState(ProgressStore progressStore) : this(progressStore, DEFAULT_FORCE_REPORT_PROGRRESS)
        {
        }
        /// <summary>
        /// Iniital a new ProgressState withoud ProgressStore
        /// </summary>
        /// <param name="forceReportProgress">Should reporting the prgoress been forced</param>
        public ProgressState(bool forceReportProgress) : this(null, forceReportProgress)
        {
        }
        /// <summary>
        /// Iniital a new ProgressState with ProgressStore
        /// </summary>
        /// <param name="progressStore">The prograss store</param>
        /// <param name="forceReportProgress">Should reporting the prgoress been forced</param>
        public ProgressState(ProgressStore progressStore, bool forceReportProgress)
        {
            if (progressStore != null) this.ProgressStore = progressStore.Clone();
            this.ForceReportProgress = forceReportProgress;
        }
        #endregion
    }
}