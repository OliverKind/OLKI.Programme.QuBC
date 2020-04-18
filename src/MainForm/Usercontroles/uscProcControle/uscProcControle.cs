/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * User defined controle to controle the backup and restore process
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
using OLKI.Programme.QBC.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.MainForm.Usercontroles.uscProcControle
{
    /// <summary>
    /// Controle to controle the backup or restore process
    /// </summary>
    public partial class ProcControle : UserControl
    {
        #region Constants
        /// <summary>
        /// A flag that forces reporting the progress.
        /// </summary>
        internal const bool FORCE_REPORTING_FLAG = true;
        #endregion

        #region Events
        /// <summary>
        /// Raised if settings where changesd
        /// </summary>
        public event EventHandler SettingsChanged;
        /// <summary>
        /// Raised if Process is finished or canceld
        /// </summary>
        public event EventHandler ProcessFinishedCanceled;
        /// <summary>
        /// Raised if process is started
        /// </summary>
        public event EventHandler ProcessStarted;
        #endregion

        #region Enums
        /// <summary>
        /// An enumeration ths specifies the mode of the controle
        /// </summary>
        public enum ControleMode
        {
            /// <summary>
            /// The controle mode is to create a file backup
            /// </summary>
            CreateBackup,
            /// <summary>
            /// Thte controle mode is to restore a file backup
            /// </summary>
            RestoreBackup
        }

        /// <summary>
        /// An enomeration to indicate the step of an process
        /// </summary>
        internal enum ProcessStep
        {
            None,
            Count_Start,
            Count_Busy,
            Count_Finish,
            Copy_Start,
            Copy_Busy,
            Copy_Finish,
            Cancel,
            Exception
        }
        #endregion

        #region Fiels
        /// <summary>
        /// Element to copy items
        /// </summary>
        private BackupProject.Process.CopyItems _copier;
        /// <summary>
        /// Element to count items and bytes
        /// </summary>
        private readonly BackupProject.Process.CountItems _counter = new BackupProject.Process.CountItems();
        /// <summary>
        /// Dialog to browse to directorys
        /// </summary>
        private readonly FolderBrowserDialog _directoryBrowser = new FolderBrowserDialog();
        /// <summary>
        /// The time where the last report where commited to process controle.
        /// </summary>
        private DateTime _lastReportTime = new DateTime();
        /// <summary>
        /// The actual step of the current running process
        /// </summary>
        private ProcessStep _processStep = ProcessStep.None;
        /// <summary>
        /// Save dialog for logfiles
        /// </summary>
        private readonly SaveFileDialog _saveLogFile = new SaveFileDialog();
        /// <summary>
        /// Backound worker for count and copy items
        /// </summary>
        private readonly BackgroundWorker _worker = null;
        #endregion

        #region Properties
        /// <summary>
        /// The application MainForm
        /// </summary>
        private MainForm _mainForm;
        [Browsable(false)]
        /// <summary>
        /// Set the application MainForm
        /// </summary>
        public MainForm MainForm
        {
            set
            {
                this._mainForm = value;
            }
        }

        /// <summary>
        /// The mode to run the controle
        /// </summary>
        private ControleMode _mode = ControleMode.CreateBackup;
        /// <summary>
        /// Get and set the mode to run the controle
        /// </summary>
        [Browsable(true)]
        [Description("Controle Mode, create or restore backup")]
        [Category("Darstellung")]
        [DisplayName("Mode")]
        public ControleMode Mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
                switch (value)
                {
                    case ControleMode.CreateBackup:
                        this._directoryBrowser.Description = src.MainForm.Usercontroles.uscProcControle.Stringtable.DirectoryBrowser_Description_Backup;
                        this._directoryBrowser.ShowNewFolderButton = true;
                        this.btnProcessStart.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.btnProcessStart_Text__Backup;
                        this.chkLogFileCreate.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.chkLogFileCreate_Text__Backup;
                        this.chkLogFileAutoPath.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.chkLogFileAutoPath_Text__Backup;
                        this.chkRootDirectory.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.chkRootDirectory_Text__Backup;
                        this.lblDirectory.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.lblDirectory_Text__Backup;
                        this._saveLogFile.Title = src.MainForm.Usercontroles.uscProcControle.Stringtable.SaveLogFile_Title_Backup;
                        this.lblTargetDirectory.Visible = false;
                        this.txtTargetDirectory.Visible = false;
                        this.btnBrowseTargetDirectory.Visible = false;
                        break;
                    case ControleMode.RestoreBackup:
                        this._directoryBrowser.Description = src.MainForm.Usercontroles.uscProcControle.Stringtable.DirectoryBrowser_Description_Restore;
                        this._directoryBrowser.ShowNewFolderButton = false;
                        this.btnProcessStart.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.btnProcessStart_Text__Restore;
                        this.chkLogFileCreate.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.chkLogFileCreate_Text__Restore;
                        this.chkLogFileAutoPath.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.chkLogFileAutoPath_Text__Restore;
                        this.chkRootDirectory.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.chkRootDirectory_Text__Restore;
                        this.lblDirectory.Text = src.MainForm.Usercontroles.uscProcControle.Stringtable.lblDirectory_Text__Restore;
                        this._saveLogFile.Title = src.MainForm.Usercontroles.uscProcControle.Stringtable.SaveLogFile_Title_Restore;
                        this.lblTargetDirectory.Visible = true;
                        this.txtTargetDirectory.Visible = true;
                        this.btnBrowseTargetDirectory.Visible = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// The Projectmanager of the application to run main project functions
        /// </summary>
        private ProjectManager _projectManager;
        /// <summary>
        /// Get and set the Projectmanager of the application to run main project functions
        /// </summary>
        [Browsable(false)]
        internal ProjectManager ProjectManager
        {
            get
            {
                return this._projectManager;
            }
            set
            {
                this._projectManager = value;
            }
        }

        /// <summary>
        /// Controle to show the progress of a process
        /// </summary>
        private uscProgress.ProcProgress _uscProgress;
        /// <summary>
        /// Set the controle to show the progress of a process
        /// </summary>
        [Browsable(false)]
        public uscProgress.ProcProgress ProgressControle
        {
            set
            {
                this._uscProgress = value;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Inital an new process controle
        /// </summary>
        public ProcControle()
        {
            InitializeComponent();

            //TODO: ROMOVE --> future versions to write log files
            this.grbLogFiles.Visible = false;

            // Initial BackgroundWorker
            this._worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            this.chkLogFileCreate_CheckedChanged(this, new EventArgs());
            this.chkLogFileAutoPath_CheckedChanged(this, new EventArgs());

            this._worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
            this._worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
            this._worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
        }

        /// <summary>
        /// Load project settings to controle
        /// </summary>
        public void LoadSettings()
        {
            this._projectManager.ActiveProject.Settings.RestrainChangedEvent = true;

            BackupProject.Settings.Controle.Controle ControleSettings;
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    ControleSettings = this._projectManager.ActiveProject.Settings.ControleBackup;
                    break;
                case ControleMode.RestoreBackup:
                    ControleSettings = this._projectManager.ActiveProject.Settings.ControleRestore;
                    break;
                default:
                    throw new ArgumentException("uscControleProcess->LoadSettings->Invalid value", nameof(this._mode));
            }

            //Set Settings
            this.txtDirectory.Text = ControleSettings.Directory.Path;
            this.chkRootDirectory.Checked = ControleSettings.Directory.CreateDriveDirectroy;
            this.txtTargetDirectory.Text = ControleSettings.Directory.RestoreTargetPath; //Not used if mode is CreateBackup
            this.chkCountItemsAndBytes.Checked = ControleSettings.Action.CountItemsAndBytes;
            this.chkCopyData.Checked = ControleSettings.Action.CopyData;
            this.chkLogFileCreate.Checked = ControleSettings.Logfile.Create;
            this.chkLogFileAutoPath.Checked = ControleSettings.Logfile.AutoPath;
            this.txtLogFilePath.Text = ControleSettings.Logfile.Path;

            this._projectManager.ActiveProject.Settings.RestrainChangedEvent = false;

            this.SetExistingFileTextBoxes(null);
        }

        /// <summary>
        /// Toggle settings changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSettingsChanged(object sender, EventArgs e)
        {
            if (this.SettingsChanged != null && !this._projectManager.ActiveProject.Settings.RestrainChangedEvent) SettingsChanged(this, new EventArgs());
        }

        /// <summary>
        /// Set existing file text boxes to selected actions
        /// </summary>
        /// <param name="handleFilesDialog">Referece form to get text from, if null it will be created</param>
        private void SetExistingFileTextBoxes(HandleExistingFilesForm handleFilesDialog)
        {
            if (handleFilesDialog is null) handleFilesDialog = new HandleExistingFilesForm(HandleExistingFilesForm.FormMode.DefaultSettings, null, null, this._projectManager.ActiveProject.Settings.Common.ExisitingFiles.HandleExistingItem, this._projectManager.ActiveProject.Settings.Common.ExisitingFiles.AddTextToExistingFile, true);

            if (handleFilesDialog.ActionHandleExistingFiles == HandleExistingFiles.HowToHandleExistingItem.AddText)
            {
                this.txtAddTextToExistingFileText.Text = handleFilesDialog.ActionAddTextText;
                this.lblAddTextToExistingFileText.Visible = true;
                this.txtAddTextToExistingFileText.Visible = true;
            }
            else
            {
                this.txtAddTextToExistingFileText.Text = string.Empty;
                this.lblAddTextToExistingFileText.Visible = false;
                this.txtAddTextToExistingFileText.Visible = false;
            }
            this.txtHandleExistingFileText.Text = handleFilesDialog.GetActionAsText(handleFilesDialog.ActionHandleExistingFiles);
        }

        #region Form User Events
        #region Progrcess Changed
        private void btnProcessCancel_Click(object sender, EventArgs e)
        {
            if (!this._worker.IsBusy) return;
            this._worker.CancelAsync();
        }

        private void btnProcessStart_Click(object sender, EventArgs e)
        {
            // If Nothing to do, exit
            if (!this.chkCountItemsAndBytes.Checked && !this.chkCopyData.Checked) return;
            if (this._worker != null && this._worker.IsBusy) return;

            this.btnProcessCancel.Enabled = true;
            this.btnProcessStart.Enabled = false;

            // Initial BackgroundWorker
            this._worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Debug.Print("uscControleProcess::_worker_DoWork::START");
            BackgroundWorker worker = sender as BackgroundWorker;
            if (this.ProcessStarted != null) ProcessStarted(this, new EventArgs());

            //Initial Progress Store
            //The procedures use Invoke, otherwise the application will crash in some conditions.
            //This can also be prevented by replace this two function calls to the _worker_ProgressChanged function
            this._uscProgress.ProgressStore.Initial();
            this._uscProgress.SetProgressStates.SetControlesValue.InitialControles();

            // Count Data
            if (this.chkCountItemsAndBytes.Checked)
            {
                // Initial counter
                this._counter.Progress = this._uscProgress.ProgressStore;
                this._counter.Project = this._projectManager.ActiveProject;

                //Start count progress
                this._worker.ReportProgress((int)ProcessStep.Count_Start, FORCE_REPORTING_FLAG);
                System.Diagnostics.Debug.Print("    this._mode::" + this._mode.ToString());
                switch (this._mode)
                {
                    //Start Counting in backup mode
                    case ControleMode.CreateBackup:
                        this._counter.Backup(worker, e);
                        break;
                    //Start Counting in restore mode
                    case ControleMode.RestoreBackup:
                        this._counter.Restore(worker, e);
                        break;
                    default:
                        throw new ArgumentException("uscControleProcess->worker_DoWork(Count)->Invalid value", nameof(this._mode));
                }
                worker.ReportProgress((int)ProcessStep.Count_Finish, FORCE_REPORTING_FLAG);
            }

            //Copy Data
            if (this.chkCopyData.Checked)
            {
                // Initial copier
                this._copier = new BackupProject.Process.CopyItems(this._mainForm)
                {
                    Progress = this._uscProgress.ProgressStore,
                    Project = this._projectManager.ActiveProject
                };

                //Start copy progress
                this._worker.ReportProgress((int)ProcessStep.Copy_Start, FORCE_REPORTING_FLAG);
                switch (this._mode)
                {
                    //Start Counting in backup mode
                    case ControleMode.CreateBackup:
                        this._copier.Backup(worker, e);
                        break;
                    //Start Counting in restore mode
                    case ControleMode.RestoreBackup:
                        this._copier.Restore(worker, e);
                        break;
                    default:
                        throw new ArgumentException("uscControleProcess->worker_DoWork(CopyData)->Invalid value", nameof(this._mode));
                }
                if (!e.Cancel) worker.ReportProgress((int)ProcessStep.Copy_Finish, FORCE_REPORTING_FLAG);
            }
            System.Diagnostics.Debug.Print("uscControleProcess::_worker_DoWork::FINISH");
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            System.Diagnostics.Debug.Print("uscControleProcess::_worker_ProgressChanged::START");
            // Use the progress in percentage to differ the different steps.
            // It looks a little bit stange. The real progress is stored in an member of this class.
            // Try to save the step in an member, lead to glitches. I used the ProgressPercentage as work around
            bool ForceReport = false;
            if (e.UserState != null && e.UserState.GetType().Equals(typeof(bool))) ForceReport = (bool)e.UserState;

            // Report Progress by time Interval or if report is forced
            if (ForceReport || this._uscProgress.CheckUpdateInterval(this._lastReportTime))
            {
                this._lastReportTime = DateTime.Now;

                this._processStep = (ProcessStep)e.ProgressPercentage;
                System.Diagnostics.Debug.Print("    this._controleStep::" + this._processStep.ToString());
                switch (this._processStep)
                {
                    case ProcessStep.Count_Start:
                        this._uscProgress.SetProgressStates.SetProgress_CountStart();
                        break;
                    case ProcessStep.Count_Busy:
                        this._uscProgress.SetProgressStates.SetPRogress_CountBusy();
                        break;
                    case ProcessStep.Count_Finish:
                        this._uscProgress.SetProgressStates.SetProgress_CountFinish();
                        break;
                    case ProcessStep.Copy_Start:
                        this._uscProgress.SetProgressStates.SetProgress_CopyStart();
                        break;
                    case ProcessStep.Copy_Busy:
                        this._uscProgress.SetProgressStates.SetProgress_CopyBusy();
                        break;
                    case ProcessStep.Copy_Finish:
                        this._uscProgress.SetProgressStates.SetProgress_CopyFinish();
                        break;
                    case ProcessStep.Cancel:
                        this._uscProgress.SetProgressStates.SetProgress_Cancel();
                        break;
                    case ProcessStep.Exception:
                        this._uscProgress.SetProgressStates.SetProgress_Exception();
                        break;
                    default:
                        throw new ArgumentException("uscControleProcess->worker_ProgressChanged->Invalid value", nameof(this._processStep));
                }
            }
            System.Diagnostics.Debug.Print("uscControleProcess::_worker_ProgressChanged::FINISH");
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Diagnostics.Debug.Print("uscControleProcess::_worker_RunWorkerCompleted::START");
            System.Diagnostics.Debug.Print("    this._controleStep::" + this._processStep.ToString());
            if (this.ProcessFinishedCanceled != null) ProcessFinishedCanceled(this, new EventArgs());

            if (e.Cancelled) this._processStep = ProcessStep.Cancel;
            if (this._uscProgress.ProgressStore.Exception.Exception != null) this._processStep = ProcessStep.Exception;

            switch (this._processStep)
            {
                case ProcessStep.Count_Start:
                case ProcessStep.Count_Busy:
                case ProcessStep.Count_Finish:
                    this._uscProgress.SetProgressStates.SetProgress_CountFinish();
                    break;
                case ProcessStep.Copy_Start:
                case ProcessStep.Copy_Busy:
                case ProcessStep.Copy_Finish:
                    this._uscProgress.SetProgressStates.SetProgress_CopyFinish();
                    break;
                case ProcessStep.Cancel:
                    MessageBox.Show(this.ParentForm, Stringtable._0x0007m, Stringtable._0x0007m, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this._uscProgress.SetProgressStates.SetProgress_Cancel();
                    break;
                case ProcessStep.Exception:
                    MessageBox.Show(this.ParentForm, string.Format(Stringtable._0x0008m, new object[] { this._uscProgress.ProgressStore.Exception.Exception.Message }), Stringtable._0x0008c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this._uscProgress.SetProgressStates.SetProgress_Cancel();
                    break;
                default:
                    throw new ArgumentException("uscControleProcess->worker_RunWorkerCompleted->Invalid value", nameof(this._processStep));
            }

            this.btnProcessCancel.Enabled = false;
            this.btnProcessStart.Enabled = true;
            System.Diagnostics.Debug.Print("uscControleProcess::_worker_RunWorkerCompleted::FINISH");
        }
        #endregion

        #region Settings Changes
        private void btnBrowseDirectory_Click(object sender, EventArgs e)
        {
            this._directoryBrowser.SelectedPath = this.txtDirectory.Text;
            if (this._directoryBrowser.ShowDialog() == DialogResult.OK)
            {
                this.txtDirectory.Text = this._directoryBrowser.SelectedPath;
            }
        }

        private void btnHandleExistingFile_SetDefault_Click(object sender, EventArgs e)
        {
            HandleExistingFilesForm HandleFilesDialog = new HandleExistingFilesForm(HandleExistingFilesForm.FormMode.DefaultSettings, null, null, this._projectManager.ActiveProject.Settings.Common.ExisitingFiles.HandleExistingItem, this._projectManager.ActiveProject.Settings.Common.ExisitingFiles.AddTextToExistingFile, true);
            if (HandleFilesDialog.ShowDialog() == DialogResult.OK)
            {
                this._projectManager.ActiveProject.Settings.Common.ExisitingFiles.AddTextToExistingFile = HandleFilesDialog.ActionAddTextText;
                this._projectManager.ActiveProject.Settings.Common.ExisitingFiles.HandleExistingItem = HandleFilesDialog.ActionHandleExistingFiles;
                this.SetExistingFileTextBoxes(HandleFilesDialog);
            }
            this.ToggleSettingsChanged(sender, e);
        }

        private void btnLogFilePath_Click(object sender, EventArgs e)
        {
            this._saveLogFile.FileName = this.txtLogFilePath.Text;
            this._saveLogFile.Filter = Settings.Default._UNUSED_Logfile_FilterList;
            this._saveLogFile.FilterIndex = Settings.Default._UNUSED_Logfile_FilterIndex;
            if (this._saveLogFile.ShowDialog() == DialogResult.OK)
            {
                this.txtLogFilePath.Text = this._saveLogFile.FileName;
            }
        }

        private void chkCountItemsAndBytes_CheckedChanged(object sender, EventArgs e)
        {
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Action.CountItemsAndBytes = this.chkCountItemsAndBytes.Checked;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Action.CountItemsAndBytes = this.chkCountItemsAndBytes.Checked;
                    break;
                default:
                    break;
            }
            this.ToggleSettingsChanged(sender, e);
        }

        private void chkCopyData_CheckedChanged(object sender, EventArgs e)
        {
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Action.CopyData = this.chkCopyData.Checked;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Action.CopyData = this.chkCopyData.Checked;
                    break;
                default:
                    break;
            }

            this.ToggleSettingsChanged(sender, e);
        }

        private void chkRootDirectory_CheckedChanged(object sender, EventArgs e)
        {
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Directory.CreateDriveDirectroy = this.chkRootDirectory.Checked;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Directory.CreateDriveDirectroy = this.chkRootDirectory.Checked;
                    this.lblTargetDirectory.Enabled = !this.chkRootDirectory.Checked;
                    this.txtTargetDirectory.Enabled = !this.chkRootDirectory.Checked;
                    this.btnBrowseTargetDirectory.Enabled = !this.chkRootDirectory.Checked;
                    break;
                default:
                    break;
            }
            this.ToggleSettingsChanged(sender, e);
        }

        private void chkLogFileAutoPath_CheckedChanged(object sender, EventArgs e)
        {
            this.btnLogFilePath.Enabled = !this.chkLogFileAutoPath.Checked;
            this.txtLogFilePath.Enabled = !this.chkLogFileAutoPath.Checked;

            if (this._projectManager == null || this._projectManager.ActiveProject == null) return;
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Logfile.AutoPath = this.chkLogFileAutoPath.Checked;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Logfile.AutoPath = this.chkLogFileAutoPath.Checked;
                    break;
                default:
                    break;
            }
            this.ToggleSettingsChanged(sender, e);
        }

        private void chkLogFileCreate_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlLogfilePath.Enabled = this.chkLogFileCreate.Checked;

            if (this._projectManager == null || this._projectManager.ActiveProject == null) return;
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Logfile.Create = this.chkLogFileCreate.Checked;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Logfile.Create = this.chkLogFileCreate.Checked;
                    break;
                default:
                    break;
            }
            this.ToggleSettingsChanged(sender, e);
        }

        private void txtDirectory_TextChanged(object sender, EventArgs e)
        {
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Directory.Path = this.txtDirectory.Text;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Directory.Path = this.txtDirectory.Text;
                    break;
                default:
                    break;
            }
            this.ToggleSettingsChanged(sender, e);
        }

        private void txtLogFilePath_TextChanged(object sender, EventArgs e)
        {
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Logfile.Path = this.txtLogFilePath.Text;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Logfile.Path = this.txtLogFilePath.Text;
                    break;
                default:
                    break;
            }
            this.ToggleSettingsChanged(sender, e);
        }
        #endregion
        #endregion
        #endregion
    }
}