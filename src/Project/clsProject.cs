/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
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

using OLKI.Programme.QBC.Properties;
using OLKI.Tools.CommonTools.DirectoryAndFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OLKI.Programme.QBC.BackupProject
{
    /// <summary>
    /// A class that provides all information to handle and save backup project. Setting it true will raise the
    /// </summary>
    internal class Project
    {
        #region Constants
        private const string XML_DIRECTORYS_ELEMENT_NAME = "DirectoryList";
        private const string XML_DIRECTORY_ITEM_NAME = "DirectoryItem";
        private const string XML_DIRECTORY_ITEM_PATH_NAME = "Path";
        private const string XML_DIRECTORY_ITEM_SCOPE_NAME = "Scope";
        private const string XML_FILES_ELEMENT_NAME = "FilesToCopy";
        private const string XML_FILE_ITEM_NAME = "File";
        private const string XML_FILE_ITEM_PATH_NAME = "FilePath";
        private const string XML_FILE_SETTINGS_NODE = "Settings";
        #endregion

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
        internal enum DirectoryScope
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
        private Dictionary<string, DirectoryScope> _toBackupDirectorys = new Dictionary<string, QBC.BackupProject.Project.DirectoryScope>();
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
        /// Event handler for an change event in the settings of this project
        /// </summary>
        private void SettingsChanged(object sender, EventArgs e)
        {
            this._changed = true;
            this.ToggleProjectChanged(sender,e);
        }

        /// <summary>
        /// Toggle ProjectChanged event
        /// </summary>
        /// <param name="sender">Sender of changed event</param>
        /// <param name="e">EventArgs of changed event</param>
        private void ToggleProjectChanged(object sender, EventArgs e)
        {
            if (!this._restrainChangedEvent && this.ProjectChanged != null) ProjectChanged(this, new EventArgs());
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
        internal void  DirectoryRemove(string directory)
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
            XElement ProjectRoot = new XElement("QBC_ProjectData");
            ProjectRoot.Add(new XAttribute("Version", "2"));

            //Get Directorys (get files)
            XElement DirectoryList = new XElement(XML_DIRECTORYS_ELEMENT_NAME);
            foreach (string Dkey in this._toBackupDirectorys.Keys)
            {
                XElement DirectoryItem = new XElement(XML_DIRECTORY_ITEM_NAME);
                //Add Values to directroy Item
                DirectoryItem.Add(new XElement(XML_DIRECTORY_ITEM_PATH_NAME, Dkey));
                DirectoryItem.Add(new XElement(XML_DIRECTORY_ITEM_SCOPE_NAME,
                    (int)this._toBackupDirectorys[Dkey],
                    new XAttribute("PlainText", this._toBackupDirectorys[Dkey]))    // Add plain text attribute to make debugging easyer
                );

                //Get Files
                XElement FileList = new XElement(XML_FILES_ELEMENT_NAME);
                if (this._toBackupDirectorys[Dkey] == DirectoryScope.Selected && this._toBackupFiles.ContainsKey(Dkey))
                {
                    foreach (string item in this._toBackupFiles[Dkey])
                    {
                        XElement FileItem = new XElement(XML_FILE_ITEM_NAME);
                        FileItem.Add(new XElement(XML_FILE_ITEM_PATH_NAME, item));
                        FileList.Add(FileItem);
                    }
                }
                DirectoryItem.Add(FileList);
                DirectoryList.Add(DirectoryItem);
            }
            ProjectRoot.Add(DirectoryList);

            //Get Settings
            XElement SettingsRoot = new XElement(XML_FILE_SETTINGS_NODE);
            SettingsRoot.Add(
                new XElement("Common",
                    new XElement("ExistringFiles",
                        new XElement("AddTextToExistingFile", this._settings.Common.ExisitingFiles.AddTextToExistingFile),
                        new XElement("HandleExistingItem",
                            (int)this._settings.Common.ExisitingFiles.HandleExistingItem,
                            new XAttribute("PlainText", this._settings.Common.ExisitingFiles.HandleExistingItem)    // Add plain text attribute to make debugging easyer
                        )
                    )
                ),
                new XElement("ControleBackup",
                    new XElement("Action",
                        new XElement("CopyData", this._settings.ControleBackup.Action.CopyData),
                        new XElement("CountItemsAndBytes", this._settings.ControleBackup.Action.CountItemsAndBytes)
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

                //Check Fileversion
                if (!this.Project_FromXMLString_CheckVersion(inputProject.Attribute("Version").Value)) return false;
                //Read Directorys
                foreach (XElement DirectoryItem in inputProject.Element(XML_DIRECTORYS_ELEMENT_NAME).Elements(XML_DIRECTORY_ITEM_NAME))
                {
                    string DirectoryPath = DirectoryItem.Element(XML_DIRECTORY_ITEM_PATH_NAME).Value;
                    DirectoryScope DirectoryScope = (DirectoryScope)System.Convert.ToInt32(DirectoryItem.Element(XML_DIRECTORY_ITEM_SCOPE_NAME).Value);
                    this.DirectoryAdd(DirectoryPath, DirectoryScope);
                    //Read Files
                    foreach (XElement FileItem in DirectoryItem.Element(XML_FILES_ELEMENT_NAME).Elements())
                    {
                        string FilePath = FileItem.Element(XML_FILE_ITEM_PATH_NAME).Value;
                        this.FileAdd(DirectoryPath, FilePath);
                    }
                }

                //Read Settings
                //this._settings = new Settings.Settings();
                this._settings.RestrainChangedEvent = true;
                XElement Settings = inputProject.Element(XML_FILE_SETTINGS_NODE);
                {
                    XElement Common = Settings.Element("Common");
                    {
                        XElement ExistringFiles = Common.Element("ExistringFiles");
                        {
                            this._settings.Common.ExisitingFiles.AddTextToExistingFile = (string)ExistringFiles.Element("AddTextToExistingFile");
                            this._settings.Common.ExisitingFiles.HandleExistingItem = (HandleExistingFiles.HowToHandleExistingItem)(int)ExistringFiles.Element("HandleExistingItem");
                        }
                    }
                    XElement ControleBackup = Settings.Element("ControleBackup");
                    {
                        XElement Action = ControleBackup.Element("Action");
                        {
                            this._settings.ControleBackup.Action.CopyData = (bool)Action.Element("CopyData");
                            this._settings.ControleBackup.Action.CountItemsAndBytes = (bool)Action.Element("CountItemsAndBytes");
                        }
                        XElement Directory = ControleBackup.Element("Directory");
                        {
                            this._settings.ControleBackup.Directory.CreateDriveDirectroy = (bool)Directory.Element("CreateDriveDirectroy");
                            this._settings.ControleBackup.Directory.Path = (string)Directory.Element("Path");
                        }
                        XElement Logfile = ControleBackup.Element("Logfile");
                        {
                            this._settings.ControleBackup.Logfile.AutoPath = (bool)Logfile.Element("AutoPath");
                            this._settings.ControleBackup.Logfile.Create = (bool)Logfile.Element("Create");
                            this._settings.ControleBackup.Logfile.Path = (string)Logfile.Element("Path");
                        }
                    }
                    XElement ControleRestore = Settings.Element("ControleRestore");
                    {
                        XElement Action = ControleRestore.Element("Action");
                        {
                            this._settings.ControleRestore.Action.CopyData = (bool)Action.Element("CopyData");
                            this._settings.ControleRestore.Action.CountItemsAndBytes = (bool)Action.Element("CountItemsAndBytes");
                        }
                        XElement Directory = ControleRestore.Element("Directory");
                        {
                            this._settings.ControleRestore.Directory.CreateDriveDirectroy = (bool)Directory.Element("CreateDriveDirectroy");
                            this._settings.ControleRestore.Directory.Path = (string)Directory.Element("Path");
                            this._settings.ControleRestore.Directory.RestoreTargetPath = (string)Directory.Element("RestoreTargetPath");

                        }
                        XElement Logfile = ControleRestore.Element("Logfile");
                        {
                            this._settings.ControleRestore.Logfile.AutoPath = (bool)Logfile.Element("AutoPath");
                            this._settings.ControleRestore.Logfile.Create = (bool)Logfile.Element("Create");
                            this._settings.ControleRestore.Logfile.Path = (string)Logfile.Element("Path");
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
            List<string> FullCompatibleVersionList = Properties.Settings.Default.ProjectFile_VersionCompatibleNative.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (FileVersionList.Intersect(FullCompatibleVersionList).Count() > 0)
            {
                return true;    // Full Compatible Return true
            }

            // Fileversions they are compatible if the file would been converted
            List<string> ConvCompatibleVersionList = Properties.Settings.Default.ProjectFile_VersionCompatibleConvert.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (FileVersionList.Intersect(ConvCompatibleVersionList).Count() > 0)
            {
                //TODO: Add code to convert if necessary --> in future version
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