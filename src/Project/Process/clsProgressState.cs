/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
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

namespace OLKI.Programme.QBC.BackupProject.Process
{
    public class ProgressState
    {
        #region Constants
        private const bool DEFAULT_FORCE_REPORT_PROGRRESS = false;
        #endregion

        #region Properties
        private readonly ProgressStore _progressStore;
        public ProgressStore ProgressStore
        {
            get
            {
                return this._progressStore;
            }
        }

        public bool ForceReportProgress { get; set; } = DEFAULT_FORCE_REPORT_PROGRRESS;
        #endregion

        #region Methodes
        public ProgressState(ProgressStore progressStore) : this(progressStore, DEFAULT_FORCE_REPORT_PROGRRESS)
        {
        }
        public ProgressState(bool forceReportProgress) : this(null, forceReportProgress)
        {
        }
        public ProgressState(ProgressStore progressStore, bool forceReportProgress)
        {
            if (progressStore != null) this._progressStore = progressStore.Clone();
            this.ForceReportProgress = forceReportProgress;
        }
        #endregion
    }
}