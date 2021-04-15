/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
 * License:     LGPL
 * 
 * Desctiption:
 * How to handle existing foles in a project
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

using OLKI.Toolbox.DirectoryAndFile;
using System;
using System.ComponentModel;

namespace OLKI.Programme.QuBC.src.Project.Settings.Common
{
    /// <summary>
    /// A class that provides project common settings for file handling
    /// </summary>
    public class ExisitingFiles : SettingsBase
    {
        #region Properties
        /// <summary>
        /// How to handle existing items
        /// </summary>
        private HandleExistingFiles.HowToHandleExistingItem _handleAction = HandleExistingFiles.HowToHandleExistingItem.OverwriteAll;
        /// <summary>
        /// Get or set how to handle existing items
        /// </summary>
        [Category("Properties")]
        [DefaultValue(HandleExistingFiles.HowToHandleExistingItem.OverwriteAll)]
        [Description("How to handle existing items")]
        [DisplayName("HandleExistingItem")]
        public HandleExistingFiles.HowToHandleExistingItem HandleExistingItem
        {
            get
            {
                return this._handleAction;
            }
            set
            {
                this._handleAction = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Text to add to an existing file
        /// </summary>
        private string _textToAdd = Properties.Settings.Default.Copy_FileExisitngAddTextDefault;
        /// <summary>
        /// Get or set text to add to an existing file
        /// </summary>
        [Category("Properties")]
        [DefaultValue("")]
        [Description("Text to add to an existing file")]
        [DisplayName("AddTextToExistingFile")]
        public string AddTextToExistingFile
        {
            get
            {
                return this._textToAdd;
            }
            set
            {
                this._textToAdd = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Create as new settings instance
        /// </summary>
        public ExisitingFiles()
        {
        }
        #endregion
    }
}