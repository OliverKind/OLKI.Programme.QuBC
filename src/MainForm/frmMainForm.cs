/*
 * QBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
 * 
 * Desctiption:
 * The MainForm of the application
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

using OLKI.Programme.QBC.Properties;
using OLKI.Tools.CommonTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.src.MainForm
{
    /// <summary>
    /// The MainForm of the application
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants
        /// <summary>
        /// Defines the index of the tab page to select by defaukt.
        /// </summary>
        private const int DEBAULT_TAB_PAGE_INDEX = 0;
        /// <summary>
        /// Defines the format of title line text of these form
        /// </summary>
        private const string TITLE_LINE_FORMAT = "{0} [{1}{2}]";
        #endregion 

        #region Fields
        private readonly string _conclusionTabPageOriginalText;
        /// <summary>
        /// Provides helper functions for the application MainForm
        /// </summary>
        private readonly MainFormHelper _mainFormHelper = null;
        /// <summary>
        /// Specifies the projectmanager object
        /// </summary>
        private readonly ProjectManager _projectManager = new ProjectManager();
        /// <summary>
        /// Specifies the recent files object
        /// </summary>
        private readonly RecentFiles _recentFiles = new RecentFiles();
        /// <summary>
        /// Set True if changes on widgeds was done by system and change events should been ignored
        /// </summary>
        private bool _suppressControleEvents = false;
        /// <summary>
        /// A SaveFileDilaog to specifies the path where the Logfile should been saved to
        /// </summary9
        private readonly SaveFileDialog _saveLogFileDialog = new SaveFileDialog();
        /// <summary>
        /// Specifies the application about form
        /// </summary>
        private readonly SubForms.AboutForm _frmAbout = new SubForms.AboutForm();
        /// <summary>
        /// Specifies the application settings form
        /// </summary>
        private SubForms.ApplicationSettingsForm _frmApplicationSettings = null;
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new application MainForm
        /// </summary>
        /// <param name="args">Application start up arguments</param>
        internal MainForm(string[] args)
        {
            InitializeComponent();

            //TODO: REMOVE --> future versions to restore backups
            //tabPageRestore
            this.tabControlMain.TabPages.Remove(this.tabPageRestore);
            this.tabControlMain.Refresh();

            this.tabControlMain.SelectTab(DEBAULT_TAB_PAGE_INDEX);
            this._conclusionTabPageOriginalText = this.tabPageConclusion.Text;
            this.Text = string.Format(this.Text, new object[] { ProductName });

            // Inital ProjectManager
            this._projectManager = new ProjectManager();
            this._projectManager.ActiveProjecChanged += new EventHandler(this.ProjectManager_ProjectChanged);
            this._projectManager.ProjecOpenOrNew += new EventHandler(this.ProjectManager_ProjecOpenOrNew);
            this._mainFormHelper = new MainFormHelper(_projectManager);

            // Initial rectent files
            this._recentFiles.MaxLength = Settings.Default.RecentFiles_MaxLength;
            this._recentFiles.Seperator = Settings.Default.RecentFiles_Seperator;
            this._recentFiles.SetFromString(Settings.Default.RecentFiles_FileList);
            //this.SetRecentFilesSettingsAndMenue(); --> Raisid while loading inital projects

            // Initial save dialog
            this._saveLogFileDialog.DefaultExt = Settings.Default.ProjectFile_DefaultExtension;
            this._saveLogFileDialog.Filter = Settings.Default._UNUSED_Logfile_FilterList;
            this._saveLogFileDialog.FilterIndex = Settings.Default._UNUSED_Logfile_FilterIndex;
            this._saveLogFileDialog.InitialDirectory = Settings.Default.ProjectFile_DefaultPath;

            this.uscProgressBackup.ConclusionDirectoriesTextBox = this.txtConclusionDirectories;
            this.uscProgressBackup.ConclusionDurationTextBox = this.txtConclusionDuration;
            this.uscProgressBackup.ConclusionFilesTextBox = this.txtConclusionFiles;
            this.uscProgressBackup.ConclusionTabPage = this.tabPageConclusion;
            this.uscProgressBackup.ExceptionListView = this.lsvErrorLog;
            this.uscProgressBackup.ExceptionCount = this.txtExceptionCount;

            this.uscControleBackup.ProgressControle = this.uscProgressBackup;
            this.uscControleBackup.ProjectManager = this._projectManager;
            this.uscControleBackup.MainForm = this;
            this.uscControleRestore.ProgressControle = this.uscProgressRestore;
            this.uscControleRestore.ProjectManager = this._projectManager;
            this.uscControleRestore.MainForm = this;

            // Intital helper
            this._mainFormHelper = new MainFormHelper(this._projectManager);

            // Initial project on startup
            this.LoadInitialProject(args);

            this.trvExplorer.LoadDrives();
            this.trvExplorer.DirectoryList = this._projectManager.ActiveProject.ToBackupDirectorys;
            this.trvExplorer.ShowHiddenDirectories = Settings.Default.ListItems_ShowHidden;
            this.trvExplorer.ShowDirectoriesWithoutAccess = Settings.Default.ListItems_ShowWithoutAccess;
            this.trvExplorer.ShowSystemDirectories = Settings.Default.ListItems_ShowSystem;
            this.trvExplorer.SetImageVariant();

            // Initial directory content area
            this.trvExplorer_AfterSelect(this, new TreeViewEventArgs(null));
        }

        /// <summary>
        /// Load the project on application startup. In the priority from args, from default settings, empy project
        /// </summary>
        /// <param name="args">Application start up arguments</param>
        private void LoadInitialProject(string[] args)
        {
            // Load project file from args
            foreach (string Arg in args)
            {
                if (new FileInfo(Arg).Exists)
                {
                    this._projectManager.Project_Open(Arg);
                    break;
                }
            }

            // Load default project file
            if (this._projectManager.ActiveProject == null && !string.IsNullOrEmpty(Settings.Default.Startup_DefaultFileOpen))
            {
                this._projectManager.Project_Open(Settings.Default.Startup_DefaultFileOpen);
            }

            // Load empty project
            if (this._projectManager.ActiveProject == null)
            {
                this._projectManager.Project_New();
            }

            // Raise Events to inital form
            this.ProjectManager_ProjectFileChanged(this, new EventArgs());
        }

        /// <summary>
        /// Add a new item to recent file list and sets the menue item
        /// </summary>
        private void SetRecentFilesSettingsAndMenue()
        {
            if (this._projectManager.ActiveProject != null && this._projectManager.ActiveProject.File != null)
            {
                this._recentFiles.AddToList(this._projectManager.ActiveProject.File.FullName);
                Settings.Default.RecentFiles_FileList = this._recentFiles.GetAsString();
            }

            this._recentFiles.SetMenueItem(new List<ToolStripMenuItem> { this.mnuMain_File_RecentFiles_File0, this.mnuMain_File_RecentFiles_File1, this.mnuMain_File_RecentFiles_File2, this.mnuMain_File_RecentFiles_File3 }, this.mnuMain_File_RecentFiles, this.mnuMain_File_SepRecentFiles);
        }


        /// <summary>
        /// Open a project from recent file list
        /// </summary>
        /// <param name="index">Index of file list to open</param>
        private void OpenRecentFile(int index)
        {
            if (!string.IsNullOrEmpty(this._recentFiles.FileList[index]))
            {
                this._projectManager.Project_Open(this._recentFiles.FileList[index]);
                this.ProjectManager_ProjectFileChanged(this, new EventArgs());
                this.SetRecentFilesSettingsAndMenue();
            }
        }

        #region Projec Events
        private void ProjectManager_ProjectChanged(object sender, EventArgs e)
        {
            if (this._suppressControleEvents)
            {
                if (this._projectManager.ActiveProject != null) this._projectManager.ActiveProject.Changed = false;
                return;
            }
            this.ProjectManager_ProjectFileChanged(sender, e);
        }
        private void ProjectManager_ProjectFileChanged(object sender, EventArgs e)
        {
            if (this._suppressControleEvents)
            {
                if (this._projectManager.ActiveProject != null) this._projectManager.ActiveProject.Changed = false;
                return;
            }

            string ApplicationName = string.Empty;
            object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false);
            if (attributes.Length > 0)
            {
                ApplicationName = ((System.Reflection.AssemblyProductAttribute)attributes[0]).Product;
            }
            string ProjectName = Stringtable._0x0004;
            if (this._projectManager.ActiveProject.File != null && !string.IsNullOrEmpty(this._projectManager.ActiveProject.File.Name))
            {
                ProjectName = this._projectManager.ActiveProject.File.Name;
            }
            string ProjectChanged = this._projectManager.ActiveProject.Changed ? "*" : string.Empty;
            this.Text = string.Format(TITLE_LINE_FORMAT, new object[] { ApplicationName, ProjectName, ProjectChanged });

            // Load settings to controle
            this.uscControleBackup.LoadSettings();
            this.uscControleRestore.LoadSettings();
        }

        private void ProjectManager_ProjecOpenOrNew(object sender, EventArgs e)
        {
            this.SetRecentFilesSettingsAndMenue();
            Settings.Default.Save();
        }
        #endregion

        #region Form User Events
        #region Form Events
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._projectManager.ActiveProject.Changed)
            {
                string ProjectName = Stringtable._0x0004;
                if (this._projectManager.ActiveProject.File != null && !string.IsNullOrEmpty(this._projectManager.ActiveProject.File.Name))
                {
                    ProjectName = this._projectManager.ActiveProject.File.Name;
                }
                switch (MessageBox.Show(string.Format(Stringtable._0x0002m, new object[] { ProjectName }), Stringtable._0x0002c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:
                        //this.mnuMainFileSave_Click(sender, new EventArgs());
                        if (!this._projectManager.Project_Save())
                        {
                            e.Cancel = true;
                        }
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Check for Admin Rights
            if (Settings.Default.Internal_CheckAdminRights && !new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show(Stringtable._0x0003m, Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Check for file association
            if (Settings.Default.FileAssociation_CheckOnStartup)
            {
                Program.CheckFileAssociationAndSet(false);
                Settings.Default.FileAssociation_CheckOnStartup = false;
            }
        }
        #endregion

        #region Tab Pages
        #region tabPage_Select
        private void trvExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this._suppressControleEvents = true;

            // Clear if no node is selected
            if (this.trvExplorer.SelectedNode == null)
            {
                this.btnExplorerGoTop.Enabled = false;
                this.btnGoToFolder.Enabled = false;
                this.btnRefresh.Enabled = false;
                this.grbDirectoryScope.Enabled = false;
                this.lsvDirectoryContent.Enabled = false;
                this.prgItemProperty.Enabled = false;
                this.txtExplorerPath.Enabled = false;
                this.rabSaveNothing.Checked = true;
                this.prgItemProperty.SelectedObject = null;

                this._suppressControleEvents = false;
                return;
            }

            // Show directroy properties
            bool DirectroyAccess;
            if (Tools.CommonTools.DirectoryAndFile.Path.IsDrive(this.trvExplorer.LastSelectedNode.DirectoryInfo.FullName))
            {
                DriveInfo SelectedDrive = new DriveInfo(this.trvExplorer.LastSelectedNode.DirectoryInfo.Name);
                DirectroyAccess = SelectedDrive.IsReady;
                this.prgItemProperty.SelectedObject = SelectedDrive;
            }
            else
            {
                DirectroyAccess = this.trvExplorer.LastSelectedNode.DirectoryInfo.Exists;
                this.prgItemProperty.SelectedObject = this.trvExplorer.LastSelectedNode.DirectoryInfo;
            }
            this.btnExplorerGoTop.Enabled = DirectroyAccess;
            this.btnGoToFolder.Enabled = DirectroyAccess;
            this.btnRefresh.Enabled = DirectroyAccess;
            this.grbDirectoryScope.Enabled = DirectroyAccess;
            this.lsvDirectoryContent.Enabled = DirectroyAccess;
            this.prgItemProperty.Enabled = true;
            this.txtExplorerPath.Enabled = DirectroyAccess;

            this.txtExplorerPath.Text = this.trvExplorer.LastSelectedNode.DirectoryInfo.FullName;

            //List directrory items
            this._mainFormHelper.DirectoryContentListView_ListDirectoryItems(this.trvExplorer.LastSelectedNode.DirectoryInfo, this.lsvDirectoryContent);

            // Search in project for an defined scrope or set default to nothing selected
            switch (this._mainFormHelper.DirectoryContentListView_GetDirectoryScope(this.trvExplorer.LastSelectedNode.DirectoryInfo))
            {
                case Project.Project.DirectoryScope.Nothing:
                    this.rabSaveNothing.Checked = true;
                    break;
                case Project.Project.DirectoryScope.All:
                    this.rabSaveAll.Checked = true;
                    break;
                case Project.Project.DirectoryScope.Selected:
                    this.rabSaveSelected.Checked = true;
                    break;
                default:
                    this.rabSaveNothing.Checked = true;
                    break;
            }

            this.rabSaveXXX_CheckedChanged(sender, e);
            this._suppressControleEvents = false;
        }

        private void rabSaveNothing_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._suppressControleEvents && this.rabSaveNothing.Checked) this.rabSaveXXX_CheckedChanged(sender, e);
        }

        private void rabSaveAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._suppressControleEvents && this.rabSaveAll.Checked) this.rabSaveXXX_CheckedChanged(sender, e);
        }

        private void rabSaveSelected_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._suppressControleEvents && this.rabSaveSelected.Checked) this.rabSaveXXX_CheckedChanged(sender, e);
        }

        private void rabSaveXXX_CheckedChanged(object sender, EventArgs e)
        {
            // Set Explorer
            this.btnLsvExplorerChangeSelect.Enabled = this.rabSaveSelected.Checked;
            this.lsvDirectoryContent.BackColor = System.Drawing.SystemColors.Window;
            if (this.rabSaveAll.Checked) this.lsvDirectoryContent.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            if (this.rabSaveNothing.Checked) this.lsvDirectoryContent.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            if (this.rabSaveSelected.Checked) this.lsvDirectoryContent.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);

            // Set Directory and TreeView
            if (!this._suppressControleEvents)
            {
                if (this.trvExplorer.LastSelectedNode == null) return;

                if (this.rabSaveAll.Checked) this._mainFormHelper.Project_AddDirectorysToProject(this.trvExplorer.LastSelectedNode.DirectoryInfo, Project.Project.DirectoryScope.All);
                if (this.rabSaveNothing.Checked) this._mainFormHelper.Project_RemoveDirectorysFromBackup(this.trvExplorer.LastSelectedNode.DirectoryInfo);
                if (this.rabSaveSelected.Checked) this._mainFormHelper.Project_AddDirectorysToProject(this.trvExplorer.LastSelectedNode.DirectoryInfo, Project.Project.DirectoryScope.Selected);

                //Set TreeNodes
                if (this.trvExplorer.LastSelectedNode != null)
                {
                    this.trvExplorer.SetImageVariant(this.trvExplorer.LastSelectedNode.DirectoryInfo.Root, true);                
                }
            }
        }

        private void btnLsvExplorerChangeSelect_Click(object sender, EventArgs e)
        {
            if (this.lsvDirectoryContent.CheckedItems.Count > 0)
            {
                foreach (ListViewItem Item in this.lsvDirectoryContent.CheckedItems)
                {
                    Item.Checked = false;
                }
            }
            else if (this.lsvDirectoryContent.Items.Count > 0)
            {
                foreach (ListViewItem Item in this.lsvDirectoryContent.Items)
                {
                    Item.Checked = true;
                }
            }
        }

        private void btnExplorerGoTop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.trvExplorer.LastSelectedNode != null && this.trvExplorer.LastSelectedNode.Parent != null)
                {
                    ExtendedTreeNode SelectedTreeNode = this.trvExplorer.SearchTreeNode(((ExtendedTreeNode)this.trvExplorer.LastSelectedNode.Parent).DirectoryInfo);
                    if (SelectedTreeNode != null) this.trvExplorer.SelectedNode = SelectedTreeNode;
                }
            }
            catch
            {
            }
        }

        private void btnGoToFolder_Click(object sender, EventArgs e)
        {
            OLKI.Tools.CommonTools.DirectoryAndFile.Directory.Open(this.trvExplorer.LastSelectedNode.DirectoryInfo.FullName, false, string.Empty);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this._mainFormHelper.DirectoryContentListView_ListDirectoryItems(this.trvExplorer.LastSelectedNode.DirectoryInfo, this.lsvDirectoryContent);
        }

        private void lsvExplorer_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //Is Directory
            if (!this._suppressControleEvents && e.Item.Tag.GetType().Equals(typeof(DirectoryInfo)))
            {
                DirectoryInfo Directory = (DirectoryInfo)e.Item.Tag;

                if (e.Item.Checked)
                {
                    this._mainFormHelper.Project_AddDirectorysToProject(Directory, Project.Project.DirectoryScope.All);
                    e.Item.ImageIndex = 18;
                }
                else
                {
                    this._mainFormHelper.Project_RemoveDirectorysFromBackup(Directory);
                    e.Item.ImageIndex = 17;
                }
            }

            //Is File
            if (!this._suppressControleEvents && e.Item.Tag.GetType().Equals(typeof(FileInfo)))
            {
                if (e.Item.Checked)
                {
                    this._projectManager.ActiveProject.FileAdd(this.trvExplorer.LastSelectedNode.DirectoryInfo, ((FileInfo)e.Item.Tag));
                }
                else
                {
                    this._projectManager.ActiveProject.FileRemove(this.trvExplorer.LastSelectedNode.DirectoryInfo, ((FileInfo)e.Item.Tag));
                }
            }
            if (!this._suppressControleEvents) this.rabSaveSelected.Checked = true;
        }

        private void lsvExplorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._suppressControleEvents && this.lsvDirectoryContent.SelectedItems.Count > 0)
            {
                this.prgItemProperty.SelectedObject = this.lsvDirectoryContent.SelectedItems[0].Tag;
            }
        }
        #endregion

        #region tabPage_Conclusion
        private void LsvErrorLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvErrorLog.SelectedItems.Count > 0)
            {
                Project.Process.ProcessException Exception = (Project.Process.ProcessException)this.lsvErrorLog.SelectedItems[0].Tag;
                string ExceptionText = "";
                if (!string.IsNullOrEmpty(Exception.Description)) ExceptionText += Exception.Description;
                if (!string.IsNullOrEmpty(Exception.Description) && !string.IsNullOrEmpty(Exception.Exception.Message)) ExceptionText += "\r\n\r\n";
                if (!string.IsNullOrEmpty(Exception.Exception.Message)) ExceptionText += Exception.Exception.Message;

                this.txtExceptionSourcePath.Text = Exception.Source;
                this.txtExceptionDestinationPath.Text = Exception.Target;
                this.txtExceptionMessage.Text = ExceptionText;
            }
            else
            {
                this.txtExceptionSourcePath.Text = string.Empty;
                this.txtExceptionDestinationPath.Text = string.Empty;
                this.txtExceptionMessage.Text = string.Empty;
            }
        }

        private void txtExceptionCount_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtExceptionCount.Text) || this.txtExceptionCount.Text == "0")
            {
                this.tabPageConclusion.Text = this._conclusionTabPageOriginalText;
            }
            else
            {
                this.tabPageConclusion.Text = string.Format("{0}: {1}", new string[] { this._conclusionTabPageOriginalText, this.txtExceptionCount.Text });
            }
        }
        #endregion
        #endregion

        #region Menue Events
        private void mnuMain_File_New_Click(object sender, EventArgs e)
        {
            if (!this._projectManager.GetSaveActiveProject()) return;
            this._projectManager.Project_New();
            this.ProjectManager_ProjectFileChanged(sender, e);
        }

        private void mnuMain_File_Open_Click(object sender, EventArgs e)
        {
            if (!this._projectManager.GetSaveActiveProject()) return;
            this._projectManager.Project_Open();
            this.ProjectManager_ProjectFileChanged(sender, e);
            this.SetRecentFilesSettingsAndMenue();
        }

        private void mnuMain_File_Save_Click(object sender, EventArgs e)
        {
            this._projectManager.Project_Save();
        }

        private void mnuMain_File_SaveAs_Click(object sender, EventArgs e)
        {
            this._projectManager.Project_SaveAs();
            this.SetRecentFilesSettingsAndMenue();
        }

        private void mnuMain_File_RecentFiles_File0_Click(object sender, EventArgs e)
        {
            this.OpenRecentFile(0);
        }

        private void mnuMain_File_RecentFiles_File1_Click(object sender, EventArgs e)
        {
            this.OpenRecentFile(1);
        }

        private void mnuMain_File_RecentFiles_File2_Click(object sender, EventArgs e)
        {
            this.OpenRecentFile(2);
        }

        private void mnuMain_File_RecentFiles_File3_Click(object sender, EventArgs e)
        {
            this.OpenRecentFile(3);
        }

        private void mnuMain_File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuMain_Extras_Options_Click(object sender, EventArgs e)
        {
            this._frmApplicationSettings = new SubForms.ApplicationSettingsForm();
            if (this._frmApplicationSettings.ShowDialog(this) == DialogResult.OK && this._frmApplicationSettings.ClearRecentFiles)
            {
                this._recentFiles.FileList.Clear();
                this.SetRecentFilesSettingsAndMenue();
            }
            this.trvExplorer.ShowHiddenDirectories = Properties.Settings.Default.ListItems_ShowHidden;
            this.trvExplorer.ShowDirectoriesWithoutAccess = Properties.Settings.Default.ListItems_ShowWithoutAccess;
            this.trvExplorer.ShowSystemDirectories = Properties.Settings.Default.ListItems_ShowSystem;
        }

        private void mnuMain_Help_About_Click(object sender, EventArgs e)
        {
            this._frmAbout.ShowDialog(this);
        }
        #endregion
        #endregion
        #endregion
    }
}