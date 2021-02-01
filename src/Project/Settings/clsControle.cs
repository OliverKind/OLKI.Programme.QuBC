/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
 * License:     LGPL
 * 
 * Desctiption:
 * Settings to controle a process
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
    public class Controle : SettingsBase
    {
        #region Properties
        /// <summary>
        /// Action settings
        /// </summary>
        private Action _action = new Action();
        /// <summary>
        /// Get or set action settings
        /// </summary>
        [Category("Sub Properties")]
        [Description("Action settings")]
        [DisplayName("Action")]
        public Action Action
        {
            get
            {
                return this._action;
            }
            set
            {
                this._action = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Directroy settings
        /// </summary>
        private Directroy _directory = new Directroy();
        /// <summary>
        /// Get or set directroy settings
        /// </summary>
        [Category("Sub Properties")]
        [Description("Directory settings")]
        [DisplayName("Directory")]
        public Directroy Directory
        {
            get
            {
                return this._directory;
            }
            set
            {
                this._directory = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Logfile settings
        /// </summary>
        private Logfile _logfile = new Logfile();
        /// <summary>
        /// Get or set logfile settings
        /// </summary>
        [Category("Sub Properties")]
        [Description("Logfile settings")]
        [DisplayName("Logfile")]
        public Logfile Logfile
        {
            get
            {
                return this._logfile;
            }
            set
            {
                this._logfile = value;
                base.ToggleSettingsChanged(this, new EventArgs());
            }
        }
        #endregion

        #region Methodes
        public Controle()
        {
            this._action.SettingsChanged += new EventHandler(base.ToggleSettingsChanged);
            this._directory.SettingsChanged += new EventHandler(base.ToggleSettingsChanged);
            this._logfile.SettingsChanged += new EventHandler(base.ToggleSettingsChanged);
        }
        #endregion
    }
}