/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
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

namespace OLKI.Programme.QuBC.src.Project.Process
{
    /// <summary>
    /// Class that provide informations if an process (count or copy) throws an exception
    /// </summary>
    public class ProcessException
    {
        /// <summary>
        /// Default exception level
        /// </summary>
        private const ExceptionLevel DEFAULT_EXCEPTIOON_LEVEL = ExceptionLevel.NoException;

        /// <summary>
        /// The level of an exception
        /// </summary>
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
        /// <summary>
        /// Inner exception, original thrown
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Levvel of the exception
        /// </summary>
        public ExceptionLevel Level { get; set; } = DEFAULT_EXCEPTIOON_LEVEL;

        /// <summary>
        /// Directory or file source path, to copy
        /// </summary>
        public string Source { get; set; } = "";

        /// <summary>
        /// Directory or file target path, to copy
        /// </summary>
        public string Target { get; set; } = "";

        /// <summary>
        /// A Text to descrie the exception
        /// </summary>
        public string Text
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Description) && string.IsNullOrEmpty(this.Exception.Message)) return this.Description;
                if (string.IsNullOrEmpty(this.Description) && !string.IsNullOrEmpty(this.Exception.Message)) return this.Exception.Message;
                if (!string.IsNullOrEmpty(this.Description) && !string.IsNullOrEmpty(this.Exception.Message)) return this.Description + ": " + this.Exception.Message;
                return "";
            }
        }

        /// <summary>
        /// Description of the exception
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Initialise a new ProcessException
        /// </summary>
        public ProcessException()
        {
        }
        #endregion
    }
}