using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OLKI.Programme.QBC.BackupProject.LogFileWriter
{
    public partial class LogFile
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
        private readonly string _logFilePath = string.Empty;
        #endregion

        #region Methodes
        public LogFile(string logFilePath)
        {
            this._logFilePath = logFilePath;
        }

        public void WriteHead()
        {

        }
        
        public void WriteCountStart()
        {

        }

        public void WriteCountFinish()
        {

        }
        
        public void WriteCopyStart()
        {

        }

        public void WriteCopyFinish()
        {

        }

        public void WriteCancel()
        {

        }

        public void WriteException()
        {

        }
        #endregion
    }
}
