/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * What actions sould been done, during a process (backup or restore)
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
    public class Action : SettingsBase
    {
        /// <summary>
        /// Specifies if the "Copy data" step should been done
        /// </summary>
        private bool _copyData = true;
        /// <summary>
        /// Get or set if the "Copy data" step should been done
        /// </summary>
        [Category("Properties")]
        [DefaultValue(true)]
        [Description("Specifies if the \"Copy data\" step should been done")]
        [DisplayName("CopyData")]
        public bool CopyData
        {
            get
            {
                return this._copyData;
            }
            set
            {
                this._copyData = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Specifies if the "Count items and byte" step should been done
        /// </summary>
        private bool _countItemsAndBytes = true;
        /// <summary>
        /// Get or set if the "Count items and byte" step should been done
        /// </summary>
        [Category("Properties")]
        [DefaultValue(true)]
        [Description("Specifies if the \"Count items and byte\" step should been done")]
        [DisplayName("CountItemsAndBytes")]
        public bool CountItemsAndBytes
        {
            get
            {
                return this._countItemsAndBytes;
            }
            set
            {
                this._countItemsAndBytes = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Specifies if the "Delete old data" step should been done
        /// </summary>
        private bool _deleteOldData = true;
        /// <summary>
        /// Get or set if the "Delete old data" step should been done
        /// </summary>
        [Category("Properties")]
        [DefaultValue(true)]
        [Description("Specifies if the \"Delete old data\" step should been done")]
        [DisplayName("CopyData")]
        public bool DeleteOldData
        {
            get
            {
                return this._deleteOldData;
            }
            set
            {
                this._deleteOldData = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }
    }
}