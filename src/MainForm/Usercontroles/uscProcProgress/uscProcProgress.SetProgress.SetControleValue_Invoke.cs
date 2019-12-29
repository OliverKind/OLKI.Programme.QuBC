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
                /// <param name="label">TextBox to set the text</param>
                /// <param name="text">Text to set to TextBox.Text</param>
                public void Invoke_Label_Text(Label label, string text)
                {
                    if (label.InvokeRequired)
                    {
                        label.Invoke(new Action<Label, string>(this.Invoke_Label_Text), new object[] { label, text });
                    }
                    else
                    {
                        label.Text = text;
                    }
                }

                /// <summary>
                /// Clears ListView by Invoke, if required
                /// </summary>
                /// <param name="listView">ListView to clear</param>
                public void Invoke_ListView_ClearItems(ListView listView)
                {
                    if (listView.InvokeRequired)
                    {
                        listView.Invoke(new Action<ListView>(this.Invoke_ListView_ClearItems), new object[] { listView });
                    }
                    else
                    {
                        listView.Items.Clear();
                    }
                }

                /// <summary>
                /// Add new item to listview, if required invoke
                /// </summary>
                /// <param name="listView">ListView to clear to add item</param>
                /// <param name="newItem">Item to add to ListView</param>
                public void Invoke_ListView_AddItem(ListView listView, ListViewItem newItem)
                {
                    if (listView.InvokeRequired)
                    {
                        listView.Invoke(new Action<ListView, ListViewItem>(this.Invoke_ListView_AddItem), new object[] { listView, newItem });
                    }
                    else
                    {
                        listView.Items.Add(newItem);
                    }
                }


                /// <summary>
                /// Set Progressbar style, if required invoke
                /// </summary>
                /// <param name="progressBar">Progressbar to set the style</param>
                /// <param name="style">Style to set to ProgressBar.Style</param>
                public void Invoke_ProgressBar_Style(ProgressBar progressBar, ProgressBarStyle style)
                {
                    if (progressBar.InvokeRequired)
                    {
                        progressBar.Invoke(new Action<ProgressBar, ProgressBarStyle>(this.Invoke_ProgressBar_Style), new object[] { progressBar, style });
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
                public void Invoke_ProgressBar_Value(ProgressBar progressBar, int value)
                {
                    if (progressBar.InvokeRequired)
                    {
                        progressBar.Invoke(new Action<ProgressBar, int>(this.Invoke_ProgressBar_Value), new object[] { progressBar, value });
                    }
                    else
                    {
                        progressBar.Value = value;
                    }
                }

                public void Invoke_TabPage_ImageIndex(TabPage tabPage, int imageIndex)
                {
                    if (tabPage.InvokeRequired)
                    {
                        tabPage.Invoke(new Action<TabPage, int>(this.Invoke_TabPage_ImageIndex), new object[] { tabPage, imageIndex });
                    }
                    else
                    {
                        tabPage.ImageIndex = imageIndex;
                    }
                }

                /// <summary>
                /// Set TextBox text, if required invoke
                /// </summary>
                /// <param name="textBox">TextBox to set the text</param>
                /// <param name="text">Text to set to TextBox.Text</param>
                public void Invoke_TextBox_Text(TextBox textBox, string text)
                {
                    if (textBox.InvokeRequired)
                    {
                        textBox.Invoke(new Action<TextBox, string>(this.Invoke_TextBox_Text), new object[] { textBox, text });
                    }
                    else
                    {
                        textBox.Text = text;
                    }
                }
                #endregion
            }
        }
    }
}