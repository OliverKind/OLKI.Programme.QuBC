using OLKI.Programme.QuBC.src.Project.Settings.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OLKI.Programme.QuBC.src.MainForm.SubForms
{
    public partial class ProjectSettingsForm : Form
    {
        #region Properties
        /// <summary>
        /// Get the Apllication MainForm.
        /// </summary>
        public Form ApplicationMainForm { get; private set; }
        #endregion

        #region Events
        /// <summary>
        /// Thrown if the a project cleanup is requested
        /// </summary>
        internal event EventHandler RequestProjectCelanUp;
        #endregion

        #region Fields
        /// <summary>
        /// Project Settings
        /// </summary>
        Project.Settings.Settings _projectSettings;
        #endregion

        #region Methodes
        public ProjectSettingsForm(Project.Settings.Settings projectSettings, Form applicationMainForm)
        {
            InitializeComponent();

            this.ApplicationMainForm = applicationMainForm;
            this._projectSettings = projectSettings;

            Toolbox.Widgets.Tools.ComboBox.AutoDropDownWidth(this.cboDefaultTab);
            Toolbox.Widgets.Tools.ComboBox.AutoDropDownWidth(this.cboFinishAction);

            this.cboDefaultTab.SelectedIndex = this._projectSettings.Common.DefaultTab + 1;
            this.cboFinishAction.SelectedIndex = (int)this._projectSettings.Common.AutomationFinishAction;
            this.nudWaitTime.Value = this._projectSettings.Common.AutomationWaitTime;

            switch (this._projectSettings.Common.Automation)
            {
                case Common.AutomationMode.None:
                    this.rabAutomationNone.Checked = true;
                    break;
                case Common.AutomationMode.Backup:
                    this.rabAutomationBackup.Checked = true;
                    break;
                case Common.AutomationMode.Restore:
                    this.rabAutomationRestore.Checked = true;
                    break;
                default:
                    this.rabAutomationNone.Checked = true;
                    throw new ArgumentException();
            }
        }

        private void rabAutomationBackup_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) this.grbAutomationSettings.Enabled = true;

        }

        private void rabAutomationNone_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) this.grbAutomationSettings.Enabled = false;
        }

        private void rabAutomationRestore_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) this.grbAutomationSettings.Enabled = true;
        }

        #region FormEvents
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.rabAutomationBackup.Checked) this._projectSettings.Common.Automation = Common.AutomationMode.Backup;
            if (this.rabAutomationNone.Checked) this._projectSettings.Common.Automation = Common.AutomationMode.None;
            if (this.rabAutomationRestore.Checked) this._projectSettings.Common.Automation = Common.AutomationMode.Restore;

            this._projectSettings.Common.AutomationFinishAction = (Common.FinishAction)this.cboFinishAction.SelectedIndex;
            this._projectSettings.Common.AutomationWaitTime = (int)this.nudWaitTime.Value;
            this._projectSettings.Common.DefaultTab = this.cboDefaultTab.SelectedIndex - 1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnProjectCleanUp_Click(object sender, EventArgs e)
        {
            this.RequestProjectCelanUp?.Invoke(this, new EventArgs());
        }
        #endregion
        #endregion
    }
}