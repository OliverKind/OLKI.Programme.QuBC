﻿/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Provide tool to write a new line to a log file
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
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.src.Project.LogFileWriter
{
    public partial class LogFile
    {
        #region Methodes
        /// <summary>
        /// Write an log file line with an specified char and repeting and an indent of 0 chars
        /// </summary>
        /// <param name="lineChar">Spezifies the char to repeat an write to log file line</param>
        /// <param name="charRepeat">Specifies how many times the specified char should been repeated</param>
        internal void WriteLogLine(char lineChar, int charRepeat, string writeStep)
        {
            this.WriteLogLine(lineChar, charRepeat, DEFAULT_INDENT, writeStep);
        }

        /// <summary>
        /// Write an log file line with an specified char and repeting and an spcified indent
        /// </summary>
        /// <param name="lineChar">Spezifies the char to repeat an write to log file line</param>
        /// <param name="charRepeat">Specifies how many times the specified char should been repeated</param>
        /// <param name="indent">Specifies the indent of the line to write</param>
        internal void WriteLogLine(char lineChar, int charRepeat, int indent, string writeStep)
        {
            string CharLine = string.Empty;
            //CharLine.PadLeft(charRepeat, lineChar);
            for (int i = 0; i < charRepeat; i++)
            {
                CharLine += lineChar;
            }
            this.WriteLogLine(CharLine, indent, writeStep);
        }

        /// <summary>
        /// Write an log file line with an specified text and an indent of 0 chars
        /// </summary>
        /// <param name="text">Specifies the text to write to log file line</param>
        internal void WriteLogLine(string text, string writeStep)
        {
            this.WriteLogLine(text, DEFAULT_INDENT, writeStep);
        }

        /// <summary>
        /// Write an log file line with an specified text and an specified indent
        /// </summary>
        /// <param name="text">Specifies the text to write to log file line</param>
        /// <param name="indent">Specifies the indent of the line to write</param>
        /// <param name="writeStep">The Step of the Logfile Line to write, shown at exception Message</param>
        internal void WriteLogLine(string text, int indent, string writeStep)
        {
            //Create indent
            string Indent = string.Empty;
            for (int i = 0; i < indent; i++)
            {
                Indent += " ";
            }
            //Write to Logfile
            try
            {
                if (string.IsNullOrEmpty(this.LogFilePath)) return;

                //Create Logfile Direcotry if needet
                DirectoryInfo LogfileDirecotry = new FileInfo(this.LogFilePath).Directory;
                if (!LogfileDirecotry.Exists) LogfileDirecotry.Create();

                //Write line to Logfile
                using (StreamWriter sw = new StreamWriter(this.LogFilePath, true, Encoding.UTF8))
                {
                    sw.WriteLine(Indent + text);
                }
            }
            catch (Exception ex)
            {
                if (this._showExceptionMessage)
                {
                    MessageBox.Show(string.Format(Properties.Stringtable._0x0025m, new object[] { this.LogFilePath, ex.Message, writeStep }), Properties.Stringtable._0x0025c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this._showExceptionMessage = false;
                }
            }
        }
        #endregion
    }
}