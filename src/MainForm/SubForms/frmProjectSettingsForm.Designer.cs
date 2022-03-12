
namespace OLKI.Programme.QuBC.src.MainForm.SubForms
{
    partial class ProjectSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSettingsForm));
            this.btnProjectCleanUp = new System.Windows.Forms.Button();
            this.grbCommon = new System.Windows.Forms.GroupBox();
            this.lblDefaultTab = new System.Windows.Forms.Label();
            this.cboDefaultTab = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grbAutomation = new System.Windows.Forms.GroupBox();
            this.rabAutomationRestore = new System.Windows.Forms.RadioButton();
            this.grbAutomationSettings = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFinishAction = new System.Windows.Forms.ComboBox();
            this.lblWaitTime2 = new System.Windows.Forms.Label();
            this.lblWaitTime = new System.Windows.Forms.Label();
            this.nudWaitTime = new System.Windows.Forms.NumericUpDown();
            this.rabAutomationBackup = new System.Windows.Forms.RadioButton();
            this.rabAutomationNone = new System.Windows.Forms.RadioButton();
            this.grbCommon.SuspendLayout();
            this.grbAutomation.SuspendLayout();
            this.grbAutomationSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaitTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProjectCleanUp
            // 
            this.btnProjectCleanUp.Image = ((System.Drawing.Image)(resources.GetObject("btnProjectCleanUp.Image")));
            this.btnProjectCleanUp.Location = new System.Drawing.Point(6, 19);
            this.btnProjectCleanUp.Name = "btnProjectCleanUp";
            this.btnProjectCleanUp.Size = new System.Drawing.Size(150, 23);
            this.btnProjectCleanUp.TabIndex = 0;
            this.btnProjectCleanUp.Text = "Projekt bereinigen";
            this.btnProjectCleanUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProjectCleanUp.UseVisualStyleBackColor = true;
            this.btnProjectCleanUp.Click += new System.EventHandler(this.btnProjectCleanUp_Click);
            // 
            // grbCommon
            // 
            this.grbCommon.Controls.Add(this.lblDefaultTab);
            this.grbCommon.Controls.Add(this.cboDefaultTab);
            this.grbCommon.Controls.Add(this.btnProjectCleanUp);
            this.grbCommon.Location = new System.Drawing.Point(12, 12);
            this.grbCommon.Name = "grbCommon";
            this.grbCommon.Size = new System.Drawing.Size(705, 48);
            this.grbCommon.TabIndex = 0;
            this.grbCommon.TabStop = false;
            this.grbCommon.Text = "Allgemein";
            // 
            // lblDefaultTab
            // 
            this.lblDefaultTab.AutoSize = true;
            this.lblDefaultTab.Location = new System.Drawing.Point(218, 22);
            this.lblDefaultTab.Name = "lblDefaultTab";
            this.lblDefaultTab.Size = new System.Drawing.Size(179, 13);
            this.lblDefaultTab.TabIndex = 1;
            this.lblDefaultTab.Text = "Registerkarte beim Öffnen anzeigen:";
            // 
            // cboDefaultTab
            // 
            this.cboDefaultTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboDefaultTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDefaultTab.FormattingEnabled = true;
            this.cboDefaultTab.Items.AddRange(new object[] {
            "-- Programmeinstellungen --",
            "Sicherung - Quelle",
            "Sicherung - Ausführen",
            "Sicherung - Wiederherstellen",
            "Zusammenfassung - Fehler"});
            this.cboDefaultTab.Location = new System.Drawing.Point(403, 19);
            this.cboDefaultTab.Name = "cboDefaultTab";
            this.cboDefaultTab.Size = new System.Drawing.Size(296, 21);
            this.cboDefaultTab.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(607, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 238);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(110, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grbAutomation
            // 
            this.grbAutomation.Controls.Add(this.rabAutomationRestore);
            this.grbAutomation.Controls.Add(this.grbAutomationSettings);
            this.grbAutomation.Controls.Add(this.rabAutomationBackup);
            this.grbAutomation.Controls.Add(this.rabAutomationNone);
            this.grbAutomation.Location = new System.Drawing.Point(12, 66);
            this.grbAutomation.Name = "grbAutomation";
            this.grbAutomation.Size = new System.Drawing.Size(705, 166);
            this.grbAutomation.TabIndex = 1;
            this.grbAutomation.TabStop = false;
            this.grbAutomation.Text = "Automatisation";
            // 
            // rabAutomationRestore
            // 
            this.rabAutomationRestore.AutoSize = true;
            this.rabAutomationRestore.Location = new System.Drawing.Point(6, 65);
            this.rabAutomationRestore.Name = "rabAutomationRestore";
            this.rabAutomationRestore.Size = new System.Drawing.Size(171, 17);
            this.rabAutomationRestore.TabIndex = 2;
            this.rabAutomationRestore.TabStop = true;
            this.rabAutomationRestore.Text = "Datenwiederherstellung starten";
            this.rabAutomationRestore.UseVisualStyleBackColor = true;
            this.rabAutomationRestore.CheckedChanged += new System.EventHandler(this.rabAutomationRestore_CheckedChanged);
            // 
            // grbAutomationSettings
            // 
            this.grbAutomationSettings.Controls.Add(this.label1);
            this.grbAutomationSettings.Controls.Add(this.cboFinishAction);
            this.grbAutomationSettings.Controls.Add(this.lblWaitTime2);
            this.grbAutomationSettings.Controls.Add(this.lblWaitTime);
            this.grbAutomationSettings.Controls.Add(this.nudWaitTime);
            this.grbAutomationSettings.Location = new System.Drawing.Point(6, 88);
            this.grbAutomationSettings.Name = "grbAutomationSettings";
            this.grbAutomationSettings.Size = new System.Drawing.Size(693, 72);
            this.grbAutomationSettings.TabIndex = 3;
            this.grbAutomationSettings.TabStop = false;
            this.grbAutomationSettings.Text = "Einstellungen";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Aktion nach Beenden des Automatischen Vorganges:";
            // 
            // cboFinishAction
            // 
            this.cboFinishAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboFinishAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFinishAction.FormattingEnabled = true;
            this.cboFinishAction.Items.AddRange(new object[] {
            "Nichts unternehmen",
            "Programm beenden (Meldungen gehen verloren)",
            "System herunterfahren (erzwungen, Meldungen gehen verloren)"});
            this.cboFinishAction.Location = new System.Drawing.Point(272, 45);
            this.cboFinishAction.Name = "cboFinishAction";
            this.cboFinishAction.Size = new System.Drawing.Size(415, 21);
            this.cboFinishAction.TabIndex = 4;
            // 
            // lblWaitTime2
            // 
            this.lblWaitTime2.AutoSize = true;
            this.lblWaitTime2.Location = new System.Drawing.Point(126, 21);
            this.lblWaitTime2.Name = "lblWaitTime2";
            this.lblWaitTime2.Size = new System.Drawing.Size(56, 13);
            this.lblWaitTime2.TabIndex = 2;
            this.lblWaitTime2.Text = "Sekunden";
            // 
            // lblWaitTime
            // 
            this.lblWaitTime.AutoSize = true;
            this.lblWaitTime.Location = new System.Drawing.Point(6, 21);
            this.lblWaitTime.Name = "lblWaitTime";
            this.lblWaitTime.Size = new System.Drawing.Size(70, 13);
            this.lblWaitTime.TabIndex = 0;
            this.lblWaitTime.Text = "Verzögerung:";
            // 
            // nudWaitTime
            // 
            this.nudWaitTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.nudWaitTime.Location = new System.Drawing.Point(82, 19);
            this.nudWaitTime.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudWaitTime.Name = "nudWaitTime";
            this.nudWaitTime.Size = new System.Drawing.Size(38, 20);
            this.nudWaitTime.TabIndex = 1;
            this.nudWaitTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // rabAutomationBackup
            // 
            this.rabAutomationBackup.AutoSize = true;
            this.rabAutomationBackup.Location = new System.Drawing.Point(6, 42);
            this.rabAutomationBackup.Name = "rabAutomationBackup";
            this.rabAutomationBackup.Size = new System.Drawing.Size(268, 17);
            this.rabAutomationBackup.TabIndex = 1;
            this.rabAutomationBackup.TabStop = true;
            this.rabAutomationBackup.Text = "Sicherungskopie Starten beim Öffnen des Projektes";
            this.rabAutomationBackup.UseVisualStyleBackColor = true;
            this.rabAutomationBackup.CheckedChanged += new System.EventHandler(this.rabAutomationBackup_CheckedChanged);
            // 
            // rabAutomationNone
            // 
            this.rabAutomationNone.AutoSize = true;
            this.rabAutomationNone.Location = new System.Drawing.Point(6, 19);
            this.rabAutomationNone.Name = "rabAutomationNone";
            this.rabAutomationNone.Size = new System.Drawing.Size(52, 17);
            this.rabAutomationNone.TabIndex = 0;
            this.rabAutomationNone.TabStop = true;
            this.rabAutomationNone.Text = "Keine";
            this.rabAutomationNone.UseVisualStyleBackColor = true;
            this.rabAutomationNone.CheckedChanged += new System.EventHandler(this.rabAutomationNone_CheckedChanged);
            // 
            // ProjectSettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(729, 273);
            this.Controls.Add(this.grbAutomation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grbCommon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Projekteinstellungen";
            this.grbCommon.ResumeLayout(false);
            this.grbCommon.PerformLayout();
            this.grbAutomation.ResumeLayout(false);
            this.grbAutomation.PerformLayout();
            this.grbAutomationSettings.ResumeLayout(false);
            this.grbAutomationSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaitTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProjectCleanUp;
        private System.Windows.Forms.GroupBox grbCommon;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grbAutomation;
        private System.Windows.Forms.RadioButton rabAutomationRestore;
        private System.Windows.Forms.GroupBox grbAutomationSettings;
        private System.Windows.Forms.RadioButton rabAutomationBackup;
        private System.Windows.Forms.RadioButton rabAutomationNone;
        private System.Windows.Forms.Label lblWaitTime2;
        private System.Windows.Forms.Label lblWaitTime;
        private System.Windows.Forms.NumericUpDown nudWaitTime;
        private System.Windows.Forms.Label lblDefaultTab;
        private System.Windows.Forms.ComboBox cboDefaultTab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboFinishAction;
    }
}