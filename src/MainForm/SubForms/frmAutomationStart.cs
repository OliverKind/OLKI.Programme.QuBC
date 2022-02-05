/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * The AutomationStart Formn, to wait until the Automation starts
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

using System;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.src.MainForm.SubForms
{
    /// <summary>
    /// The AutomationStart Formn, to wait until the Automation starts
    /// </summary>
    public partial class AutomationStart : Form
    {
        #region Fields
        /// <summary>
        /// Application MainForm
        /// </summary>
        private readonly MainForm _mainForm;

        /// <summary>
        /// Project common settings, to get automation settings
        /// </summary>
        readonly Project.Settings.Common.Common _settings;

        /// <summary>
        /// Timer to start the automation
        /// </summary>
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// Count the timer Ticks, to start the automation
        /// </summary>
        private int _tickCounter = 0;
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new AutomationStart Form
        /// </summary>
        /// <param name="mainForm">Application MainForm</param>
        /// <param name="projectCommonSettings">Project common settings, to get automation settings</param>
        public AutomationStart(MainForm mainForm, Project.Settings.Common.Common projectCommonSettings)
        {
            InitializeComponent();

            this._mainForm = mainForm;
            this._settings = projectCommonSettings;

            this.progressBar1.Maximum = this._settings.AutomationWaitTime * 10;
            this._timer.Tick += new EventHandler(this.timer_Tick);


            switch (this._settings.Automation)
            {
                case Project.Settings.Common.Common.AutomationMode.Backup:
                    this.Text = string.Format(this.Text, Properties.Stringtable._0x002C);
                    this._mainForm.tabControlMain.SelectTab(1);
                    this._timer.Start();
                    break;
                case Project.Settings.Common.Common.AutomationMode.Restore:
                    this.Text = string.Format(this.Text, Properties.Stringtable._0x002C);
                    this._mainForm.tabControlMain.SelectTab(2);
                    this._timer.Start();
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this._tickCounter++;
            if (this._tickCounter <= this._settings.AutomationWaitTime * 10) this.progressBar1.Value = this._tickCounter;
            if (this._tickCounter == this._settings.AutomationWaitTime * 10) this.btnStart_Click(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._timer.Stop();
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this._timer.Stop();
            this.Close();

            switch (this._settings.Automation)
            {
                case Project.Settings.Common.Common.AutomationMode.Backup:
                    this._mainForm.uscTaskControleBackup.btnTaskStart_Click(sender, e, true);
                    break;
                case Project.Settings.Common.Common.AutomationMode.Restore:
                    this._mainForm.uscTaskControleRestore.btnTaskStart_Click(sender, e, true);
                    break;
                default:
                    this.Close();
                    break;
            }
        }
        #endregion
    }
}