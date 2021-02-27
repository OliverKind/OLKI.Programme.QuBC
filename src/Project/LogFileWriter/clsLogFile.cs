/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
 * License:     LGPL
 * 
 * Desctiption:
 * Provide tools to write a Logfile for create an backup or restore it
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
using OLKI.Programme.QuBC.src.Project.Process;
using System;

namespace OLKI.Programme.QuBC.src.Project.LogFileWriter
{
    public partial class LogFile
    {
        #region Constants
        /// <summary>
        /// Specifies the default indent
        /// </summary>
        private const int DEFAULT_INDENT = 0;
        #endregion

        #region Fields
        /// <summary>
        /// Should an exception message been shown, if an exception was thrown
        /// </summary>
        private bool _showExceptionMessage = false;
        /// <summary>
        /// The store for the progress
        /// </summary>
        private readonly ProgressStore _progressStore = null;
        /// <summary>
        /// Controle to controle the backup or restore process
        /// </summary>
        private readonly ProcControle _procControle;
        /// <summary>
        /// Should Logfiles been written. If it is false, no Logfiles will be created.
        /// </summary>
        private bool _writeLogFile;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the the path where the log file should been created
        /// </summary>
        public string LogFilePath { get; set; } = string.Empty;
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new LogFile-Writer
        /// </summary>
        /// <param name="procControle">Controle to controle the backup or restore process</param>
        /// <param name="progressStore">The store for the progress</param>
        /// <param name="writeLogFile">Should Logfiles been written. If it is false, no Logfiles will be created.</param>
        public LogFile(ProcControle procControle, ProgressStore progressStore, bool writeLogFile)
        {
            this._progressStore = progressStore;
            this._procControle = procControle;
            this._writeLogFile = writeLogFile;
        }

        /// <summary>
        /// Check if the Logfile Line could been written: writeLogFile is true, LogFilePath is not empth;
        /// </summary>
        /// <returns>True if the Logfile Line should, could been written or false</returns>
        private bool CheckWriteLine()
        {
            if (!this._writeLogFile) return false;
            if (string.IsNullOrEmpty(this.LogFilePath)) return false;
            return true;
        }

        /// <summary>
        /// Write Template-Text to logfile: Process Canceled
        /// </summary>
        public void WriteCancel()
        {
            if (!CheckWriteLine()) return;
            this.WriteLogLine(string.Format(LogTemplates.WriteCancel, DateTime.Now));
        }

        /// <summary>
        /// Write Template-Text to logfile: Count Start
        /// </summary>
        public void WriteCountStart()
        {
            if (!CheckWriteLine()) return;
            this.WriteLogLine(string.Format(LogTemplates.WriteCountStart, DateTime.Now));
        }

        /// <summary>
        /// Write Template-Text to logfile: Count Finish
        /// </summary>
        public void WriteCountFinish()
        {
            if (!CheckWriteLine()) return;
            object[] Args = new object[4];
            Args[0] = DateTime.Now;
            Args[1] = this._progressStore.TotalDirectories.MaxValue;
            Args[2] = this._progressStore.TotalFiles.MaxValue;
            Args[3] = this._progressStore.TotalBytes.MaxValue;

            this.WriteLogLine(string.Format(LogTemplates.WriteCountFinish, Args));
        }

        /// <summary>
        /// Write Template-Text to logfile: Copy Start
        /// </summary>
        public void WriteCopyStart()
        {
            if (!CheckWriteLine()) return;
            this.WriteLogLine(string.Format(LogTemplates.WriteCopyStart, DateTime.Now));
        }

        /// <summary>
        /// Write Template-Text to logfile: Copy Finish
        /// </summary>
        public void WriteCopyFinish()
        {
            if (!CheckWriteLine()) return;
            object[] Args = new object[4];
            Args[0] = DateTime.Now;
            Args[1] = this._progressStore.TotalDirectories.ActualValue;
            Args[2] = this._progressStore.TotalFiles.ActualValue;
            Args[3] = this._progressStore.TotalBytes.ActualValue;

            this.WriteLogLine(string.Format(LogTemplates.WriteCopyFinish, Args));
        }

        /// <summary>
        /// Write Template-Text to logfile: Delete Start
        /// </summary>
        public void WriteDeleteSart()
        {
            if (!CheckWriteLine()) return;
            this.WriteLogLine(string.Format(LogTemplates.WriteDeleteSart, DateTime.Now));
        }

        /// <summary>
        /// Write Template-Text to logfile: Delete Finish
        /// </summary>
        public void WriteDeleteFinish()
        {
            if (!CheckWriteLine()) return;
            this.WriteLogLine(string.Format(LogTemplates.WriteDeleteFinish, DateTime.Now));
        }

        /// <summary>
        /// Write Template-Text to logfile: Process Exception
        /// </summary>
        /// <param name="exception">Exception that was thrown</param>
        public void WriteException(ProcessException exception)
        {
            if (!CheckWriteLine()) return;
            object[] Args = new object[4];
            Args[0] = DateTime.Now;
            Args[1] = exception.Source;
            Args[2] = exception.Target;
            Args[3] = exception.Text;

            this.WriteLogLine(string.Format(LogTemplates.WriteException, Args));
        }

        /// <summary>
        /// Write Template-Text to logfile: Process Finished
        /// </summary>
        public void WriteFoot()
        {
            if (!CheckWriteLine()) return;
            this.WriteLogLine(string.Format(LogTemplates.WriteFoot, DateTime.Now));
        }

        /// <summary>
        /// Write Template-Text to logfile: Process Started
        /// </summary>
        public void WriteHead()
        {
            if (!CheckWriteLine()) return;
            object[] Args = new object[15];
            switch (this._procControle.Mode)
            {
                case ProcControle.ControleMode.CreateBackup:
                    Args[0] = "X";
                    Args[1] = " ";
                    Args[2] = DateTime.Now;
                    Args[3] = this._procControle.txtDirectory.Text;
                    Args[4] = this._procControle.chkRootDirectory.Checked ? "X" : " ";
                    Args[5] = this._procControle.txtHandleExistingFileText.Text;
                    Args[6] = this._procControle.chkCountItemsAndBytes.Checked ? "X" : " ";
                    Args[7] = this._procControle.chkCopyData.Checked ? "X" : " ";
                    Args[8] = this._procControle.chkDeleteOld.Checked ? "X" : " ";
                    Args[9] = "";
                    Args[10] = " ";
                    Args[11] = "";
                    Args[12] = "";
                    Args[13] = " ";
                    Args[14] = " ";
                    break;
                case ProcControle.ControleMode.RestoreBackup:
                    Args[0] = " ";
                    Args[1] = "X";
                    Args[2] = DateTime.Now;
                    Args[3] = "";
                    Args[4] = " ";
                    Args[5] = "";
                    Args[6] = " ";
                    Args[7] = " ";
                    Args[8] = " ";
                    Args[9] = this._procControle.txtDirectory.Text;
                    Args[10] = this._procControle.chkRootDirectory.Checked ? "X" : " ";
                    Args[11] = this._procControle.txtRestoreTargetDirectory.Text;
                    Args[12] = this._procControle.txtHandleExistingFileText.Text;
                    Args[13] = this._procControle.chkCountItemsAndBytes.Checked ? "X" : " ";
                    Args[14] = this._procControle.chkCopyData.Checked ? "X" : " ";
                    break;
                default:
                    throw new ArgumentException();
            }

            this.WriteLogLine(string.Format(LogTemplates.WriteHead, Args));
        }
        #endregion
    }
}