/*
 * 
 * FILE NOT USED
 * 
 * It is from an older, not released version. The function will be added in a later version
 * 
 */

//TODO: Create Logfiles in future versions

/*
* QBC- QuickBackupCreator
* 
* Copyright:   Oliver Kind - 2019
* License:     LGPL
* 
* Desctiption:
* Provides tools to write logfiles
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.Proj
{
    /// <summary>
    /// Provides tools to write logfiles
    /// </summary>
    internal class LogFileWriter
    {
        #region Constants
        /// <summary>
        /// Specifies the default indent
        /// </summary>
        private const int DEFAULT_INDENT = 0;
        #endregion

        #region Properties
        /// <summary>
        /// A string that specifies the path where the log file should been created
        /// </summary>
        private string _logFilePath = string.Empty;
        /// <summary>
        /// Specifies if hide messages in future was activated
        /// </summary>
        private bool _hideExceptionMessage = false;
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new logfile writer with an specified log file path
        /// </summary>
        /// <param name="logFilePath">Specifies A string that specifies the path where the log file should been created</param>
        internal LogFileWriter(string logFilePath)
        {
            this._logFilePath = logFilePath;
        }

        /// <summary>
        /// Write an log file line with an specified char and repeting and an indent of 0 chars
        /// </summary>
        /// <param name="lineChar">Spezifies the char to repeat an write to log file line</param>
        /// <param name="charRepeat">Specifies how many times the specified char should been repeated</param>
        internal void WriteLogLine(char lineChar, int charRepeat)
        {
            this.WriteLogLine(lineChar, charRepeat, DEFAULT_INDENT);
        }
        /// <summary>
        /// Write an log file line with an specified char and repeting and an spcified indent
        /// </summary>
        /// <param name="lineChar">Spezifies the char to repeat an write to log file line</param>
        /// <param name="charRepeat">Specifies how many times the specified char should been repeated</param>
        /// <param name="indent">Specifies the indent of the line to write</param>
        internal void WriteLogLine(char lineChar, int charRepeat, int indent)
        {
            string CharLine = string.Empty;
            //CharLine.PadLeft(charRepeat, lineChar);
            for (int i = 0; i < charRepeat; i++)
            {
                CharLine += lineChar;
            }
            this.WriteLogLine(CharLine, indent);
        }
        /// <summary>
        /// Write an log file line with an specified text and an indent of 0 chars
        /// </summary>
        /// <param name="text">Specifies the text to write to log file line</param>
        internal void WriteLogLine(string text)
        {
            this.WriteLogLine(text, DEFAULT_INDENT);
        }
        /// <summary>
        /// Write an log file line with an specified text and an specified indent
        /// </summary>
        /// <param name="text">Specifies the text to write to log file line</param>
        /// <param name="indent">Specifies the indent of the line to write</param>
        internal void WriteLogLine(string text, int indent)
        {
            //Create indent
            string Indent = string.Empty;
            for (int i = 0; i < indent; i++)
            {
                Indent += " ";
            }
            // Write logfile
            try
            {
                if (!string.IsNullOrEmpty(this._logFilePath))
                {
                    using (StreamWriter sw = new StreamWriter(this._logFilePath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(Indent + text);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this._hideExceptionMessage)
                {
                    if (MessageBox.Show(string.Format("Beim Schreiben in die allgemeine Protokolldatei \"{0}\" ist ein Fehler aufgetreten.\n\n{1}\n\nSollen weitere Meldungen dieser Art unterdrückt werden?", new object[] { this._logFilePath, ex.Message }), "Fehler beim Schreiben in die Protokolldatei ", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        this._hideExceptionMessage = true;
                    }
                }
            }
        }
        #endregion
    }
}