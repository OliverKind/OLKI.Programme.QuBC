namespace OLKI.Programme.QuBC.MainForm.Usercontroles.uscProgress
{
    partial class TaskProgress
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
            this.lblAllByte = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.lblCopyRemainItem = new System.Windows.Forms.Label();
            this.txtCopyRemainTime = new System.Windows.Forms.TextBox();
            this.lblCopyElapsed = new System.Windows.Forms.Label();
            this.txtCopyElapsed = new System.Windows.Forms.TextBox();
            this.lblCopyStart = new System.Windows.Forms.Label();
            this.txtCopyStart = new System.Windows.Forms.TextBox();
            this.lblActualFileByte = new System.Windows.Forms.Label();
            this.lblActualFile = new System.Windows.Forms.Label();
            this.lblActualDirFiles = new System.Windows.Forms.Label();
            this.lblActualDir = new System.Windows.Forms.Label();
            this.lblAllDir = new System.Windows.Forms.Label();
            this.lblpbaAllItems = new System.Windows.Forms.Label();
            this.lblUpdateInterval = new System.Windows.Forms.Label();
            this.nudUpdateInterval = new System.Windows.Forms.NumericUpDown();
            this.lblUpdateIntervalDimension = new System.Windows.Forms.Label();
            this.lblStepText = new System.Windows.Forms.Label();
            this.expActualDir = new OLKI.Toolbox.Widgets.ExtProgressBar();
            this.expActualFile = new OLKI.Toolbox.Widgets.ExtProgressBar();
            this.expAllDir = new OLKI.Toolbox.Widgets.ExtProgressBar();
            this.expAllByte = new OLKI.Toolbox.Widgets.ExtProgressBar();
            this.expAllItems = new OLKI.Toolbox.Widgets.ExtProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAllByte
            // 
            this.lblAllByte.Location = new System.Drawing.Point(0, 113);
            this.lblAllByte.Name = "lblAllByte";
            this.lblAllByte.Size = new System.Drawing.Size(190, 23);
            this.lblAllByte.TabIndex = 17;
            this.lblAllByte.Text = "Gesamtfortschritt alle Daten:";
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(489, 29);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(94, 13);
            this.lblStep.TabIndex = 9;
            this.lblStep.Text = "Aktueller Vorgang:";
            this.lblStep.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCopyRemainItem
            // 
            this.lblCopyRemainItem.Location = new System.Drawing.Point(0, 55);
            this.lblCopyRemainItem.Name = "lblCopyRemainItem";
            this.lblCopyRemainItem.Size = new System.Drawing.Size(190, 23);
            this.lblCopyRemainItem.TabIndex = 4;
            this.lblCopyRemainItem.Text = "Erwartete Restzeit:";
            // 
            // txtCopyRemainTime
            // 
            this.txtCopyRemainTime.Location = new System.Drawing.Point(196, 52);
            this.txtCopyRemainTime.Name = "txtCopyRemainTime";
            this.txtCopyRemainTime.ReadOnly = true;
            this.txtCopyRemainTime.Size = new System.Drawing.Size(200, 20);
            this.txtCopyRemainTime.TabIndex = 5;
            // 
            // lblCopyElapsed
            // 
            this.lblCopyElapsed.Location = new System.Drawing.Point(0, 29);
            this.lblCopyElapsed.Name = "lblCopyElapsed";
            this.lblCopyElapsed.Size = new System.Drawing.Size(190, 23);
            this.lblCopyElapsed.TabIndex = 2;
            this.lblCopyElapsed.Text = "Vergangene Zeit:";
            // 
            // txtCopyElapsed
            // 
            this.txtCopyElapsed.Location = new System.Drawing.Point(196, 26);
            this.txtCopyElapsed.Name = "txtCopyElapsed";
            this.txtCopyElapsed.ReadOnly = true;
            this.txtCopyElapsed.Size = new System.Drawing.Size(200, 20);
            this.txtCopyElapsed.TabIndex = 3;
            // 
            // lblCopyStart
            // 
            this.lblCopyStart.Location = new System.Drawing.Point(0, 3);
            this.lblCopyStart.Name = "lblCopyStart";
            this.lblCopyStart.Size = new System.Drawing.Size(190, 23);
            this.lblCopyStart.TabIndex = 0;
            this.lblCopyStart.Text = "Vorgang gestartet um:";
            // 
            // txtCopyStart
            // 
            this.txtCopyStart.Location = new System.Drawing.Point(196, 0);
            this.txtCopyStart.Name = "txtCopyStart";
            this.txtCopyStart.ReadOnly = true;
            this.txtCopyStart.Size = new System.Drawing.Size(200, 20);
            this.txtCopyStart.TabIndex = 1;
            // 
            // lblActualFileByte
            // 
            this.lblActualFileByte.Location = new System.Drawing.Point(-3, 252);
            this.lblActualFileByte.Name = "lblActualFileByte";
            this.lblActualFileByte.Size = new System.Drawing.Size(190, 23);
            this.lblActualFileByte.TabIndex = 34;
            this.lblActualFileByte.Text = "Fortschritt aktuelle Datei:";
            // 
            // lblActualFile
            // 
            this.lblActualFile.Location = new System.Drawing.Point(0, 223);
            this.lblActualFile.Name = "lblActualFile";
            this.lblActualFile.Size = new System.Drawing.Size(190, 23);
            this.lblActualFile.TabIndex = 32;
            this.lblActualFile.Text = "Aktuelle Datei:";
            // 
            // lblActualDirFiles
            // 
            this.lblActualDirFiles.Location = new System.Drawing.Point(0, 197);
            this.lblActualDirFiles.Name = "lblActualDirFiles";
            this.lblActualDirFiles.Size = new System.Drawing.Size(190, 23);
            this.lblActualDirFiles.TabIndex = 28;
            this.lblActualDirFiles.Text = "Fortschritt Dateien im Ordner:";
            // 
            // lblActualDir
            // 
            this.lblActualDir.Location = new System.Drawing.Point(0, 168);
            this.lblActualDir.Name = "lblActualDir";
            this.lblActualDir.Size = new System.Drawing.Size(190, 23);
            this.lblActualDir.TabIndex = 26;
            this.lblActualDir.Text = "Aktueller Ordner:";
            // 
            // lblAllDir
            // 
            this.lblAllDir.Location = new System.Drawing.Point(0, 139);
            this.lblAllDir.Name = "lblAllDir";
            this.lblAllDir.Size = new System.Drawing.Size(190, 23);
            this.lblAllDir.TabIndex = 22;
            this.lblAllDir.Text = "Fortschritt Ordner:";
            // 
            // lblpbaAllItems
            // 
            this.lblpbaAllItems.Location = new System.Drawing.Point(0, 84);
            this.lblpbaAllItems.Name = "lblpbaAllItems";
            this.lblpbaAllItems.Size = new System.Drawing.Size(190, 23);
            this.lblpbaAllItems.TabIndex = 13;
            this.lblpbaAllItems.Text = "Gesamtfortschritt alle Objekte:";
            // 
            // lblUpdateInterval
            // 
            this.lblUpdateInterval.AutoSize = true;
            this.lblUpdateInterval.Location = new System.Drawing.Point(502, 3);
            this.lblUpdateInterval.Name = "lblUpdateInterval";
            this.lblUpdateInterval.Size = new System.Drawing.Size(81, 13);
            this.lblUpdateInterval.TabIndex = 6;
            this.lblUpdateInterval.Text = "Updateintervall:";
            this.lblUpdateInterval.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // nudUpdateInterval
            // 
            this.nudUpdateInterval.DecimalPlaces = 1;
            this.nudUpdateInterval.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudUpdateInterval.Location = new System.Drawing.Point(589, 1);
            this.nudUpdateInterval.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudUpdateInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudUpdateInterval.Name = "nudUpdateInterval";
            this.nudUpdateInterval.Size = new System.Drawing.Size(42, 20);
            this.nudUpdateInterval.TabIndex = 7;
            this.nudUpdateInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lblUpdateIntervalDimension
            // 
            this.lblUpdateIntervalDimension.AutoSize = true;
            this.lblUpdateIntervalDimension.Location = new System.Drawing.Point(637, 3);
            this.lblUpdateIntervalDimension.Name = "lblUpdateIntervalDimension";
            this.lblUpdateIntervalDimension.Size = new System.Drawing.Size(56, 13);
            this.lblUpdateIntervalDimension.TabIndex = 8;
            this.lblUpdateIntervalDimension.Text = "Sekunden";
            // 
            // lblStepText
            // 
            this.lblStepText.AutoSize = true;
            this.lblStepText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStepText.Location = new System.Drawing.Point(589, 29);
            this.lblStepText.Name = "lblStepText";
            this.lblStepText.Size = new System.Drawing.Size(165, 13);
            this.lblStepText.TabIndex = 10;
            this.lblStepText.Text = "Aktueller Vorgang - Beschreibung";
            // 
            // expActualDir
            // 
            this.expActualDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expActualDir.AutoByteDimension = false;
            this.expActualDir.ByteDimension = OLKI.Toolbox.DirectoryAndFile.FileSize.Dimension.NoDimension;
            this.expActualDir.Location = new System.Drawing.Point(196, 165);
            this.expActualDir.MaxValue = ((long)(0));
            this.expActualDir.MinimumSize = new System.Drawing.Size(300, 23);
            this.expActualDir.Name = "expActualDir";
            this.expActualDir.ShowDimensionComboBox = false;
            this.expActualDir.Size = new System.Drawing.Size(755, 49);
            this.expActualDir.TabIndex = 44;
            this.expActualDir.Value = ((long)(-1));
            // 
            // expActualFile
            // 
            this.expActualFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expActualFile.ByteDimension = OLKI.Toolbox.DirectoryAndFile.FileSize.Dimension.NoDimension;
            this.expActualFile.Location = new System.Drawing.Point(196, 220);
            this.expActualFile.MaxValue = ((long)(0));
            this.expActualFile.MinimumSize = new System.Drawing.Size(300, 23);
            this.expActualFile.Name = "expActualFile";
            this.expActualFile.Size = new System.Drawing.Size(755, 49);
            this.expActualFile.TabIndex = 43;
            this.expActualFile.Value = ((long)(-1));
            // 
            // expAllDir
            // 
            this.expAllDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expAllDir.AutoByteDimension = false;
            this.expAllDir.ByteDimension = OLKI.Toolbox.DirectoryAndFile.FileSize.Dimension.NoDimension;
            this.expAllDir.Location = new System.Drawing.Point(196, 136);
            this.expAllDir.MaxValue = ((long)(0));
            this.expAllDir.MinimumSize = new System.Drawing.Size(300, 23);
            this.expAllDir.Name = "expAllDir";
            this.expAllDir.ShowDescriptionText = false;
            this.expAllDir.ShowDimensionComboBox = false;
            this.expAllDir.Size = new System.Drawing.Size(755, 23);
            this.expAllDir.TabIndex = 41;
            this.expAllDir.Value = ((long)(-1));
            // 
            // expAllByte
            // 
            this.expAllByte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expAllByte.ByteDimension = OLKI.Toolbox.DirectoryAndFile.FileSize.Dimension.NoDimension;
            this.expAllByte.Location = new System.Drawing.Point(196, 107);
            this.expAllByte.MaxValue = ((long)(0));
            this.expAllByte.MinimumSize = new System.Drawing.Size(300, 23);
            this.expAllByte.Name = "expAllByte";
            this.expAllByte.ShowDescriptionText = false;
            this.expAllByte.Size = new System.Drawing.Size(755, 23);
            this.expAllByte.TabIndex = 40;
            this.expAllByte.Value = ((long)(-1));
            // 
            // expAllItems
            // 
            this.expAllItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expAllItems.AutoByteDimension = false;
            this.expAllItems.ByteDimension = OLKI.Toolbox.DirectoryAndFile.FileSize.Dimension.NoDimension;
            this.expAllItems.Location = new System.Drawing.Point(196, 78);
            this.expAllItems.MaxValue = ((long)(0));
            this.expAllItems.MinimumSize = new System.Drawing.Size(300, 23);
            this.expAllItems.Name = "expAllItems";
            this.expAllItems.ShowDescriptionText = false;
            this.expAllItems.ShowDimensionComboBox = false;
            this.expAllItems.Size = new System.Drawing.Size(755, 23);
            this.expAllItems.TabIndex = 39;
            this.expAllItems.Value = ((long)(-1));
            // 
            // TaskProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.expActualDir);
            this.Controls.Add(this.expActualFile);
            this.Controls.Add(this.expAllDir);
            this.Controls.Add(this.expAllByte);
            this.Controls.Add(this.expAllItems);
            this.Controls.Add(this.lblStepText);
            this.Controls.Add(this.lblUpdateIntervalDimension);
            this.Controls.Add(this.nudUpdateInterval);
            this.Controls.Add(this.lblUpdateInterval);
            this.Controls.Add(this.lblAllByte);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.lblCopyRemainItem);
            this.Controls.Add(this.txtCopyRemainTime);
            this.Controls.Add(this.lblCopyElapsed);
            this.Controls.Add(this.txtCopyElapsed);
            this.Controls.Add(this.lblCopyStart);
            this.Controls.Add(this.txtCopyStart);
            this.Controls.Add(this.lblActualFileByte);
            this.Controls.Add(this.lblActualFile);
            this.Controls.Add(this.lblActualDirFiles);
            this.Controls.Add(this.lblActualDir);
            this.Controls.Add(this.lblAllDir);
            this.Controls.Add(this.lblpbaAllItems);
            this.Name = "TaskProgress";
            this.Size = new System.Drawing.Size(951, 270);
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblAllByte;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Label lblCopyRemainItem;
        internal System.Windows.Forms.TextBox txtCopyRemainTime;
        private System.Windows.Forms.Label lblCopyElapsed;
        internal System.Windows.Forms.TextBox txtCopyElapsed;
        private System.Windows.Forms.Label lblCopyStart;
        internal System.Windows.Forms.TextBox txtCopyStart;
        private System.Windows.Forms.Label lblActualFileByte;
        private System.Windows.Forms.Label lblActualFile;
        private System.Windows.Forms.Label lblActualDirFiles;
        private System.Windows.Forms.Label lblActualDir;
        private System.Windows.Forms.Label lblAllDir;
        private System.Windows.Forms.Label lblpbaAllItems;
        private System.Windows.Forms.Label lblUpdateInterval;
        private System.Windows.Forms.NumericUpDown nudUpdateInterval;
        private System.Windows.Forms.Label lblUpdateIntervalDimension;
        internal System.Windows.Forms.Label lblStepText;
        private Toolbox.Widgets.ExtProgressBar expActualFile;
        private Toolbox.Widgets.ExtProgressBar expAllDir;
        private Toolbox.Widgets.ExtProgressBar expAllByte;
        private Toolbox.Widgets.ExtProgressBar expAllItems;
        private Toolbox.Widgets.ExtProgressBar expActualDir;
    }
}
