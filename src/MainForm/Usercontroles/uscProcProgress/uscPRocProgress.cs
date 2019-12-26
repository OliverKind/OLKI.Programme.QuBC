/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * User defined controle to show the progress of creating a backup or restore it
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

using OLKI.Tools.CommonTools.DirectoryAndFile;
using System;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.MainForm.Usercontroles.uscProgress
{
    public partial class ProcProgress : UserControl
    {
        #region Constants
        /// <summary>
        /// Defines the SelectedIndex of this.cboAllByteNum by default
        /// </summary>
        private const int DEBAULT_COMBOBOX_ALL_BYTE_NUM_SELECTED_INDEX = 3;
        /// <summary>
        /// Defines the SelectedIndex of this.cboActualFileByteNum by default
        /// </summary>
        private const int DEBAULT_COMBOBOX_ACTUAL_BYTE_NUM_SELECTED_INDEX = 1;
        /// <summary>
        /// Defines the format for a actual and maximum value
        /// </summary>
        private const string FORMAT_ACTUAL_MAX_VALUE = "{0} / {1}";
        /// <summary>
        /// Defines the format to show time values
        /// </summary>
        private const string FORMAT_TIME = @"hh\:mm\:ss";
        /// <summary>
        /// Defines the format for a string with a percentage number value
        /// </summary>
        private const string FORMAT_PERCENTAGE = @"{0}%";
        /// <summary>
        /// Defines the format for a string with a value number value
        /// </summary>
        const string FORMAT_VALUE = "{0:n0}";
        #endregion

        #region Fields
        /// <summary>
        /// Contains all information for the Progress of a process
        /// </summary>
        public BackupProject.Process.ProgressStore ProgressStore = new BackupProject.Process.ProgressStore();
        #endregion

        #region Properties
        /// <summary>
        /// The time where the progress started
        /// </summary>
        private DateTime _progressStart = new DateTime();
        /// <summary>
        /// Get the elapsed time, since the progress started
        /// </summary>
        private TimeSpan ElapsedTime
        {
            get
            {
                return DateTime.Now - this._progressStart; ;
            }
        }

        /// <summary>
        /// Object to set progress controles
        /// </summary>
        private readonly SetProgress _setProgressStates = null;
        /// <summary>
        /// Get tehe object to set progress controles
        /// </summary>
        public SetProgress SetProgressStates
        {
            get
            {
                return this._setProgressStates;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new ProcProgress controle
        /// </summary>
        public ProcProgress()
        {
            InitializeComponent();

            this._setProgressStates = new SetProgress(this);

            // Initial size dimension comboboxes
            FileSize.SetDimensionlistToComboBox(this.cboActualFileByteNum, FileSize.ByteBase.IEC, FileSize.ByteBase.SI, true, true);
            this.cboActualFileByteNum.SelectedIndex = DEBAULT_COMBOBOX_ACTUAL_BYTE_NUM_SELECTED_INDEX;
            FileSize.SetDimensionlistToComboBox(this.cboAllByteNum, FileSize.ByteBase.IEC, FileSize.ByteBase.SI, true, true);
            this.cboAllByteNum.SelectedIndex = DEBAULT_COMBOBOX_ALL_BYTE_NUM_SELECTED_INDEX;
        }

        /// <summary>
        /// Report ony in Time Interval or by process percentage
        /// </summary>
        /// <param name="lastUpdate">The time when the last update of the progress controles was done</param>
        /// <returns>True if an update of the controles should been done</returns>
        public bool CheckUpdateInterval(DateTime lastUpdate)
        {
            // Check by TimeSpan
            TimeSpan TimeSpan = DateTime.Now - lastUpdate;
            double TimeSinceLastUpdate = TimeSpan.TotalMilliseconds / 1000;
            if (TimeSinceLastUpdate >= (double)this.nudUpdateInterval.Value) return true;

            //No update requested
            return false;
        }
        #endregion
    }
}