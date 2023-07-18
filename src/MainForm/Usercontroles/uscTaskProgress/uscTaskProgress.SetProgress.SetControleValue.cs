/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Set the progress elements in progress controle
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

using OLKI.Programme.QuBC.src.Project.Task;
using OLKI.Toolbox.Widgets;
using OLKI.Toolbox.Widgets.Invoke;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscProgress
{
    public partial class TaskProgress
    {
        public partial class SetProgress
        {
            /// <summary>
            /// Class to set the values for controles. Set whole cluster of controles. Set by invoke if required
            /// </summary>
            public partial class SetControleValue
            {
                #region Fields
                /// <summary>
                /// Parent progress controle object
                /// </summary>
                private readonly TaskProgress _progressControle = null;
                #endregion

                #region Functions
                /// <summary>
                /// Inital a new class to the values to controles
                /// </summary>
                /// <param name="parent"></param>
                public SetControleValue(TaskProgress parent)
                {
                    this._progressControle = parent;
                }

                #region Set progresses controles
                #region Initial Controles
                /// <summary>
                /// Initial controles. Reset TextBoxes and ProgressBars
                /// </summary>
                public void InitialControles()
                {
                    LabelInv.Text(this._progressControle.lblStepText, "");

                    TextBoxInv.Text(this._progressControle.txtCopyStart, "");
                    TextBoxInv.Text(this._progressControle.txtCopyElapsed, "");
                    TextBoxInv.Text(this._progressControle.txtCopyRemainTime, "");

                    this.ResetAllProgressBars(true, true);
                }

                /// <summary>
                /// Set ProgressBars to blockstyle an zero if requested
                /// </summary>
                /// <param name="setBlockStyle">Set ProgressBars blockstyle if true</param>
                /// <param name="clearProgressBars">Set ProgressBar values to zero if true</param>
                public void ResetAllProgressBars(bool setBlockStyle, bool clearProgressBars)
                {
                    ExtProgrBarInv.DescriptionText(this._progressControle.expActualObject, "");

                    if (setBlockStyle)
                    {
                        ExtProgrBarInv.Style(this._progressControle.expAllByte, ProgressBarStyle.Blocks);
                        ExtProgrBarInv.Style(this._progressControle.expAllItems, ProgressBarStyle.Blocks);
                        ExtProgrBarInv.Style(this._progressControle.expActualObject, ProgressBarStyle.Blocks);
                    }

                    if (clearProgressBars)
                    {
                        ExtProgrBarInv.Value(this._progressControle.expAllByte, null);
                        ExtProgrBarInv.Value(this._progressControle.expAllItems, null);
                        ExtProgrBarInv.Value(this._progressControle.expActualObject, null);
                    }
                }
                #endregion

                #region Set controoles, invoke Items if approriated
                /// <summary>
                /// Set the values to the extendes ProgressBar, given by progress
                /// </summary>
                /// <param name="progressBar">ProgressBar controle to set</param>
                /// <param name="progressElement">The progress of the process</param>
                public void SetProgressCluster(ExtProgressBar progressBar, ProgressStore.ProgressElement progressElement)
                {
                    ExtProgrBarInv.DescriptionText(progressBar, progressElement.ElemenName);
                    if (progressElement.ActualValue == null)    //Workaround because null will blank the numeric TextBoxes
                    {
                        ExtProgrBarInv.Value(progressBar, progressElement.MaxValue);
                    }
                    else
                    {
                        ExtProgrBarInv.MaxValue(progressBar, progressElement.MaxValue);
                        ExtProgrBarInv.Value(progressBar, progressElement.ActualValue);
                    }
                }
                #endregion
                #endregion
                #endregion
            }
        }
    }
}