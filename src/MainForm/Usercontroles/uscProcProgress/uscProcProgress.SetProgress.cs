/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Set the progress in progress controle
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
using OLKI.Programme.QBC.Properties;
using OLKI.Widgets.Invoke;
using System;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.MainForm.Usercontroles.uscProgress
{
    public partial class ProcProgress
    {
        public partial class SetProgress
        {
            #region Fields
            /// <summary>
            /// Parent progress controle object
            /// </summary>
            private readonly ProcProgress _progressControle = null;
            #endregion

            #region Properties
            /// <summary>
            /// Object to set the values for controles
            /// </summary>
            private readonly SetControleValue _setControleValue = null;
            /// <summary>
            /// Get the object to set the values for controles
            /// </summary>
            public SetControleValue SetControlesValue
            {
                get
                {
                    return this._setControleValue;
                }
            }
            #endregion

            #region Methodes
            /// <summary>
            /// Inital a new SetProgres object
            /// </summary>
            public SetProgress(ProcProgress parent)
            {
                this._progressControle = parent;
                this._setControleValue = new SetControleValue(parent);
            }

            #region Progress count items
            /// <summary>
            /// Set controles for state: CountStart
            /// </summary>
            internal void SetProgress_CountStart()
            {
                this._progressControle._progressStart = DateTime.Now;

                this._setControleValue.InitialControles();
                ListViewInv.ClearItems(this._progressControle._exceptionListView);
                TextBoxInv.Text(this._progressControle._exceptionCount, "0");
                TabPageInv.ImageIndex(this._progressControle._conclusionTabPage, -1);
                TextBoxInv.Text(this._progressControle._conclusionDirectoriesTextBox, "");
                TextBoxInv.Text(this._progressControle._conclusionFilesTextBox, "");
                TextBoxInv.Text(this._progressControle._conclusionDurationTextBox, "");


                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0015);
                TextBoxInv.Text(this._progressControle.txtCopyStart, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));
                ProgressBarInv.Style(this._progressControle.pbaAllByte, ProgressBarStyle.Marquee);
                ProgressBarInv.Style(this._progressControle.pbaAllDir, ProgressBarStyle.Marquee);
                ProgressBarInv.Style(this._progressControle.pbaAllItems, ProgressBarStyle.Marquee);
            }

            /// <summary>
            /// Set controles for state: CountBusy
            /// </summary>
            internal void SetPRogress_CountBusy()
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0016);
                TextBoxInv.Text(this._progressControle.txtCopyElapsed, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));

                this._setControleValue.SetProgressCluster(this._progressControle.pbaAllItems, null, this._progressControle.txtAllItemsPer, this._progressControle.txtAllItemsNum, null, this._progressControle.ProgressStore.TotalItems);
                this._setControleValue.SetProgressCluster(this._progressControle.pbaAllByte, null, this._progressControle.txtAllBytePer, this._progressControle.txtAllByteNum, this._progressControle.cboAllByteNum, this._progressControle.ProgressStore.TotalBytes);
                this._setControleValue.SetProgressCluster(this._progressControle.pbaAllDir, null, this._progressControle.txtAllDirPer, this._progressControle.txtAllDirNum, null, this._progressControle.ProgressStore.DirectroyFiles);
                this._setControleValue.SetProgressCluster(null, this._progressControle.txtActualDir, null, null, null, this._progressControle.ProgressStore.DirectroyFiles);
            }

            /// <summary>
            /// Set controles for state: CountFinish
            /// </summary>
            internal void SetProgress_CountFinish()
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0017);
                this._setControleValue.ResetAllProgressBars(true, false);
            }
            #endregion

            #region Progress copy items
            /// <summary>
            /// Set controles for state: CopyStart
            /// Also set progress start time
            /// </summary>
            internal void SetProgress_CopyStart()
            {
                DateTime StartTime = DateTime.Now;
                this._progressControle._progressStart = StartTime;
                // Set controles
                this._setControleValue.InitialControles();
                ListViewInv.ClearItems(this._progressControle._exceptionListView);
                TextBoxInv.Text(this._progressControle._exceptionCount, "0");
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0018);
                TabPageInv.ImageIndex(this._progressControle._conclusionTabPage, -1);
                TextBoxInv.Text(this._progressControle._conclusionDirectoriesTextBox, "");
                TextBoxInv.Text(this._progressControle._conclusionFilesTextBox, "");
                TextBoxInv.Text(this._progressControle._conclusionDurationTextBox, "");

                TextBoxInv.Text(this._progressControle.txtCopyStart, StartTime.ToString(FORMAT_TIME));
                if (this._progressControle.ProgressStore.TotalItems.MaxValue == null)
                {
                    ProgressBarInv.Style(this._progressControle.pbaAllByte, ProgressBarStyle.Marquee);
                    ProgressBarInv.Style(this._progressControle.pbaAllDir, ProgressBarStyle.Marquee);
                    ProgressBarInv.Style(this._progressControle.pbaAllItems, ProgressBarStyle.Marquee);
                }
            }

            /// <summary>
            /// Set controles for state: CopyBusy
            /// </summary>
            internal void SetProgress_CopyBusy()
            {
                System.Diagnostics.Debug.Print(this._progressControle.ProgressStore.DirectroyFiles.ElemenName);
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0019);
                TextBoxInv.Text(this._progressControle.txtCopyElapsed, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));
                TextBoxInv.Text(this._progressControle._conclusionDirectoriesTextBox, this._progressControle.ProgressStore.TotalDirectories.ActualValue.ToString());
                TextBoxInv.Text(this._progressControle._conclusionFilesTextBox, this._progressControle.ProgressStore.TotalFiles.ActualValue.ToString());
                TextBoxInv.Text(this._progressControle._conclusionDurationTextBox, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));

                this._setControleValue.SetProgressCluster(this._progressControle.pbaAllItems, null, this._progressControle.txtAllItemsPer, this._progressControle.txtAllItemsNum, null, this._progressControle.ProgressStore.TotalItems);
                this._setControleValue.SetProgressCluster(this._progressControle.pbaAllByte, null, this._progressControle.txtAllBytePer, this._progressControle.txtAllByteNum, this._progressControle.cboAllByteNum, this._progressControle.ProgressStore.TotalBytes);
                this._setControleValue.SetProgressCluster(this._progressControle.pbaAllDir, null, this._progressControle.txtAllDirPer, this._progressControle.txtAllDirNum, null, this._progressControle.ProgressStore.TotalDirectories);
                this._setControleValue.SetProgressCluster(this._progressControle.pbaActualDirFiles, this._progressControle.txtActualDir, this._progressControle.txtActualDirFilesPer, this._progressControle.txtActualDirFilesNum, null, this._progressControle.ProgressStore.DirectroyFiles);
                this._setControleValue.SetProgressCluster(this._progressControle.pbaActualFileByte, this._progressControle.txtActualFile, this._progressControle.txtActualFileBytePer, this._progressControle.txtActualFileByteNum, this._progressControle.cboActualFileByteNum, this._progressControle.ProgressStore.FileBytes);

                // Get remaining time if counting was done
                if (this._progressControle.ProgressStore.TotalItems.MaxValue != null && this._progressControle.ProgressStore.TotalItems.MaxValue > 0)
                {
                    TimeSpan RemainingTimeByte = Tools.CommonTools.Matehmatics.RemainingTime(this._progressControle.ElapsedTime, this._progressControle.ProgressStore.TotalBytes.ActualValue, this._progressControle.ProgressStore.TotalBytes.MaxValue);
                    TimeSpan RemainingTimeItem = Tools.CommonTools.Matehmatics.RemainingTime(this._progressControle.ElapsedTime, this._progressControle.ProgressStore.TotalItems.ActualValue, this._progressControle.ProgressStore.TotalItems.MaxValue);
                    TimeSpan RemainingTime = RemainingTimeByte > RemainingTimeItem ? RemainingTimeByte : RemainingTimeItem;
                    if (RemainingTime.Days > 0)
                    {
                        TextBoxInv.Text(this._progressControle.txtCopyRemainTime, RemainingTime.ToString(Properties.Settings.Default.Copy_RemainTimeDays));
                    }
                    else
                    {
                        TextBoxInv.Text(this._progressControle.txtCopyRemainTime, RemainingTime.ToString(Properties.Settings.Default.Copy_RemainTimeNoDays));
                    }
                }
            }

            /// <summary>
            /// Set controles for state: CopyFinish
            /// </summary>
            internal void SetProgress_CopyFinish()
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x001A);
                TextBoxInv.Text(this._progressControle.txtCopyStart, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));
                TextBoxInv.Text(this._progressControle.txtCopyElapsed, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));

                TextBoxInv.Text(this._progressControle._conclusionDirectoriesTextBox, this._progressControle.ProgressStore.TotalDirectories.ActualValue.ToString());
                TextBoxInv.Text(this._progressControle._conclusionFilesTextBox, this._progressControle.ProgressStore.TotalFiles.ActualValue.ToString());
                TextBoxInv.Text(this._progressControle._conclusionDurationTextBox, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));
            }
            #endregion

            #region Progress Cancel, Exception
            /// <summary>
            /// Set controles for state: Cancel
            /// </summary>
            internal void SetProgress_Cancel()
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x001B);
                ProgressBarInv.Style(this._progressControle.pbaAllItems, ProgressBarStyle.Blocks);
                ProgressBarInv.Style(this._progressControle.pbaAllByte, ProgressBarStyle.Blocks);
                ProgressBarInv.Style(this._progressControle.pbaAllDir, ProgressBarStyle.Blocks);
            }

            /// <summary>
            /// Set controles for state: Exception
            /// Add exception to ListView
            /// </summary>
            internal void SetProgress_Exception()
            {
                ProcessException Exception = this._progressControle.ProgressStore.Exception;
                string ExceptionText = string.Empty;

                if (!string.IsNullOrEmpty(Exception.Description) && string.IsNullOrEmpty(Exception.Exception.Message)) ExceptionText = Exception.Description;
                if (string.IsNullOrEmpty(Exception.Description) && !string.IsNullOrEmpty(Exception.Exception.Message)) ExceptionText = Exception.Exception.Message;
                if (!string.IsNullOrEmpty(Exception.Description) && !string.IsNullOrEmpty(Exception.Exception.Message)) ExceptionText = Exception.Description + ": " + Exception.Exception.Message;

                System.Drawing.Color ItemBackground;
                switch (Exception.Level)
                {
                    case ProcessException.ExceptionLevel.Slight:
                        ItemBackground = System.Drawing.Color.FromArgb(255, 255, 192);
                        break;
                    case ProcessException.ExceptionLevel.Medium:
                        ItemBackground = System.Drawing.Color.FromArgb(255, 224, 192);
                        break;
                    case ProcessException.ExceptionLevel.Critical:
                        ItemBackground = System.Drawing.Color.FromArgb(255, 192, 192);
                        break;
                    default:
                        ItemBackground = System.Drawing.SystemColors.Window;
                        break;
                }

                ListViewItem ExItem = new ListViewItem
                {
                    BackColor = ItemBackground,
                    Tag = Exception,
                    Text = (this._progressControle._exceptionListView.Items.Count + 1).ToString()
                };
                ExItem.SubItems.Add(Exception.Source);
                ExItem.SubItems.Add(Exception.Target);
                ExItem.SubItems.Add(ExceptionText);

                ListViewInv.AddItem(this._progressControle._exceptionListView, ExItem);
                TabPageInv.ImageIndex(this._progressControle._conclusionTabPage, EXCEPTION_ICON_INDEX);
                TextBoxInv.Text(this._progressControle._exceptionCount, this._progressControle._exceptionListView.Items.Count.ToString());
            }
            #endregion
            #endregion
        }
    }
}