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
                this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyStart, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));
                this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllByte, ProgressBarStyle.Marquee);
                this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllDir, ProgressBarStyle.Marquee);
                this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllItems, ProgressBarStyle.Marquee);
            }

            /// <summary>
            /// Set controles for state: CountBusy
            /// </summary>
            internal void SetPRogress_CountBusy()
            {
                this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyElapsed, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));

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
                
                this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyStart, StartTime.ToString(FORMAT_TIME));
                if (this._progressControle.ProgressStore.TotalItems.MaxValue == null)
                {
                    this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllByte, ProgressBarStyle.Marquee);
                    this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllDir, ProgressBarStyle.Marquee);
                    this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllItems, ProgressBarStyle.Marquee);
                }
            }

            /// <summary>
            /// Set controles for state: CopyBusy
            /// </summary>
            internal void SetProgress_CopyBusy()
            {
                System.Diagnostics.Debug.Print(this._progressControle.ProgressStore.DirectroyFiles.ElemenName);
                this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyElapsed, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));

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
                        this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyRemainTime, RemainingTime.ToString(Properties.Settings.Default.Copy_RemainTimeDays));
                    } else
                    {
                        this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyRemainTime, RemainingTime.ToString(Properties.Settings.Default.Copy_RemainTimeNoDays));
                    }
                }
            }

            /// <summary>
            /// Set controles for state: CopyFinish
            /// </summary>
            internal void SetProgress_CopyFinish()
            {
                this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyStart, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));
                this._setControleValue.SetTextboxTextInvoke(this._progressControle.txtCopyElapsed, this._progressControle.ElapsedTime.ToString(FORMAT_TIME));
            }
            #endregion

            #region Progress Cancel, Exception
            /// <summary>
            /// Set controles for state: Cancel
            /// </summary>
            internal void SetProgress_Cancel()
            {
                this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllItems, ProgressBarStyle.Blocks);
                this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllByte, ProgressBarStyle.Blocks);
                this._setControleValue.SetProgressbarStyleInvoke(this._progressControle.pbaAllDir, ProgressBarStyle.Blocks);
                return;
            }

            /// <summary>
            /// Set controles for state: Exception
            /// </summary>
            internal void SetProgress_Exception()
            {
                // Nothing to do during normal operation
                return;
            }
            #endregion
            #endregion
        }
    }
}