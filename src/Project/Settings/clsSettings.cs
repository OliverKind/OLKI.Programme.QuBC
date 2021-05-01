/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * All settings of a backup project
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

namespace OLKI.Programme.QuBC.Project.Settings
{
    /// <summary>
    /// A class that provides project settings
    /// </summary>
    public class Settings : SettingsBase
    {
        #region Properties
        /// <summary>
        /// Common project settings
        /// </summary>
        private Common.Common _common = new Common.Common();
        /// <summary>
        /// Get or set common project settings
        /// </summary>
        [Category("Sub Properties")]
        [Description("Common project settings")]
        [DisplayName("Common")]
        public Common.Common Common
        {
            get
            {
                return this._common;
            }
            set
            {
                this._common = value;
                this.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Settings for controle creating a backup
        /// </summary>
        private Controle.Controle _controleBackup = new Controle.Controle();
        /// <summary>
        /// Get or set settings for controle creating a backup
        /// </summary>
        [Category("Sub Properties")]
        [Description("Settings for controle creating a backup")]
        [DisplayName("ControleBackup")]
        public Controle.Controle ControleBackup
        {
            get
            {
                return this._controleBackup;
            }
            set
            {
                this._controleBackup = value;
                this.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Settings for controle restore a backup
        /// </summary>
        private Controle.Controle _controleRestore = new Controle.Controle();
        /// <summary>
        /// Get or set settings for controle restore a backup
        /// </summary>
        [Category("Sub Properties")]
        [Description("Settings for controle restore a backup")]
        [DisplayName("ControleRestore")]
        public Controle.Controle ControleRestore
        {
            get
            {
                return this._controleRestore;
            }
            set
            {
                this._controleRestore = value;
                this.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Specifies if the change events should restrained
        /// </summary>
        private bool _restrainChangedEvent = false;
        /// <summary>
        /// Get or set if the change events should restrained
        /// </summary>
        [Category("Internal Properties")]
        [DefaultValue(false)]
        [Description("Specifies if the change events should restrained")]
        [DisplayName("RestrainChangedEvent")]
        public bool RestrainChangedEvent
        {
            get
            {
                return this._restrainChangedEvent;
            }
            set
            {
                this._restrainChangedEvent = value;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Create as new settings instance
        /// </summary>
        public Settings()
        {
            this._common.SettingsChanged += new EventHandler(this.ToggleSettingsChanged);
            this._controleBackup.SettingsChanged += new EventHandler(this.ToggleSettingsChanged);
            this._controleRestore.SettingsChanged += new EventHandler(this.ToggleSettingsChanged);
        }

        /// <summary>
        /// Toggle changed event and set change state to true, if chenge event is not restrained
        /// </summary>
        internal override void ToggleSettingsChanged()
        {
            this.ToggleSettingsChanged(this, new EventArgs());
        }
        /// <summary>
        /// Toggle changed event and set change state to true, if chenge event is not restrained
        /// </summary>
        internal override void ToggleSettingsChanged(object sender, EventArgs e)
        {
            if (!this._restrainChangedEvent) base.ToggleSettingsChanged(sender, e);
        }
        #endregion
    }
}