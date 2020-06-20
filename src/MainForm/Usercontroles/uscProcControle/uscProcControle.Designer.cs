﻿namespace OLKI.Programme.QBC.src.MainForm.Usercontroles.uscProcControle
{
    partial class ProcControle
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcControle));
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.lblDirectory = new System.Windows.Forms.Label();
            this.chkRootDirectory = new System.Windows.Forms.CheckBox();
            this.btnBrowseDirectory = new System.Windows.Forms.Button();
            this.btnHandleExistingFile_SetDefault = new System.Windows.Forms.Button();
            this.txtHandleExistingFileText = new System.Windows.Forms.TextBox();
            this.txtAddTextToExistingFileText = new System.Windows.Forms.TextBox();
            this.lblAddTextToExistingFileText = new System.Windows.Forms.Label();
            this.grbHandleExistingFiles = new System.Windows.Forms.GroupBox();
            this.chkCountItemsAndBytes = new System.Windows.Forms.CheckBox();
            this.chkCopyData = new System.Windows.Forms.CheckBox();
            this.grbToDo = new System.Windows.Forms.GroupBox();
            this.btnProcessStart = new System.Windows.Forms.Button();
            this.btnProcessCancel = new System.Windows.Forms.Button();
            this.grbLogFiles = new System.Windows.Forms.GroupBox();
            this.pnlLogfilePath = new System.Windows.Forms.Panel();
            this.chkLogFileAutoPath = new System.Windows.Forms.CheckBox();
            this.btnLogFilePath = new System.Windows.Forms.Button();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.lblLogFilePath = new System.Windows.Forms.Label();
            this.chkLogFileCreate = new System.Windows.Forms.CheckBox();
            this.btnBrowseTargetDirectory = new System.Windows.Forms.Button();
            this.txtTargetDirectory = new System.Windows.Forms.TextBox();
            this.lblTargetDirectory = new System.Windows.Forms.Label();
            this.grbHandleExistingFiles.SuspendLayout();
            this.grbToDo.SuspendLayout();
            this.grbLogFiles.SuspendLayout();
            this.pnlLogfilePath.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.Location = new System.Drawing.Point(96, 3);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(722, 20);
            this.txtDirectory.TabIndex = 1;
            this.txtDirectory.TextChanged += new System.EventHandler(this.txtDirectory_TextChanged);
            // 
            // lblDirectory
            // 
            this.lblDirectory.AutoSize = true;
            this.lblDirectory.Location = new System.Drawing.Point(3, 6);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(98, 13);
            this.lblDirectory.TabIndex = 0;
            this.lblDirectory.Text = "lblDirectory_Text__";
            // 
            // chkRootDirectory
            // 
            this.chkRootDirectory.Checked = true;
            this.chkRootDirectory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRootDirectory.Location = new System.Drawing.Point(96, 29);
            this.chkRootDirectory.Name = "chkRootDirectory";
            this.chkRootDirectory.Size = new System.Drawing.Size(255, 24);
            this.chkRootDirectory.TabIndex = 3;
            this.chkRootDirectory.Text = "chkRootDirectory_Text__";
            this.chkRootDirectory.UseVisualStyleBackColor = true;
            this.chkRootDirectory.CheckedChanged += new System.EventHandler(this.chkRootDirectory_CheckedChanged);
            // 
            // btnBrowseDirectory
            // 
            this.btnBrowseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDirectory.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDirectory.Image")));
            this.btnBrowseDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowseDirectory.Location = new System.Drawing.Point(824, 1);
            this.btnBrowseDirectory.Name = "btnBrowseDirectory";
            this.btnBrowseDirectory.Size = new System.Drawing.Size(130, 23);
            this.btnBrowseDirectory.TabIndex = 2;
            this.btnBrowseDirectory.Text = "Durchsuchen";
            this.btnBrowseDirectory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowseDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseDirectory.Click += new System.EventHandler(this.btnBrowseDirectory_Click);
            // 
            // btnHandleExistingFile_SetDefault
            // 
            this.btnHandleExistingFile_SetDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnHandleExistingFile_SetDefault.Image")));
            this.btnHandleExistingFile_SetDefault.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHandleExistingFile_SetDefault.Location = new System.Drawing.Point(6, 19);
            this.btnHandleExistingFile_SetDefault.Name = "btnHandleExistingFile_SetDefault";
            this.btnHandleExistingFile_SetDefault.Size = new System.Drawing.Size(338, 23);
            this.btnHandleExistingFile_SetDefault.TabIndex = 0;
            this.btnHandleExistingFile_SetDefault.Text = "Standardaktion auswählen";
            this.btnHandleExistingFile_SetDefault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHandleExistingFile_SetDefault.UseVisualStyleBackColor = true;
            this.btnHandleExistingFile_SetDefault.Click += new System.EventHandler(this.btnHandleExistingFile_SetDefault_Click);
            // 
            // txtHandleExistingFileText
            // 
            this.txtHandleExistingFileText.Location = new System.Drawing.Point(6, 48);
            this.txtHandleExistingFileText.Name = "txtHandleExistingFileText";
            this.txtHandleExistingFileText.ReadOnly = true;
            this.txtHandleExistingFileText.Size = new System.Drawing.Size(338, 20);
            this.txtHandleExistingFileText.TabIndex = 1;
            // 
            // txtAddTextToExistingFileText
            // 
            this.txtAddTextToExistingFileText.Location = new System.Drawing.Point(94, 74);
            this.txtAddTextToExistingFileText.Name = "txtAddTextToExistingFileText";
            this.txtAddTextToExistingFileText.ReadOnly = true;
            this.txtAddTextToExistingFileText.Size = new System.Drawing.Size(250, 20);
            this.txtAddTextToExistingFileText.TabIndex = 3;
            // 
            // lblAddTextToExistingFileText
            // 
            this.lblAddTextToExistingFileText.AutoSize = true;
            this.lblAddTextToExistingFileText.Location = new System.Drawing.Point(6, 77);
            this.lblAddTextToExistingFileText.Name = "lblAddTextToExistingFileText";
            this.lblAddTextToExistingFileText.Size = new System.Drawing.Size(82, 13);
            this.lblAddTextToExistingFileText.TabIndex = 2;
            this.lblAddTextToExistingFileText.Text = "Text anhängen:";
            // 
            // grbHandleExistingFiles
            // 
            this.grbHandleExistingFiles.Controls.Add(this.lblAddTextToExistingFileText);
            this.grbHandleExistingFiles.Controls.Add(this.txtAddTextToExistingFileText);
            this.grbHandleExistingFiles.Controls.Add(this.txtHandleExistingFileText);
            this.grbHandleExistingFiles.Controls.Add(this.btnHandleExistingFile_SetDefault);
            this.grbHandleExistingFiles.Location = new System.Drawing.Point(3, 58);
            this.grbHandleExistingFiles.Name = "grbHandleExistingFiles";
            this.grbHandleExistingFiles.Size = new System.Drawing.Size(350, 100);
            this.grbHandleExistingFiles.TabIndex = 7;
            this.grbHandleExistingFiles.TabStop = false;
            this.grbHandleExistingFiles.Text = "Verhalten bei vorhandenen Dateien";
            // 
            // chkCountItemsAndBytes
            // 
            this.chkCountItemsAndBytes.AutoSize = true;
            this.chkCountItemsAndBytes.Checked = true;
            this.chkCountItemsAndBytes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCountItemsAndBytes.Location = new System.Drawing.Point(6, 19);
            this.chkCountItemsAndBytes.Name = "chkCountItemsAndBytes";
            this.chkCountItemsAndBytes.Size = new System.Drawing.Size(191, 17);
            this.chkCountItemsAndBytes.TabIndex = 0;
            this.chkCountItemsAndBytes.Text = "1. Aufgabenumfang vorab ermitteln";
            this.chkCountItemsAndBytes.UseVisualStyleBackColor = true;
            this.chkCountItemsAndBytes.CheckedChanged += new System.EventHandler(this.chkCountItemsAndBytes_CheckedChanged);
            // 
            // chkCopyData
            // 
            this.chkCopyData.AutoSize = true;
            this.chkCopyData.Checked = true;
            this.chkCopyData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopyData.Location = new System.Drawing.Point(6, 42);
            this.chkCopyData.Name = "chkCopyData";
            this.chkCopyData.Size = new System.Drawing.Size(153, 17);
            this.chkCopyData.TabIndex = 1;
            this.chkCopyData.Text = "2. Datenkopie durchführen";
            this.chkCopyData.UseVisualStyleBackColor = true;
            this.chkCopyData.CheckedChanged += new System.EventHandler(this.chkCopyData_CheckedChanged);
            // 
            // grbToDo
            // 
            this.grbToDo.Controls.Add(this.chkCopyData);
            this.grbToDo.Controls.Add(this.chkCountItemsAndBytes);
            this.grbToDo.Location = new System.Drawing.Point(3, 164);
            this.grbToDo.Name = "grbToDo";
            this.grbToDo.Size = new System.Drawing.Size(350, 68);
            this.grbToDo.TabIndex = 9;
            this.grbToDo.TabStop = false;
            this.grbToDo.Text = "Umfang der Sicherungskopie";
            // 
            // btnProcessStart
            // 
            this.btnProcessStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessStart.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessStart.Image")));
            this.btnProcessStart.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcessStart.Location = new System.Drawing.Point(359, 164);
            this.btnProcessStart.Name = "btnProcessStart";
            this.btnProcessStart.Size = new System.Drawing.Size(361, 68);
            this.btnProcessStart.TabIndex = 10;
            this.btnProcessStart.Text = "btnProcessStart_Text__";
            this.btnProcessStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProcessStart.UseVisualStyleBackColor = true;
            this.btnProcessStart.Click += new System.EventHandler(this.btnProcessStart_Click);
            // 
            // btnProcessCancel
            // 
            this.btnProcessCancel.Enabled = false;
            this.btnProcessCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnProcessCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessCancel.Image")));
            this.btnProcessCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcessCancel.Location = new System.Drawing.Point(726, 164);
            this.btnProcessCancel.Name = "btnProcessCancel";
            this.btnProcessCancel.Size = new System.Drawing.Size(228, 68);
            this.btnProcessCancel.TabIndex = 11;
            this.btnProcessCancel.Text = "Abbrechen";
            this.btnProcessCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProcessCancel.UseVisualStyleBackColor = true;
            this.btnProcessCancel.Click += new System.EventHandler(this.btnProcessCancel_Click);
            // 
            // grbLogFiles
            // 
            this.grbLogFiles.Controls.Add(this.pnlLogfilePath);
            this.grbLogFiles.Controls.Add(this.chkLogFileCreate);
            this.grbLogFiles.Location = new System.Drawing.Point(359, 58);
            this.grbLogFiles.Name = "grbLogFiles";
            this.grbLogFiles.Size = new System.Drawing.Size(595, 100);
            this.grbLogFiles.TabIndex = 8;
            this.grbLogFiles.TabStop = false;
            this.grbLogFiles.Text = "Protokolldateien";
            // 
            // pnlLogfilePath
            // 
            this.pnlLogfilePath.Controls.Add(this.chkLogFileAutoPath);
            this.pnlLogfilePath.Controls.Add(this.btnLogFilePath);
            this.pnlLogfilePath.Controls.Add(this.txtLogFilePath);
            this.pnlLogfilePath.Controls.Add(this.lblLogFilePath);
            this.pnlLogfilePath.Location = new System.Drawing.Point(6, 42);
            this.pnlLogfilePath.Name = "pnlLogfilePath";
            this.pnlLogfilePath.Size = new System.Drawing.Size(583, 53);
            this.pnlLogfilePath.TabIndex = 1;
            // 
            // chkLogFileAutoPath
            // 
            this.chkLogFileAutoPath.AutoSize = true;
            this.chkLogFileAutoPath.Checked = true;
            this.chkLogFileAutoPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLogFileAutoPath.Location = new System.Drawing.Point(0, 0);
            this.chkLogFileAutoPath.Name = "chkLogFileAutoPath";
            this.chkLogFileAutoPath.Size = new System.Drawing.Size(161, 17);
            this.chkLogFileAutoPath.TabIndex = 0;
            this.chkLogFileAutoPath.Text = "chkLogFileAutoPath_Text__";
            this.chkLogFileAutoPath.UseVisualStyleBackColor = true;
            this.chkLogFileAutoPath.CheckedChanged += new System.EventHandler(this.chkLogFileAutoPath_CheckedChanged);
            // 
            // btnLogFilePath
            // 
            this.btnLogFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnLogFilePath.Image")));
            this.btnLogFilePath.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogFilePath.Location = new System.Drawing.Point(459, 25);
            this.btnLogFilePath.Name = "btnLogFilePath";
            this.btnLogFilePath.Size = new System.Drawing.Size(124, 23);
            this.btnLogFilePath.TabIndex = 3;
            this.btnLogFilePath.Text = "Durchsuchen";
            this.btnLogFilePath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogFilePath.UseVisualStyleBackColor = true;
            this.btnLogFilePath.Click += new System.EventHandler(this.btnLogFilePath_Click);
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Location = new System.Drawing.Point(38, 27);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.Size = new System.Drawing.Size(415, 20);
            this.txtLogFilePath.TabIndex = 2;
            // 
            // lblLogFilePath
            // 
            this.lblLogFilePath.AutoSize = true;
            this.lblLogFilePath.Location = new System.Drawing.Point(0, 30);
            this.lblLogFilePath.Name = "lblLogFilePath";
            this.lblLogFilePath.Size = new System.Drawing.Size(32, 13);
            this.lblLogFilePath.TabIndex = 1;
            this.lblLogFilePath.Text = "Pfad:";
            // 
            // chkLogFileCreate
            // 
            this.chkLogFileCreate.AutoSize = true;
            this.chkLogFileCreate.Checked = true;
            this.chkLogFileCreate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLogFileCreate.Location = new System.Drawing.Point(6, 19);
            this.chkLogFileCreate.Name = "chkLogFileCreate";
            this.chkLogFileCreate.Size = new System.Drawing.Size(148, 17);
            this.chkLogFileCreate.TabIndex = 0;
            this.chkLogFileCreate.Text = "chkLogFileCreate_Text__";
            this.chkLogFileCreate.UseVisualStyleBackColor = true;
            this.chkLogFileCreate.CheckedChanged += new System.EventHandler(this.chkLogFileCreate_CheckedChanged);
            // 
            // btnBrowseTargetDirectory
            // 
            this.btnBrowseTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTargetDirectory.Enabled = false;
            this.btnBrowseTargetDirectory.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseTargetDirectory.Image")));
            this.btnBrowseTargetDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowseTargetDirectory.Location = new System.Drawing.Point(824, 28);
            this.btnBrowseTargetDirectory.Name = "btnBrowseTargetDirectory";
            this.btnBrowseTargetDirectory.Size = new System.Drawing.Size(130, 23);
            this.btnBrowseTargetDirectory.TabIndex = 6;
            this.btnBrowseTargetDirectory.Text = "Durchsuchen";
            this.btnBrowseTargetDirectory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowseTargetDirectory.UseVisualStyleBackColor = true;
            // 
            // txtTargetDirectory
            // 
            this.txtTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetDirectory.Enabled = false;
            this.txtTargetDirectory.Location = new System.Drawing.Point(435, 30);
            this.txtTargetDirectory.Name = "txtTargetDirectory";
            this.txtTargetDirectory.Size = new System.Drawing.Size(383, 20);
            this.txtTargetDirectory.TabIndex = 5;
            // 
            // lblTargetDirectory
            // 
            this.lblTargetDirectory.AutoSize = true;
            this.lblTargetDirectory.Enabled = false;
            this.lblTargetDirectory.Location = new System.Drawing.Point(349, 33);
            this.lblTargetDirectory.Name = "lblTargetDirectory";
            this.lblTargetDirectory.Size = new System.Drawing.Size(80, 13);
            this.lblTargetDirectory.TabIndex = 4;
            this.lblTargetDirectory.Text = "Zielverzeichnis:";
            // 
            // ProcControle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTargetDirectory);
            this.Controls.Add(this.txtTargetDirectory);
            this.Controls.Add(this.btnBrowseTargetDirectory);
            this.Controls.Add(this.grbLogFiles);
            this.Controls.Add(this.btnProcessCancel);
            this.Controls.Add(this.btnProcessStart);
            this.Controls.Add(this.grbToDo);
            this.Controls.Add(this.grbHandleExistingFiles);
            this.Controls.Add(this.btnBrowseDirectory);
            this.Controls.Add(this.chkRootDirectory);
            this.Controls.Add(this.lblDirectory);
            this.Controls.Add(this.txtDirectory);
            this.Name = "ProcControle";
            this.Size = new System.Drawing.Size(957, 235);
            this.grbHandleExistingFiles.ResumeLayout(false);
            this.grbHandleExistingFiles.PerformLayout();
            this.grbToDo.ResumeLayout(false);
            this.grbToDo.PerformLayout();
            this.grbLogFiles.ResumeLayout(false);
            this.grbLogFiles.PerformLayout();
            this.pnlLogfilePath.ResumeLayout(false);
            this.pnlLogfilePath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Label lblDirectory;
        internal System.Windows.Forms.CheckBox chkRootDirectory;
        private System.Windows.Forms.Button btnBrowseDirectory;
        private System.Windows.Forms.Button btnHandleExistingFile_SetDefault;
        private System.Windows.Forms.TextBox txtHandleExistingFileText;
        private System.Windows.Forms.TextBox txtAddTextToExistingFileText;
        private System.Windows.Forms.Label lblAddTextToExistingFileText;
        private System.Windows.Forms.GroupBox grbHandleExistingFiles;
        internal System.Windows.Forms.CheckBox chkCountItemsAndBytes;
        internal System.Windows.Forms.CheckBox chkCopyData;
        private System.Windows.Forms.GroupBox grbToDo;
        internal System.Windows.Forms.Button btnProcessStart;
        internal System.Windows.Forms.Button btnProcessCancel;
        private System.Windows.Forms.GroupBox grbLogFiles;
        internal System.Windows.Forms.CheckBox chkLogFileCreate;
        private System.Windows.Forms.Panel pnlLogfilePath;
        internal System.Windows.Forms.CheckBox chkLogFileAutoPath;
        private System.Windows.Forms.Button btnLogFilePath;
        internal System.Windows.Forms.TextBox txtLogFilePath;
        private System.Windows.Forms.Label lblLogFilePath;
        private System.Windows.Forms.Button btnBrowseTargetDirectory;
        internal System.Windows.Forms.TextBox txtTargetDirectory;
        private System.Windows.Forms.Label lblTargetDirectory;
    }
}
