/*
 * QBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
 * 
 * Desctiption:
 * Directory settings for process
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

namespace OLKI.Programme.QBC.src.Project.Settings.Controle
{
    /// <summary>
    /// Class that provides the target for a backup or the source to restore it
    /// </summary>
    public class Directroy : SettingsBase
    {
        #region Properties
        #region Directroy
        /// <summary>
        /// A string that specifies the path of an directory or drive where the backup should been created
        /// </summary>
        private string _path = string.Empty;
        /// <summary>
        /// Get or set A string that specifies the path of an directory or drive where the backup should been created
        /// </summary>
        [Category("Properties")]
        [Description("A string that specifies the path of an directory or drive where the backup should been created")]
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
        /// <summary>
        /// Get the name of the drive where the backup should been created
        /// </summary>
        [Browsable(false)]
        internal string DriveName
        {
            get
            {
                return new System.IO.DriveInfo(new System.IO.DirectoryInfo(this._path).Root.Name).Name;
            }
        }
        /// <summary>
        /// Get the drive where the backup should been created
        /// </summary>
        [Browsable(false)]
        internal System.IO.DriveInfo Drive
        {
            get
            {
                return new System.IO.DriveInfo(this.DriveName);
            }
        }

        /// <summary>
        /// A string that specifies the path of an directory or drive where the backup should been restored, if first directory didn't specifies the target drive
        /// </summary>
        private string _restoreTargetPath = string.Empty;
        /// <summary>
        /// Get or set A string that specifies the path of an directory or drive where the backup should been restored, if first directory didn't specifies the target drive
        /// </summary>
        [Category("Properties")]
        [Description("A string that specifies the path of an directory or drive where the backup should been restored, if first directory didn't specifies the target drive")]
        [DisplayName("RestoreTargetPath")]
        public string RestoreTargetPath
        {
            get
            {
                return this._restoreTargetPath;
            }
            set
            {
                this._restoreTargetPath = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }
        #endregion

        #region DriveDirectroy
        /// <summary>
        /// Specifies if a root directory for every drive will be created during backup process or was been created
        /// </summary>
        private bool _createDriveDirectroy = true;
        /// <summary>
        /// Get or set if a root directory for every drive will be created during backup process or was been created
        /// </summary>
        [Category("Properties")]
        [DefaultValue(true)]
        [Description("Specifies if a root directory for every drive will be created during backup process or was been created")]
        [DisplayName("CreateDriveDirectroy")]
        public bool CreateDriveDirectroy
        {
            get
            {
                return this._createDriveDirectroy;
            }
            set
            {
                this._createDriveDirectroy = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }
        #endregion
        #endregion
    }
}