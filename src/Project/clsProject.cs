/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * A class that provides all information to handle and save backup project.
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

using OLKI.Programme.QuBC.Properties;
using OLKI.Programme.QuBC.src.Project.Settings.Common;
using OLKI.Toolbox.Common;
using OLKI.Toolbox.DirectoryAndFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OLKI.Programme.QuBC.src.Project
{
    /// <summary>
    /// A class that provides all information to handle and save backup project. Setting it true will raise the
    /// </summary>
    public class Project
    {
        #region Events
        /// <summary>
        /// Thrown if the project was changed, settings or data
        /// </summary>
        internal event EventHandler ProjectChanged;
        #endregion

        #region Enums
        /// <summary>
        /// An Enumeration ths specifies what to copy of an direcrtory during the copy progress
        /// </summary>
        public enum DirectoryScope
        {
            /// <summary>
            /// The directory and all files and sub directories will skiped during copy prograss
            /// </summary>
            Nothing,
            /// <summary>
            /// The selected files and subdirectories will be copied
            /// </summary>
            Selected,
            /// <summary>
            /// All files and sub directories of the directorie will be copied
            /// </summary>
            All
        }
        #endregion

        #region Members
        /// <summary>
        /// Specifies if the change events should restrained
        /// </summary>
        private bool _restrainChangedEvent = false;

        /// <summary>
        /// Form to change Project Settings
        /// </summary>
        MainForm.SubForms.ProjectSettingsForm _settingsForm;
        #endregion

        #region Properties
        /// <summary>
        /// True if the project was changed, settings or data, and the was not saved since this time
        /// </summary>
        private bool _changed = false;
        /// <summary>
        /// Get or set the change state of the project. True if the project was changed, settings or data, and the was not saved since this time. If setting it to true will raise the ProjectChanged__Event event
        /// </summary>
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

        /// <summary>
        /// The file of the active project (or null if the project did not have a file?)
        /// </summary>
        private FileInfo _file = null;
        /// <summary>
        /// Get the file of the active project (or null if the project did not have a file?)
        /// </summary>
        internal FileInfo File
        {
            get
            {
                return this._file;
            }
            set
            {
                this._file = value;
            }
        }

        /// <summary>
        /// Settings of the project
        /// </summary>
        private Settings.Settings _settings = new Settings.Settings();
        /// <summary>
        /// Get or set the settings of the project
        /// </summary>
        internal Settings.Settings Settings
        {
            get
            {
                return this._settings;
            }
            set
            {
                this.Changed = true;
                this._settings = value;
            }
        }

        /// <summary>
        /// List with all directorys to copy in backup and backup scope of the directory
        /// </summary>
        private Dictionary<string, DirectoryScope> _toBackupDirectorys = new Dictionary<string, Project.DirectoryScope>();
        /// <summary>
        /// Get or set the list with all directorys to copy in backup and backup scope of the directory
        /// </summary>
        internal Dictionary<string, DirectoryScope> ToBackupDirectorys
        {
            get
            {
                return this._toBackupDirectorys;
            }
            set
            {
                this._toBackupDirectorys = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// List with files they should copies from an directory
        /// </summary>
        private Dictionary<string, List<string>> _toBackupFiles = new Dictionary<string, System.Collections.Generic.List<string>>();
        /// <summary>
        /// Get or set the list with files they should copies from an directory
        /// </summary>
        internal Dictionary<string, List<string>> ToBackupFiles
        {
            get
            {
                return this._toBackupFiles;
            }
            set
            {
                this._toBackupFiles = value;
                this.Changed = true;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new backup project
        /// </summary>
        /// <param name="path">A string that specifies the path of the project file. If it is an empty project withoud file give an empth string</param>
        internal Project(string path)
        {
            if (!string.IsNullOrEmpty(path)) this._file = new FileInfo(path);
            this._settings = new Settings.Settings();
            this._settings.SettingsChanged += new EventHandler(this.SettingsChanged);
        }

        /// <summary>
        /// Change Project settings
        /// </summary>
        /// <param name="parenForm">Form to use as parent for Settings Form</param>
        public void ChangeSettings(Form parenForm)
        {
            this._settingsForm = new MainForm.SubForms.ProjectSettingsForm(this._settings, parenForm);
            this._settingsForm.RequestProjectCelanUp += new EventHandler(this.SettingsForm_RequestProjectCelanUp);
            this._settingsForm.ShowDialog(parenForm);
        }

        /// <summary>
        /// Clean up the project. Delete not existing directory-references and not existing file-references.
        /// </summary>
        /// <param name="parenForm">Form to use as parent for MessageBoxes</param>
        public void CleanUp(Form parenForm)
        {
            if (MessageBox.Show(parenForm, Stringtable._0x0028m, Stringtable._0x0028c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                List<string> Deleted = new List<string>();
                int DeletedD = 0;
                int DeletedF = 0;

                parenForm.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                //Delete not existing directory-references
                foreach (KeyValuePair<string, Project.DirectoryScope> itemD in this._toBackupDirectorys.OrderBy(o => o.Key))
                {
                    if (!new DirectoryInfo(itemD.Key).Exists)
                    {
                        DeletedD++;
                        Deleted.Add(itemD.Key);
                        this._toBackupDirectorys.Remove(itemD.Key);
                    }
                }

                //Delete not existing file-references
                foreach (KeyValuePair<string, List<string>> itemDF in this._toBackupFiles.OrderBy(o => o.Key))
                {
                    if (!this._toBackupDirectorys.ContainsKey(itemDF.Key))
                    {
                        DeletedF += this._toBackupFiles[itemDF.Key].Count;
                        Deleted.AddRange(this._toBackupFiles[itemDF.Key]);
                        this._toBackupFiles.Remove(itemDF.Key);
                    }
                    else
                    {
                        foreach (string itemF in itemDF.Value.OrderBy(o => o))
                        {
                            if (!new FileInfo(itemF).Exists)
                            {
                                DeletedF++;
                                Deleted.Add(itemF);
                                this._toBackupFiles[itemDF.Key].Remove(itemF);
                            }
                        }
                    }
                }

                this.Changed = true;
                this.ToggleProjectChanged(this, new EventArgs());
                Cursor.Current = Cursors.Default;
                parenForm.Enabled = true;

                _ = Deleted;
                MessageBox.Show(parenForm, string.Format(Stringtable._0x0029m, new object[] { DeletedD, DeletedF }), Stringtable._0x0029c, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Event handler for an change event in the settings of this project
        /// </summary>
        private void SettingsChanged(object sender, EventArgs e)
        {
            this._changed = true;
            this.ToggleProjectChanged(sender, e);
        }

        /// <summary>
        /// Event handler for an RequestProjectCelanUp in SettingsForm
        /// </summary>
        internal void SettingsForm_RequestProjectCelanUp(object sender, EventArgs e)
        {
            this.CleanUp(this._settingsForm.ApplicationMainForm);
        }

        /// <summary>
        /// Toggle ProjectChanged event
        /// </summary>
        /// <param name="sender">Sender of changed event</param>
        /// <param name="e">EventArgs of changed event</param>
        internal void ToggleProjectChanged(object sender, EventArgs e)
        {
            if (!this._restrainChangedEvent && this.ProjectChanged != null) this.ProjectChanged(this, new EventArgs());
        }

        #region Directory an file handling
        #region DirectoryAdd
        /// <summary>
        /// Add a specified directory to directory list
        /// </summary>
        /// <param name="directory">Specifies the directory to add to directory list</param>
        /// <param name="scope">Specifies the scope of what to do with the specified directory</param>
        internal void DirectoryAdd(DirectoryInfo directory, DirectoryScope scope)
        {
            this.DirectoryAdd(directory.FullName, scope);
        }
        /// <summary>
        /// Add a specified directory to directory list
        /// </summary>
        /// <param name="directroy">A string that specifies the path of the directory to add to directory list</param>
        /// <param name="scope">Specifies the scope of what to do with the specified directory</param>
        internal void DirectoryAdd(string directroy, DirectoryScope scope)
        {
            this._changed = true;
            if (!this._toBackupDirectorys.ContainsKey(directroy))
            {
                this._toBackupDirectorys.Add(directroy, DirectoryScope.Selected);
            }
            this._toBackupDirectorys[directroy] = scope;
            this.ToggleProjectChanged(this, new EventArgs());
        }
        #endregion

        #region DirectoryRemove
        /// <summary>
        /// Remove a specified directory from directory list and removes associated files from file list
        /// </summary>
        /// <param name="directory">Specifies the directory to remove from directory list</param>
        /// <returns>True if the directory was removed sucessfull from directory list</returns>
        internal void DirectoryRemove(DirectoryInfo directory)
        {
            this.DirectoryRemove(directory.FullName);
        }
        /// <summary>
        /// Remove a specified directory from directory list and removes associated files from file list
        /// </summary>
        /// <param name="directory">A string that specifies the path of the directory to remove from directory list</param>
        /// <returns>True if the directory was removed sucessfull from directory list</returns>
        internal void DirectoryRemove(string directory)
        {
            this.Changed = true;
            this._toBackupFiles.Remove(directory);
            this._toBackupDirectorys.Remove(directory);
            this.ToggleProjectChanged(this, new EventArgs());
        }
        #endregion

        #region FileAdd
        /// <summary>
        /// Add a specified file to file list witout an specified directory. The directory will be identified automaticaly
        /// </summary>
        /// <param name="file">A string that specifies the path of the file to add to file list</param>
        internal void FileAdd(string file)
        {
            this.FileAdd(new FileInfo(file));
        }
        /// <summary>
        /// Add a specified file to file list witout an specified directory. The directory will be identified automaticaly
        /// </summary>
        /// <param name="file">Specifies the file to add to file list</param>
        internal void FileAdd(FileInfo file)
        {
            this.FileAdd(file.Directory, file);
        }
        /// <summary>
        /// Add a specified file to file list with an specified directory
        /// </summary>
        /// <param name="directroy">Specifies the directory where the file, which should been added to file list, is strored</param>
        /// <param name="file">Specifies the file to add to file list</param>
        internal void FileAdd(DirectoryInfo directroy, FileInfo file)
        {
            this.FileAdd(directroy.FullName, file.FullName);
        }
        /// <summary>
        /// Add a specified file to file list with an specified directory
        /// </summary>
        /// <param name="directroy">Specifies the directory where the file, which should been added to file list, is strored</param>
        /// <param name="file">Specifies the file to add to file list</param>
        internal void FileAdd(string directroy, FileInfo file)
        {
            this.FileAdd(directroy, file.FullName);
        }
        /// <summary>
        /// Add a specified file to file list with an specified directory
        /// </summary>
        /// <param name="directroy">Specifies the directory where the file, which should been added to file list, is strored</param>
        /// <param name="file">A string that specifies the path of the file to add to file list</param>
        internal void FileAdd(DirectoryInfo directroy, string file)
        {
            this.FileAdd(directroy.FullName, file);
        }
        /// <summary>
        /// Add a specified file to file list with an specified directory
        /// </summary>
        /// <param name="directroy">A string that specifies the directory where the file, which should been added to file list, is strored</param>
        /// <param name="file">A string that specifies the path of the file to add to file list</param>
        internal void FileAdd(string directroy, string file)
        {
            this.Changed = true;
            if (!this._toBackupFiles.ContainsKey(directroy))
            {
                this._toBackupFiles.Add(directroy, new List<string>());
            }
            this._toBackupFiles[directroy].Add(file);
            this.ToggleProjectChanged(this, new EventArgs());
        }
        #endregion

        #region FileRemove
        /// <summary>
        /// Remove a specified file from file list
        /// </summary>
        /// <param name="file">A string that specifies the path of the file to remove from file list</param>
        /// <returns>True if the file was removed sucessfull from file list</returns>
        internal void FileRemove(string file)
        {
            this.FileRemove(new FileInfo(file));
        }
        /// <summary>
        /// Remove a specified file from file list
        /// </summary>
        /// <param name="file">Specifies the file to remove from file list</param>
        /// <returns>True if the file was removed sucessfull from file list</returns>
        internal void FileRemove(FileInfo file)
        {
            this.FileRemove(file.Directory, file);
        }
        /// <summary>
        /// Remove a specified file from file list
        /// </summary>
        /// <param name="directroy">Specifies the directory where the file, which should been removed from file list, is strored</param>
        /// <param name="file">Specifies the file to remove from file list</param>
        /// <returns>True if the file was removed sucessfull from file list</returns>
        internal void FileRemove(DirectoryInfo directroy, FileInfo file)
        {
            this.FileRemove(directroy.FullName, file.FullName);
        }
        /// <summary>
        /// Remove a specified file from file list
        /// </summary>
        /// <param name="directroy">A string that specifies the directory where the file, which should been removed from file list, is strored</param>
        /// <param name="file">Specifies the file to remove from file list</param>
        /// <returns>True if the file was removed sucessfull from file list</returns>
        internal void FileRemove(string directroy, FileInfo file)
        {
            this.FileRemove(directroy, file.FullName);
        }
        /// <summary>
        /// Remove a specified file from file list
        /// </summary>
        /// <param name="directroy">Specifies the directory where the file, which should been removed from file list, is strored</param>
        /// <param name="file">A string that specifies the path of the file to remove from file list</param>
        /// <returns>True if the file was removed sucessfull from file list</returns>
        internal void FileRemove(DirectoryInfo directroy, string file)
        {
            this.FileRemove(directroy.FullName, file);
        }
        /// <summary>
        /// Remove a specified file from file list
        /// </summary>
        /// <param name="directroy">A string that specifies the directory where the file, which should been removed from file list, is strored</param>
        /// <param name="file">A string that specifies the path of the file to remove from file list</param>
        /// <returns>True if the file was removed sucessfull from file list</returns>
        internal void FileRemove(string directroy, string file)
        {
            this.Changed = true;
            if (this._toBackupFiles.ContainsKey(directroy))
            {
                this._toBackupFiles[directroy].Remove(file);
            }
            this.ToggleProjectChanged(this, new EventArgs());
        }
        #endregion
        #endregion

        #region Project export and import
        // This side was helpful: http://stackoverflow.com/questions/1799767/easy-way-to-convert-a-dictionarystring-string-to-xml-and-visa-versa
        #region Project_ToXMLString
        /// <summary>
        /// Converts the project with all data and settings to an XML formated string
        /// </summary>
        /// <returns>An XML-format string that represents the project content</returns>
        internal string Project_ToXMLString()
        {
            XElement ProjectRoot = new XElement("QuBC_ProjectData");
            //TODO: CHANGE to settings
            ProjectRoot.Add(new XAttribute("Version", Settings_AppConst.Default.ProjectFile_Version_Actual));

            //Get Directorys (get files)
            XElement DirectoryList = new XElement("DirectoryList");
            foreach (string Dkey in this._toBackupDirectorys.Keys)
            {
                XElement DirectoryItem = new XElement("DirectoryItem");
                //Add Values to directroy Item
                DirectoryItem.Add(new XElement("Path", Dkey));
                DirectoryItem.Add(new XElement("Scope",
                    (int)this._toBackupDirectorys[Dkey],
                    new XAttribute("PlainText", this._toBackupDirectorys[Dkey]))    // Add plain text attribute to make debugging easyer
                );

                //Get Files
                XElement FileList = new XElement("FilesToCopy");
                if (this._toBackupDirectorys[Dkey] == DirectoryScope.Selected && this._toBackupFiles.ContainsKey(Dkey))
                {
                    foreach (string item in this._toBackupFiles[Dkey])
                    {
                        XElement FileItem = new XElement("File");
                        FileItem.Add(new XElement("FilePath", item));
                        FileList.Add(FileItem);
                    }
                }
                DirectoryItem.Add(FileList);
                DirectoryList.Add(DirectoryItem);
            }
            ProjectRoot.Add(DirectoryList);

            //Get Settings
            XElement SettingsRoot = new XElement("Settings");
            SettingsRoot.Add(
                new XElement("Common",
                    new XElement("ExistringFiles",
                        new XElement("AddTextToExistingFile", this._settings.Common.ExisitingFiles.AddTextToExistingFile),
                        new XElement("HandleExistingItem",
                            (int)this._settings.Common.ExisitingFiles.HandleExistingItem,
                            new XAttribute("PlainText", this._settings.Common.ExisitingFiles.HandleExistingItem)    // Add plain text attribute to make debugging easyer
                        )
                    ),
                    new XElement("DefaultTab", (int)this._settings.Common.DefaultTab),
                    new XElement("Automation", (int)this._settings.Common.Automation),
                    new XElement("AutomationFinishAction", (int)this._settings.Common.AutomationFinishAction),
                    new XElement("AutomationWaitTime", this._settings.Common.AutomationWaitTime)
                ),
                new XElement("ControleBackup",
                    new XElement("Action",
                        new XElement("CopyData", this._settings.ControleBackup.Action.CopyData),
                        new XElement("CountItemsAndBytes", this._settings.ControleBackup.Action.CountItemsAndBytes),
                        new XElement("DeleteOldData", this._settings.ControleBackup.Action.DeleteOldData)
                    ),
                    new XElement("Directory",
                        new XElement("CreateDriveDirectroy", this._settings.ControleBackup.Directory.CreateDriveDirectroy),
                        new XElement("Path", this._settings.ControleBackup.Directory.Path)
                    ),
                    new XElement("Logfile",
                        new XElement("AutoPath", this._settings.ControleBackup.Logfile.AutoPath),
                        new XElement("Create", this._settings.ControleBackup.Logfile.Create),
                        new XElement("Path", this._settings.ControleBackup.Logfile.Path)
                    )
                ),
                new XElement("ControleRestore",
                    new XElement("Action",
                        new XElement("CopyData", this._settings.ControleRestore.Action.CopyData),
                        new XElement("CountItemsAndBytes", this._settings.ControleRestore.Action.CountItemsAndBytes)
                    ),
                    new XElement("Directory",
                        new XElement("CreateDriveDirectroy", this._settings.ControleRestore.Directory.CreateDriveDirectroy),
                        new XElement("Path", this._settings.ControleRestore.Directory.Path),
                        new XElement("RestoreTargetPath", this._settings.ControleRestore.Directory.RestoreTargetPath)
                    ),
                    new XElement("Logfile",
                        new XElement("AutoPath", this._settings.ControleRestore.Logfile.AutoPath),
                        new XElement("Create", this._settings.ControleRestore.Logfile.Create),
                        new XElement("Path", this._settings.ControleRestore.Logfile.Path)
                    )
                )
            );
            ProjectRoot.Add(SettingsRoot);
            return ProjectRoot.ToString();
        }
        #endregion

        #region Project_FromXMLString
        /// <summary>
        /// Set all project data from an specified XML formated string
        /// </summary>
        /// <param name="inputProject">Specifies an XML format string which represents the project</param>
        internal bool Project_FromXMLString(string inputProject)
        {
            return this.Project_FromXMLString(XElement.Parse(inputProject));
        }
        /// <summary>
        /// Set all project data from an specified XElement which parsed an XML formated string
        /// </summary>
        /// <param name="inputProject">Specifies XElement XElement which parsed an XML formated string which represents the project</param>
        internal bool Project_FromXMLString(XElement inputProject)
        {
            try
            {
                this._restrainChangedEvent = true;

                this._toBackupDirectorys.Clear();
                this._toBackupFiles.Clear();

                //Check Fileversion
                if (!this.Project_FromXMLString_CheckVersion(inputProject.Attribute("Version").Value)) return false;
                //Read Directorys
                foreach (XElement DirectoryItem in inputProject.Element("DirectoryList").Elements("DirectoryItem"))
                {
                    string DirectoryPath = Serialize.GetFromXElement(DirectoryItem, "Path", "");
                    DirectoryScope DirectoryScope = (DirectoryScope)Serialize.GetFromXElement(DirectoryItem, "Scope", (int)DirectoryScope.Nothing);
                    this.DirectoryAdd(DirectoryPath, DirectoryScope);
                    //Read Files
                    foreach (XElement FileItem in DirectoryItem.Element("FilesToCopy").Elements())
                    {
                        string FilePath = Serialize.GetFromXElement(FileItem, "FilePath", "");
                        this.FileAdd(DirectoryPath, FilePath);
                    }
                }

                //Read Settings
                this._settings.RestrainChangedEvent = true;
                XElement Settings = inputProject.Element("Settings");
                {
                    XElement Common = Settings.Element("Common");
                    {
                        XElement ExistringFiles = Common.Element("ExistringFiles");
                        {

                            this._settings.Common.ExisitingFiles.AddTextToExistingFile = Serialize.GetFromXElement(ExistringFiles, "AddTextToExistingFile", this._settings.Common.ExisitingFiles.AddTextToExistingFile);
                            this._settings.Common.ExisitingFiles.HandleExistingItem = (HandleExistingFiles.HowToHandleExistingItem)Serialize.GetFromXElement(ExistringFiles, "HandleExistingItem", (int)this._settings.Common.ExisitingFiles.HandleExistingItem);
                        }

                        this._settings.Common.DefaultTab = Serialize.GetFromXElement(Common, "DefaultTab", this._settings.Common.DefaultTab);
                        this._settings.Common.Automation = (Common.AutomationMode)Serialize.GetFromXElement(Common, "Automation", (int)this._settings.Common.Automation);
                        this._settings.Common.AutomationFinishAction = (Common.FinishAction)Serialize.GetFromXElement(Common, "AutomationFinishAction", (int)this._settings.Common.AutomationFinishAction);
                        this._settings.Common.AutomationWaitTime = Serialize.GetFromXElement(Common, "AutomationWaitTime", (int)this._settings.Common.AutomationWaitTime);
                    }
                    XElement ControleBackup = Settings.Element("ControleBackup");
                    {
                        XElement Action = ControleBackup.Element("Action");
                        {
                            this._settings.ControleBackup.Action.CopyData = Serialize.GetFromXElement(Action, "CopyData", this._settings.ControleBackup.Action.CopyData);
                            this._settings.ControleBackup.Action.CountItemsAndBytes = Serialize.GetFromXElement(Action, "CountItemsAndBytes", this._settings.ControleBackup.Action.CountItemsAndBytes);
                            this._settings.ControleBackup.Action.DeleteOldData = Serialize.GetFromXElement(Action, "DeleteOldData", this._settings.ControleBackup.Action.DeleteOldData);
                        }
                        XElement Directory = ControleBackup.Element("Directory");
                        {
                            this._settings.ControleBackup.Directory.CreateDriveDirectroy = Serialize.GetFromXElement(Directory, "CreateDriveDirectroy", this._settings.ControleBackup.Directory.CreateDriveDirectroy);
                            this._settings.ControleBackup.Directory.Path = Serialize.GetFromXElement(Directory, "Path", this._settings.ControleBackup.Directory.Path);
                        }
                        XElement Logfile = ControleBackup.Element("Logfile");
                        {
                            this._settings.ControleBackup.Logfile.AutoPath = Serialize.GetFromXElement(Directory, "AutoPath", this._settings.ControleBackup.Logfile.AutoPath);
                            this._settings.ControleBackup.Logfile.Create = Serialize.GetFromXElement(Directory, "Create", this._settings.ControleBackup.Logfile.Create);
                            this._settings.ControleBackup.Logfile.Path = Serialize.GetFromXElement(Directory, "Path", this._settings.ControleBackup.Logfile.Path);
                        }
                    }
                    XElement ControleRestore = Settings.Element("ControleRestore");
                    {
                        XElement Action = ControleRestore.Element("Action");
                        {
                            this._settings.ControleRestore.Action.CopyData = Serialize.GetFromXElement(Action, "CopyData", this._settings.ControleRestore.Action.CopyData);
                            this._settings.ControleRestore.Action.CountItemsAndBytes = Serialize.GetFromXElement(Action, "CountItemsAndBytes", this._settings.ControleRestore.Action.CountItemsAndBytes);
                        }
                        XElement Directory = ControleRestore.Element("Directory");
                        {
                            this._settings.ControleRestore.Directory.CreateDriveDirectroy = Serialize.GetFromXElement(Directory, "CreateDriveDirectroy", this._settings.ControleRestore.Directory.CreateDriveDirectroy);
                            this._settings.ControleRestore.Directory.Path = Serialize.GetFromXElement(Directory, "Path", this._settings.ControleRestore.Directory.Path);
                            this._settings.ControleRestore.Directory.RestoreTargetPath = Serialize.GetFromXElement(Directory, "RestoreTargetPath", this._settings.ControleRestore.Directory.RestoreTargetPath);
                        }
                        XElement Logfile = ControleRestore.Element("Logfile");
                        {
                            this._settings.ControleRestore.Logfile.AutoPath = Serialize.GetFromXElement(Logfile, "AutoPath", this._settings.ControleRestore.Logfile.AutoPath);
                            this._settings.ControleRestore.Logfile.Create = Serialize.GetFromXElement(Logfile, "Create", this._settings.ControleRestore.Logfile.Create);
                            this._settings.ControleRestore.Logfile.Path = Serialize.GetFromXElement(Logfile, "Path", this._settings.ControleRestore.Logfile.Path);
                        }
                    }
                }
                this._changed = false;
                this._settings.RestrainChangedEvent = false;
                this._restrainChangedEvent = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Stringtable._0x001Fm, new object[] { ex.Message }), Stringtable._0x001Fc, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Check if the file to open is compatible or compatible after conwerted, with this application
        /// </summary>
        /// <param name="FileVersion">Version of the file to check</param>
        /// <returns>True if file is compatible or converted, otherwise false</returns>
        private bool Project_FromXMLString_CheckVersion(string FileVersion)
        {
            // Create list with file Versions
            // Highest Version ist file version, lower versions are for compability with older versions
            List<string> FileVersionList = FileVersion.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Full Compatible fileversions
            List<string> FullCompatibleVersionList = Settings_AppConst.Default.ProjectFile_VersionCompatibleNative.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (FileVersionList.Intersect(FullCompatibleVersionList).Count() > 0)
            {
                return true;    // Full Compatible Return true
            }

            // Fileversions they are compatible if the file would been converted
            List<string> ConvCompatibleVersionList = Settings_AppConst.Default.ProjectFile_VersionCompatibleConvert.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (FileVersionList.Intersect(ConvCompatibleVersionList).Count() > 0)
            {
                //TODO: ADD CODE --> in future version to convert if necessary 
                return true;    // Compatible if converted
            }

            // Not Compatible file, return false
            MessageBox.Show(Stringtable._0x0001m, Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion
        #endregion
        #endregion
    }
}