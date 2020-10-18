/*
 * QBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
 * 
 * Desctiption:
 * Class that provide tools to manage the project
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

using OLKI.Programme.QBC.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.src
{
    /// <summary>
    /// Class that provide tools to manage the project
    /// </summary>
    internal class ProjectManager
    {
        #region Events
        /// <summary>
        /// Occurs if the active project was changed, settings or data
        /// </summary>
        internal event EventHandler ActiveProjecChanged;
        /// <summary>
        /// Occurs if a project was open from file or a new project was created
        /// </summary>
        internal event EventHandler ProjecOpenOrNew;
        #endregion

        #region Properties
        /// <summary>
        /// An object with the active project
        /// </summary>
        private Project.Project _activeProject = null;
        /// <summary>
        /// Get the active project
        /// </summary>
        internal Project.Project ActiveProject
        {
            get
            {
                return this._activeProject;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new ProjectManager
        /// </summary>
        internal ProjectManager()
        {
            // Initial with a new, empty project. Relevant on first start up
            // An empty project is loaded on program startup
            //this.Project_New(); <-- Not used
        }

        /// <summary>
        /// Event handler for an change event in active project and release the ActiveProjecChanged__Event
        /// </summary>
        internal void ToggleActiveProjecChanged(object sender, EventArgs e)
        {
            if (this.ActiveProjecChanged != null)
            {
                ActiveProjecChanged(sender, e);
            }
        }

        /// <summary>
        /// Check if there are unsaved changes in active project and ask if the sould been saved.
        /// </summary>
        /// <returns>True if the active project should been overwritten</returns>
        internal bool GetSaveActiveProject()
        {
            if (this._activeProject.Changed)
            {
                if (MessageBox.Show(Stringtable._0x0009m, Stringtable._0x0009c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Creates a new, empty project
        /// </summary>
        /// <returns>True if a new project was created withoud exceptions</returns>
        internal bool Project_New()
        {
            return this.Project_Open("", true);
        }

        /// <summary>
        /// Open a new project with showing the file open dialog
        /// </summary>
        internal bool Project_Open()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog()
            {
                DefaultExt = Settings.Default.ProjectFile_DefaultExtension,
                Filter = Settings.Default.ProjectFile_FilterList,
                FilterIndex = Settings.Default.ProjectFile_FilterIndex,
                InitialDirectory = Settings.Default.ProjectFile_DefaultPath
            };
            DialogResult DialogResult = OpenFileDialog.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                return this.Project_Open(OpenFileDialog.FileName);
            }
            return false;
        }

        /// <summary>
        /// Open a new specified project with the specified file or shows the file open dialog file file path ist empth
        /// </summary>
        /// <param name="path">A string that specifies the path of the project file to open</param>
        internal bool Project_Open(string path)
        {
            return this.Project_Open(path, false);
        }

        /// <summary>
        /// Open a new specified project with the specified file or shows the file open dialog file file path ist empth
        /// </summary>
        /// <param name="path">A string that specifies the path of the project file to open</param>
        /// <param name="newProject">Specifies that the project to open is a new project and no file path is specified</param>
        private bool Project_Open(string path, bool newProject)
        {
            try
            {
                Project.Project NewProject = new Project.Project(path);
                // If it ist not an new project that should been loaded, ask for file to open or open the file if path is given
                if (!newProject)
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        return this.Project_Open();  // Recursive to Project_Open to show the OpenFileDialog
                    }
                    else
                    {
                        if (!NewProject.Project_FromXMLString(File.ReadAllText(path))) return false;
                    }
                }
                this._activeProject = NewProject;
                this._activeProject.ProjectChanged += new EventHandler(this.ToggleActiveProjecChanged);

                this.ProjecOpenOrNew?.Invoke(this, new EventArgs());

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Stringtable._0x000Am, new object[] { path, ex.Message }), Stringtable._0x000Ac, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Save the active project to the project file or call Project_SaveAs if the project has no file
        /// </summary>
        /// <returns>True if save of the active project was sucessfull an withoud exceptions</returns>
        internal bool Project_Save()
        {
            // Save the actual project file info
            // If an exception occurs the old fileinfo will be restored
            FileInfo ProjectPathBackup = this._activeProject.File;
            try
            {
                // If there is no project file, call Project_SaveAs() and continue saving by recalling tis function in Project_SaveAs()
                if (this._activeProject.File == null ||
                    string.IsNullOrEmpty(this._activeProject.File.FullName) ||
                    !this._activeProject.File.Exists)
                {
                    if (!this.Project_SaveAs())
                    {
                        // Nothing to do, beccause no file was specified to save to
                        // Otherwise the Project_SaveAs function will call this function again 
                        return false;
                    }
                }
                else
                {
                    string Header = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                    string Content = this._activeProject.Project_ToXMLString();
                    using (StreamWriter sw = new StreamWriter(File.Open(this._activeProject.File.FullName, FileMode.Create), Encoding.UTF8))
                    {
                        sw.WriteLine(Header);
                        sw.WriteLine(Content);
                    }
                    //Remove change state, because of changes where saved.
                    //Raise Change Event to rewrite Form to show possibly new file name and remove star
                    //behind file name, that indikates the file has unsaved changes
                    this._activeProject.Changed = false;
                    this.ToggleActiveProjecChanged(this, new EventArgs());
                    return true;
                }
                return false;  // Nothing to do, beccause no file was specified to save to
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Stringtable._0x000Bm, new object[] { this._activeProject.File.FullName, ex.Message }), Stringtable._0x000Bc, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this._activeProject.File = ProjectPathBackup;
                return false;
            }
        }

        /// <summary>
        /// Save the active project to a new file, specified in a SaveAs dialog
        /// </summary>
        /// <returns>True if save of the active project was sucessfull an withoud exceptions</returns>
        internal bool Project_SaveAs()
        {
            string NewFilePath = string.Empty;
            try
            {
                SaveFileDialog SaveFileDialog = new SaveFileDialog()
                {
                    DefaultExt = Settings.Default.ProjectFile_DefaultExtension,
                    Filter = Settings.Default.ProjectFile_FilterList,
                    FilterIndex = Settings.Default.ProjectFile_FilterIndex,
                    InitialDirectory = Settings.Default.ProjectFile_DefaultPath
                };
                DialogResult DialogResult = SaveFileDialog.ShowDialog();
                if (DialogResult == DialogResult.OK)
                {
                    using (FileStream fswrite = new FileStream(SaveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None, 1))
                    {
                        // Create an new, empty file. Content will be written in Project_Save
                    }
                    this._activeProject.File = new FileInfo(SaveFileDialog.FileName);
                    return this.Project_Save();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Stringtable._0x000Bm, new object[] { NewFilePath, ex.Message }), Stringtable._0x000Bc, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion
    }
}