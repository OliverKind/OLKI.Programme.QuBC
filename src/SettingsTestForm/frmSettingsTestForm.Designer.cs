namespace OLKI.Programme.QBC
{
    partial class frmSettingsTestForm
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRestrainChangedEvent = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.propertyGrid7 = new System.Windows.Forms.PropertyGrid();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.propertyGrid6 = new System.Windows.Forms.PropertyGrid();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.propertyGrid5 = new System.Windows.Forms.PropertyGrid();
            this.propertyGrid4 = new System.Windows.Forms.PropertyGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.propertyGrid3 = new System.Windows.Forms.PropertyGrid();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.btnRefreshProerpygrids = new System.Windows.Forms.Button();
            this.btnResetChanged = new System.Windows.Forms.Button();
            this.txtEventLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(312, 296);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridX_PropertyValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEventLog);
            this.groupBox1.Controls.Add(this.btnResetChanged);
            this.groupBox1.Controls.Add(this.btnRefreshProerpygrids);
            this.groupBox1.Controls.Add(this.chkRestrainChangedEvent);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.propertyGrid1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1013, 965);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // chkRestrainChangedEvent
            // 
            this.chkRestrainChangedEvent.AutoSize = true;
            this.chkRestrainChangedEvent.Location = new System.Drawing.Point(6, 321);
            this.chkRestrainChangedEvent.Name = "chkRestrainChangedEvent";
            this.chkRestrainChangedEvent.Size = new System.Drawing.Size(136, 17);
            this.chkRestrainChangedEvent.TabIndex = 3;
            this.chkRestrainChangedEvent.Text = "RestrainChangedEvent";
            this.chkRestrainChangedEvent.UseVisualStyleBackColor = true;
            this.chkRestrainChangedEvent.CheckedChanged += new System.EventHandler(this.chkRestrainChangedEvent_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.propertyGrid4);
            this.groupBox4.Location = new System.Drawing.Point(324, 265);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(673, 688);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Controle - Backup";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.propertyGrid7);
            this.groupBox7.Location = new System.Drawing.Point(324, 461);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(328, 220);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Logfile";
            // 
            // propertyGrid7
            // 
            this.propertyGrid7.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid7.Name = "propertyGrid7";
            this.propertyGrid7.Size = new System.Drawing.Size(312, 190);
            this.propertyGrid7.TabIndex = 0;
            this.propertyGrid7.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridX_PropertyValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.propertyGrid6);
            this.groupBox6.Location = new System.Drawing.Point(324, 235);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(328, 220);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Directory";
            // 
            // propertyGrid6
            // 
            this.propertyGrid6.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid6.Name = "propertyGrid6";
            this.propertyGrid6.Size = new System.Drawing.Size(312, 190);
            this.propertyGrid6.TabIndex = 0;
            this.propertyGrid6.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridX_PropertyValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.propertyGrid5);
            this.groupBox5.Location = new System.Drawing.Point(324, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(328, 210);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Action";
            // 
            // propertyGrid5
            // 
            this.propertyGrid5.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid5.Name = "propertyGrid5";
            this.propertyGrid5.Size = new System.Drawing.Size(312, 180);
            this.propertyGrid5.TabIndex = 0;
            this.propertyGrid5.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridX_PropertyValueChanged);
            // 
            // propertyGrid4
            // 
            this.propertyGrid4.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid4.Name = "propertyGrid4";
            this.propertyGrid4.Size = new System.Drawing.Size(312, 210);
            this.propertyGrid4.TabIndex = 0;
            this.propertyGrid4.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridX_PropertyValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.propertyGrid2);
            this.groupBox2.Location = new System.Drawing.Point(324, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(673, 240);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Common";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.propertyGrid3);
            this.groupBox3.Location = new System.Drawing.Point(324, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 210);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ExisitingFile";
            // 
            // propertyGrid3
            // 
            this.propertyGrid3.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid3.Name = "propertyGrid3";
            this.propertyGrid3.Size = new System.Drawing.Size(312, 180);
            this.propertyGrid3.TabIndex = 0;
            this.propertyGrid3.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridX_PropertyValueChanged);
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.Size = new System.Drawing.Size(312, 180);
            this.propertyGrid2.TabIndex = 0;
            this.propertyGrid2.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridX_PropertyValueChanged);
            // 
            // btnRefreshProerpygrids
            // 
            this.btnRefreshProerpygrids.Location = new System.Drawing.Point(6, 373);
            this.btnRefreshProerpygrids.Name = "btnRefreshProerpygrids";
            this.btnRefreshProerpygrids.Size = new System.Drawing.Size(312, 32);
            this.btnRefreshProerpygrids.TabIndex = 5;
            this.btnRefreshProerpygrids.Text = "RefreshProerpygrids";
            this.btnRefreshProerpygrids.UseVisualStyleBackColor = true;
            this.btnRefreshProerpygrids.Click += new System.EventHandler(this.BtnRefreshProerpygrids_Click);
            // 
            // btnResetChanged
            // 
            this.btnResetChanged.Location = new System.Drawing.Point(6, 462);
            this.btnResetChanged.Name = "btnResetChanged";
            this.btnResetChanged.Size = new System.Drawing.Size(312, 32);
            this.btnResetChanged.TabIndex = 6;
            this.btnResetChanged.Text = "ResetChanged";
            this.btnResetChanged.UseVisualStyleBackColor = true;
            this.btnResetChanged.Click += new System.EventHandler(this.BtnResetChanged_Click);
            // 
            // txtEventLog
            // 
            this.txtEventLog.Location = new System.Drawing.Point(6, 500);
            this.txtEventLog.Multiline = true;
            this.txtEventLog.Name = "txtEventLog";
            this.txtEventLog.ReadOnly = true;
            this.txtEventLog.Size = new System.Drawing.Size(312, 453);
            this.txtEventLog.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 984);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRestrainChangedEvent;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid propertyGrid3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PropertyGrid propertyGrid5;
        private System.Windows.Forms.PropertyGrid propertyGrid4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PropertyGrid propertyGrid7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.PropertyGrid propertyGrid6;
        private System.Windows.Forms.Button btnResetChanged;
        private System.Windows.Forms.Button btnRefreshProerpygrids;
        private System.Windows.Forms.TextBox txtEventLog;
    }
}

