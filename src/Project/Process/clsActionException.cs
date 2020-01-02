/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * A class to handle exceptions durin an process action
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
using System.Linq;
using System.Text;

namespace OLKI.Programme.QBC.BackupProject.Process
{
    public class ProcessException
    {
        private const ExceptionLevel DEFAULT_EXCEPTIOON_LEVEL = ExceptionLevel.NoException;

        public enum ExceptionLevel
        {
            /// <summary>
            /// No Exception level given, default for empty exception.
            /// </summary>
            NoException,
            /// <summary>
            /// Slight exceptions cause in an message in log, the process will not be terminated
            /// </summary>
            Slight,
            /// <summary>
            /// Medium exceptions shold cause in an question to user, if the process shold been terminated
            /// </summary>
            Medium,
            /// <summary>
            /// Critical exceptions shold terminate the whole copy process imideatly
            /// </summary>
            Critical
        };

        #region Properties
        public Exception Exception { get; set; } = null;

        public ExceptionLevel Level { get; set; } = DEFAULT_EXCEPTIOON_LEVEL;
        public string Source { get; set; } = "";
        public string Target { get; set; } = "";
        public string Description { get; set; } = "";

        public ProcessException()
        {
        }
        #endregion
    }
}