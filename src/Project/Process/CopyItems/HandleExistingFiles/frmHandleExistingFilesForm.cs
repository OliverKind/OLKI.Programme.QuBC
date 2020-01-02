using OLKI.Programme.QBC.src.BackupProject.Process.CopyItems.HandleExistingFiles;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.BackupProject.Process
{
    /// <summary>
    /// A Form to set the default action for handling existing directories and files or how to handle an specific existing directory or file
    /// </summary>
    internal partial class HandleExistingFilesForm : Form
    {
        #region Constants
        /// <summary>
        /// Praefix to identify action radiobuttons to sett action for existing files
        /// </summary>
        private const string ACTION_RADIOBUTTON_PRAEFIX = "rabAction_";
        /// <summary>
        /// Default selected index of cboAction_AddText_Template
        /// </summary>
        private const int ADD_TEXT_DEFAULT_COMBOBOX_INDEX = 1;
        #endregion

        #region Enums
        /// <summary>
        /// An Enumeration ths specifies the mode of the form
        /// </summary>
        internal enum FormMode
        {
            /// <summary>
            /// The form mode is to set the default handling of existing directories and files
            /// </summary>
            DefaultSettings,
            /// <summary>
            /// Thte form mode is to handle an specifig existing directory or file
            /// </summary>
            FileExists
        }
        #endregion

        #region Properties
        /// <summary>
        /// Action to do with exisiting files
        /// </summary>
        private HandleExistingFiles.HowToHandleExistingItem _actionHandleExistingFiles = HandleExistingFiles.HowToHandleExistingItem.AskAnyTime;
        /// <summary>
        /// Get the action how to handle existing files, choose in this Form
        /// </summary>
        internal HandleExistingFiles.HowToHandleExistingItem ActionHandleExistingFiles
        {
            get
            {
                return this._actionHandleExistingFiles;
            }
        }

        /// <summary>
        /// Get if the coosen action for handle existing files should been taken for the folowing files
        /// </summary>
        internal bool RememberActionHandleExistingFiles
        {
            get
            {
                return this.chkForAllFollowing.Checked;
            }
        }

        /// <summary>
        /// The text to add to an existing file, if the option is selected
        /// </summary>
        private string _actionAddTextText = string.Empty;
        /// <summary>
        /// Get the text to add to an existing file, if the option is selected
        /// </summary>
        internal string ActionAddTextText
        {
            get
            {
                return this._actionAddTextText;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new overwrite options Form
        /// </summary>
        /// <param name="formMode">Specifies the mode of the form</param>
        /// <param name="sourceFile">File info of source file or null</param>
        /// <param name="targetFile">File info of destination file or null</param>
        /// <param name="defaultAction">Initial value for action to handle existing files</param>
        internal HandleExistingFilesForm(FormMode formMode, FileInfo sourceFile, FileInfo targetFile, HandleExistingFiles.HowToHandleExistingItem defaultAction, string defaultAddText, bool defaultRememberAction)
        {
            this._actionHandleExistingFiles = defaultAction;

            InitializeComponent();

            this.cboAction_AddText_Template.SelectedIndex = ADD_TEXT_DEFAULT_COMBOBOX_INDEX;
            OLKI.Widgets.Tools.ComboBox_AutoDropDownWidth(this.cboAction_AddText_Template);
            // Set form mode specific options
            switch (formMode)
            {
                case FormMode.DefaultSettings:
                    this.grbDestinationProperty.Visible = false;
                    this.grbSourceProperty.Visible = false;
                    this.lblActionDefaultSettings.Visible = true;
                    this.lblActionDoubleFile.Visible = true;
                    this.rabAction_AskAnyTime.Enabled = true;

                    // Move Controles, because the conrtoles on the left site are not visible
                    int PositionDisplacement = this.grbAction.Location.X - this.grbSourceProperty.Location.X;
                    this.Width -= PositionDisplacement;
                    this.btnCancel.Location = new Point(this.btnCancel.Location.X - PositionDisplacement, this.btnCancel.Location.Y);
                    this.btnOk.Location = new Point(this.btnOk.Location.X - PositionDisplacement, this.btnOk.Location.Y);
                    this.chkForAllFollowing.Location = new Point(this.chkForAllFollowing.Location.X - PositionDisplacement, this.chkForAllFollowing.Location.Y);
                    this.grbAction.Location = new Point(this.grbAction.Location.X - PositionDisplacement, this.grbAction.Location.Y);
                    this.lblActionDefaultSettings.Location = new Point(this.lblActionDefaultSettings.Location.X - PositionDisplacement, this.lblActionDefaultSettings.Location.Y);
                    break;
                case FormMode.FileExists:
                    this.lblActionDefaultSettings.Visible = false;
                    this.lblActionDoubleFile.Visible = true;
                    this.prgSourceProperty.SelectedObject = sourceFile;
                    this.prgDestinationProperty.SelectedObject = targetFile;
                    this.rabAction_AskAnyTime.Enabled = false;
                    break;
                default:
                    break;
            }
            // Set action from settings
            // Use the name of the radiobutton and the name of the enum to find the selected item
            foreach (Control Control in this.grbAction.Controls)
            {
                if (Control is RadioButton && Control.Name == ACTION_RADIOBUTTON_PRAEFIX + defaultAction.ToString())
                {
                    ((RadioButton)Control).Checked = true;
                    break;
                }
            }

            this.chkForAllFollowing.Checked = defaultRememberAction;
            this.chkForAllFollowing.Enabled = !this.rabAction_AskAnyTime.Enabled;
            this.chkForAllFollowing.Visible = this.chkForAllFollowing.Enabled;
            this.txtAction_AddText_Text.Text = defaultAddText;

            this.rabActionXXX_CheckedChanged(this, new EventArgs());
        }

        /// <summary>
        /// Return the text of the radio button controle, associated with an specified HandleExistingItem Enumeration item
        /// </summary>
        /// <param name="enumToText">Specifies the Enumeration to associated with the radiobutton to return the text from it</param>
        /// <returns>The text of the radio button controle, associated with the specified HandleExistingItem Enumeration item</returns>
        internal string GetActionAsText(HandleExistingFiles.HowToHandleExistingItem enumToText)
        {
            foreach (Control Control in this.grbAction.Controls)
            {
                if (Control is RadioButton && Control.Name == ACTION_RADIOBUTTON_PRAEFIX + enumToText.ToString())
                {
                    return ((RadioButton)Control).Text;
                }
            }
            return string.Empty;
        }

        #region Form User Events
        private void btnAction_AddText_Transfair_Click(object sender, EventArgs e)
        {
            String[] AttTextSubstrings = this.cboAction_AddText_Template.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None);
            this.txtAction_AddText_Text.Text += AttTextSubstrings[1];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.rabAction_AddText.Checked && string.IsNullOrEmpty(this.txtAction_AddText_Text.Text))
            {
                
                MessageBox.Show(Stringtable._0x0001m, Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void rabActionXXX_CheckedChanged(object sender, EventArgs e)
        {
            // Check for checked Items
            bool OptionIsChecked = false;
            foreach (Control Control in this.grbAction.Controls)
            {
                if (Control is RadioButton && ((RadioButton)Control).Enabled && ((RadioButton)Control).Checked)
                {
                    this._actionHandleExistingFiles = (HandleExistingFiles.HowToHandleExistingItem)Enum.Parse(typeof(HandleExistingFiles.HowToHandleExistingItem), Control.Name.Replace(ACTION_RADIOBUTTON_PRAEFIX, string.Empty));

                    // Set an checked option
                    OptionIsChecked = true;
                    break;
                }
            }
            this.btnOk.Enabled = OptionIsChecked;

            // Set AddText controles
            if (this.rabAction_AddText.Checked)
            {
                this.txtAction_AddText_Text.Enabled = true;
                this.btnAction_AddText_Transfair.Enabled = true;
                this.cboAction_AddText_Template.Enabled = true;
            }
            else
            {
                this.txtAction_AddText_Text.Enabled = false;
                this.btnAction_AddText_Transfair.Enabled = false;
                this.cboAction_AddText_Template.Enabled = false;
            }

        }

        private void txtAction_AddText_Text_TextChanged(object sender, EventArgs e)
        {
            this._actionAddTextText = this.txtAction_AddText_Text.Text;
        }
        #endregion
        #endregion
    }
}