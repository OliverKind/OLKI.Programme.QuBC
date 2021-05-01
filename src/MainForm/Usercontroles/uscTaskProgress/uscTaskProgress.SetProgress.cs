/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
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

using OLKI.Programme.QuBC.Properties;
using OLKI.Programme.QuBC.Project.Task;
using OLKI.Toolbox.Widgets.Invoke;
using System;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.MainForm.Usercontroles.uscProgress
{
    public partial class TaskProgress
    {
        public partial class SetProgress
        {
            #region Fields
            /// <summary>
            /// Parent progress controle object
            /// </summary>
            private readonly TaskProgress _progressControle = null;
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
            public SetProgress(TaskProgress parent)
            {
                this._progressControle = parent;
                this._setControleValue = new SetControleValue(parent);
            }

            internal void ClearExceptionlog()
            {
                TabPageInv.ImageIndex(this._progressControle._conclusionTabPage, -1);
                ListViewInv.ClearItems(this._progressControle._exceptionListView);
                TextBoxInv.Text(this._progressControle._exceptionCount, "0");
            }

            #region Progress count items
            /// <summary>
            /// Set controles for state: CountStart
            /// </summary>
            internal void SetProgress_CountStart()
            {
                this._progressControle._progressStart = DateTime.Now;

                this._setControleValue.InitialControles();

                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0015);

                DateTime ProgressStart = this._progressControle._progressStart;
                TextBoxInv.Text(this._progressControle.txtCopyStart, ProgressStart.ToString(FORMAT_TIME));
                ExtProgrBarInv.Style(this._progressControle.expAllByte, ProgressBarStyle.Marquee);
                ExtProgrBarInv.Style(this._progressControle.expAllDir, ProgressBarStyle.Marquee);
                ExtProgrBarInv.Style(this._progressControle.expAllItems, ProgressBarStyle.Marquee);
            }

            /// <summary>
            /// Set controles for state: CountBusy
            /// </summary>
            internal void SetPRogress_CountBusy(ProgressStore progressStore)
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0016);
                TextBoxInv.Text(this._progressControle.txtCopyElapsed, this.TimeSpanForamt(this._progressControle.ElapsedTime));

                this._setControleValue.SetProgressCluster(this._progressControle.expAllItems, progressStore.TotalItems);
                this._setControleValue.SetProgressCluster(this._progressControle.expAllByte, progressStore.TotalBytes);
                this._setControleValue.SetProgressCluster(this._progressControle.expAllDir, progressStore.DirectroyFiles);
                this._setControleValue.SetProgressCluster(this._progressControle.expActualDir, progressStore.DirectroyFiles);
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
            internal void SetProgress_CopyStart(ProgressStore progressStore)
            {
                DateTime StartTime = DateTime.Now;
                this._progressControle._progressStart = StartTime;
                // Set controles
                this._setControleValue.InitialControles();
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0018);

                TextBoxInv.Text(this._progressControle.txtCopyStart, StartTime.ToString(FORMAT_TIME));
                if (progressStore.TotalItems.MaxValue == null)
                {
                    ExtProgrBarInv.Style(this._progressControle.expAllByte, ProgressBarStyle.Marquee);
                    ExtProgrBarInv.Style(this._progressControle.expAllDir, ProgressBarStyle.Marquee);
                    this._progressControle.expAllItems.Style = ProgressBarStyle.Marquee;
                }
            }

            /// <summary>
            /// Set controles for state: CopyBusy
            /// </summary>
            internal void SetProgress_CopyBusy(ProgressStore progressStore)
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0019);
                TextBoxInv.Text(this._progressControle.txtCopyElapsed, this.TimeSpanForamt(this._progressControle.ElapsedTime));
                TextBoxInv.Text(this._progressControle._conclusionDirectoriesTextBox, string.Format(FORMAT_VALUE, progressStore.TotalDirectories.ActualValue));
                TextBoxInv.Text(this._progressControle._conclusionFilesTextBox, string.Format(FORMAT_VALUE, progressStore.TotalFiles.ActualValue));

                this._setControleValue.SetProgressCluster(this._progressControle.expAllItems, progressStore.TotalItems);
                this._setControleValue.SetProgressCluster(this._progressControle.expAllByte, progressStore.TotalBytes);
                this._setControleValue.SetProgressCluster(this._progressControle.expAllDir, progressStore.TotalDirectories);
                this._setControleValue.SetProgressCluster(this._progressControle.expActualDir, progressStore.DirectroyFiles);
                this._setControleValue.SetProgressCluster(this._progressControle.expActualFile, progressStore.FileBytes);

                // Get remaining time if counting was done
                if (progressStore.TotalItems.MaxValue != null && progressStore.TotalItems.MaxValue > 0)
                {
                    TimeSpan RemainingTimeByte = OLKI.Toolbox.Common.Matehmatics.RemainingTime(this._progressControle.ElapsedTime, progressStore.TotalBytes.ActualValue, progressStore.TotalBytes.MaxValue);
                    TimeSpan RemainingTimeItem = OLKI.Toolbox.Common.Matehmatics.RemainingTime(this._progressControle.ElapsedTime, progressStore.TotalItems.ActualValue, progressStore.TotalItems.MaxValue);
                    TimeSpan RemainingTime = RemainingTimeByte > RemainingTimeItem ? RemainingTimeByte : RemainingTimeItem;
                    TextBoxInv.Text(this._progressControle.txtCopyRemainTime, this.TimeSpanForamt(RemainingTime));
                }
            }

            /// <summary>
            /// Set controles for state: CopyFinish
            /// </summary>
            internal void SetProgress_CopyFinish()
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x001A);
                DateTime ProgressStart = this._progressControle._progressStart;
                TextBoxInv.Text(this._progressControle.txtCopyStart, ProgressStart.ToString(FORMAT_TIME));
                TextBoxInv.Text(this._progressControle.txtCopyElapsed, this.TimeSpanForamt(this._progressControle.ElapsedTime));
                this._progressControle.expActualDir.DescriptionText = "";

                TextBoxInv.Text(this._progressControle._conclusionDurationTextBox, this.TimeSpanForamt(this._progressControle.ElapsedTime));

                ExtProgrBarInv.DescriptionText(this._progressControle.expActualDir, "");
                ExtProgrBarInv.DescriptionText(this._progressControle.expActualFile, "");
                ExtProgrBarInv.Style(this._progressControle.expAllByte, ProgressBarStyle.Blocks);
                ExtProgrBarInv.Style(this._progressControle.expAllDir, ProgressBarStyle.Blocks);
                ExtProgrBarInv.Style(this._progressControle.expAllItems, ProgressBarStyle.Blocks);
                ExtProgrBarInv.Style(this._progressControle.expActualDir, ProgressBarStyle.Blocks);
                ExtProgrBarInv.Value(this._progressControle.expActualDir, null);
                ExtProgrBarInv.Value(this._progressControle.expActualFile, null);
            }
            #endregion

            #region Delte old items
            /// <summary>
            /// Set controles for state: DeleteStart
            /// </summary>
            internal void SetProgress_DeleteStart()
            {
                if (this._progressControle._progressStart == new DateTime(0)) this._progressControle._progressStart = DateTime.Now;

                this._setControleValue.InitialControles();

                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0021);

                DateTime ProgressStart = this._progressControle._progressStart;
                TextBoxInv.Text(this._progressControle.txtCopyStart, ProgressStart.ToString(FORMAT_TIME));
                ExtProgrBarInv.Style(this._progressControle.expAllByte, ProgressBarStyle.Marquee);
                ExtProgrBarInv.Style(this._progressControle.expAllDir, ProgressBarStyle.Marquee);
                ExtProgrBarInv.Style(this._progressControle.expActualDir, ProgressBarStyle.Marquee);
                ExtProgrBarInv.Style(this._progressControle.expAllItems, ProgressBarStyle.Marquee);
            }

            /// <summary>
            /// Set controles for state: DeleteBusy
            /// </summary>
            internal void SetPRogress_DeleteBusy(ProgressStore progressStore)
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0022);
                TextBoxInv.Text(this._progressControle.txtCopyElapsed, this.TimeSpanForamt(this._progressControle.ElapsedTime));

                this._setControleValue.SetProgressCluster(this._progressControle.expActualDir, progressStore.DirectroyFiles);
                this._setControleValue.SetProgressCluster(this._progressControle.expActualFile, progressStore.FileBytes);
            }

            /// <summary>
            /// Set controles for state: DeleteFinish
            /// </summary>
            internal void SetProgress_DeleteFinish()
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x0023);
                this._setControleValue.ResetAllProgressBars(true, true);
            }
            #endregion

            #region Progress Cancel, Exception
            /// <summary>
            /// Set controles for state: Cancel
            /// </summary>
            internal void SetProgress_Cancel()
            {
                LabelInv.Text(this._progressControle.lblStepText, Stringtable._0x001B);
                ExtProgrBarInv.Style(this._progressControle.expAllItems, ProgressBarStyle.Blocks);
                ExtProgrBarInv.Style(this._progressControle.expAllByte, ProgressBarStyle.Blocks);
                ExtProgrBarInv.Style(this._progressControle.expAllDir, ProgressBarStyle.Blocks);
            }

            /// <summary>
            /// Set controles for state: Exception
            /// Add exception to ListView
            /// </summary>
            internal void SetProgress_Exception(TaskException exception)
            {
                System.Drawing.Color ItemBackground;
                switch (exception.Level)
                {
                    case TaskException.ExceptionLevel.Slight:
                        ItemBackground = System.Drawing.Color.FromArgb(255, 255, 192);
                        break;
                    case TaskException.ExceptionLevel.Medium:
                        ItemBackground = System.Drawing.Color.FromArgb(255, 224, 192);
                        break;
                    case TaskException.ExceptionLevel.Critical:
                        ItemBackground = System.Drawing.Color.FromArgb(255, 192, 192);
                        break;
                    default:
                        ItemBackground = System.Drawing.SystemColors.Window;
                        break;
                }

                ListViewItem ExItem = new ListViewItem
                {
                    BackColor = ItemBackground,
                    Tag = exception,
                    Text = (this._progressControle._exceptionListView.Items.Count + 1).ToString()
                };
                ExItem.SubItems.Add(exception.Source);
                ExItem.SubItems.Add(exception.Target);
                ExItem.SubItems.Add(exception.Text);

                ListViewInv.AddItem(this._progressControle._exceptionListView, ExItem);
                TabPageInv.ImageIndex(this._progressControle._conclusionTabPage, EXCEPTION_ICON_INDEX);
                TextBoxInv.Text(this._progressControle._exceptionCount, string.Format(FORMAT_VALUE, this._progressControle._exceptionListView.Items.Count));
            }

            /// <summary>
            /// Formats the timespan to output format, distinguish to show dates.
            /// </summary>
            /// <param name="timeSpan"></param>
            /// <returns></returns>
            private string TimeSpanForamt(TimeSpan timeSpan)
            {
                if (timeSpan.Days > 0) return timeSpan.ToString(FORMAT_TIMESPAN_WITH_DAYS);
                return timeSpan.ToString(FORMAT_TIMESPAN);
            }
            #endregion
            #endregion
        }
    }
}