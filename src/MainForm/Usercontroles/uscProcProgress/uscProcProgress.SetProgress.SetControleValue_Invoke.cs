/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Set the progress elemetns by invoking, if required, in progress controle
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
            public partial class SetControleValue
            {
                #region Set Items, check if invoke is required
                /// <summary>
                /// Set TextBox text, if required invoke
                /// </summary>
                /// <param name="textBox">TextBox to set the text</param>
                /// <param name="text">Text to set to TextBox.Text</param>
                public void SetTextboxTextInvoke(TextBox textBox, string text)
                {
                    if (this._progressControle.InvokeRequired)
                    {
                        this._progressControle.Invoke(new Action<TextBox, string>(this.SetTextboxTextInvoke), new object[] { textBox, text });
                    }
                    else
                    {
                        textBox.Text = text;
                    }
                }

                /// <summary>
                /// Set Progressbar style, if required invoke
                /// </summary>
                /// <param name="progressBar">Progressbar to set the style</param>
                /// <param name="style">Style to set to ProgressBar.Style</param>
                public void SetProgressbarStyleInvoke(ProgressBar progressBar, ProgressBarStyle style)
                {
                    if (this._progressControle.InvokeRequired)
                    {
                        this._progressControle.Invoke(new Action<ProgressBar, ProgressBarStyle>(this.SetProgressbarStyleInvoke), new object[] { progressBar, style });
                    }
                    else
                    {
                        progressBar.Style = style;
                    }
                }

                /// <summary>
                /// Set Progressbar value, if required invoke
                /// </summary>
                /// <param name="progressBar">Progressbar to set the value</param>
                /// <param name="value">Value to set to ProgressBar.Value</param>
                public void SetProgressbarValueInvoke(ProgressBar progressBar, int value)
                {
                    if (this._progressControle.InvokeRequired)
                    {
                        this._progressControle.Invoke(new Action<ProgressBar, int>(this.SetProgressbarValueInvoke), new object[] { progressBar, value });
                    }
                    else
                    {
                        progressBar.Value = value;
                    }
                }
                #endregion
            }
        }
    }
}