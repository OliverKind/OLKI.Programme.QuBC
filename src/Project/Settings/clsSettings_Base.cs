/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Base class for all settings
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

namespace OLKI.Programme.QuBC.src.Project.Settings
{
    /// <summary>
    /// A class that provides project settings
    /// </summary>
    public class SettingsBase
    {
        #region Events
        /// <summary>
        /// Occurs if the settings was changed
        /// </summary>
        internal event EventHandler SettingsChanged;
        #endregion

        #region Properties
        /// <summary>
        /// The Changed state of the settings
        /// </summary>
        private bool _changed = false;
        /// <summary>
        /// Get or set the changed state of the settings
        /// </summary>
        [Category("Internal Properties <-- Settings Base")]
        [DefaultValue(false)]
        [Description("The Changed state of the settings")]
        [DisplayName("HasChanged")]
        [ReadOnly(true)]
        public bool Changed
        {
            get
            {
                return this._changed;
            }
            set
            {
                this._changed = value;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Toggle changed event and set change state to true
        /// </summary>
        internal virtual void ToggleSettingsChanged()
        {
            //Changed(this, new EventArgs());
            this.ToggleSettingsChanged(this, new EventArgs());
        }
        /// <summary>
        /// Toggle changed event and set change state to true
        /// </summary>
        internal virtual void ToggleSettingsChanged(object sender, EventArgs e)
        {
            this._changed = true;
            this.SettingsChanged?.Invoke(sender, e);
        }
        #endregion
    }
}