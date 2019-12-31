/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * A form to change the settings of the application
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
using System;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.MainForm.SubForms
{
    /// <summary>
    /// A form to change the settings of the application
    /// </summary>
    internal partial class ApplicationSettingsForm : Form
    {
        #region Properties
        /// <summary>
        /// True if clearing of the recent file list was requested
        /// </summary>
        private bool _clearRecentFiles = false;
        /// <summary>
        /// True if clearing of the recent file list was requested
        /// </summary>
        internal bool ClearRecentFiles
        {
            get
            {
                return this._clearRecentFiles;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialise a new application SettingsForm
        /// </summary>
        internal ApplicationSettingsForm()
        {
            InitializeComponent();
            this.SetControlesFromSettings();
        }

        /// <summary>
        /// Set the properties of the controls from actual application settings
        /// </summary>
        private void SetControlesFromSettings()
        {
            this.txtDefaultPath.Text = Settings.Default.ProjectFile_DefaultPath;
            this.txtDefaultFileOpen.Text = Settings.Default.Startup_DefaultFileOpen;
            this.chkAutoCheckFileAssociation.Checked = Settings.Default.FileAssociation_CheckOnStartup;
            this.chkShowDirectorysWithoutAccess.Checked = Settings.Default.ListItems_ShowDirectorysWithoutAccess;
            this.chkShowSystemDirectory.Checked = Settings.Default.ListItems_ShowSystemDirectory;
            this.chkEypandTreeNodeOnClick.Checked = Settings.Default.ListItems_ExpandTreeNodeOnClick;
            this.txtLogfileDateFormat.Text = Settings.Default._UNUSED_Logfile_DateFormat;

            this.txtAddTextToFileDefaultText.Text = Settings.Default.Copy_FileExisitngAddTextDefault;
            this.txtAddTextToFileDateFormat.Text = Settings.Default.Copy_FileExisitngAddTextDateFormat;
        }

        /// <summary>
        /// Set the form OK button, debeding by the error states of the date format textbox contents
        /// </summary>
        private void ValidateDateFormats()
        {
            bool AddTextFormatValid;
            bool LogFileFormatValid;

            this.erpDateFormat.Clear();

            try
            {
                _ = DateTime.Now.ToString(this.txtAddTextToFileDateFormat.Text);
                AddTextFormatValid = true;
            }
            catch
            {
                this.erpDateFormat.SetError(this.txtAddTextToFileDateFormat, Stringtable._0x001C);
                AddTextFormatValid = false;
            }

            try
            {
                _ = DateTime.Now.ToString(this.txtLogfileDateFormat.Text);
                LogFileFormatValid = true;
            }
            catch
            {
                this.erpDateFormat.SetError(this.txtLogfileDateFormat, Stringtable._0x001C);
                LogFileFormatValid = false;
            }

            this.btnOk.Enabled = AddTextFormatValid && LogFileFormatValid;
        }

        #region Form events
        private void btnDefaultPath_Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowserDialog = new FolderBrowserDialog
            {
                Description = Stringtable._0x0006,
                SelectedPath = Settings.Default.ProjectFile_DefaultPath
            };
            if (FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.txtDefaultPath.Text = FolderBrowserDialog.SelectedPath;
                Settings.Default.ProjectFile_DefaultPath = FolderBrowserDialog.SelectedPath;
            }
        }

        private void btnDefaultPath_Delete_Click(object sender, EventArgs e)
        {
            this.txtDefaultPath.Text = string.Empty;
        }

        private void btnDefaultFileOpen_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                DefaultExt = Settings.Default.ProjectFile_DefaultExtension,
                Filter = Settings.Default.ProjectFile_FilterList,
                InitialDirectory = Settings.Default.ProjectFile_DefaultPath,
                Multiselect = false
            };
            if (OpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.txtDefaultFileOpen.Text = OpenFileDialog.FileName;
            }
        }

        private void btnDefaultFileOpen_Delete_Click(object sender, EventArgs e)
        {
            this.txtDefaultFileOpen.Text = string.Empty;
        }

        private void btnRecentFilesClear_Click(object sender, EventArgs e)
        {
            this._clearRecentFiles = true;
        }

        private void btnCheckFileAssociation_Click(object sender, EventArgs e)
        {
            Program.CheckFileAssociationAndSet(true);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.Default.FileAssociation_CheckOnStartup = this.chkAutoCheckFileAssociation.Checked;
            Settings.Default.ProjectFile_DefaultPath = this.txtDefaultPath.Text;
            Settings.Default.Startup_DefaultFileOpen = this.txtDefaultFileOpen.Text;
            Settings.Default.ListItems_ShowDirectorysWithoutAccess = this.chkShowDirectorysWithoutAccess.Checked;
            Settings.Default.ListItems_ShowSystemDirectory = this.chkShowSystemDirectory.Checked;
            Settings.Default.ListItems_ExpandTreeNodeOnClick = this.chkEypandTreeNodeOnClick.Checked;
            Settings.Default._UNUSED_Logfile_DateFormat = this.txtLogfileDateFormat.Text;

            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnSetDefaults_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            this.SetControlesFromSettings();
        }

        private void txtAddTextToFileDateFormat_TextChanged(object sender, EventArgs e)
        {
            this.ValidateDateFormats();
        }

        private void txtLogfileDateFormat_TextChanged(object sender, EventArgs e)
        {
            this.ValidateDateFormats();
        }
        #endregion
        #endregion
    }
}