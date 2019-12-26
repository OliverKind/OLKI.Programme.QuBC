using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO: DELTE FILE
namespace OLKI.Programme.QBC
{
    public partial class frmSettingsTestForm : Form
    {

        private readonly OLKI.Programme.QBC.BackupProject.Settings.Settings _settings = null;
        public frmSettingsTestForm(OLKI.Programme.QBC.BackupProject.Settings.Settings settings)
        {
            this._settings = settings;

            InitializeComponent();
            this.propertyGrid1.SelectedObject = this._settings;
            this.propertyGrid2.SelectedObject = this._settings.Common;
            this.propertyGrid3.SelectedObject = this._settings.Common.ExisitingFiles;
            this.propertyGrid4.SelectedObject = this._settings.ControleBackup;
            this.propertyGrid5.SelectedObject = this._settings.ControleBackup.Action;
            this.propertyGrid6.SelectedObject = this._settings.ControleBackup.Directory;
            this.propertyGrid7.SelectedObject = this._settings.ControleBackup.Logfile;

            this._settings.SettingsChanged += new EventHandler(this.Settings_Changed);
        }

        private void chkRestrainChangedEvent_CheckedChanged(object sender, EventArgs e)
        {
            this._settings.RestrainChangedEvent = this.chkRestrainChangedEvent.Checked;
            this.propertyGrid1.Refresh();
        }

        private void PropertyGridX_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.BtnRefreshProerpygrids_Click(s, new EventArgs());
        }

        private void Settings_Changed(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString());
            this.txtEventLog.Text = sender.ToString() + "\n" + this.txtEventLog.Text;
        }

        private void BtnRefreshProerpygrids_Click(object sender, EventArgs e)
        {
            this.propertyGrid1.Refresh();
            this.propertyGrid2.Refresh();
            this.propertyGrid3.Refresh();
            this.propertyGrid4.Refresh();
            this.propertyGrid5.Refresh();
            this.propertyGrid6.Refresh();
            this.propertyGrid7.Refresh();
        }

        private void BtnResetChanged_Click(object sender, EventArgs e)
        {
            
            this._settings.Changed = false;
            this._settings.Common.Changed = false;
            this._settings.Common.ExisitingFiles.Changed = false;
            this._settings.ControleBackup.Changed = false;
            this._settings.ControleBackup.Action.Changed = false;
            this._settings.ControleBackup.Directory.Changed = false;
            this._settings.ControleBackup.Changed = false;
            this._settings.ControleBackup.Logfile.Changed = false;

            this.BtnRefreshProerpygrids_Click(sender, e);
        }
    }
}