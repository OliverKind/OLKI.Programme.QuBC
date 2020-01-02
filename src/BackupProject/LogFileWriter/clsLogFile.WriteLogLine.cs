using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.BackupProject.LogFileWriter
{
    public partial class LogFile
    {
        #region Methodes
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
                //if (!this._hideExceptionMessage)
                //{
                if (MessageBox.Show(string.Format("Beim Schreiben in die allgemeine Protokolldatei \"{0}\" ist ein Fehler aufgetreten.\n\n{1}\n\nSollen weitere Meldungen dieser Art unterdrückt werden?", new object[] { this._logFilePath, ex.Message }), "Fehler beim Schreiben in die Protokolldatei ", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    //this._hideExceptionMessage = true;
                }
                //}
            }
        }
        #endregion
    }
}