﻿/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
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

using OLKI.Toolbox.DirectoryAndFile;
using OLKI.Programme.QuBC.src.Project.Task;
using OLKI.Programme.QuBC.Properties;
using OLKI.Programme.QuBC.src.Project.LogFileWriter;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscTaskControle
{
    /// <summary>
    /// Controle to controle the backup or restore process
    /// </summary>
    public partial class TaskControle : UserControl
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
        /// Raised if Task is finished or canceld
        /// </summary>
        public event EventHandler TaskFinishedCanceled;
        /// <summary>
        /// Raised if process is started
        /// </summary>
        public event EventHandler TaskStarted;
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
        internal enum TaskStep
        {
            None,
            Count_Start,
            Count_Busy,
            Count_Finish,
            Copy_Start,
            Copy_Busy,
            Copy_Finish,
            DeleteOldItems_Start,
            DeleteOldItems_Busy,
            DeleteOldItems_Finish,
            Cancel,
            Exception
        }
        #endregion

        #region Fiels
        /// <summary>
        /// Is the current Action in AutomationMde;
        /// </summary>
        private bool _automationRun = false;

        /// <summary>
        /// Element to copy items
        /// </summary>
        private CopyItems _copier;
        /// <summary>
        /// Element to count items and bytes
        /// </summary>
        private readonly CountItems _counter = new CountItems();
        /// <summary>
        /// Element to delete old items
        /// </summary>
        private DeleteOldItems _deleter;
        /// <summary>
        /// Dialog to browse to directorys
        /// </summary>
        private readonly FolderBrowserDialog _directoryBrowser = new FolderBrowserDialog();
        /// <summary>
        /// The time where the last report where commited to process controle.
        /// </summary>
        private DateTime _lastReportTime = new DateTime();
        /// <summary>
        /// Write LogFiles
        /// </summary>
        private LogFile _logFile;
        /// <summary>
        /// Path, where to write the Logfile to
        /// </summary>
        private string _logFilePath;
        /// <summary>
        /// The actual step of the current running task
        /// </summary>
        private TaskStep _taskStep = TaskStep.None;
        /// <summary>
        /// The store for the progress
        /// </summary>
        private ProgressStore _progressStore = null;
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
                        this._directoryBrowser.Description = Stringtable.DirectoryBrowser_Description_Backup;
                        this._directoryBrowser.ShowNewFolderButton = true;
                        this.btnTaskStart.Image = Resources.BackupCreate;
                        this.btnTaskStart.Text = Stringtable.btnTaskStart_Text__Backup;
                        this.chkDeleteOld.Visible = true;
                        this.chkLogFileCreate.Text = Stringtable.chkLogFileCreate_Text__Backup;
                        this.chkLogFileAutoPath.Text = Stringtable.chkLogFileAutoPath_Text__Backup;
                        this.chkRootDirectory.Text = Stringtable.chkRootDirectory_Text__Backup;
                        this.lblDirectory.Text = Stringtable.lblDirectory_Text__Backup;
                        this._saveLogFile.Title = Stringtable.SaveLogFile_Title_Backup;
                        this.lblTargetDirectory.Visible = false;
                        this.txtRestoreTargetDirectory.Visible = false;
                        this.btnBrowseRestoreTargetDirectory.Visible = false;
                        break;
                    case ControleMode.RestoreBackup:
                        this._directoryBrowser.Description = Stringtable.DirectoryBrowser_Description_Restore;
                        this._directoryBrowser.ShowNewFolderButton = false;
                        this.btnTaskStart.Image = Resources.BackupRestore;
                        this.btnTaskStart.Text = Stringtable.btnTaskStart_Text__Restore;
                        this.chkDeleteOld.Visible = false;
                        this.chkLogFileCreate.Text = Stringtable.chkLogFileCreate_Text__Restore;
                        this.chkLogFileAutoPath.Text = Stringtable.chkLogFileAutoPath_Text__Restore;
                        this.chkRootDirectory.Text = Stringtable.chkRootDirectory_Text__Restore;
                        this.lblDirectory.Text = Stringtable.lblDirectory_Text__Restore;
                        this._saveLogFile.Title = Stringtable.SaveLogFile_Title_Restore;
                        this.lblTargetDirectory.Visible = true;
                        this.txtRestoreTargetDirectory.Visible = true;
                        this.btnBrowseRestoreTargetDirectory.Visible = true;
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
        private uscProgress.TaskProgress _uscProgress;
        /// <summary>
        /// Set the controle to show the progress of a process
        /// </summary>
        [Browsable(false)]
        public uscProgress.TaskProgress ProgressControle
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
        public TaskControle()
        {
            InitializeComponent();

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

            Project.Settings.Controle.Controle ControleSettings;
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    ControleSettings = this._projectManager.ActiveProject.Settings.ControleBackup;
                    break;
                case ControleMode.RestoreBackup:
                    ControleSettings = this._projectManager.ActiveProject.Settings.ControleRestore;
                    break;
                default:
                    throw new ArgumentException("uscTaskControle->LoadSettings->Invalid value", nameof(this._mode));
            }

            //Set Settings
            this.txtDirectory.Text = ControleSettings.Directory.Path;
            this.chkRootDirectory.Checked = ControleSettings.Directory.CreateDriveDirectroy;
            this.txtRestoreTargetDirectory.Text = ControleSettings.Directory.RestoreTargetPath; //Not used if mode is CreateBackup
            this.chkCountItemsAndBytes.Checked = ControleSettings.Action.CountItemsAndBytes;
            this.chkCopyData.Checked = ControleSettings.Action.CopyData;
            this.chkDeleteOld.Checked = ControleSettings.Action.DeleteOldData;
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
        private void btnTaskCancel_Click(object sender, EventArgs e)
        {
            if (!this._worker.IsBusy) return;
            this._worker.CancelAsync();
        }

        private void btnTaskStart_Click(object sender, EventArgs e)
        {
            this.btnTaskStart_Click(sender, e, false);
        }

        internal void btnTaskStart_Click(object sender, EventArgs e, bool automationRun)
        {
            _ = sender;
            _ = e;
            this._automationRun = automationRun;

            if (this.Mode == ControleMode.CreateBackup && this.chkRootDirectory.Checked && this.chkDeleteOld.Checked)
            {
                switch (MessageBox.Show(this._mainForm, Properties.Stringtable._0x0024m, Properties.Stringtable._0x0024c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        this.chkDeleteOld.Checked = false;
                        break;
                    case DialogResult.Cancel:
                        return;
                    default:
                        return;
                }
            }

            // If Nothing to do, exit
            if (!this.chkCountItemsAndBytes.Checked && !this.chkCopyData.Checked && !this.chkDeleteOld.Checked) return;
            if (this._worker != null && this._worker.IsBusy) return;

            this.btnTaskCancel.Enabled = true;
            this.btnTaskStart.Enabled = false;
            this.pnlSourceAndTarget.Enabled = false;
            this.grbHandleExistingFiles.Enabled = false;
            this.grbLogFiles.Enabled = false;
            this.grbToDo.Enabled = false;

            // Create Automatic LogFilePath
            if (this.chkLogFileAutoPath.Checked)
            {
                string LogFilePath = this.txtDirectory.Text;
                LogFilePath += @"\";
                switch (this._mode)
                {
                    case ControleMode.CreateBackup:
                        LogFilePath += @"LogFile_Backup__";
                        break;
                    case ControleMode.RestoreBackup:
                        LogFilePath += @"LogFile_Restore__";
                        break;
                    default:
                        throw new ArgumentException("uscTaskCtronle->btnTaskStart_Click->Invalid value", nameof(this._mode));
                }
                LogFilePath += DateTime.Now.ToString(Settings.Default.Logfile_DateFormat);
                LogFilePath += ".log";

                this._logFilePath = LogFilePath;
                this.txtLogFilePath.Text = LogFilePath;
            }

            // Initial BackgroundWorker
            this.TaskStarted?.Invoke(this, new EventArgs());
            this._worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //Initial Progress Store
            this._progressStore = new ProgressStore();
            //The procedures use Invoke, otherwise the application will crash in some conditions.
            //This can also be prevented by replace this two function calls to the _worker_ProgressChanged function
            this._uscProgress.SetProgressStates.SetControlesValue.InitialControles();

            //Initial LogFile Writer
            this._logFile = new LogFile(this._projectManager.ActiveProject.Settings, this.Mode, this._progressStore, this.chkLogFileCreate.Checked)
            {
                LogFilePath = this._logFilePath
            };
            this._logFile.WriteHead(this.txtHandleExistingFileText.Text);

            // Clear exception List
            if (Settings.Default.Log_ClearExceptionListOnStartProcess) this._uscProgress.SetProgressStates.ClearExceptionlog();

            try
            {
                // Count Data
                if (this.chkCountItemsAndBytes.Checked)
                {
                    // Initial counter
                    this._counter.Project = this._projectManager.ActiveProject;

                    //Start count progress
                    worker.ReportProgress((int)TaskStep.Count_Start, new ProgressState(this._progressStore, true));
                    switch (this._mode)
                    {
                        //Start Counting in backup mode
                        case ControleMode.CreateBackup:
                            if (!this._counter.Backup(worker, e, this._progressStore)) return;
                            break;
                        //Start Counting in restore mode
                        case ControleMode.RestoreBackup:
                            if (!this._counter.Restore(worker, e, this._progressStore)) return;
                            break;
                        default:
                            throw new ArgumentException("uscTaskControle->worker_DoWork(Count)->Invalid value", nameof(this._mode));
                    }
                    worker.ReportProgress((int)TaskStep.Count_Finish, new ProgressState(this._progressStore, true));
                }

                //Copy Data
                if (this.chkCopyData.Checked)
                {
                    // Initial copier
                    this._copier = new CopyItems(this._mainForm)
                    {
                        Project = this._projectManager.ActiveProject
                    };

                    //Start copy progress
                    worker.ReportProgress((int)TaskStep.Copy_Start, new ProgressState(this._progressStore, true));
                    switch (this._mode)
                    {
                        //Start Counting in backup mode
                        case ControleMode.CreateBackup:
                            if (!this._copier.Backup(worker, e, this._progressStore)) return;
                            break;
                        //Start Counting in restore mode
                        case ControleMode.RestoreBackup:
                            switch ((DialogResult)this.Invoke((Func<DialogResult>)(() => MessageBox.Show(this._mainForm, Properties.Stringtable._0x002Fm, Properties.Stringtable._0x002Fc, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))))
                            {
                                case DialogResult.Yes:
                                    if (!this._copier.Restore(worker, e, this._progressStore)) return;
                                    break;
                                case DialogResult.No:
                                case DialogResult.Cancel:
                                    if (worker.IsBusy) worker.CancelAsync();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            throw new ArgumentException("uscTaskControle->worker_DoWork(CopyData)->Invalid value", nameof(this._mode));
                    }
                    if (!e.Cancel) worker.ReportProgress((int)TaskStep.Copy_Finish, new ProgressState(true));
                }

                //Delete old Data at Backup Target
                if (this._mode == ControleMode.CreateBackup && this.chkRootDirectory.Checked && this.chkDeleteOld.Checked)
                {
                    // Initial deleter
                    this._deleter = new DeleteOldItems()
                    {
                        Project = this._projectManager.ActiveProject
                    };

                    //Start delete old items
                    worker.ReportProgress((int)TaskStep.DeleteOldItems_Start, new ProgressState(this._progressStore, true));
                    if (!this._deleter.Backup(worker, e, this._progressStore)) return;
                    if (!e.Cancel) worker.ReportProgress((int)TaskStep.DeleteOldItems_Finish, new ProgressState(true));
                }
            }
            catch (Exception ex)
            {
                this._progressStore.Exception = new TaskException
                {
                    Description = "",
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Critical,
                    Source = "",
                    Target = ""
                };
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progressStore, true));
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Use the progress in percentage to differ the different steps.
            // It looks a little bit stange. The real progress is stored in an member of this class.
            // Try to save the step in an member, lead to glitches. I used the ProgressPercentage as work around
            ProgressState ProgressState = (ProgressState)e.UserState;

            // Report Progress by time Interval or if report is forced
            if (ProgressState.ForceReportProgress || this._uscProgress.CheckUpdateInterval(this._lastReportTime))
            {
                this._lastReportTime = DateTime.Now;

                this._taskStep = (TaskStep)e.ProgressPercentage;
                switch (this._taskStep)
                {
                    case TaskStep.Count_Start:
                        this._logFile.WriteCountStart();
                        this._uscProgress.SetProgressStates.SetProgress_CountStart();
                        break;
                    case TaskStep.Count_Busy:
                        this._uscProgress.SetProgressStates.SetPRogress_CountBusy(ProgressState.ProgressStore);
                        break;
                    case TaskStep.Count_Finish:
                        this._logFile.WriteCountFinish();
                        this._uscProgress.SetProgressStates.SetProgress_CountFinish();
                        break;
                    case TaskStep.Copy_Start:
                        this._logFile.WriteCopyStart();
                        this._uscProgress.SetProgressStates.SetProgress_CopyStart(ProgressState.ProgressStore);
                        break;
                    case TaskStep.Copy_Busy:
                        this._uscProgress.SetProgressStates.SetProgress_CopyBusy(ProgressState.ProgressStore);
                        break;
                    case TaskStep.Copy_Finish:
                        this._logFile.WriteCopyFinish();
                        this._uscProgress.SetProgressStates.SetProgress_CopyFinish();
                        break;
                    case TaskStep.DeleteOldItems_Start:
                        this._logFile.WriteDeleteSart();
                        this._uscProgress.SetProgressStates.SetProgress_DeleteStart();
                        break;
                    case TaskStep.DeleteOldItems_Busy:
                        this._uscProgress.SetProgressStates.SetPRogress_DeleteBusy(ProgressState.ProgressStore);
                        break;
                    case TaskStep.DeleteOldItems_Finish:
                        this._logFile.WriteDeleteFinish();
                        this._uscProgress.SetProgressStates.SetProgress_DeleteFinish();
                        break;
                    case TaskStep.Cancel:
                        this._uscProgress.SetProgressStates.SetProgress_Cancel();
                        break;
                    case TaskStep.Exception:
                        this._logFile.WriteException(ProgressState.ProgressStore.Exception);
                        this._uscProgress.SetProgressStates.SetProgress_Exception(ProgressState.ProgressStore.Exception);
                        if (ProgressState.ProgressStore.Exception.Level != TaskException.ExceptionLevel.Critical) ProgressState.ProgressStore.Exception = null;
                        break;
                    default:
                        throw new ArgumentException("uscTaskControle->worker_ProgressChanged->Invalid value", nameof(this._taskStep));
                }
            }
            this._progressStore = ProgressState.ProgressStore;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) this._taskStep = TaskStep.Cancel;
            if (this._progressStore != null && this._progressStore.Exception != null && this._progressStore.Exception.Level == TaskException.ExceptionLevel.Critical) this._taskStep = TaskStep.Exception;

            switch (this._taskStep)
            {
                case TaskStep.Count_Start:
                case TaskStep.Count_Busy:
                case TaskStep.Count_Finish:
                    this.AutomationFinshAction();
                    this._uscProgress.SetProgressStates.SetProgress_CountFinish();
                    this.ShowCompletedMessage(this._taskStep);
                    break;
                case TaskStep.Copy_Start:
                case TaskStep.Copy_Busy:
                case TaskStep.Copy_Finish:
                    this.AutomationFinshAction();
                    this._uscProgress.SetProgressStates.SetProgress_CopyFinish();
                    this.ShowCompletedMessage(this._taskStep);
                    break;
                case TaskStep.DeleteOldItems_Start:
                case TaskStep.DeleteOldItems_Busy:
                case TaskStep.DeleteOldItems_Finish:
                    this.AutomationFinshAction();
                    this._uscProgress.SetProgressStates.SetProgress_DeleteFinish();
                    this.ShowCompletedMessage(this._taskStep);
                    break;
                case TaskStep.Cancel:
                    MessageBox.Show(this.ParentForm, Properties.Stringtable._0x0007m, Properties.Stringtable._0x0007m, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this._uscProgress.SetProgressStates.SetProgress_Cancel();
                    break;
                case TaskStep.Exception:
                    MessageBox.Show(this.ParentForm, string.Format(Properties.Stringtable._0x0008m, new object[] { this._progressStore.Exception.Exception.Message }), Properties.Stringtable._0x0008c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this._uscProgress.SetProgressStates.SetProgress_Cancel();
                    break;
                default:
                    throw new ArgumentException("uscTaskControle->worker_RunWorkerCompleted->Invalid value", nameof(this._taskStep));
            }

            this.btnTaskCancel.Enabled = false;
            this.btnTaskStart.Enabled = true;
            this.pnlSourceAndTarget.Enabled = true;
            this.grbHandleExistingFiles.Enabled = true;
            this.grbLogFiles.Enabled = true;
            this.grbToDo.Enabled = true;

            //Write log file foot
            if (this._taskStep == TaskStep.Cancel)
            {
                this._logFile.WriteCancel();
            }
            else
            {
                this._logFile.WriteFoot();
            }
            this.TaskFinishedCanceled?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Run action if Automation has finished
        /// </summary>
        private void AutomationFinshAction()
        {
            if (!this._automationRun) return;
            this._automationRun = false;

            switch (this._projectManager.ActiveProject.Settings.Common.AutomationFinishAction)
            {
                case Project.Settings.Common.Common.FinishAction.ExitApllication:
                    Application.Exit();
                    break;
                case Project.Settings.Common.Common.FinishAction.SystemShutdown:
                    System.Diagnostics.Process.Start("shutdown", "/f /s /t 1");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Shows a message if the BackgroundWorker is completet and wich options where selected
        /// </summary>
        private void ShowCompletedMessage(TaskStep taskStep)
        {
            switch (taskStep)
            {
                case TaskStep.Count_Finish:
                case TaskStep.Copy_Finish:
                case TaskStep.DeleteOldItems_Finish:
                    string SelectedOptions = "";
                    if (this.chkCountItemsAndBytes.Checked) SelectedOptions += "\n    - " + this.chkCountItemsAndBytes.Text;
                    if (this.chkCopyData.Checked) SelectedOptions += "\n    - " + this.chkCopyData.Text;
                    if (this.chkCopyData.Checked && this.chkDeleteOld.Checked) SelectedOptions += "\n    - " + this.chkDeleteOld.Text;
                    MessageBox.Show(this.ParentForm, string.Format(Properties.Stringtable._0x0030m, new object[] { SelectedOptions }), Properties.Stringtable._0x0030c, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    return;
            }
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

        private void btnBrowseRestoreTargetDirectory_Click(object sender, EventArgs e)
        {
            this._directoryBrowser.SelectedPath = this.txtRestoreTargetDirectory.Text;
            if (this._directoryBrowser.ShowDialog() == DialogResult.OK)
            {
                this.txtRestoreTargetDirectory.Text = this._directoryBrowser.SelectedPath;
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
            //this._saveLogFile.FileName = this.txtLogFilePath.Text;
            this._saveLogFile.Filter = Settings_AppConst.Default.Logfile_FilterList;
            this._saveLogFile.FilterIndex = Settings_AppConst.Default.Logfile_FilterIndex;
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

        private void chkDeleteOld_CheckedChanged(object sender, EventArgs e)
        {
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Action.DeleteOldData = chkDeleteOld.Checked;
                    break;
                case ControleMode.RestoreBackup:
                    break;
                default:
                    break;
            }
        }

        private void chkRootDirectory_CheckedChanged(object sender, EventArgs e)
        {
            switch (this._mode)
            {
                case ControleMode.CreateBackup:
                    this._projectManager.ActiveProject.Settings.ControleBackup.Directory.CreateDriveDirectroy = this.chkRootDirectory.Checked;
                    this.chkDeleteOld.Enabled = this.chkRootDirectory.Checked;
                    break;
                case ControleMode.RestoreBackup:
                    this._projectManager.ActiveProject.Settings.ControleRestore.Directory.CreateDriveDirectroy = this.chkRootDirectory.Checked;
                    this.lblTargetDirectory.Enabled = !this.chkRootDirectory.Checked;
                    this.txtRestoreTargetDirectory.Enabled = !this.chkRootDirectory.Checked;
                    this.btnBrowseRestoreTargetDirectory.Enabled = !this.chkRootDirectory.Checked;
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
            if (!this.chkLogFileCreate.Checked || this.chkLogFileAutoPath.Checked) return;
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

        private void txtRestoreTargetDirectory_TextChanged(object sender, EventArgs e)
        {
            this._projectManager.ActiveProject.Settings.ControleRestore.Directory.RestoreTargetPath = this.txtRestoreTargetDirectory.Text;
        }
        #endregion
        #endregion
        #endregion
    }
}