/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Logilfe settings for a process
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

namespace OLKI.Programme.QuBC.src.Project.Settings.Controle
{
    /// <summary>
    /// Class that provides the settings what action sould been done
    /// </summary>
    public class Logfile : SettingsBase
    {
        /// <summary>
        /// Specifies if the path to the log file should been created automaticaly. In target directory for backup and source directory for restore
        /// </summary>
        private bool _autoPath = true;
        /// <summary>
        /// Get or set if the path to the log file should been created automaticaly. In target directory for backup and source directory for restore
        /// </summary>
        [Category("Properties")]
        [DefaultValue(true)]
        [Description("Specifies if the path to the log file should been created automaticaly. In target directory for backup and source directory for restore.")]
        [DisplayName("AutoPath")]
        public bool AutoPath
        {
            get
            {
                return this._autoPath;
            }
            set
            {
                this._autoPath = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Specifies if the logfile should been created
        /// </summary>
        private bool _create = true;
        /// <summary>
        /// Get or set if the logfile should been created
        /// </summary>
        [Category("Properties")]
        [DefaultValue(true)]
        [Description("Specifies if the logfile should been created")]
        [DisplayName("Create")]
        public bool Create
        {
            get
            {
                return this._create;
            }
            set
            {
                this._create = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// A string that specifies the path where the log file should been created
        /// </summary>
        private string _path = string.Empty;
        /// <summary>
        /// Get or set A string that specifies the path where the log file should been created
        /// </summary>
        [Category("Properties")]
        [Description("A string that specifies the path where the log file should been created")]
        [DisplayName("Path")]
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }
    }
}