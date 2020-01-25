/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
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

namespace OLKI.Programme.QBC.MainForm
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
        /// <summary>
        /// Specifies the current selected directory
        /// </summary>
        private DirectoryInfo _activeDirectory = null;
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
        private bool _systemChange = false;
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

            //TODO: Remove in future versions to restore backups
            //tabPageRestore
            this.tabControlMain.TabPages.Remove(this.tabPageRestore);
            this.tabControlMain.Refresh();

            this.tabControlMain.SelectTab(DEBAULT_TAB_PAGE_INDEX);
            this.Text = string.Format(this.Text, new object[] { ProductName });

            // Inital ProjectManager
            this._projectManager = new ProjectManager();
            this._projectManager.ActiveProjecChanged += new EventHandler(this.ProjectManager_ProjectChanged);
            this._projectManager.ProjecOpenOrNew__Event += new EventHandler(this.ProjectManager_ProjecOpenOrNew);
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

            this.trvExplorer_AfterSelect(this, new TreeViewEventArgs(null));

            this.uscProgressBackup.ConclusionDirectoriesTextBox = this.txtConclusionDirectories;
            this.uscProgressBackup.ConclusionDurationTextBox = this.txtConclusionDuration;
            this.uscProgressBackup.ConclusionFilesTextBox = this.txtConclusionFiles;
            this.uscProgressBackup.ConclusionTabPage = this.tabPageConclusion;
            this.uscProgressBackup.ExceptionListView = this.lsvErrorLog;
            this.uscProgressBackup.ExceptionCount = this.txtExceptionCount;

            this.uscControleBackup.ProgressControle = this.uscProgressBackup;
            this.uscControleBackup.ProjectManager = this._projectManager;
            this.uscControleRestore.ProgressControle = this.uscProgressRestore;
            this.uscControleRestore.ProjectManager = this._projectManager;

            // Intital helper
            this._mainFormHelper = new MainFormHelper(this._projectManager);

            // Initial project on startup
            this.LoadInitialProject(args);
            //this._mainFormHelper.ExplorerTreeView_InitialTreeView(this.trvExplorer);
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

        #region Projec Events
        private void ProjectManager_ProjectChanged(object sender, EventArgs e)
        {
            this.ProjectManager_ProjectFileChanged(sender, e);
        }
        private void ProjectManager_ProjectFileChanged(object sender, EventArgs e)
        {
            string ProductName = string.Empty;
            object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false);
            if (attributes.Length > 0)
            {
                ProductName = ((System.Reflection.AssemblyProductAttribute)attributes[0]).Product;
            }
            string ProjectName = Stringtable._0x0004;
            if (this._projectManager.ActiveProject.File != null && !string.IsNullOrEmpty(this._projectManager.ActiveProject.File.Name))
            {
                ProjectName = this._projectManager.ActiveProject.File.Name;
            }
            string ProjectChanged = this._projectManager.ActiveProject.Changed ? "*" : string.Empty;
            this.Text = string.Format(TITLE_LINE_FORMAT, new object[] { ProductName, ProjectName, ProjectChanged });

            // Load settings to controle
            this._mainFormHelper.ExplorerTreeView_InitialTreeView(this.trvExplorer);
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Get New Tabcontrole Size
            int BorderH = 75;
            int BorderW = 40;
            this.tabControlMain.Height = this.Height - BorderH < this.tabControlMain.MinimumSize.Height ? this.tabControlMain.MinimumSize.Height : this.Height - BorderH;   //598
            this.tabControlMain.Width = this.Width - BorderW < this.tabControlMain.MinimumSize.Width ? this.tabControlMain.MinimumSize.Width : this.Width - BorderW;  //983 
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
            this._systemChange = true;
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
                return;
            }

            //Set properties of selected node (directroy or drive)
            bool HasAccess;
            if (Tools.CommonTools.DirectoryAndFile.Path.IsDrive(((ExtendedTreeNode)this.trvExplorer.SelectedNode).DirectoryInfo.FullName))
            {
                DriveInfo SelectedDrive = new DriveInfo(((ExtendedTreeNode)this.trvExplorer.SelectedNode).DirectoryInfo.Name);
                HasAccess = SelectedDrive.IsReady;
                this.prgItemProperty.SelectedObject = SelectedDrive;
            }
            else
            {
                DirectoryInfo Selectedirectory = ((ExtendedTreeNode)this.trvExplorer.SelectedNode).DirectoryInfo;
                HasAccess = Selectedirectory.Exists;
                this.prgItemProperty.SelectedObject = Selectedirectory;
            }
            this.btnExplorerGoTop.Enabled = HasAccess;
            this.btnGoToFolder.Enabled = HasAccess;
            this.btnRefresh.Enabled = HasAccess;
            this.grbDirectoryScope.Enabled = HasAccess;
            this.lsvDirectoryContent.Enabled = HasAccess;
            this.prgItemProperty.Enabled = true;
            this.txtExplorerPath.Enabled = HasAccess;

            //Remember selected directroy
            this._activeDirectory = ((ExtendedTreeNode)this.trvExplorer.SelectedNode).DirectoryInfo;

            //Create Subnodes of selected directroy
            if (((ExtendedTreeNode)this.trvExplorer.SelectedNode).Nodes.Count == 0)
            {
                this._mainFormHelper.ExplorerTreeView_AddSubNotes((ExtendedTreeNode)this.trvExplorer.SelectedNode);
            }

            //List directrory items
            this._mainFormHelper.DirectoryContentListView_ListDirectoryItems(this._activeDirectory, this.lsvDirectoryContent);
            if (Settings.Default.ListItems_ExpandTreeNodeOnClick) this.trvExplorer.SelectedNode.Expand();
            this.txtExplorerPath.Text = (this._activeDirectory.FullName);

            // Search in project for an defined scrope or set default to nothing selected
            switch (this._mainFormHelper.DirectoryContentListView_GetDirectoryScope(this._activeDirectory))
            {
                case BackupProject.Project.DirectoryScope.Nothing:
                    this.rabSaveNothing.Checked = true;
                    break;
                case BackupProject.Project.DirectoryScope.All:
                    this.rabSaveAll.Checked = true;
                    break;
                case BackupProject.Project.DirectoryScope.Selected:
                    this.rabSaveSelected.Checked = true;
                    break;
                default:
                    this.rabSaveNothing.Checked = true;
                    break;
            }
            this.rabSaveXXX_CheckedChanged(sender, e);
            this._systemChange = false;
        }

        private void rabSaveNothing_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._systemChange && this.rabSaveNothing.Checked) this.rabSaveXXX_CheckedChanged(sender, e);
        }

        private void rabSaveAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._systemChange && this.rabSaveAll.Checked) this.rabSaveXXX_CheckedChanged(sender, e);
        }

        private void rabSaveSelected_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._systemChange && this.rabSaveSelected.Checked) this.rabSaveXXX_CheckedChanged(sender, e);
        }

        private void rabSaveXXX_CheckedChanged(object sender, EventArgs e)
        {
            // Set Explorer
            this.btnLsvExplorerChangeSelect.Enabled = this.rabSaveSelected.Checked;
            this.lsvDirectoryContent.Enabled = this.btnLsvExplorerChangeSelect.Enabled;

            // Set Directory and TreeView
            if (!this._systemChange)
            {
                if (this.rabSaveAll.Checked) this._mainFormHelper.Project_AddDirectorysToProjectAndSetTree(this._activeDirectory, (OLKI.Programme.QBC.MainForm.ExtendedTreeNode)this.trvExplorer.SelectedNode, QBC.BackupProject.Project.DirectoryScope.All);
                if (this.rabSaveNothing.Checked) this._mainFormHelper.Project_RemoveDirectorysFromBackupAndSetTree(this._activeDirectory, (OLKI.Programme.QBC.MainForm.ExtendedTreeNode)this.trvExplorer.SelectedNode);
                if (this.rabSaveSelected.Checked) this._mainFormHelper.Project_AddDirectorysToProjectAndSetTree(this._activeDirectory, (OLKI.Programme.QBC.MainForm.ExtendedTreeNode)this.trvExplorer.SelectedNode, QBC.BackupProject.Project.DirectoryScope.Selected);
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
                this._mainFormHelper.ExplorerTreeView_SelectTreeViewItem(this._activeDirectory.Parent.FullName, this.trvExplorer.Nodes);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private void btnGoToFolder_Click(object sender, EventArgs e)
        {
            OLKI.Tools.CommonTools.DirectoryAndFile.Directory.Open(this._activeDirectory.FullName, false, string.Empty);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this._mainFormHelper.DirectoryContentListView_ListDirectoryItems(this._activeDirectory, this.lsvDirectoryContent);
        }

        private void lsvExplorer_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //Is Directory
            if (!this._systemChange && e.Item.Tag.GetType().Equals(typeof(DirectoryInfo)))
            {
                DirectoryInfo Directory = (DirectoryInfo)e.Item.Tag;

                //Search Treeview, then set directory
                foreach (ExtendedTreeNode CTreeNode in this.trvExplorer.SelectedNode.Nodes)
                {
                    if (CTreeNode.DirectoryInfo.FullName == Directory.FullName)
                    {
                        if (e.Item.Checked)
                        {
                            this._mainFormHelper.Project_AddDirectorysToProjectAndSetTree(Directory, CTreeNode, BackupProject.Project.DirectoryScope.All);
                            e.Item.ImageIndex = 18;
                        }
                        else
                        {
                            this._mainFormHelper.Project_RemoveDirectorysFromBackupAndSetTree(Directory, CTreeNode);
                            e.Item.ImageIndex = 17;
                        }
                    }
                }
            }
            //Is File
            if (!this._systemChange && e.Item.Tag.GetType().Equals(typeof(FileInfo)))
            {
                if (e.Item.Checked)
                {
                    this._projectManager.ActiveProject.FileAdd(this._activeDirectory, ((FileInfo)e.Item.Tag));
                }
                else
                {
                    this._projectManager.ActiveProject.FileRemove(this._activeDirectory, ((FileInfo)e.Item.Tag));
                }
            }
        }

        private void lsvExplorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._systemChange && this.lsvDirectoryContent.SelectedItems.Count > 0)
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
                BackupProject.Process.ProcessException Exception = (BackupProject.Process.ProcessException)this.lsvErrorLog.SelectedItems[0].Tag;
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
        #endregion
        #endregion

        #region Menue Events
        private void mnuMain_File_New_Click(object sender, EventArgs e)
        {
            if (!this._projectManager.GetOverwriteActiveProject()) return;
            this._projectManager.Project_New();
            this.ProjectManager_ProjectFileChanged(sender, e);
            //todo: REMOVE this._mainFormHelper = new MainFormHelper(this._projectManager);
            //todo: REMOVE this._mainFormHelper.ExplorerTreeView_InitialTreeView(this.trvExplorer);
        }

        private void mnuMain_File_Open_Click(object sender, EventArgs e)
        {
            if (!this._projectManager.GetOverwriteActiveProject()) return;
            this._projectManager.Project_Open();
            this.ProjectManager_ProjectFileChanged(sender, e);
            //todo: REMOVE this._mainFormHelper = new MainFormHelper(this._projectManager);
            //todo: REMOVE this._mainFormHelper.ExplorerTreeView_InitialTreeView(this.trvExplorer);
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