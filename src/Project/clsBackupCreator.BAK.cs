/*
 * Filename:      clsBackupCreator.cs
 * Created:       2017-06-11
 * Last modified: 2017-06-11
 * Copyright:     Oliver Kind - 2017
 * 
 * File Content:
 * - Constants
 * - Fields
 * - Properties
 * - Methodes
 *  1. BackupCreator - Constructor
 *  2. Helpers
 *   a. StartBackup
 *   b. ResetTextBoxes
 *   c. CheckCancelWorker
 *   d. GetCancelGoToNextStep
 *  3. Backgroundworker
 *   3a. backgroundWorker CleanProject
 *    a. StartWorker_CleanProject
 *    b. bgwCleanProject_DoWork
 *    c. bgwCleanProject_ProgressChanged
 *    d. bgwCleanProject_RunWorkerCompleted
 *   3b. backgroundWorker CalculateVolume
 *    a. StartWorker_CalculateVolume
 *    b. CancelBackup_CalculateVolume
 *    c. bgwCalculateVolume_DoWork
 *    d. bgwCalculateVolume_ProgressChanged
 *    e. bgwCalculateVolume_RunWorkerCompleted
 *    f. CalculateVolume_CountItemsAndBytes
 *   3c. backgroundWorker BackupCreator
 *    a. StartWorker_CreateBackup
 *    b. bgwCreateBackup_DoWork
 *    c. bgwCreateBackup_ProgressChanged
 *    d. bgwCreateBackup_RunWorkerCompleted
 *    e. CopyDirectory
 *    f. CopyFileBytewise
 * - SubClasses
 *  # BackgroundWorkerTransferArgs - Used to transfer  properties and and object to the backgroundworker and there functions
 *   - Properties
 * 
 * Desctiption:
 * Provides tools to create a directroy and file backup
 * 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.BackupProject
{
    /// <summary>
    /// Provides tools to create a directroy and file backup
    /// </summary>
    internal class BackupCreator
    {
        #region Constants
        /// <summary>
        /// Add this extension to the extension of a file during copieng a file and setting the file attributes. This can provide exception causing in lacking of access rights
        /// </summary>
        private const string COPY_EXTENSION_ADDITION = "__COPY";
        /// <summary>
        /// Specifies if backround worker report progress by default
        /// </summary>
        private const bool BACKGROUNWORKERS_REPORTS_PROGRESS = true;
        /// <summary>
        /// Specifies if backround worker support cancellation by default
        /// </summary>
        private const bool BACKGROUNWORKERS_SUPPORTS_CANCELLATION = true;
        #endregion

        #region Fields
        /// <summary>
        /// Defindes the backgroundworker for cleaning the project file
        /// </summary>
        private BackgroundWorker _bgwCleanProject = new BackgroundWorker();
        /// <summary>
        /// Defindes the backgroundworker for counting items and bytes to copy
        /// </summary>
        private BackgroundWorker _bgwCalculateVolume = new BackgroundWorker();
        /// <summary>
        /// Defindes the backgroundworker for counting items and bytes to copy for restoring items from backup
        /// </summary>
        private BackgroundWorker _bgwCalculateVolumeRestore = new BackgroundWorker();
        /// <summary>
        /// Defindes the backgroundworker for creating the backup. Copies files and directories
        /// </summary>
        private BackgroundWorker _bgwCreateBackup = new BackgroundWorker();
        /// <summary>
        /// Defindes the backgroundworker for restoring from backup. Copies files and directories
        /// </summary>
        private BackgroundWorker _bgwRestoreBackup = new BackgroundWorker();

        //private QBC.BackupProject.BackupResponse _response = null;
        //private QBC.BackupProject.Project this._projectManager.ActiveProject = null;
        /// <summary>
        /// The project manager object of the application to get the project data
        /// </summary>
        private QBC.ProjectManager _projectManager = null;
        /// <summary>
        /// The application mainframe, for internal use to set controles on mainframe
        /// </summary>
        private QBC.MainForm _mainForm = null;
        /// <summary>
        /// Defines the log file writer for wirting log files
        /// </summary>
        private QBC.BackupProject.LogFileWriter _logWriter = null;
        #endregion

        #region Methodes
        /// <summary>
        /// Creates a new BackupCreator instance
        /// </summary>
        /// <param name="projectManager">Specifies the project manager object of the application to get the project data</param>
        /// <param name="mainForm">Specifies the application mainframe, for internal use to set controles on mainframe</param>
        internal BackupCreator(QBC.ProjectManager projectManager, QBC.MainForm mainForm)
        {
            //this._projectManager.ActiveProject = projectManager.ActiveProject;
            this._projectManager = projectManager;
            this._mainForm = mainForm;
            //this._backgroundWorkerTransferArgs = new BackgroundWorkerTransferArgs(this._logWriter, this._projectManager);

            // Handle _bgwCleanProject Events
            this._bgwCleanProject.WorkerReportsProgress = BACKGROUNWORKERS_REPORTS_PROGRESS;
            this._bgwCleanProject.WorkerSupportsCancellation = BACKGROUNWORKERS_SUPPORTS_CANCELLATION;
            this._bgwCleanProject.DoWork += new DoWorkEventHandler(this.bgwCleanProject_DoWork);
            this._bgwCleanProject.ProgressChanged += new ProgressChangedEventHandler(this.bgwCleanProject_ProgressChanged);
            this._bgwCleanProject.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwCleanProject_RunWorkerCompleted);

            // Handle _bgwCalculateVolume Events
            this._bgwCalculateVolume.WorkerReportsProgress = BACKGROUNWORKERS_REPORTS_PROGRESS;
            this._bgwCalculateVolume.WorkerSupportsCancellation = BACKGROUNWORKERS_SUPPORTS_CANCELLATION;
            this._bgwCalculateVolume.DoWork += new DoWorkEventHandler(this.bgwCalculateVolume_DoWork);
            this._bgwCalculateVolume.ProgressChanged += new ProgressChangedEventHandler(this.bgwCalculateVolume_ProgressChanged);
            this._bgwCalculateVolume.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwCalculateVolume_RunWorkerCompleted);

            // Handle _bgwCalculateVolumeRestore Events
            this._bgwCalculateVolumeRestore.WorkerReportsProgress = BACKGROUNWORKERS_REPORTS_PROGRESS;
            this._bgwCalculateVolumeRestore.WorkerSupportsCancellation = BACKGROUNWORKERS_SUPPORTS_CANCELLATION;
            this._bgwCalculateVolumeRestore.DoWork += new DoWorkEventHandler(this.bgwCalculateVolumeRestore_DoWork);
            this._bgwCalculateVolumeRestore.ProgressChanged += new ProgressChangedEventHandler(this.bgwCalculateVolumeRestore_ProgressChanged);
            this._bgwCalculateVolumeRestore.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwCalculateVolumeRestore_RunWorkerCompleted);

            // Handle _bgwCreateBackup Events
            this._bgwCreateBackup.WorkerReportsProgress = BACKGROUNWORKERS_REPORTS_PROGRESS;
            this._bgwCreateBackup.WorkerSupportsCancellation = BACKGROUNWORKERS_SUPPORTS_CANCELLATION;
            this._bgwCreateBackup.DoWork += new DoWorkEventHandler(this.bgwCreateBackup_DoWork);
            this._bgwCreateBackup.ProgressChanged += new ProgressChangedEventHandler(this.bgwCreateBackup_ProgressChanged);
            this._bgwCreateBackup.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwCreateBackup_RunWorkerCompleted);
        }

        #region Helpers
        /// <summary>
        /// Starts a automatic backup progess, specified by tproject data
        /// </summary>
        internal void StartBackup()
        {
            //this._response = new BackupResponse();
            this._projectManager.ActiveProject.Progress = new QBC.BackupProject.BackupResponse();
            this._logWriter = new LogFileWriter(this._projectManager.ActiveProject.Settings.LogFilePath);

            if (this._bgwCleanProject.IsBusy != true && this._bgwCalculateVolume.IsBusy != true && this._bgwCreateBackup.IsBusy != true)
            {
                this._mainForm.lsvErrorLog.Items.Clear();

                this._mainForm.btnStartBackup.Enabled = false;
                this._mainForm.btnCancelBackupCleanProject.Enabled = false;
                this._mainForm.btnCancelBackupCalculateVolume.Enabled = false;
                this._mainForm.btnCancelBackupCreateBackup.Enabled = false;
                this._projectManager.ActiveProject.Progress = new QBC.BackupProject.BackupResponse();

                //this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Projekt zur Datensicherung gestartet");
                this._logWriter.WriteLogLine("====================================\r\n");
                this._logWriter.WriteLogLine("Softwareinformationen:\r\n");
                this._logWriter.WriteLogLine('-', 50);
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Stapelverarbeitung gestartet: " + DateTime.Now.ToString());
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Projektdatei: " + this._projectManager.ActiveProjectFile.FullName);
                this._logWriter.WriteLogLine("Zielordner:   " + this._projectManager.ActiveProject.Settings.BackupTargetPath);
                this._logWriter.WriteLogLine("Loog-Datei:   " + this._projectManager.ActiveProject.Settings.LogFilePath);
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Aufgabenumfang:");
                this._logWriter.WriteLogLine("1. Projektdatei bereinigen:         " + (this._projectManager.ActiveProject.Settings.CleanProjectFile ? "Ja" : "Nein"));
                this._logWriter.WriteLogLine("2. Auftragsvolumen vorab ermitteln: " + (this._projectManager.ActiveProject.Settings.CountItemsAndBytes ? "Ja" : "Nein"));
                this._logWriter.WriteLogLine("3. Sicherungskopie durchführen:     " + (this._projectManager.ActiveProject.Settings.CreateBackup ? "Ja" : "Nein"));
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine('-', 50);
                this._logWriter.WriteLogLine("");

                // Start the asynchronous operation.
                if (this._projectManager.ActiveProject.Settings.CleanProjectFile)
                {
                    this.StartWorker_CleanProject();
                }
                else if (!this._projectManager.ActiveProject.Settings.CleanProjectFile && this._projectManager.ActiveProject.Settings.CountItemsAndBytes)
                {
                    this.StartWorker_CalculateVolume();
                }
                else if (!this._projectManager.ActiveProject.Settings.CleanProjectFile && !this._projectManager.ActiveProject.Settings.CountItemsAndBytes && this._projectManager.ActiveProject.Settings.CreateBackup)
                {
                    this.StartWorker_CreateBackup();
                }
                else /*if (!this._projectManager.ActiveProject.Settings.CleanProjectFile && !this._projectManager.ActiveProject.Settings.CountItemsAndBytes && !this._projectManager.ActiveProject.Settings.BackupCreator)*/
                {
                    this._mainForm.btnStartBackup.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Der Sicherungsvorgang konnte nicht gestartet werden, da ein anderer Sicherungsvorgang noch nicht abgeschlossen ist.\n\nDer Vorgang und alle nachfolgenden Vorgänge wurden abgebrochen.", "Vorgang abgebropchen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Starts a automatic restore progess of backup data
        /// </summary>
        internal void StartRestore()
        {
            this._projectManager.ActiveProject.Progress = new QBC.BackupProject.BackupResponse();
            this._logWriter = new LogFileWriter(this._projectManager.ActiveProject.Settings.LogFilePath);

            if (this._bgwCalculateVolumeRestore.IsBusy != true && this._bgwRestoreBackup.IsBusy != true)
            {
                this._mainForm.lsvErrorLog.Items.Clear();

                this._mainForm.btnStartRestoreBackup.Enabled = false;
                this._mainForm.btnCancelRestorelBackupCalculateVolume.Enabled = false;
                this._mainForm.btnCancelRestorelBackupRestoreData.Enabled = false;

                this._projectManager.ActiveProject.Progress = new QBC.BackupProject.BackupResponse();

                //this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Projekt zur Datenwiederherstellung, von Sicherungskopie, gestartet");
                this._logWriter.WriteLogLine("==================================================================\r\n");
                this._logWriter.WriteLogLine("Softwareinformationen:\r\n");
                this._logWriter.WriteLogLine('-', 50);
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Stapelverarbeitung gestartet: " + DateTime.Now.ToString());
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Projektdatei: " + this._projectManager.ActiveProjectFile.FullName);
                this._logWriter.WriteLogLine("Zielordner:   " + this._projectManager.ActiveProject.Settings.BackupTargetPath);
                this._logWriter.WriteLogLine("Loog-Datei:   " + this._projectManager.ActiveProject.Settings.LogFilePath);
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine("Aufgabenumfang:");
                this._logWriter.WriteLogLine("1. Auftragsvolumen vorab ermitteln: " + (this._projectManager.ActiveProject.Settings.CountItemsAndBytesRestore ? "Ja" : "Nein"));
                this._logWriter.WriteLogLine("2. Sicherungskopie durchführen:     " + (this._projectManager.ActiveProject.Settings.RestoreBackup ? "Ja" : "Nein"));
                this._logWriter.WriteLogLine("");
                this._logWriter.WriteLogLine('-', 50);
                this._logWriter.WriteLogLine("");

                // Start the asynchronous operation.
                if (this._projectManager.ActiveProject.Settings.CountItemsAndBytesRestore)
                {
                    this.StartWorker_CalculateVolumeRestore();
                }
                else if (!this._projectManager.ActiveProject.Settings.CountItemsAndBytesRestore && this._projectManager.ActiveProject.Settings.RestoreBackup)
                {
                    //!!!!this.StartWorker_RestoreProject();
                }
                else /*if (!this._projectManager.ActiveProject.Settings.CountItemsAndBytesRestore && !this._projectManager.ActiveProject.Settings.RestoreBackup)*/
                {
                    this._mainForm.btnStartRestoreBackup.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Der Wiederherstellungsvorgang konnte nicht gestartet werden, da ein anderer Wiederherstellungsvorgang noch nicht abgeschlossen ist.\n\nDer Vorgang und alle nachfolgenden Vorgänge wurden abgebrochen.", "Vorgang abgebropchen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset the progess textboxes on main form
        /// </summary>
        private void ResetTextBoxes()
        {
            this._mainForm.txtCopyStart.Text = string.Empty;
            this._mainForm.txtCopyElapsed.Text = string.Empty;
            this._mainForm.txtCopyRemainItem.Text = string.Empty;

            this._mainForm.txtActualDir.Text = string.Empty;
            this._mainForm.txtActualFile.Text = string.Empty;

            this._mainForm.txtAllItemsPer.Text = string.Empty;
            this._mainForm.txtAllItemsNum.Text = string.Empty;
            this._mainForm.txtAllBytePer.Text = string.Empty;
            this._mainForm.txtAllByteNum.Text = string.Empty;
            this._mainForm.txtAllByteNum.Tag = null;
            this._mainForm.txtAllDirPer.Text = string.Empty;
            this._mainForm.txtAllDirNum.Text = string.Empty;
            this._mainForm.txtActualDirFilesPer.Text = string.Empty;
            this._mainForm.txtActualDirFilesNum.Text = string.Empty;
            this._mainForm.txtActualFileBytePer.Text = string.Empty;
            this._mainForm.txtActualFileByteNum.Text = string.Empty;
            this._mainForm.txtActualFileByteNum.Tag = null;

            this._mainForm.txtCopiedDirectories.Text = string.Empty;
            this._mainForm.txtCopiedFiles.Text = string.Empty;
            this._mainForm.txtCopiedDuration.Text = string.Empty;
            this._mainForm.txtExceptionCount.Text = string.Empty;
        }

        /// <summary>
        /// Check if the specified backgroundworoker was canced an set the cancel do the event args to true
        /// </summary>
        /// <param name="worker">Specifies the background worker to check for an cancelation event</param>
        /// <param name="e">Spevifies the DoWorkerEventArgs of the specified background worker</param>
        /// <returns>True if the worker was canceled</returns>
        private bool CheckCancelWorker(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return true;
            }
            else
            {
                e.Cancel = false;
                return false;
            }
        }

        /// <summary>
        /// Ask if after a cancelation of a backup step the next step should been done or not
        /// </summary>
        /// <param name="askQuestionGoToNextStep">Specifies if the question should been shown or not. Set false if not and the answer shold been always DialogResult.Yes</param>
        /// <returns>DialogResult.Yes if the next step sould been done, DialogResult.No if not</returns>
        private DialogResult GetCancelGoToNextStep(bool askQuestionGoToNextStep)
        {
            if (!askQuestionGoToNextStep) return DialogResult.Yes;
            return MessageBox.Show("Der Vorgang wurde abgebrochen, möchten Sie mit dem nächsten Schritt vorfahren?", "Aktueller Schritt abgebrochen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
        #endregion

        #region Backgroundworker
        #region backgroundWorker CleanProject
        /// <summary>
        /// Starts the project cleaning process
        /// </summary>
        private void StartWorker_CleanProject()
        {
            this._logWriter.WriteLogLine('+', 100);
            this._logWriter.WriteLogLine("Starte Bereinigung der Projektdatei");
            if (this._bgwCleanProject.IsBusy != true && this._bgwCalculateVolume.IsBusy != true && this._bgwCreateBackup.IsBusy != true)
            {
                this._mainForm.btnCancelBackupCleanProject.Enabled = true;
                this._mainForm.pbaAllItems.Style = ProgressBarStyle.Continuous;
                this._mainForm.txtBackupStep.Text = "Reinige Projektdaten: Gestartet";
                this.ResetTextBoxes();
                //this._bgwCleanProject.RunWorkerAsync(this._projectManager.ActiveProject);
                this._bgwCleanProject.RunWorkerAsync(new BackgroundWorkerTransferArgs(this._logWriter, this._projectManager));
            }
            else
            {
                this._logWriter.WriteLogLine("Die Bereinigung der Projektdatei konnte nicht gestartet werden, da ein anderer Sicherungsvorgang noch nicht abgeschlossen ist");
                MessageBox.Show("Die Bereinigung der Projektdatei konnte nicht gestartet werden, da ein anderer Sicherungsvorgang noch nicht abgeschlossen ist.\n\nDer Vorgang und alle nachfolgenden Vorgänge wurden abgebrochen.", "Vorgang abgebropchen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cencels the ClenProject process
        /// </summary>
        internal void CancelBackup_CleanProject()
        {
            this._bgwCleanProject.CancelAsync();
        }

        private void bgwCleanProject_DoWork(object sender, DoWorkEventArgs e)
        {
            //QBC.BackupProject.Project Project = (QBC.BackupProject.Project)e.Argument;
            BackgroundWorker Worker = sender as BackgroundWorker;
            QBC.BackupProject.Project Project = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).ProjectManage.ActiveProject;
            QBC.BackupProject.LogFileWriter LogFileWriter = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).LogFileWriter;
            Project.Progress.Clean_ItemsToCheck = 0;

            // Get numer of Items
            Project.Progress.Clean_ItemsToCheck += Project.ToDoDirectory.Count;
            Project.Progress.Clean_ItemsToCheck += Project.ToDoFile.Count;
            foreach (KeyValuePair<string, System.Collections.Generic.List<string>> FileIst in Project.ToDoFile)
            {
                Project.Progress.Clean_ItemsToCheck += FileIst.Value.Count;
            }
            LogFileWriter.WriteLogLine(string.Format("Zu überprüfende Elemente: {0}\r\n", Project.Progress.Clean_ItemsToCheck));
            LogFileWriter.WriteLogLine("Überprüfte Elemente:");
            LogFileWriter.WriteLogLine("--------------------\r\n");
            Worker.ReportProgress(0, Project.Progress.Clone());

            // Clean items
            // Directorys
            LogFileWriter.WriteLogLine("Ordner:");
            foreach (KeyValuePair<string, QBC.BackupProject.Project.enmDirectoryScope> Directory in new Dictionary<string, QBC.BackupProject.Project.enmDirectoryScope>(Project.ToDoDirectory))
            {
                LogFileWriter.WriteLogLine("Pfad:               " + Directory.Key, 4);
                LogFileWriter.WriteLogLine("Vorgehen:           " + Directory.Value.ToString(), 4);
                Project.Progress.Clean_ItemsChecked++;
                if (!new DirectoryInfo(Directory.Key).Exists)
                {
                    Project.ToDoDirectory.Remove(Directory.Key);
                    LogFileWriter.WriteLogLine("Bereinigung Aktion: Entfernen - Ordner nicht gefunden\r\n", 4);
                }
                else
                {
                    LogFileWriter.WriteLogLine("Bereinigung Aktion: Behalten\r\n", 4);
                }
                Worker.ReportProgress(0, Project.Progress.Clone());
            }
            //Files
            LogFileWriter.WriteLogLine("Dateien:");
            foreach (KeyValuePair<string, System.Collections.Generic.List<string>> FileList in new Dictionary<string, System.Collections.Generic.List<string>>(Project.ToDoFile).OrderBy(o => o.Key))
            {
                LogFileWriter.WriteLogLine("Ordner Pfad:        " + FileList.Key, 4);
                Project.Progress.Clean_ItemsChecked++;
                if (!new DirectoryInfo(FileList.Key).Exists)
                {
                    Project.Progress.Clean_ItemsChecked += FileList.Value.Count;
                    Project.ToDoDirectory.Remove(FileList.Key);
                    LogFileWriter.WriteLogLine("Bereinigung Aktion: Entfernen, inklusive aller zugehörigen Dateien - Ordner nicht gefunden\r\n", 4);
                    Worker.ReportProgress(0, Project.Progress.Clone());
                }
                else
                {
                    LogFileWriter.WriteLogLine("Bereinigung Aktion: Ordner behalten\r\n", 4);
                    System.Collections.Generic.List<string> FileListSort = new System.Collections.Generic.List<string>(Project.ToDoFile[FileList.Key]);
                    FileListSort.Sort(); // Sort Filelist
                    foreach (string FileItem in FileListSort)
                    {
                        LogFileWriter.WriteLogLine("Dateipfad:          " + FileItem, 8);
                        if (!new FileInfo(FileItem).Exists)
                        {
                            LogFileWriter.WriteLogLine("Bereinigung Aktion: Entfernen - Datei nicht gefunden\r\n", 8);
                            Project.ToDoFile[FileList.Key].Remove(FileItem);
                        }
                        else
                        {
                            LogFileWriter.WriteLogLine("Bereinigung Aktion: Datei behalten\r\n", 8);
                        }
                        Project.Progress.Clean_ItemsChecked++;
                        Worker.ReportProgress(0, Project.Progress.Clone());
                    }
                }
            }
        }

        private void bgwCleanProject_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackupResponse Progress = (BackupResponse)e.UserState;

            double CleanItemPercent = OLKI.Tools.CommonTools.Matehmatics.Percentages(Progress.Clean_ItemsChecked, Progress.Clean_ItemsToCheck);
            this._mainForm.txtAllItemsPer.Text = string.Format(@"{0}%", CleanItemPercent);
            this._mainForm.txtAllItemsNum.Text = string.Format(@"{0:n0} / {1:n0}", new object[] { Progress.Clean_ItemsChecked, Progress.Clean_ItemsToCheck });
            this._mainForm.pbaAllItems.Value =(int) CleanItemPercent;
        }

        private void bgwCleanProject_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this._logWriter.WriteLogLine("\r\nBereinigung der Projektdatei abgeschlossen");
            this._logWriter.WriteLogLine("Durch Benutzer abgebrochen: " + e.Cancelled.ToString());
            this._logWriter.WriteLogLine("Durch Fehler abgebrochen:   " + (e.Error != null).ToString());
            this._logWriter.WriteLogLine('+', 100);
            this._logWriter.WriteLogLine("\r\n");
            this._mainForm.txtBackupStep.Text = "Reinige Projektdaten: Beendet";
            this._mainForm.btnCancelBackupCleanProject.Enabled = false;
            this._mainForm.pbaAllItems.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllItems.Value = 0;
            this._mainForm.pbaAllByte.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllDir.Style = ProgressBarStyle.Continuous;
            this.ResetTextBoxes();

            DialogResult GoToNextStep = this.GetCancelGoToNextStep(e.Cancelled && (this._projectManager.ActiveProject.Settings.CountItemsAndBytes || this._projectManager.ActiveProject.Settings.CreateBackup));
            if (GoToNextStep == DialogResult.Yes && this._projectManager.ActiveProject.Settings.CountItemsAndBytes)
            {
                this.StartWorker_CalculateVolume();
            }
            else if (GoToNextStep == DialogResult.Yes && this._projectManager.ActiveProject.Settings.CreateBackup)
            {
                this.StartWorker_CreateBackup();
            }
            else
            {
                this._mainForm.btnStartBackup.Enabled = true;
            }
        }
        #endregion

        #region backgroundWorker CalculateVolume
        /// <summary>
        /// Starts the counting process for items an bytes
        /// </summary>
        private void StartWorker_CalculateVolume()
        {
            this._logWriter.WriteLogLine('+', 100);
            this._logWriter.WriteLogLine("Starte ermittlung des Aufgabenumfangs");
            if (this._bgwCalculateVolume.IsBusy != true && this._bgwCreateBackup.IsBusy != true)
            {
                this._mainForm.txtBackupStep.Text = "Bestimme Aufgabenumfang: Gestartet";
                this._mainForm.btnCancelBackupCalculateVolume.Enabled = true;
                this._mainForm.pbaAllItems.Style = ProgressBarStyle.Marquee;
                this._mainForm.pbaAllByte.Style = ProgressBarStyle.Marquee;
                this._mainForm.pbaAllDir.Style = ProgressBarStyle.Marquee;
                this.ResetTextBoxes();
                this._bgwCalculateVolume.RunWorkerAsync(new BackgroundWorkerTransferArgs(this._logWriter, this._projectManager));
            }
            else
            {
                this._logWriter.WriteLogLine("Die Ermittlung des Aufgabenumfangs konnte nicht gestartet werden, da ein anderer Sicherungsvorgang noch nicht abgeschlossen ist.");
                MessageBox.Show("Die Ermittlung des Aufgabenumfangs konnte nicht gestartet werden, da ein anderer Sicherungsvorgang noch nicht abgeschlossen ist.\n\nDer Vorgang und alle nachfolgenden Vorgänge wurden abgebrochen.", "Vorgang abgebropchen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cencels the CalculatVolume process
        /// </summary>
        internal void CancelBackup_CalculateVolume()
        {
            this._bgwCalculateVolume.CancelAsync();
        }

        private void bgwCalculateVolume_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker Worker = sender as BackgroundWorker;
                QBC.BackupProject.Project Project = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).ProjectManage.ActiveProject;
                QBC.BackupProject.LogFileWriter LogFileWriter = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).LogFileWriter;

                LogFileWriter.WriteLogLine("Gestartet um:      " + DateTime.Now.ToString());
                Worker.ReportProgress(0, Project.Progress.Clone());
                foreach (KeyValuePair<string, QBC.BackupProject.Project.enmDirectoryScope> item in Project.ToDoDirectory.OrderBy(o => o.Key))
                {
                    if (this.CheckCancelWorker(Worker, e)) return;
                    DirectoryInfo Directory = new DirectoryInfo(item.Key);
                    /*
                    //!!!!
                    //Problem - Zählt Laufwerke nicht als Ordner weil System, Aber Zählt Ordner beim Kopieren hoch
                    //!!!!
                    */
                    if ((OLKI.Tools.CommonTools.DirectoryAndFile.Directory_CheckAccess(Directory) || QBC.Properties.Settings.Default.ShowDirectorysWithoutAccess) /*&& (QBC.Properties.Settings.Default.ShowSystemDirectory || (Directory.Attributes & FileAttributes.System) != FileAttributes.System )*/)
                    {
                        this.CalculateVolume_CountItemsAndBytes(item.Key, item.Value, Project, Worker, e);
                    }
                }
                e.Result = Project.Progress;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Beim ermitteln des Aufgabenumfangs ist ein Fehler aufgetreten.\n\n{0}", new object[] { ex.Message }), "Fehler beim Ermitteln des Aufgabenumfangs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bgwCalculateVolume_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackupResponse Progress = (BackupResponse)e.UserState;
            this._mainForm.txtActualDir.Text = Progress.Count_ActDireName;
            this._mainForm.txtActualFile.Text = Progress.Count_ActFileName;

            this._mainForm.txtAllItemsNum.Text = string.Format("{0:n0}", (Progress.Count_AllDirec + Progress.Count_AllFiles).ToString());

            int DimensionCount = 0;
            OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase BaseCount = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.IEC;
            string CountSize = Progress.Copy_AllByte.ToString();
            if (this._mainForm.cboAllByteNum.SelectedIndex > -1)
            {
                if (this._mainForm.cboAllByteNum.SelectedIndex < OLKI.Tools.CommonTools.DirectoryAndFile.UnitPrefix_IEC.Length)
                {
                    BaseCount = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.IEC;
                    DimensionCount = this._mainForm.cboAllByteNum.SelectedIndex;
                }
                else
                {
                    BaseCount = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.SI;
                    DimensionCount = this._mainForm.cboAllByteNum.SelectedIndex - OLKI.Tools.CommonTools.DirectoryAndFile.UnitPrefix_IEC.Length;
                }
                CountSize = OLKI.Tools.CommonTools.DirectoryAndFile.ConvertSize_Convert(Progress.Count_AllBytes, 2, BaseCount, DimensionCount, true);
            }
            this._mainForm.txtAllByteNum.Text = string.Format("{0:n0}", CountSize);
            this._mainForm.txtAllByteNum.Tag = new QBC.BackupProject.BackupResponse.SizeCopyData(Progress.Count_AllBytes, -1);
            this._mainForm.txtAllDirNum.Text = string.Format("{0:n0}", Progress.Count_AllDirec.ToString());
        }

        private void bgwCalculateVolume_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            QBC.BackupProject.Project Project = null;
            try
            {
                Project = (QBC.BackupProject.Project)e.Result;
            }
            catch
            {
                //Project = new QBC.BackupProject.Project();
                //Project.Progress = new QBC.BackupProject.BackupResponse();
            }
            if (Project != null)
            {
                this._logWriter.WriteLogLine("Gefundene Ordner:  " + Project.Progress.Count_AllDirec.ToString());
                this._logWriter.WriteLogLine("Gefundene Dateien: " + Project.Progress.Count_AllFiles.ToString());
                this._logWriter.WriteLogLine("Gefundene Bytes:   " + Project.Progress.Count_AllBytes.ToString());
            }
            else
            {
                this._logWriter.WriteLogLine("Kopiertre Ordner:  Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
                this._logWriter.WriteLogLine("Kopiertre Dateien: Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
                this._logWriter.WriteLogLine("Kopiertre Bytes:   Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
            }
            this._logWriter.WriteLogLine("Beendet um:        " + DateTime.Now.ToString());
            this._logWriter.WriteLogLine("Ermittlung des Aufgabenumfangs abgeschlossen");
            this._logWriter.WriteLogLine("Durch Benutzer abgebrochen: " + e.Cancelled.ToString());
            this._logWriter.WriteLogLine("Durch Fehler abgebrochen:   " + (e.Error != null).ToString());
            this._logWriter.WriteLogLine('+', 100);
            this._logWriter.WriteLogLine("\r\n");
            this._mainForm.txtBackupStep.Text = "Bestimme Aufgabenumfang: Beendet";
            this._mainForm.btnCancelBackupCalculateVolume.Enabled = false;
            this._mainForm.pbaAllItems.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllItems.Value = 0;
            this._mainForm.pbaAllByte.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllByte.Value = 0;
            this._mainForm.pbaAllDir.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllDir.Value = 0;
            this._mainForm.txtActualDir.Text = string.Empty;
            this._mainForm.txtActualFile.Text = string.Empty;

            DialogResult GoToNextStep = this.GetCancelGoToNextStep(e.Cancelled && this._projectManager.ActiveProject.Settings.CreateBackup);
            if (GoToNextStep == DialogResult.Yes && this._projectManager.ActiveProject.Settings.CreateBackup)
            {
                this.StartWorker_CreateBackup();
            }
            else
            {
                this._mainForm.btnStartBackup.Enabled = true;
            }
        }

        /// <summary>
        /// Counts items and bytes to copy by a recusive process
        /// </summary>
        /// <param name="directory">The address of a null-terminated string that specifies the path of the directory to cound and goes recusive to sub directories</param>
        /// <param name="scope">Specifies the scope of what to do with the specified directory</param>
        /// <param name="project">Specifies the project object</param>
        /// <param name="worker">Specifies the background worker to use for count items</param>
        /// <param name="e">Specifies the DoWorkerEventArgs of the speified background worker</param>
        private void CalculateVolume_CountItemsAndBytes(string directory, QBC.BackupProject.Project.enmDirectoryScope scope, QBC.BackupProject.Project project, BackgroundWorker worker, DoWorkEventArgs e)
        {
            project.Progress.Count_ActDireName = directory;
            project.Progress.Count_ActFileName = string.Empty;

            // Count nothing
            if (scope == BackupProject.Project.enmDirectoryScope.Nothing)
            {
                worker.ReportProgress(0, project.Progress);
                return; // Skip all other lines to save tiem
            }
            // Count selected files
            else if (scope == BackupProject.Project.enmDirectoryScope.Selected)
            {
                project.Progress.Count_AllDirec++;
                worker.ReportProgress(0, project.Progress.Clone());
                if (this._projectManager.ActiveProject.ToDoFile.ContainsKey(directory))
                {
                    System.Collections.Generic.List<string> FileListSort = new System.Collections.Generic.List<string>(this._projectManager.ActiveProject.ToDoFile[directory]);
                    FileListSort.Sort(); // Sort Filelist
                    //foreach (string SFile in this._projectManager.ActiveProject.ToDoFile[directory])
                    FileInfo FileInfo = null;
                    foreach (string SFile in FileListSort)
                    {
                        if (this.CheckCancelWorker(worker, e)) return;
                        FileInfo = new FileInfo(SFile);
                        if (FileInfo.Exists)
                        {
                            project.Progress.Count_AllFiles++;
                            project.Progress.Count_AllBytes += SFile.Length;
                            worker.ReportProgress(0, project.Progress.Clone());
                        }
                    }
                }
                return; // Skip all other lines to save tiem
            }
            // Count all Files and go deeper recusivce
            else if (scope == BackupProject.Project.enmDirectoryScope.All)
            {
                project.Progress.Count_AllDirec++;
                worker.ReportProgress(0, project.Progress.Clone());
                // Count Files
                foreach (System.IO.FileInfo SFile in new System.IO.DirectoryInfo(directory).GetFiles().OrderBy(o => o.Name))
                {
                    if (this.CheckCancelWorker(worker, e)) return;
                    project.Progress.Count_AllFiles++;
                    project.Progress.Count_AllBytes += SFile.Length;
                    worker.ReportProgress(0, project.Progress.Clone());
                }
                // Go deeper
                foreach (System.IO.DirectoryInfo SDirectory in new System.IO.DirectoryInfo(directory).GetDirectories().OrderBy(o => o.Name))
                {
                    if (this.CheckCancelWorker(worker, e)) return;
                    if ((OLKI.Tools.CommonTools.DirectoryAndFile.Directory_CheckAccess(SDirectory) || QBC.Properties.Settings.Default.ShowDirectorysWithoutAccess) && (QBC.Properties.Settings.Default.ShowSystemDirectory || (SDirectory.Attributes & FileAttributes.System) != FileAttributes.System))
                    {
                        this.CalculateVolume_CountItemsAndBytes(SDirectory.FullName, BackupProject.Project.enmDirectoryScope.All, project, worker, e);
                    }
                }
                return; // Skip all other lines to save tiem
            }
        }
        #endregion

        #region backgroundWorker BackupCreator
        /// <summary>
        /// Starts the copieng process
        /// </summary>
        private void StartWorker_CreateBackup()
        {
            if (this._bgwCreateBackup.IsBusy != true)
            {
                this._logWriter.WriteLogLine('+', 100);
                this._logWriter.WriteLogLine("Starte Datensicherung");
                if (this._projectManager.ActiveProject.Progress.Count_AllBytes > 0)
                {
                    if ((this._projectManager.ActiveProject.Settings.BackupTargetDrive.AvailableFreeSpace - this._projectManager.ActiveProject.Settings.BackupTargetDrive.AvailableFreeSpace * 0.1) < this._projectManager.ActiveProject.Progress.Count_AllBytes)
                    {
                        string DriveSpaceMessage = string.Empty;
                        DriveSpaceMessage += "Auf dem Zieldatenträger scheint weniger oder nur wenig mehr Speicherplatz vorhanden zu sein,";
                        DriveSpaceMessage += "als für das Sichern der Daten erforderlich ist.\nDurch";
                        DriveSpaceMessage += "unterschiedliche Formatierungen kann es jedoch zu ungenauigkeiten kommen.\n\n";
                        DriveSpaceMessage += "Erforderlicher Speicherplatz: {0}\n";
                        DriveSpaceMessage += "Vorhandener Speicherplatz: {1}\n\n";
                        DriveSpaceMessage += "Möchten Sie den Sicherungsvorgang dennoch durchführen?";

                        string SizeToDo = OLKI.Tools.CommonTools.DirectoryAndFile.ConvertSize_Convert(this._projectManager.ActiveProject.Progress.Count_AllBytes);
                        string SpaceTarget = OLKI.Tools.CommonTools.DirectoryAndFile.ConvertSize_Convert(this._projectManager.ActiveProject.Settings.BackupTargetDrive.AvailableFreeSpace);
                        if (MessageBox.Show(string.Format(DriveSpaceMessage, new object[] { SizeToDo, SpaceTarget }), "Speicherplatzwarnung!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        {
                            this._logWriter.WriteLogLine("Auf dem Zieldatenträger war nicht genug Speicherplatz vorhanden. Vorgang durch den Bentuzer abgebrochen.");
                            this._mainForm.btnStartBackup.Enabled = true;
                            return;
                        }
                    }
                }

                if (this._projectManager.ActiveProject.Settings.CountItemsAndBytes)
                {
                    this._mainForm.pbaAllItems.Style = ProgressBarStyle.Continuous;
                    this._mainForm.pbaAllByte.Style = ProgressBarStyle.Continuous;
                    this._mainForm.pbaAllDir.Style = ProgressBarStyle.Continuous;
                }
                else
                {
                    this._mainForm.pbaAllItems.Style = ProgressBarStyle.Marquee;
                    this._mainForm.pbaAllByte.Style = ProgressBarStyle.Marquee;
                    this._mainForm.pbaAllDir.Style = ProgressBarStyle.Marquee;
                }

                this._projectManager.ActiveProject.Progress.Copy_Startime = System.DateTime.Now;
                this._mainForm.btnCancelBackupCreateBackup.Enabled = true;
                this._mainForm.txtBackupStep.Text = "Kopiere Daten: Gestartet";
                this.ResetTextBoxes();
                this._mainForm.txtCopyStart.Text = this._projectManager.ActiveProject.Progress.Copy_Startime.ToString();
                //this._bgwCreateBackup.RunWorkerAsync(this._projectManager.ActiveProject);
                this._bgwCreateBackup.RunWorkerAsync((new BackgroundWorkerTransferArgs(this._logWriter, this._projectManager)));
            }
            else
            {
                this._logWriter.WriteLogLine("Der Kopiervorgang konnte nicht gestartet werden, da ein anderer Sicherungsvorgang noch nicht abgeschlossen ist.");
                MessageBox.Show("Der Kopiervorgang konnte nicht gestartet werden, da ein anderer Sicherungsvorgang noch nicht abgeschlossen ist.\n\nDer Vorgang und alle nachfolgenden Vorgänge wurden abgebrochen.", "Vorgang abgebropchen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cencels the CreateBackup  process
        /// </summary>
        internal void CancelBackup_CreateBackup()
        {
            this._bgwCreateBackup.CancelAsync();
        }

        private void bgwCreateBackup_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker Worker = sender as BackgroundWorker;
            QBC.BackupProject.Project Project = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).ProjectManage.ActiveProject;
            QBC.BackupProject.LogFileWriter LogFileWriter = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).LogFileWriter;
            string BackupPath = Project.Settings.BackupTargetPath;

            LogFileWriter.WriteLogLine("Gestartet um:      " + DateTime.Now.ToString());
            LogFileWriter.WriteLogLine("Im folgenden werden nur Fehler aufgelistet\r\n");
            LogFileWriter.WriteLogLine("Fehler:\r\n");
            //Worker.ReportProgress(0, Project.Progress.Clone());
            
            Worker.ReportProgress(0, Project.Progress.Clone());
            foreach (KeyValuePair<string, QBC.BackupProject.Project.enmDirectoryScope> item in Project.ToDoDirectory.OrderBy(o => o.Key))
            {
                if (this.CheckCancelWorker(Worker, e)) return;

                // Get Target Path
                if (item.Key.Length == 3)
                {
                    BackupPath = Project.Settings.BackupTargetPath + @"\";
                    if (Project.Settings.CreateDriveRootDirectory)
                    {
                        BackupPath += item.Key.Substring(0, 1) + @"\";
                    }
                }
                BackupPath = OLKI.Tools.CommonTools.DirectoryAndFile.Path_Repair(BackupPath);
                this.CopyDirectory(item.Key, item.Value, BackupPath, Project, Worker, e);
            }
            e.Result = Project.Progress;
        }

        private void bgwCreateBackup_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackupResponse Progress = (BackupResponse)e.UserState;

            TimeSpan ElapsedTime = (System.DateTime.Now - Progress.Copy_Startime);
            this._mainForm.txtCopyElapsed.Text = ElapsedTime.ToString(@"dd\.hh\:mm\:ss");

            // Remain time by Items
            if (Progress.Copy_AllItems > 0 && Progress.Count_AllItems > 0)
            {
                //TimeSpan ExpectedTotalTimeItems = new TimeSpan(ElapsedTime.Ticks* (Progress.Count_AllItems / Progress.Copy_AllItems));
                double FactorTimeItem = (double)Progress.Count_AllItems / (double)Progress.Copy_AllItems;
                double ExpectedTotalTimeItemsMS = ElapsedTime.TotalMilliseconds * FactorTimeItem;
                TimeSpan ExpectedTotalTimeItems = TimeSpan.FromMilliseconds(ExpectedTotalTimeItemsMS);
                this._mainForm.txtCopyRemainItem.Text = (ExpectedTotalTimeItems - ElapsedTime).ToString(@"dd\.hh\:mm\:ss");
            }

            // Remain time by Bytes
            if (Progress.Copy_AllByte > 0 && Progress.Count_AllBytes > 0)
            {
                //ExpectedTotalTimeByte = TimeSpan.FromMilliseconds
                //ExpectedTotalTimeByte.TotalMilliseconds = ExpectedTotalTimeByteDouble;
                //TimeSpan ExpectedTotalTimeByte = new TimeSpan(ElapsedTime.TotalMilliseconds * (Progress.Count_AllBytes / Progress.Copy_Byte));
                double FactorTimeByte = (double)Progress.Count_AllBytes / (double)Progress.Copy_AllByte;
                double ExpectedTotalTimeByteMS = ElapsedTime.TotalMilliseconds * FactorTimeByte;
                TimeSpan ExpectedTotalTimeByte = TimeSpan.FromMilliseconds(ExpectedTotalTimeByteMS);
                this._mainForm.txtCopyRemainByte.Text = (ExpectedTotalTimeByte - ElapsedTime).ToString(@"dd\.hh\:mm\:ss");
            }


            this._mainForm.txtActualDir.Text = Progress.Copy_ActDireName;
            this._mainForm.txtActualFile.Text = Progress.Copy_ActFileName;

            // Progress all items
            double ActItemPercent = OLKI.Tools.CommonTools.Matehmatics.Percentages(Progress.Copy_AllItems, Progress.Count_AllItems);
            this._mainForm.txtAllItemsPer.Text = string.Format(@"{0}%", ActItemPercent);
            this._mainForm.txtAllItemsNum.Text = string.Format(@"{0:n0} / {1:n0}", new object[] { Progress.Copy_AllItems, Progress.Count_AllItems });
            this._mainForm.pbaAllItems.Value = (int)ActItemPercent;
            this._mainForm.tabPageBackup.Text = string.Format("Ziel - Fortschritt {0}%", ActItemPercent);

            // Progress all bytes
            double ActBytePercent = OLKI.Tools.CommonTools.Matehmatics.Percentages(Progress.Copy_AllByte, Progress.Count_AllBytes);
            int DimensionAll = 0;
            OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase BaseAll = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.IEC;
            string Copy_AllByte = Progress.Copy_AllByte.ToString();
            string Count_AllBytes = Progress.Count_AllBytes.ToString();
            if (this._mainForm.cboAllByteNum.SelectedIndex > -1)
            {
                if (this._mainForm.cboAllByteNum.SelectedIndex < OLKI.Tools.CommonTools.DirectoryAndFile.UnitPrefix_IEC.Length)
                {
                    BaseAll = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.IEC;
                    DimensionAll = this._mainForm.cboAllByteNum.SelectedIndex;
                }
                else
                {
                    BaseAll = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.SI;
                    DimensionAll = this._mainForm.cboAllByteNum.SelectedIndex - OLKI.Tools.CommonTools.DirectoryAndFile.UnitPrefix_IEC.Length;
                }
                Copy_AllByte = OLKI.Tools.CommonTools.DirectoryAndFile.ConvertSize_Convert(Progress.Copy_AllByte, 2, BaseAll, DimensionAll, true);
                Count_AllBytes = OLKI.Tools.CommonTools.DirectoryAndFile.ConvertSize_Convert(Progress.Count_AllBytes, 2, BaseAll, DimensionAll, true);
            }
            this._mainForm.txtAllBytePer.Text = string.Format(@"{0}%", ActBytePercent);
            this._mainForm.txtAllByteNum.Tag = new QBC.BackupProject.BackupResponse.SizeCopyData(Progress.Count_AllBytes, Progress.Copy_AllByte);
            this._mainForm.txtAllByteNum.Text = string.Format(@"{0} / {1}", new object[] { Copy_AllByte, Count_AllBytes });
            this._mainForm.pbaAllByte.Value = (int)ActBytePercent;

            // Progress all directorys
            double ActDirePercent = OLKI.Tools.CommonTools.Matehmatics.Percentages(Progress.Copy_AllDire, Progress.Count_AllDirec);
            this._mainForm.txtAllDirPer.Text = string.Format(@"{0}%", ActDirePercent);
            this._mainForm.txtAllDirNum.Text = string.Format(@"{0:n0} / {1:n0}", new object[] { Progress.Copy_AllDire, Progress.Count_AllDirec });
            this._mainForm.pbaAllDir.Value = (int)ActDirePercent;

            // Progress files in directorys
            double ActFilePercent = OLKI.Tools.CommonTools.Matehmatics.Percentages(Progress.Copy_ActDireActFiles, Progress.Copy_ActDireAllFiles);
            this._mainForm.txtActualDirFilesPer.Text = string.Format(@"{0}%", ActFilePercent);
            this._mainForm.txtActualDirFilesNum.Text = string.Format(@"{0:n0} / {1:n0}", new object[] { Progress.Copy_ActDireActFiles, Progress.Copy_ActDireAllFiles });
            this._mainForm.pbaActualDirFiles.Value = (int)ActFilePercent;

            // Actual file bytes
            double ActFileBytePercent = OLKI.Tools.CommonTools.Matehmatics.Percentages(Progress.Copy_ActFileByte, Progress.Copy_ActFileAllByte);
            int DimensionFile = 0;
            OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase BaseFile = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.IEC;
            string Copy_ActFileByte = Progress.Copy_ActFileByte.ToString();
            string Copy_ActFileAllByte = Progress.Copy_ActFileAllByte.ToString();
            if (this._mainForm.cboActualFileByteNum.SelectedIndex > -1)
            {
                if (this._mainForm.cboActualFileByteNum.SelectedIndex < OLKI.Tools.CommonTools.DirectoryAndFile.UnitPrefix_IEC.Length)
                {
                    BaseFile = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.IEC;
                    DimensionFile = this._mainForm.cboActualFileByteNum.SelectedIndex;
                }
                else
                {
                    BaseFile = OLKI.Tools.CommonTools.DirectoryAndFile.FileSize.ByteBase.SI;
                    DimensionFile = this._mainForm.cboActualFileByteNum.SelectedIndex - OLKI.Tools.CommonTools.DirectoryAndFile.UnitPrefix_IEC.Length;
                }
                Copy_ActFileByte = OLKI.Tools.CommonTools.DirectoryAndFile.ConvertSize_Convert(Progress.Copy_ActFileByte, 0, BaseFile, DimensionFile, true);
                Copy_ActFileAllByte = OLKI.Tools.CommonTools.DirectoryAndFile.ConvertSize_Convert(Progress.Copy_ActFileAllByte, 0, BaseFile, DimensionFile, true);
            }
            this._mainForm.txtActualFileBytePer.Text = string.Format(@"{0}%", ActFileBytePercent);
            this._mainForm.txtActualFileByteNum.Tag = new QBC.BackupProject.BackupResponse.SizeCopyData(Progress.Copy_ActFileAllByte, Progress.Copy_ActFileByte);
            this._mainForm.txtActualFileByteNum.Text = string.Format(@"{0} / {1}", new object[] { Copy_ActFileByte, Copy_ActFileAllByte });
            this._mainForm.pbaActualFileByte.Value = (int)ActFileBytePercent;

            // Progress Summary
            if (Progress.Exception != null)
            {
                ListViewItem NewItem = new ListViewItem();
                NewItem.SubItems.Add(Progress.Exception.SourcePath);
                NewItem.SubItems.Add(Progress.Exception.TargetPath);
                NewItem.SubItems.Add(Progress.Exception.Exception.Message);
                NewItem.Tag = Progress.Exception;
                NewItem.Text = (this._mainForm.lsvErrorLog.Items.Count+1).ToString();
                //NewItem.Text = Progress.Exception.SourcePath;

                this._mainForm.lsvErrorLog.Items.Add(NewItem);
            }
            this._mainForm.tabPageConclusion.Text = string.Format("Zusammenfassung - Fehler {0:n0}", this._mainForm.lsvErrorLog.Items.Count);


            this._mainForm.txtCopiedDirectories.Text = string.Format(@"{0:n0}", new object[] { Progress.Copy_AllDire });
            this._mainForm.txtCopiedFiles.Text = string.Format(@"{0:n0}", new object[] { Progress.Copy_AllFile});
            this._mainForm.txtCopiedDuration.Text = ElapsedTime.ToString(@"dd\.hh\:mm\:ss");
            this._mainForm.txtExceptionCount.Text = string.Format(@"{0:n0}", new object[] { this._mainForm.lsvErrorLog.Items.Count });
            //this._mainForm.Refresh();
        }

        private void bgwCreateBackup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            QBC.BackupProject.Project Project = null;
            try
            {
                Project = (QBC.BackupProject.Project)e.Result;
            }
            catch
            {
                //Project = new QBC.BackupProject.Project();
                //Project.Progress = new QBC.BackupProject.BackupResponse();
            }
            if (Project != null)
            {
                this._logWriter.WriteLogLine(string.Format("Kopiertre Ordner:  {0}/{1}", new object[] { Project.Progress.Copy_AllDire, Project.Progress.Count_AllDirec }));
                this._logWriter.WriteLogLine(string.Format("Kopiertre Dateien: {0}/{1}", new object[] { Project.Progress.Copy_AllFile, Project.Progress.Count_AllFiles }));
                this._logWriter.WriteLogLine(string.Format("Kopiertre Bytes:   {0}/{1}", new object[] { Project.Progress.Copy_AllByte, Project.Progress.Count_AllBytes }));
            }
            else
            {
                this._logWriter.WriteLogLine("Kopiertre Ordner:  Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
                this._logWriter.WriteLogLine("Kopiertre Dateien: Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
                this._logWriter.WriteLogLine("Kopiertre Bytes:   Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
            }
            this._logWriter.WriteLogLine("Beendet um:        " + DateTime.Now.ToString());
            this._logWriter.WriteLogLine("Kopieren der Daten abgeschlossen");
            this._logWriter.WriteLogLine("Durch Benutzer abgebrochen: " + e.Cancelled.ToString());
            this._logWriter.WriteLogLine("Durch Fehler abgebrochen:   " + (e.Error != null).ToString());
            this._logWriter.WriteLogLine('+', 100);
            this._logWriter.WriteLogLine("\r\n");

            this._mainForm.txtBackupStep.Text = "Kopiere Daten: Beendet";
            this._mainForm.txtActualDir.Text = string.Empty;
            this._mainForm.txtActualFile.Text = string.Empty;
            this._mainForm.btnCancelBackupCreateBackup.Enabled = false;
            this._mainForm.btnStartBackup.Enabled = true;

            this._mainForm.pbaAllItems.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllItems.Value = 0;
            this._mainForm.pbaAllByte.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllByte.Value = 0;
            this._mainForm.pbaAllDir.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllDir.Value = 0;
            this._mainForm.pbaActualDirFiles.Value = 0;
            this._mainForm.pbaActualFileByte.Value = 0;
        }

        /// <summary>
        /// Copies the specified directory, files and subdirectories by a recusive process,  with all attributes
        /// </summary>
        /// <param name="directory">The address of a null-terminated string that specifies the path of the directory to copy and goes recusive to sub directories</param>
        /// <param name="scope">Specifies the scope of what to do with the specified directory</param>
        /// <param name="backupDirectory">The address of a null-terminated string that specifies the path of the directory where the specified directory should been copied to</param>
        /// <param name="project">Specifies the project object</param>
        /// <param name="worker">Specifies the background worker to use for copying items</param>
        /// <param name="e">Specifies the DoWorkerEventArgs of the speified background worker</param>
        private void CopyDirectory(string directory, QBC.BackupProject.Project.enmDirectoryScope scope, string backupDirectory, QBC.BackupProject.Project project, BackgroundWorker worker, DoWorkEventArgs e)
        {
            QBC.BackupProject.LogFileWriter LogFileWriter = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).LogFileWriter;

            project.Progress.Exception = null;
            project.Progress.Copy_ActDireName = directory;
            project.Progress.Copy_ActFileName = string.Empty;
            project.Progress.Copy_ActDireAllFiles = 0;
            project.Progress.Copy_ActFileAllByte = 0;
            project.Progress.Copy_ActDireActFiles = 0;
            project.Progress.Copy_ActFileByte = 0;

            string NewDirectoryPath = backupDirectory + @"\" + directory.Substring(3);
            NewDirectoryPath = OLKI.Tools.CommonTools.DirectoryAndFile.Path_Repair(NewDirectoryPath);

            try
            {
                project.Progress.Exception = null;
                // Nothing to do
                if (scope == BackupProject.Project.enmDirectoryScope.Nothing)
                {
                    worker.ReportProgress(0, project.Progress.Clone());
                    return; // Skip all other lines to save tiem
                }

                // Create Directroy - initial progress
                project.Progress.Exception = null;
                project.Progress.Copy_ActDireAllFiles = 0;
                project.Progress.Copy_AllDire++;

                // Create Directory
                DirectoryInfo NewDirectroyInfo = Directory.CreateDirectory(NewDirectoryPath);
                worker.ReportProgress(0, project.Progress.Clone());

                // Copy Selected content
                if (scope == BackupProject.Project.enmDirectoryScope.Selected)
                {
                    project.Progress.Exception = null;
                    project.Progress.Copy_ActDireAllFiles = 0;
                    worker.ReportProgress(0, project.Progress.Clone());

                    // Copy Files
                    if (this._projectManager.ActiveProject.ToDoFile.ContainsKey(directory))
                    {
                        System.Collections.Generic.List<string> FileListSort = new System.Collections.Generic.List<string>(this._projectManager.ActiveProject.ToDoFile[directory]);
                        FileListSort.Sort(); // Sort Filelist
                        project.Progress.Copy_ActDireAllFiles = FileListSort.Count;
                        FileInfo FileInfo = null;
                        foreach (string SFile in FileListSort)
                        {
                            if (this.CheckCancelWorker(worker, e)) return;
                            FileInfo = new FileInfo(SFile);
                            project.Progress.Copy_ActFileAllByte = 0;
                            this.CopyFileBytewise(FileInfo.FullName, NewDirectoryPath + @"\" + FileInfo.Name, project, worker, e);
                        }
                    }
                }
                else if (scope == BackupProject.Project.enmDirectoryScope.All)
                {
                    project.Progress.Exception = null;
                    project.Progress.Copy_ActDireAllFiles = new DirectoryInfo(directory).GetFiles().LongLength;
                    worker.ReportProgress(0, project.Progress.Clone());

                    // Copy Files
                    foreach (System.IO.FileInfo SFile in new System.IO.DirectoryInfo(directory).GetFiles().OrderBy(o => o.Name))
                    {
                        if (this.CheckCancelWorker(worker, e)) return;
                        project.Progress.Copy_ActFileAllByte = 0;
                        this.CopyFileBytewise(SFile.FullName, NewDirectoryPath + @"\" + SFile.Name, project, worker, e);
                    }
                    // Go deeper
                    foreach (System.IO.DirectoryInfo SDirectory in new System.IO.DirectoryInfo(directory).GetDirectories().OrderBy(o => o.Name))
                    {
                        if (this.CheckCancelWorker(worker, e)) return;
                        this.CopyDirectory(SDirectory.FullName, BackupProject.Project.enmDirectoryScope.All, backupDirectory, project, worker, e);
                    }
                    worker.ReportProgress(0, project.Progress.Clone());
                }

                // Copy Attributes
                DirectoryInfo SourceDirectoryInfo = new DirectoryInfo(directory);
                //DirectoryInfo NewDirectroyInfo = new DirectoryInfo(NewDirectoryPath);
                NewDirectroyInfo.Attributes = SourceDirectoryInfo.Attributes;
                NewDirectroyInfo.CreationTime = SourceDirectoryInfo.CreationTime;
                NewDirectroyInfo.LastAccessTime = SourceDirectoryInfo.LastAccessTime;
                NewDirectroyInfo.LastWriteTime = SourceDirectoryInfo.LastWriteTime;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteLogLine("Fehler beim Kopieren eines Ordners", 4);
                LogFileWriter.WriteLogLine("Quelle:      " + directory, 8);
                LogFileWriter.WriteLogLine("Ziel:        " + NewDirectoryPath, 8);
                LogFileWriter.WriteLogLine("Fehler:      " + ex.GetType().Name, 8);
                LogFileWriter.WriteLogLine("Meldung:     " + ex.Message, 8);
                LogFileWriter.WriteLogLine("");
                project.Progress.Exception = new BackupResponse.WorkerException(ex, directory, NewDirectoryPath);
                worker.ReportProgress(0, project.Progress.Clone());
            }
        }

        /// <summary>
        /// Copies the specified file bytewise, with all attributes
        /// </summary>
        /// <param name="source">The address of a null-terminated string that specifies the path of the file to copy</param>
        /// <param name="destination">The address of a null-terminated string that specifies the path of the destination to copy the specified file to</param>
        /// <param name="project">Specifies the project object</param>
        /// <param name="worker">Specifies the background worker to use for copying items</param>
        /// <param name="e">Specifies the DoWorkerEventArgs of the speified background worker</param>
        private void CopyFileBytewise(string source, string destination, QBC.BackupProject.Project project, BackgroundWorker worker, DoWorkEventArgs e)
        {
            QBC.BackupProject.LogFileWriter LogFileWriter = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).LogFileWriter;

            project.Progress.Exception = null;
            project.Progress.Copy_AllFile++;
            project.Progress.Copy_ActDireActFiles++;
            project.Progress.Copy_ActFileName = source;
            project.Progress.Copy_ActFileAllByte = 0;
            project.Progress.Copy_ActFileByte = 0;

            try
            {
                FileInfo SourceFileInfo = new FileInfo(source);
                project.Progress.Copy_ActFileAllByte = SourceFileInfo.Length;
                worker.ReportProgress(0, project.Progress.Clone());

                // Check for existing files
                FileInfo TargetFileCheck = new FileInfo(destination);
                if (TargetFileCheck.Exists)
                {
                    switch (project.Settings.HandleExistingItem)
                    {
                        case Project.ProjectSettings.enmHandleExistingItem.OverwriteAll:
                            TargetFileCheck.Delete();
                            break;
                        case Project.ProjectSettings.enmHandleExistingItem.OverwriteIfNewer:
                            if (DateTime.Compare(SourceFileInfo.LastWriteTime, TargetFileCheck.LastWriteTime) > 0)
                            {
                                TargetFileCheck.Delete();
                            }
                            else
                            {
                                project.Progress.Copy_ActFileByte += SourceFileInfo.Length;
                                project.Progress.Copy_AllByte += SourceFileInfo.Length;
                                worker.ReportProgress(0, project.Progress.Clone());
                                return;
                            }
                            break;
                        default:
                            string Message = string.Empty;
                            Message += string.Format("Die Zieldatei {0} existiert bereits.\n", destination);
                            Message += "Möchten Sie diese Datei überschreiben?\n\n";
                            Message += "Quelldatei:\n";
                            Message += source + "\n";
                            Message += string.Format("Erstellt am: {0}\n", SourceFileInfo.CreationTime);
                            Message += string.Format("Zuletzt bearbeitet am: {0}\n", SourceFileInfo.LastWriteTime);
                            Message += string.Format("Dateigröße (Byte): {0}\n\n", SourceFileInfo.Length);
                            Message += "Zieldatei:\n";
                            Message += destination + "\n";
                            Message += string.Format("Erstellt am: {0}\n", TargetFileCheck.CreationTime);
                            Message += string.Format("Zuletzt bearbeitet am: {0}\n", TargetFileCheck.LastWriteTime);
                            Message += string.Format("Dateigröße (Byte): {0}", TargetFileCheck.Length);

                            DialogResult ToDo = MessageBox.Show(Message, "Zieldatei vorhanden", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            switch (ToDo)
                            {
                                case DialogResult.Yes:
                                    TargetFileCheck.Delete();
                                    break;
                                case DialogResult.No:
                                    project.Progress.Copy_ActFileByte += SourceFileInfo.Length;
                                    project.Progress.Copy_AllByte += SourceFileInfo.Length;
                                    worker.ReportProgress(0, project.Progress.Clone());
                                    return;
                                default:
                                    worker.CancelAsync();
                                    break;
                            }
                            break;
                    }
                }

                // Copy Bytes
                int array_length = (int)Math.Pow(2, 19);
                //int array_length = 8;
                byte[] dataArray = new byte[array_length];
                using (FileStream fsread = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.None, array_length))
                {
                    using (BinaryReader bwread = new BinaryReader(fsread))
                    {
                        using (FileStream fswrite = new FileStream(destination + COPY_EXTENSION_ADDITION, FileMode.Create, FileAccess.Write, FileShare.None, array_length))
                        {
                            using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                            {
                                int read = 0;
                                while ((read = bwread.Read(dataArray, 0, array_length)) > 0)
                                {
                                    if (this.CheckCancelWorker(worker, e)) return;
                                    bwwrite.Write(dataArray, 0, read);
                                    project.Progress.Copy_ActFileByte += read;
                                    project.Progress.Copy_AllByte += read;
                                    worker.ReportProgress(0, project.Progress.Clone());
                                }
                            }
                        }
                    }
                }

                // Copy Attributes
                FileInfo NewFileInfo = new FileInfo(destination + COPY_EXTENSION_ADDITION);
                NewFileInfo.Attributes = SourceFileInfo.Attributes;
                NewFileInfo.CreationTime = SourceFileInfo.CreationTime;
                NewFileInfo.IsReadOnly = SourceFileInfo.IsReadOnly;
                NewFileInfo.LastAccessTime = SourceFileInfo.LastAccessTime;
                NewFileInfo.LastWriteTime = SourceFileInfo.LastWriteTime;

                // Rename, delete extension
                NewFileInfo.MoveTo(destination);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteLogLine("Fehler beim Kopieren einer Datei", 4);
                LogFileWriter.WriteLogLine("Quelle:      " + source, 8);
                LogFileWriter.WriteLogLine("Ziel:        " + destination, 8);
                LogFileWriter.WriteLogLine("Fehler:      " + ex.GetType().Name, 8);
                LogFileWriter.WriteLogLine("Meldung:     " + ex.Message, 8);
                //LogFileWriter.WriteLogLine("Stack-Trace: " + ex.StackTrace, 8);
                LogFileWriter.WriteLogLine("");
                project.Progress.Exception = new BackupResponse.WorkerException(ex, source, destination);
                worker.ReportProgress(0, project.Progress.Clone());
            }
        }
        #endregion

        #region backgroundWorker CalculateVolumeRestore
        /// <summary>
        /// Starts the counting process for items an bytes for restoring items from backup
        /// </summary>
        private void StartWorker_CalculateVolumeRestore()
        {
            this._logWriter.WriteLogLine('+', 100);
            this._logWriter.WriteLogLine("Starte ermittlung des Aufgabenumfangs für die Wiedergerstellung von gesichterten Datean");
            if (this._bgwCalculateVolume.IsBusy != true && this._bgwCreateBackup.IsBusy != true)
            {
                this._mainForm.txtBackupStep.Text = "Bestimme Aufgabenumfang für die Wiedergerstellung von gesichterten Datean: Gestartet";
                this._mainForm.btnCancelBackupCalculateVolume.Enabled = true;
                this._mainForm.pbaAllItems.Style = ProgressBarStyle.Marquee;
                this._mainForm.pbaAllByte.Style = ProgressBarStyle.Marquee;
                this._mainForm.pbaAllDir.Style = ProgressBarStyle.Marquee;
                this.ResetTextBoxes();
                this._bgwCalculateVolumeRestore.RunWorkerAsync(new BackgroundWorkerTransferArgs(this._logWriter, this._projectManager));
            }
            else
            {
                this._logWriter.WriteLogLine("Die Ermittlung des Aufgabenumfangs konnte nicht gestartet werden, da ein anderer Wiederherstellungsvorgang noch nicht abgeschlossen ist.");
                MessageBox.Show("Die Ermittlung des Aufgabenumfangs konnte nicht gestartet werden, da ein anderer Wiederherstellungsvorgang noch nicht abgeschlossen ist.\n\nDer Vorgang und alle nachfolgenden Vorgänge wurden abgebrochen.", "Vorgang abgebropchen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cencels the CalculatVolumeRestore process
        /// </summary>
        internal void CancelBackup_CalculateVolumeRestore()
        {
            this._bgwCalculateVolumeRestore.CancelAsync();
        }

        private void bgwCalculateVolumeRestore_DoWork(object sender, DoWorkEventArgs e)
        {
            //!!!!
            try
            {
                BackgroundWorker Worker = sender as BackgroundWorker;
                QBC.BackupProject.Project Project = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).ProjectManage.ActiveProject;
                QBC.BackupProject.LogFileWriter LogFileWriter = ((QBC.BackupProject.BackupCreator.BackgroundWorkerTransferArgs)e.Argument).LogFileWriter;

                LogFileWriter.WriteLogLine("Gestartet um:      " + DateTime.Now.ToString());
                Worker.ReportProgress(0, Project.Progress.Clone());
                //!!!!
/*
                foreach (KeyValuePair<string, QBC.BackupProject.Project.enmDirectoryScope> item in Project.ToDoDirectory.OrderBy(o => o.Key))
                {
                    if (this.CheckCancelWorker(Worker, e)) return;
                    DirectoryInfo Directory = new DirectoryInfo(item.Key);
                    if ((OLKI.Tools.CommonTools.DirectoryAndFile.Directory_CheckAccess(Directory) || QBC.Properties.Settings.Default.ShowDirectorysWithoutAccess) && (QBC.Properties.Settings.Default.ShowSystemDirectory || (Directory.Attributes & FileAttributes.System) != FileAttributes.System))
                    {
                        this.CalculateVolume_CountItemsAndBytes(item.Key, item.Value, Project.Progress, Worker, e);
                    }
                }
*/ 
                e.Result = Project.Progress;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Beim ermitteln des Aufgabenumfangs ist ein Fehler aufgetreten.\n\n{0}", new object[] { ex.Message }), "Fehler beim Ermitteln des Aufgabenumfangs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bgwCalculateVolumeRestore_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //????
            // The reporting is the same as for counting the items for creating the backup
            this.bgwCalculateVolume_ProgressChanged(sender, e);
        }

        private void bgwCalculateVolumeRestore_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            QBC.BackupProject.Project Project = null;
            try
            {
                Project = (QBC.BackupProject.Project)e.Result;
            }
            catch
            {
                //Project = new QBC.BackupProject.Project();
                //Project.Progress = new QBC.BackupProject.BackupResponse();
            }
            if (Project != null)
            {
                this._logWriter.WriteLogLine("Gefundene Ordner:  " + Project.Progress.Count_AllDirec.ToString());
                this._logWriter.WriteLogLine("Gefundene Dateien: " + Project.Progress.Count_AllFiles.ToString());
                this._logWriter.WriteLogLine("Gefundene Bytes:   " + Project.Progress.Count_AllBytes.ToString());
            }
            else
            {
                this._logWriter.WriteLogLine("Kopiertre Ordner:  Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
                this._logWriter.WriteLogLine("Kopiertre Dateien: Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
                this._logWriter.WriteLogLine("Kopiertre Bytes:   Konnte nicht ermittelt werden, da der Vorgang abgebrochen wurde");
            }
            this._logWriter.WriteLogLine("Beendet um:        " + DateTime.Now.ToString());
            this._logWriter.WriteLogLine("Ermittlung des Aufgabenumfangs abgeschlossen");
            this._logWriter.WriteLogLine("Durch Benutzer abgebrochen: " + e.Cancelled.ToString());
            this._logWriter.WriteLogLine("Durch Fehler abgebrochen:   " + (e.Error != null).ToString());
            this._logWriter.WriteLogLine('+', 100);
            this._logWriter.WriteLogLine("\r\n");
            this._mainForm.txtBackupStep.Text = "Bestimme Aufgabenumfang: Beendet";
            this._mainForm.btnCancelRestorelBackupCalculateVolume.Enabled = false;
            this._mainForm.pbaAllItems.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllItems.Value = 0;
            this._mainForm.pbaAllByte.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllByte.Value = 0;
            this._mainForm.pbaAllDir.Style = ProgressBarStyle.Continuous;
            this._mainForm.pbaAllDir.Value = 0;
            this._mainForm.txtActualDir.Text = string.Empty;
            this._mainForm.txtActualFile.Text = string.Empty;

            DialogResult GoToNextStep = this.GetCancelGoToNextStep(e.Cancelled && this._projectManager.ActiveProject.Settings.RestoreBackup);
            if (GoToNextStep == DialogResult.Yes && this._projectManager.ActiveProject.Settings.CreateBackup)
            {
                //!!!!this.StartWorker_RestoreBackup();
            }
            else
            {
                this._mainForm.btnStartRestoreBackup.Enabled = true;
            }
        }

        /// <summary>
        /// Counts items and bytes to copy by a recusive process for restoring items from backup
        /// </summary>
        /// <param name="directory">The address of a null-terminated string that specifies the path of the directory to cound and goes recusive to sub directories</param>
        /// <param name="scope">Specifies the scope of what to do with the specified directory</param>
        /// <param name="progress">Specifies the progress object to report the counting progress</param>
        /// <param name="worker">Specifies the background worker to use for count items</param>
        /// <param name="e">Specifies the DoWorkerEventArgs of the speified background worker</param>
        private void CalculateVolumeRestore_CountItemsAndBytes(string directory, QBC.BackupProject.Project.enmDirectoryScope scope, BackupResponse progress, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //!!!!
        }
        #endregion
        #endregion
        #endregion

        #region SubClasses
        /// <summary>
        /// Used to transfer  properties and and object to the backgroundworker and there functions
        /// </summary>
        private class BackgroundWorkerTransferArgs
        {
            #region Properties
            /// <summary>
            /// Specifies the log file writer object
            /// </summary>
            private QBC.BackupProject.LogFileWriter _logFileWriter = null;
            /// <summary>
            /// Get the log file writer object
            /// </summary>
            public QBC.BackupProject.LogFileWriter LogFileWriter
            {
                get
                {
                    return this._logFileWriter;
                }
            }

            /// <summary>
            /// Specifies the project manager of the running applications instance
            /// </summary>
            private QBC.ProjectManager _projectManage = null;
            /// <summary>
            /// Get the project manager of the running applications instance
            /// </summary>
            public QBC.ProjectManager ProjectManage
            {
                get
                {
                    return this._projectManage;
                }
            }

            /// <summary>
            /// Initialise a new instance of transfer object
            /// </summary>
            /// <param name="logFileWriter">Specifies the log file writer object</param>
            /// <param name="projectManage">Specifies the project manager of the running applications instance</param>
            internal BackgroundWorkerTransferArgs(QBC.BackupProject.LogFileWriter logFileWriter, QBC.ProjectManager projectManage)
            {
                this._logFileWriter = logFileWriter;
                this._projectManage = projectManage;
            }
            #endregion
        }
        #endregion
    }
}