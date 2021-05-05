/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Common settings of a project
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
using System.ComponentModel;

namespace OLKI.Programme.QuBC.src.Project.Settings.Common
{
    /// <summary>
    /// A class that provides project common settings
    /// </summary>
    public class Common : SettingsBase
    {
        #region Properties
        /// <summary>
        /// How to handling existing files
        /// </summary>
        private ExisitingFiles _exisitingFiles = new ExisitingFiles();
        /// <summary>
        /// Get or set how to handling existing files
        /// </summary>
        [Category("Sub Properties")]
        [Description("How to handling existing files")]
        [DisplayName("ExisitingFiles")]
        public ExisitingFiles ExisitingFiles
        {
            get
            {
                return this._exisitingFiles;
            }
            set
            {
                this._exisitingFiles = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Create as new settings instance
        /// </summary>
        public Common()
        {
            this._exisitingFiles.SettingsChanged += new EventHandler(base.ToggleSettingsChanged);
        }
        #endregion
    }
}