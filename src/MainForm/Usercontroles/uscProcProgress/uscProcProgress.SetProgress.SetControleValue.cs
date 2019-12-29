/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
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

using OLKI.Programme.QBC.BackupProject.Process;
using OLKI.Tools.CommonTools.DirectoryAndFile;
using System;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.MainForm.Usercontroles.uscProgress
{
    public partial class ProcProgress
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
                private readonly ProcProgress _progressControle = null;
                #endregion

                #region Functions
                /// <summary>
                /// Inital a new class to the values to controles
                /// </summary>
                /// <param name="parent"></param>
                public SetControleValue(ProcProgress parent)
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
                    System.Diagnostics.Debug.Print("uscProcess::InitialControles::START");
                    this.Invoke_Label_Text(this._progressControle.lblStepText, "");

                    this.Invoke_TextBox_Text(this._progressControle.txtCopyStart, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtCopyElapsed, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtCopyRemainTime, "");

                    this.ResetAllTextBoxes();
                    this.ResetAllProgressBars(true, true);
                    System.Diagnostics.Debug.Print("uscProcess::InitialControles::FINISH");
                }

                /// <summary>
                /// Set all textboxes to blank
                /// </summary>
                private void ResetAllTextBoxes()
                {
                    System.Diagnostics.Debug.Print("    uscProcess::ResetProgressTextBoxes::START");
                    this.Invoke_TextBox_Text(this._progressControle.txtActualDir, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtActualDirFilesNum, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtActualDirFilesPer, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtActualFile, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtActualFileByteNum, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtActualFileBytePer, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtAllByteNum, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtAllBytePer, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtAllDirNum, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtAllDirPer, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtAllItemsNum, "");
                    this.Invoke_TextBox_Text(this._progressControle.txtAllItemsPer, "");
                    System.Diagnostics.Debug.Print("    uscProcess::ResetProgressTextBoxes::FINISH");
                }

                /// <summary>
                /// Set ProgressBars to blockstyle an zero if requested
                /// </summary>
                /// <param name="setBlockStyle">Set ProgressBars blockstyle if true</param>
                /// <param name="setProgressToZero">Set ProgressBar values to zero if true</param>
                public void ResetAllProgressBars(bool setBlockStyle, bool setProgressToZero)
                {
                    System.Diagnostics.Debug.Print("    uscProcess::ResetProgressBars::START");
                    if (setBlockStyle)
                    {
                        this.Invoke_ProgressBar_Style(this._progressControle.pbaActualDirFiles, ProgressBarStyle.Blocks);
                        this.Invoke_ProgressBar_Style(this._progressControle.pbaActualFileByte, ProgressBarStyle.Blocks);
                        this.Invoke_ProgressBar_Style(this._progressControle.pbaAllByte, ProgressBarStyle.Blocks);
                        this.Invoke_ProgressBar_Style(this._progressControle.pbaAllDir, ProgressBarStyle.Blocks);
                        this.Invoke_ProgressBar_Style(this._progressControle.pbaAllItems, ProgressBarStyle.Blocks);
                    }

                    if (setProgressToZero)
                    {
                        this.Invoke_ProgressBar_Value(this._progressControle.pbaActualDirFiles, 0);
                        this.Invoke_ProgressBar_Value(this._progressControle.pbaActualFileByte, 0);
                        this.Invoke_ProgressBar_Value(this._progressControle.pbaAllByte, 0);
                        this.Invoke_ProgressBar_Value(this._progressControle.pbaAllDir, 0);
                        this.Invoke_ProgressBar_Value(this._progressControle.pbaAllItems, 0);
                    }
                    System.Diagnostics.Debug.Print("    uscProcess::ResetProgressBars::FINISH");
                }
                #endregion

                #region Set controoles, invoke Items if approriated
                /// <summary>
                /// Set alle defined controles to the value, given by progress
                /// </summary>
                /// <param name="progressBar">ProgressBar controle to set</param>
                /// <param name="itemTextBox">TextBox controle to set, with the item name</param>
                /// <param name="percentageTextBox">TextBox controle to set, with the percentage progress</param>
                /// <param name="valueTextBox">TextBox  controle to set, with the numeric value</param>
                /// <param name="dimensionComboBox">ComboBox with the dimension for value TextBox</param>
                /// <param name="progressElement">The progress of the process</param>
                public void SetProgressCluster(ProgressBar progressBar, TextBox itemTextBox, TextBox percentageTextBox, TextBox valueTextBox, ComboBox dimensionComboBox, ProgressStore.ProgressElement progressElement)
                {
                    this.SetItemTextBox(itemTextBox, progressElement);
                    this.SetProgressbar(progressBar, progressElement);
                    this.SetPercentateTextBox(percentageTextBox, progressElement);
                    this.SetValueTextBox(valueTextBox, dimensionComboBox, progressElement);
                }

                /// <summary>
                /// Set item name TextBox value if TextBox is defined
                /// </summary>
                /// <param name="itemTextBox">TextBox controle to set the item name</param>
                /// <param name="progressElement">The progress of the process</param>
                private void SetItemTextBox(TextBox itemTextBox, ProgressStore.ProgressElement progressElement)
                {
                    if (itemTextBox == null) return;
                    this.Invoke_TextBox_Text(itemTextBox, progressElement.ElemenName);
                }

                /// <summary>
                /// Set the ProgressBar to progress if ProgressBar is defined
                /// </summary>
                /// <param name="progressBar">ProgressBar controle to set</param>
                /// <param name="progressElement">The progress of the process</param>
                private void SetProgressbar(ProgressBar progressBar, ProgressStore.ProgressElement progressElement)
                {
                    if (progressBar == null) return;

                    if (progressElement.ActualValue == null || progressElement.MaxValue == null)
                    {
                        // If no vlaues are set clear and return
                        this.Invoke_ProgressBar_Value(progressBar, 0);
                        return;
                    }

                    //Set new Value
                    this.Invoke_ProgressBar_Value(progressBar, progressElement.Percentage);
                }

                /// <summary>
                /// Set progress in percentage to TextBox value if TextBox is defined
                /// </summary>
                /// <param name="percentageTextBox">TextBox controle to set progress percentage</param>
                /// <param name="progressElement">The progress of the process</param>
                private void SetPercentateTextBox(TextBox percentageTextBox, ProgressStore.ProgressElement progressElement)
                {
                    if (percentageTextBox == null) return;

                    if (progressElement.ActualValue == null || progressElement.MaxValue == null)
                    {
                        // If no vlaues are set clear and return
                        this.Invoke_TextBox_Text(percentageTextBox, "");
                        return;
                    }
                    //Set new Value
                    this.Invoke_TextBox_Text(percentageTextBox, string.Format(FORMAT_PERCENTAGE, new object[] { progressElement.Percentage }));
                }

                /// <summary>
                /// Set progress value to TextBox value if TextBox is defined. If dimension ComboBox is defined, change the dimension. Otherwiese use original value
                /// </summary>
                /// <param name="valueTextBox">TextBox controle to set progress value</param>
                /// <param name="dimensionComboBox">ComboBox to change the value dimension, if not defined use the original value</param>
                /// <param name="progressElement">The progress of the process</param>
                private void SetValueTextBox(TextBox valueTextBox, ComboBox dimensionComboBox, ProgressStore.ProgressElement progressElement)
                {
                    if (valueTextBox == null) return;

                    if (progressElement.ActualValue == null && progressElement.MaxValue == null)
                    {
                        // If no vlaues are set clear and return
                        valueTextBox.Text = "";
                        return;
                    }
                    else if (progressElement.ActualValue != null && progressElement.MaxValue != null)
                    {
                        // If bose values are set, set double entry
                        valueTextBox.Text = string.Format(FORMAT_ACTUAL_MAX_VALUE, new object[] { string.Format(FORMAT_VALUE, this.GetDimensioniedValue((long)progressElement.ActualValue, dimensionComboBox)), string.Format(FORMAT_VALUE, this.GetDimensioniedValue((long)progressElement.MaxValue, dimensionComboBox)) });
                    }
                    else if (progressElement.ActualValue != null && progressElement.MaxValue == null)
                    {
                        // Set single value
                        valueTextBox.Text = string.Format(FORMAT_VALUE, this.GetDimensioniedValue((long)progressElement.ActualValue, dimensionComboBox));
                    }
                    else if (progressElement.ActualValue == null && progressElement.MaxValue != null)
                    {   //Whell, nothing other is posible any more. Use this if-Request to have a uniformly syntax
                        // Set single value
                        valueTextBox.Text = string.Format(FORMAT_VALUE, this.GetDimensioniedValue((long)progressElement.MaxValue, dimensionComboBox));
                    }
                }

                /// <summary>
                /// Get the the new value, if the dimension is set
                /// </summary>
                /// <param name="value">Original value</param>
                /// <param name="dimensionComboBox">ComboBox to change the value dimension</param>
                /// <returns>The the new value, if the dimension is set</returns>
                private decimal GetDimensioniedValue(long value, ComboBox dimensionComboBox)
                {
                    // If there is no specific dimension, return original value
                    if (dimensionComboBox == null) return (decimal)value;

                    //Get Number format
                    FileSize.ByteBase ByteBase;
                    FileSize.Dimension Dimension;
                    if (dimensionComboBox.SelectedIndex < FileSize.UnitPrefix_IEC.Length)
                    {
                        ByteBase = FileSize.ByteBase.IEC;
                        Dimension = (FileSize.Dimension)dimensionComboBox.SelectedIndex;
                    }
                    else
                    {
                        ByteBase = FileSize.ByteBase.SI;
                        Dimension = (FileSize.Dimension)dimensionComboBox.SelectedIndex - FileSize.UnitPrefix_IEC.Length;
                    }
                    //TODO: Not perfect, convert from longt, to stirng, to decimal. Fix it in FileSize.Convert library.
                    return Convert.ToDecimal(FileSize.Convert(value, 2, ByteBase, Dimension, true));
                }
                #endregion
                #endregion
                #endregion
            }
        }
    }
}