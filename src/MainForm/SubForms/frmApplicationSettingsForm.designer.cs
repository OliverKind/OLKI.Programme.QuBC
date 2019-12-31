namespace OLKI.Programme.QBC.MainForm.SubForms
{
    internal partial class ApplicationSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationSettingsForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tolApplicationSettings = new System.Windows.Forms.ToolStrip();
            this.grbProjectFolder = new System.Windows.Forms.GroupBox();
            this.btnDefaultFileOpen_Delete = new System.Windows.Forms.Button();
            this.btnDefaultPath_Delete = new System.Windows.Forms.Button();
            this.btnDefaultFileOpen_Browse = new System.Windows.Forms.Button();
            this.txtDefaultFileOpen = new System.Windows.Forms.TextBox();
            this.lblDefaultFileOpen = new System.Windows.Forms.Label();
            this.btnDefaultPath_Browse = new System.Windows.Forms.Button();
            this.txtDefaultPath = new System.Windows.Forms.TextBox();
            this.lblDefaultPath = new System.Windows.Forms.Label();
            this.grbRecentFiles = new System.Windows.Forms.GroupBox();
            this.btnRecentFilesClear = new System.Windows.Forms.Button();
            this.nudNumRecentFiles = new System.Windows.Forms.NumericUpDown();
            this.lblNumRecentFiles = new System.Windows.Forms.Label();
            this.btnCheckFileAssociation = new System.Windows.Forms.Button();
            this.grbExplorerSettings = new System.Windows.Forms.GroupBox();
            this.chkEypandTreeNodeOnClick = new System.Windows.Forms.CheckBox();
            this.chkShowDirectorysWithoutAccess = new System.Windows.Forms.CheckBox();
            this.chkShowSystemDirectory = new System.Windows.Forms.CheckBox();
            this.grbLogFile = new System.Windows.Forms.GroupBox();
            this.lblLogfileDateFormat = new System.Windows.Forms.Label();
            this.txtLogfileDateFormat = new System.Windows.Forms.TextBox();
            this.btnSetDefaults = new System.Windows.Forms.Button();
            this.grbAddTextToFiile = new System.Windows.Forms.GroupBox();
            this.lblAddTextToFileDefaultText = new System.Windows.Forms.Label();
            this.txtAddTextToFileDefaultText = new System.Windows.Forms.TextBox();
            this.lblAddTextToFileDateFormat = new System.Windows.Forms.Label();
            this.txtAddTextToFileDateFormat = new System.Windows.Forms.TextBox();
            this.chkAutoCheckFileAssociation = new System.Windows.Forms.CheckBox();
            this.erpDateFormat = new System.Windows.Forms.ErrorProvider(this.components);
            this.grbProjectFolder.SuspendLayout();
            this.grbRecentFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumRecentFiles)).BeginInit();
            this.grbExplorerSettings.SuspendLayout();
            this.grbLogFile.SuspendLayout();
            this.grbAddTextToFiile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpDateFormat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(590, 324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 324);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(110, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tolApplicationSettings
            // 
            this.tolApplicationSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.tolApplicationSettings.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tolApplicationSettings.Location = new System.Drawing.Point(703, 0);
            this.tolApplicationSettings.Name = "tolApplicationSettings";
            this.tolApplicationSettings.Size = new System.Drawing.Size(26, 359);
            this.tolApplicationSettings.TabIndex = 10;
            this.tolApplicationSettings.Text = "toolStrip1";
            // 
            // grbProjectFolder
            // 
            this.grbProjectFolder.Controls.Add(this.btnDefaultFileOpen_Delete);
            this.grbProjectFolder.Controls.Add(this.btnDefaultPath_Delete);
            this.grbProjectFolder.Controls.Add(this.btnDefaultFileOpen_Browse);
            this.grbProjectFolder.Controls.Add(this.txtDefaultFileOpen);
            this.grbProjectFolder.Controls.Add(this.lblDefaultFileOpen);
            this.grbProjectFolder.Controls.Add(this.btnDefaultPath_Browse);
            this.grbProjectFolder.Controls.Add(this.txtDefaultPath);
            this.grbProjectFolder.Controls.Add(this.lblDefaultPath);
            this.grbProjectFolder.Location = new System.Drawing.Point(12, 12);
            this.grbProjectFolder.Name = "grbProjectFolder";
            this.grbProjectFolder.Size = new System.Drawing.Size(688, 71);
            this.grbProjectFolder.TabIndex = 0;
            this.grbProjectFolder.TabStop = false;
            this.grbProjectFolder.Text = "Standardordner und -dateien";
            // 
            // btnDefaultFileOpen_Delete
            // 
            this.btnDefaultFileOpen_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btnDefaultFileOpen_Delete.Image")));
            this.btnDefaultFileOpen_Delete.Location = new System.Drawing.Point(647, 42);
            this.btnDefaultFileOpen_Delete.Name = "btnDefaultFileOpen_Delete";
            this.btnDefaultFileOpen_Delete.Size = new System.Drawing.Size(35, 24);
            this.btnDefaultFileOpen_Delete.TabIndex = 7;
            this.btnDefaultFileOpen_Delete.UseVisualStyleBackColor = true;
            this.btnDefaultFileOpen_Delete.Click += new System.EventHandler(this.btnDefaultFileOpen_Delete_Click);
            // 
            // btnDefaultPath_Delete
            // 
            this.btnDefaultPath_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btnDefaultPath_Delete.Image")));
            this.btnDefaultPath_Delete.Location = new System.Drawing.Point(647, 16);
            this.btnDefaultPath_Delete.Name = "btnDefaultPath_Delete";
            this.btnDefaultPath_Delete.Size = new System.Drawing.Size(35, 24);
            this.btnDefaultPath_Delete.TabIndex = 3;
            this.btnDefaultPath_Delete.UseVisualStyleBackColor = true;
            this.btnDefaultPath_Delete.Click += new System.EventHandler(this.btnDefaultPath_Delete_Click);
            // 
            // btnDefaultFileOpen_Browse
            // 
            this.btnDefaultFileOpen_Browse.Image = ((System.Drawing.Image)(resources.GetObject("btnDefaultFileOpen_Browse.Image")));
            this.btnDefaultFileOpen_Browse.Location = new System.Drawing.Point(606, 42);
            this.btnDefaultFileOpen_Browse.Name = "btnDefaultFileOpen_Browse";
            this.btnDefaultFileOpen_Browse.Size = new System.Drawing.Size(35, 24);
            this.btnDefaultFileOpen_Browse.TabIndex = 6;
            this.btnDefaultFileOpen_Browse.UseVisualStyleBackColor = true;
            this.btnDefaultFileOpen_Browse.Click += new System.EventHandler(this.btnDefaultFileOpen_Browse_Click);
            // 
            // txtDefaultFileOpen
            // 
            this.txtDefaultFileOpen.Location = new System.Drawing.Point(140, 45);
            this.txtDefaultFileOpen.Name = "txtDefaultFileOpen";
            this.txtDefaultFileOpen.Size = new System.Drawing.Size(460, 20);
            this.txtDefaultFileOpen.TabIndex = 5;
            // 
            // lblDefaultFileOpen
            // 
            this.lblDefaultFileOpen.AutoSize = true;
            this.lblDefaultFileOpen.Location = new System.Drawing.Point(6, 48);
            this.lblDefaultFileOpen.Name = "lblDefaultFileOpen";
            this.lblDefaultFileOpen.Size = new System.Drawing.Size(128, 13);
            this.lblDefaultFileOpen.TabIndex = 4;
            this.lblDefaultFileOpen.Text = "Datei beim starten öffnen:";
            // 
            // btnDefaultPath_Browse
            // 
            this.btnDefaultPath_Browse.Image = ((System.Drawing.Image)(resources.GetObject("btnDefaultPath_Browse.Image")));
            this.btnDefaultPath_Browse.Location = new System.Drawing.Point(606, 16);
            this.btnDefaultPath_Browse.Name = "btnDefaultPath_Browse";
            this.btnDefaultPath_Browse.Size = new System.Drawing.Size(35, 24);
            this.btnDefaultPath_Browse.TabIndex = 2;
            this.btnDefaultPath_Browse.UseVisualStyleBackColor = true;
            this.btnDefaultPath_Browse.Click += new System.EventHandler(this.btnDefaultPath_Browse_Click);
            // 
            // txtDefaultPath
            // 
            this.txtDefaultPath.Location = new System.Drawing.Point(140, 19);
            this.txtDefaultPath.Name = "txtDefaultPath";
            this.txtDefaultPath.Size = new System.Drawing.Size(460, 20);
            this.txtDefaultPath.TabIndex = 1;
            // 
            // lblDefaultPath
            // 
            this.lblDefaultPath.AutoSize = true;
            this.lblDefaultPath.Location = new System.Drawing.Point(6, 22);
            this.lblDefaultPath.Name = "lblDefaultPath";
            this.lblDefaultPath.Size = new System.Drawing.Size(83, 13);
            this.lblDefaultPath.TabIndex = 0;
            this.lblDefaultPath.Text = "Standardordner:";
            // 
            // grbRecentFiles
            // 
            this.grbRecentFiles.Controls.Add(this.btnRecentFilesClear);
            this.grbRecentFiles.Controls.Add(this.nudNumRecentFiles);
            this.grbRecentFiles.Controls.Add(this.lblNumRecentFiles);
            this.grbRecentFiles.Location = new System.Drawing.Point(12, 234);
            this.grbRecentFiles.Name = "grbRecentFiles";
            this.grbRecentFiles.Size = new System.Drawing.Size(688, 48);
            this.grbRecentFiles.TabIndex = 4;
            this.grbRecentFiles.TabStop = false;
            this.grbRecentFiles.Text = "Zuletzt geöffnete Dateien";
            // 
            // btnRecentFilesClear
            // 
            this.btnRecentFilesClear.Location = new System.Drawing.Point(280, 19);
            this.btnRecentFilesClear.Name = "btnRecentFilesClear";
            this.btnRecentFilesClear.Size = new System.Drawing.Size(110, 23);
            this.btnRecentFilesClear.TabIndex = 2;
            this.btnRecentFilesClear.Text = "Liste leeren";
            this.btnRecentFilesClear.UseVisualStyleBackColor = true;
            this.btnRecentFilesClear.Click += new System.EventHandler(this.btnRecentFilesClear_Click);
            // 
            // nudNumRecentFiles
            // 
            this.nudNumRecentFiles.Location = new System.Drawing.Point(245, 19);
            this.nudNumRecentFiles.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudNumRecentFiles.Name = "nudNumRecentFiles";
            this.nudNumRecentFiles.Size = new System.Drawing.Size(29, 20);
            this.nudNumRecentFiles.TabIndex = 1;
            // 
            // lblNumRecentFiles
            // 
            this.lblNumRecentFiles.AutoSize = true;
            this.lblNumRecentFiles.Location = new System.Drawing.Point(6, 21);
            this.lblNumRecentFiles.Name = "lblNumRecentFiles";
            this.lblNumRecentFiles.Size = new System.Drawing.Size(233, 13);
            this.lblNumRecentFiles.TabIndex = 0;
            this.lblNumRecentFiles.Text = "Anzahl der zuletzt geöffneten Dateien anzeigen:";
            // 
            // btnCheckFileAssociation
            // 
            this.btnCheckFileAssociation.Location = new System.Drawing.Point(12, 288);
            this.btnCheckFileAssociation.Name = "btnCheckFileAssociation";
            this.btnCheckFileAssociation.Size = new System.Drawing.Size(134, 23);
            this.btnCheckFileAssociation.TabIndex = 5;
            this.btnCheckFileAssociation.Text = "Dateizuordnung prüfen";
            this.btnCheckFileAssociation.UseVisualStyleBackColor = true;
            this.btnCheckFileAssociation.Click += new System.EventHandler(this.btnCheckFileAssociation_Click);
            // 
            // grbExplorerSettings
            // 
            this.grbExplorerSettings.Controls.Add(this.chkEypandTreeNodeOnClick);
            this.grbExplorerSettings.Controls.Add(this.chkShowDirectorysWithoutAccess);
            this.grbExplorerSettings.Controls.Add(this.chkShowSystemDirectory);
            this.grbExplorerSettings.Location = new System.Drawing.Point(12, 89);
            this.grbExplorerSettings.Name = "grbExplorerSettings";
            this.grbExplorerSettings.Size = new System.Drawing.Size(274, 88);
            this.grbExplorerSettings.TabIndex = 1;
            this.grbExplorerSettings.TabStop = false;
            this.grbExplorerSettings.Text = "Explorer Einstellungen";
            // 
            // chkEypandTreeNodeOnClick
            // 
            this.chkEypandTreeNodeOnClick.AutoSize = true;
            this.chkEypandTreeNodeOnClick.Location = new System.Drawing.Point(6, 65);
            this.chkEypandTreeNodeOnClick.Name = "chkEypandTreeNodeOnClick";
            this.chkEypandTreeNodeOnClick.Size = new System.Drawing.Size(243, 17);
            this.chkEypandTreeNodeOnClick.TabIndex = 2;
            this.chkEypandTreeNodeOnClick.Text = "Ordnerbaum beim Einfachen klicken erweitern";
            this.chkEypandTreeNodeOnClick.UseVisualStyleBackColor = true;
            // 
            // chkShowDirectorysWithoutAccess
            // 
            this.chkShowDirectorysWithoutAccess.AutoSize = true;
            this.chkShowDirectorysWithoutAccess.Location = new System.Drawing.Point(6, 19);
            this.chkShowDirectorysWithoutAccess.Name = "chkShowDirectorysWithoutAccess";
            this.chkShowDirectorysWithoutAccess.Size = new System.Drawing.Size(164, 17);
            this.chkShowDirectorysWithoutAccess.TabIndex = 0;
            this.chkShowDirectorysWithoutAccess.Text = "Ordner ohne Zugriff anzeigen";
            this.chkShowDirectorysWithoutAccess.UseVisualStyleBackColor = true;
            // 
            // chkShowSystemDirectory
            // 
            this.chkShowSystemDirectory.AutoSize = true;
            this.chkShowSystemDirectory.Location = new System.Drawing.Point(6, 42);
            this.chkShowSystemDirectory.Name = "chkShowSystemDirectory";
            this.chkShowSystemDirectory.Size = new System.Drawing.Size(137, 17);
            this.chkShowSystemDirectory.TabIndex = 1;
            this.chkShowSystemDirectory.Text = "Systemordner Anzeigen";
            this.chkShowSystemDirectory.UseVisualStyleBackColor = true;
            // 
            // grbLogFile
            // 
            this.grbLogFile.Controls.Add(this.lblLogfileDateFormat);
            this.grbLogFile.Controls.Add(this.txtLogfileDateFormat);
            this.grbLogFile.Location = new System.Drawing.Point(293, 90);
            this.grbLogFile.Name = "grbLogFile";
            this.grbLogFile.Size = new System.Drawing.Size(407, 87);
            this.grbLogFile.TabIndex = 2;
            this.grbLogFile.TabStop = false;
            this.grbLogFile.Text = "Protokolldatei";
            // 
            // lblLogfileDateFormat
            // 
            this.lblLogfileDateFormat.AutoSize = true;
            this.lblLogfileDateFormat.Location = new System.Drawing.Point(6, 22);
            this.lblLogfileDateFormat.Name = "lblLogfileDateFormat";
            this.lblLogfileDateFormat.Size = new System.Drawing.Size(183, 13);
            this.lblLogfileDateFormat.TabIndex = 0;
            this.lblLogfileDateFormat.Text = "Datumsformat für Protokolldateiname:";
            // 
            // txtLogfileDateFormat
            // 
            this.txtLogfileDateFormat.Location = new System.Drawing.Point(195, 19);
            this.txtLogfileDateFormat.Name = "txtLogfileDateFormat";
            this.txtLogfileDateFormat.Size = new System.Drawing.Size(190, 20);
            this.txtLogfileDateFormat.TabIndex = 1;
            this.txtLogfileDateFormat.TextChanged += new System.EventHandler(this.txtLogfileDateFormat_TextChanged);
            // 
            // btnSetDefaults
            // 
            this.btnSetDefaults.Location = new System.Drawing.Point(494, 288);
            this.btnSetDefaults.Name = "btnSetDefaults";
            this.btnSetDefaults.Size = new System.Drawing.Size(206, 23);
            this.btnSetDefaults.TabIndex = 7;
            this.btnSetDefaults.Text = "Standardeinstellungen wiederherstellen";
            this.btnSetDefaults.UseVisualStyleBackColor = true;
            this.btnSetDefaults.Click += new System.EventHandler(this.btnSetDefaults_Click);
            // 
            // grbAddTextToFiile
            // 
            this.grbAddTextToFiile.Controls.Add(this.lblAddTextToFileDefaultText);
            this.grbAddTextToFiile.Controls.Add(this.txtAddTextToFileDefaultText);
            this.grbAddTextToFiile.Controls.Add(this.lblAddTextToFileDateFormat);
            this.grbAddTextToFiile.Controls.Add(this.txtAddTextToFileDateFormat);
            this.grbAddTextToFiile.Location = new System.Drawing.Point(12, 183);
            this.grbAddTextToFiile.Name = "grbAddTextToFiile";
            this.grbAddTextToFiile.Size = new System.Drawing.Size(688, 45);
            this.grbAddTextToFiile.TabIndex = 3;
            this.grbAddTextToFiile.TabStop = false;
            this.grbAddTextToFiile.Text = "Text an bereits existierende Datei anhängen";
            // 
            // lblAddTextToFileDefaultText
            // 
            this.lblAddTextToFileDefaultText.AutoSize = true;
            this.lblAddTextToFileDefaultText.Location = new System.Drawing.Point(6, 22);
            this.lblAddTextToFileDefaultText.Name = "lblAddTextToFileDefaultText";
            this.lblAddTextToFileDefaultText.Size = new System.Drawing.Size(70, 13);
            this.lblAddTextToFileDefaultText.TabIndex = 0;
            this.lblAddTextToFileDefaultText.Text = "Standardtext:";
            // 
            // txtAddTextToFileDefaultText
            // 
            this.txtAddTextToFileDefaultText.Location = new System.Drawing.Point(82, 19);
            this.txtAddTextToFileDefaultText.Name = "txtAddTextToFileDefaultText";
            this.txtAddTextToFileDefaultText.Size = new System.Drawing.Size(206, 20);
            this.txtAddTextToFileDefaultText.TabIndex = 1;
            // 
            // lblAddTextToFileDateFormat
            // 
            this.lblAddTextToFileDateFormat.AutoSize = true;
            this.lblAddTextToFileDateFormat.Location = new System.Drawing.Point(395, 22);
            this.lblAddTextToFileDateFormat.Name = "lblAddTextToFileDateFormat";
            this.lblAddTextToFileDateFormat.Size = new System.Drawing.Size(75, 13);
            this.lblAddTextToFileDateFormat.TabIndex = 2;
            this.lblAddTextToFileDateFormat.Text = "Datumsformat:";
            // 
            // txtAddTextToFileDateFormat
            // 
            this.txtAddTextToFileDateFormat.Location = new System.Drawing.Point(476, 19);
            this.txtAddTextToFileDateFormat.Name = "txtAddTextToFileDateFormat";
            this.txtAddTextToFileDateFormat.Size = new System.Drawing.Size(190, 20);
            this.txtAddTextToFileDateFormat.TabIndex = 3;
            this.txtAddTextToFileDateFormat.TextChanged += new System.EventHandler(this.txtAddTextToFileDateFormat_TextChanged);
            // 
            // chkAutoCheckFileAssociation
            // 
            this.chkAutoCheckFileAssociation.AutoSize = true;
            this.chkAutoCheckFileAssociation.Location = new System.Drawing.Point(152, 293);
            this.chkAutoCheckFileAssociation.Name = "chkAutoCheckFileAssociation";
            this.chkAutoCheckFileAssociation.Size = new System.Drawing.Size(229, 17);
            this.chkAutoCheckFileAssociation.TabIndex = 6;
            this.chkAutoCheckFileAssociation.Text = "Dateizuordnung beim Programmstart prüfen";
            this.chkAutoCheckFileAssociation.UseVisualStyleBackColor = true;
            // 
            // erpDateFormat
            // 
            this.erpDateFormat.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.erpDateFormat.ContainerControl = this;
            // 
            // ApplicationSettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(729, 359);
            this.Controls.Add(this.chkAutoCheckFileAssociation);
            this.Controls.Add(this.grbAddTextToFiile);
            this.Controls.Add(this.btnSetDefaults);
            this.Controls.Add(this.grbLogFile);
            this.Controls.Add(this.grbExplorerSettings);
            this.Controls.Add(this.btnCheckFileAssociation);
            this.Controls.Add(this.grbRecentFiles);
            this.Controls.Add(this.grbProjectFolder);
            this.Controls.Add(this.tolApplicationSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Programmeinstellungen";
            this.grbProjectFolder.ResumeLayout(false);
            this.grbProjectFolder.PerformLayout();
            this.grbRecentFiles.ResumeLayout(false);
            this.grbRecentFiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumRecentFiles)).EndInit();
            this.grbExplorerSettings.ResumeLayout(false);
            this.grbExplorerSettings.PerformLayout();
            this.grbLogFile.ResumeLayout(false);
            this.grbLogFile.PerformLayout();
            this.grbAddTextToFiile.ResumeLayout(false);
            this.grbAddTextToFiile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpDateFormat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ToolStrip tolApplicationSettings;
        private System.Windows.Forms.GroupBox grbProjectFolder;
        private System.Windows.Forms.TextBox txtDefaultFileOpen;
        private System.Windows.Forms.Label lblDefaultFileOpen;
        private System.Windows.Forms.TextBox txtDefaultPath;
        private System.Windows.Forms.Label lblDefaultPath;
        private System.Windows.Forms.GroupBox grbRecentFiles;
        private System.Windows.Forms.GroupBox grbExplorerSettings;
        private System.Windows.Forms.CheckBox chkShowSystemDirectory;
        private System.Windows.Forms.CheckBox chkShowDirectorysWithoutAccess;
        private System.Windows.Forms.CheckBox chkEypandTreeNodeOnClick;
        private System.Windows.Forms.GroupBox grbLogFile;
        private System.Windows.Forms.Label lblLogfileDateFormat;
        private System.Windows.Forms.TextBox txtLogfileDateFormat;
        private System.Windows.Forms.Button btnDefaultFileOpen_Browse;
        private System.Windows.Forms.Button btnDefaultPath_Browse;
        private System.Windows.Forms.Button btnCheckFileAssociation;
        private System.Windows.Forms.Button btnDefaultPath_Delete;
        private System.Windows.Forms.Button btnDefaultFileOpen_Delete;
        private System.Windows.Forms.Button btnSetDefaults;
        private System.Windows.Forms.GroupBox grbAddTextToFiile;
        private System.Windows.Forms.Button btnRecentFilesClear;
        private System.Windows.Forms.NumericUpDown nudNumRecentFiles;
        private System.Windows.Forms.Label lblNumRecentFiles;
        private System.Windows.Forms.Label lblAddTextToFileDateFormat;
        private System.Windows.Forms.TextBox txtAddTextToFileDateFormat;
        private System.Windows.Forms.Label lblAddTextToFileDefaultText;
        private System.Windows.Forms.TextBox txtAddTextToFileDefaultText;
        private System.Windows.Forms.CheckBox chkAutoCheckFileAssociation;
        private System.Windows.Forms.ErrorProvider erpDateFormat;
    }
}