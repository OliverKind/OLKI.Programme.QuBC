﻿/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Helper functions for the applicaiton main form
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.src.MainForm
{
    /// <summary>
    /// Tolls for handling the main form
    /// </summary>
    internal class MainFormHelper
    {
        #region Constants
        /// <summary>
        /// Specifies the default scope of directroy add or overwrite to project
        /// </summary>
        private const Project.Project.DirectoryScope DEFAULT_ADD_DIRECTROY_TO_PROJECT_DIRECTORY_SCOPE = Project.Project.DirectoryScope.Selected;
        /// <summary>
        /// Default check state to return by ExplorerTreeView_GetCheckStateDirectory if check state can not identified
        /// </summary>
        private const ExtendedTreeNode.CheckedState DEFAULT_CHECK_STATE = ExtendedTreeNode.CheckedState.NotChecked;
        #endregion

        #region Fields
        /// <summary>
        /// The application project manager to get access to the data of the active project
        /// </summary>
        private readonly ProjectManager _projectManager = null;
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new MainFormHelper object
        /// </summary>
        /// <param name="projectManager">The application project manager to get access to the data of the active project</param>
        internal MainFormHelper(ProjectManager projectManager)
        {
            this._projectManager = projectManager;
        }

        #region DirectoryContentListView
        #region DirectoryContentListView_GetDirectoryChecked
        /// <summary>
        /// Get if the specified directory has to be copied and checked in ListView from project data
        /// </summary>
        /// <param name="directory">Specifies the directory to check</param>
        /// <returns>True if the specified directory is listed to copy and  has to be checked in ListView</returns>
        internal bool DirectoryContentListView_GetDirectoryChecked(DirectoryInfo directory)
        {
            return this.DirectoryContentListView_GetDirectoryChecked(directory.FullName);
        }
        /// <summary>
        /// Get if the specified  directory has to be copied and checked in ListView from project data
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <returns>True if the specified directory is listed to copy and  has to be checked in ListView</returns>
        internal bool DirectoryContentListView_GetDirectoryChecked(string path)
        {
            return this.DirectoryContentListView_GetDirectoryScope(path) != Project.Project.DirectoryScope.Nothing;
        }
        #endregion

        #region DirectoryContentListView_GetDirectoryScope
        /// <summary>
        /// Get the Scope of the specified directory from project data
        /// </summary>
        /// <param name="directory">Specifies the directory to check</param>
        /// <returns>The scope of  the specified directory</returns>
        internal Project.Project.DirectoryScope DirectoryContentListView_GetDirectoryScope(System.IO.DirectoryInfo directory)
        {
            return this.DirectoryContentListView_GetDirectoryScope(directory.FullName);
        }
        /// <summary>
        /// Get the Scope of the specified directory from project data
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <returns>The scope of  the specified directory</returns>
        internal Project.Project.DirectoryScope DirectoryContentListView_GetDirectoryScope(string path)
        {
            if (this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(path))
            {
                return this._projectManager.ActiveProject.ToBackupDirectorys[path];
            }
            return this.DirectoryContentListView_GetDirectoryScope_FromParent(path);
        }

        /// <summary>
        /// Get the Scope of the specified directory at the parent directorys
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private Project.Project.DirectoryScope DirectoryContentListView_GetDirectoryScope_FromParent(string path)
        {
            DirectoryInfo Parent = new DirectoryInfo(path).Parent;
            if (Parent == null) return Project.Project.DirectoryScope.Nothing;

            if (this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(Parent.FullName))
            {
                if (this._projectManager.ActiveProject.ToBackupDirectorys[Parent.FullName] == Project.Project.DirectoryScope.All)
                {
                    return Project.Project.DirectoryScope.All;
                }
                return Project.Project.DirectoryScope.Nothing;
            }

            return this.DirectoryContentListView_GetDirectoryScope_FromParent(Parent.FullName);
        }
        #endregion

        #region DirectoryContentListView_GetFileChecked
        /// <summary>
        /// Get if the specified  file has to be copied and checked in ListView from project data
        /// </summary>
        /// <param name="directory">Specifies the directory where the specified file is located</param>
        /// <param name="file">Specifies the file to check</param>
        /// <returns>True if the specified file is listed to copy and  has to be checked in ListView</returns>
        internal bool DirectoryContentListView_GetFileChecked(DirectoryInfo directory, FileInfo file)
        {
            return this.DirectoryContentListView_GetFileChecked(directory.FullName, file.FullName);
        }
        /// <summary>
        /// Get if the specified  file has to be copied and checked in ListView from project data
        /// </summary>
        /// <param name="directory">Specifies the directory where the specified file is located</param>
        /// <param name="file">A string that specifies the path of the file to check</param>
        /// <returns>True if the specified file is listed to copy and  has to be checked in ListView</returns>
        internal bool DirectoryContentListView_GetFileChecked(DirectoryInfo directory, string file)
        {
            return this.DirectoryContentListView_GetFileChecked(directory.FullName, file);
        }
        /// <summary>
        /// Get if the specified  file has to be copied and checked in ListView from project data
        /// </summary>
        /// <param name="directory">A string that specifies the path of the directory where the specified file is located</param>
        /// <param name="file">Specifies the file to check</param>
        /// <returns>True if the specified file is listed to copy and  has to be checked in ListView</returns>
        internal bool DirectoryContentListView_GetFileChecked(string directory, FileInfo file)
        {
            return this.DirectoryContentListView_GetFileChecked(directory, file.FullName);
        }
        /// <summary>
        /// Get if the specified  file has to be copied and checked in ListView from project data
        /// </summary>
        /// <param name="directory">A string that specifies the path of the directory where the specified file is located</param>
        /// <param name="file">A string that specifies the path of the file to check</param>
        /// <returns>True if the specified file is listed to copy and  has to be checked in ListView</returns>
        internal bool DirectoryContentListView_GetFileChecked(string directory, string file)
        {
            if (this._projectManager.ActiveProject.ToBackupFiles.ContainsKey(directory))
            {
                return this._projectManager.ActiveProject.ToBackupFiles[directory].Contains(file);
            }
            else if (this.DirectoryContentListView_GetDirectoryScope(directory) == Project.Project.DirectoryScope.All)
            {
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Add the System Icon for the definded File to an ImageList. File-Extension is used as key.
        /// </summary>
        /// <param name="file">Path to a file to get the System Icon from</param>
        /// <param name="iconList">List to save Icon to</param>
        private void AddFileIconToList(FileInfo file, ImageList iconList)
        {
            if (!iconList.Images.ContainsKey(file.Extension)) iconList.Images.Add(file.Extension, this.GetIcon(file));
        }

        /// <summary>
        /// Get the Sytem Icon for the definded file
        /// </summary>
        /// <param name="file">Path to a file to get the System Icon from</param>
        /// <returns>Icon for the definded File</returns>
        private Icon GetIcon(FileInfo file)
        {
            return Icon.ExtractAssociatedIcon(file.FullName);
        }

        /// <summary>
        /// List the sub directories and files of the specified directroy to the directroy contenct list
        /// </summary>
        /// <param name="directory">Specifies the directory to lust sub directories and files</param>
        /// <param name="directoryContentList">Specifies the LitView where the content should been listet to</param>
        /// <param name="iconList">Imagelist with icons for files and directories</param>
        internal void DirectoryContentListView_ListDirectoryItems(DirectoryInfo directory, ListView directoryContentList, ImageList iconList)
        {
            try
            {
                directoryContentList.BeginUpdate();
                directoryContentList.Items.Clear();

                foreach (DirectoryInfo Directory in new DirectoryInfo(directory.FullName).GetDirectories().OrderBy(o => o.Name))
                {
                    if ((OLKI.Toolbox.DirectoryAndFile.Directory.CheckAccess(Directory) || Settings.Default.ListItems_ShowWithoutAccess) && (Settings.Default.ListItems_ShowSystem || (Directory.Attributes & FileAttributes.System) != FileAttributes.System))
                    {
                        ListViewItem NewItem = new ListViewItem
                        {
                            Checked = this.DirectoryContentListView_GetDirectoryChecked(Directory),
                            ImageIndex = (int)this.DirectoryContentListView_GetCheckState(Directory.FullName),
                            Tag = Directory,
                            Text = Directory.Name
                        };
                        NewItem.SubItems[0].Tag = "D_" + Directory.Name;
                        NewItem.SubItems.Add(string.Empty);
                        NewItem.SubItems[1].Tag = null;
                        NewItem.SubItems.Add(Directory.LastWriteTime.ToString());
                        NewItem.SubItems[1].Tag = Directory.LastWriteTime;
                        directoryContentList.Items.Add(NewItem);
                    }
                }

                foreach (FileInfo File in new DirectoryInfo(directory.FullName).GetFiles().OrderBy(o => o.Name))
                {
                    this.AddFileIconToList(File, iconList);

                    ListViewItem NewItem = new ListViewItem
                    {
                        Checked = this.DirectoryContentListView_GetFileChecked(directory, File),
                        ImageKey = File.Extension,
                        Tag = File,
                        Text = File.Name
                    };
                    NewItem.SubItems[0].Tag = "F_" + File.Name;
                    NewItem.SubItems.Add(OLKI.Toolbox.DirectoryAndFile.FileSize.Convert(File));
                    NewItem.SubItems[1].Tag = File.Length;
                    NewItem.SubItems.Add(File.LastWriteTime.ToString());
                    NewItem.SubItems[2].Tag = File.LastWriteTime;
                    directoryContentList.Items.Add(NewItem);
                }
            }
            catch (IOException ex)
            {
                // Show Exception Message if directroy is not an drive
                if (directory.Name.IndexOf(@"\") == 0)
                {
                    MessageBox.Show(string.Format(Stringtable._0x0005m, new object[] { ex.Message }), Stringtable._0x0005c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            finally
            {
                directoryContentList.EndUpdate();
            }
        }

        /// <summary>
        /// Get the check state of a ListViewItem by a specified directory from project data and returns the defaukt check state can not identified
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <returns>The checked state of the specified directory</returns>
        private ExtendedTreeNode.CheckedState DirectoryContentListView_GetCheckState(string path)
        {
            if (this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(path))
            {
                switch (this._projectManager.ActiveProject.ToBackupDirectorys[path])
                {
                    case Project.Project.DirectoryScope.Nothing:
                        return ExtendedTreeNode.CheckedState.NotChecked;
                    case Project.Project.DirectoryScope.Selected:
                        return ExtendedTreeNode.CheckedState.Intermediate;
                    case Project.Project.DirectoryScope.All:
                        return ExtendedTreeNode.CheckedState.Checked;
                    default:
                        return DEFAULT_CHECK_STATE;
                }
            }
            return DEFAULT_CHECK_STATE;
        }
        #endregion

        #region Project
        #region Project_AddDirectorysToProject
        /// <summary>
        /// Add or overwrite the specified directroy to the project, using a default scope and set the associated ExplorerTreeViewNode
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        internal void Project_AddDirectoryToProject(DirectoryInfo directory)
        {
            this.Project_AddDirectoryToProject(directory, DEFAULT_ADD_DIRECTROY_TO_PROJECT_DIRECTORY_SCOPE);
        }
        /// <summary>
        /// Add or overwrite the specified directroy to the project, using a specified scope and set the associated ExplorerTreeViewNode
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        /// <param name="scope">Specifies the scope of the specified directroy to add to project</param>
        internal void Project_AddDirectoryToProject(DirectoryInfo directory, Project.Project.DirectoryScope scope)
        {
            if (directory == null) return;

            if (!this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(directory.FullName))
            {
                //Add directory to ToDo list and set scope
                this._projectManager.ActiveProject.DirectoryAdd(directory, scope);
            }
            else
            {
                //Set new scope
                this._projectManager.ActiveProject.ToBackupDirectorys[directory.FullName] = scope;
            }
            this.Project_AddParentDirectoriesToProject(directory);
        }
        #endregion

        #region Project_AddParentDirectoriesToProject
        /// <summary>
        /// Add parent directories to the project, if they are not in the project
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        internal void Project_AddParentDirectoriesToProject(DirectoryInfo directory)
        {
            if (directory == null || directory.Parent == null) return;

            if (!this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(directory.Parent.FullName))
            {
                //Add directory to ToDo list and set scope
                this._projectManager.ActiveProject.DirectoryAdd(directory.Parent, Project.Project.DirectoryScope.Selected);
                Project_AddParentDirectoriesToProject(directory.Parent);
            }
        }
        #endregion

        #region Project_RemoveDirectorysFromBackup
        /// <summary>
        /// Removes the specified directroy and all sub directrories and files from project and set the asspcoated ExplorerTreeViewNodes
        /// </summary>
        /// <param name="directory">Specifies the directroy to remove</param>
        internal void Project_RemoveDirectorysFromBackup(DirectoryInfo directory)
        {
            foreach (KeyValuePair<string, Project.Project.DirectoryScope> directoryItem in new Dictionary<string, Project.Project.DirectoryScope>(this._projectManager.ActiveProject.ToBackupDirectorys).Where(A => A.Key.StartsWith(directory.FullName)))
            {
                // Remove all directories starting with directroy path
                this._projectManager.ActiveProject.DirectoryRemove(directoryItem.Key);
            }
            this._projectManager.ActiveProject.Changed = true;
        }


        internal void Project_RemoveSubDirectorysAndFilesFromBackup(DirectoryInfo directory)
        {
            foreach (KeyValuePair<string, Project.Project.DirectoryScope> directoryItem in new Dictionary<string, Project.Project.DirectoryScope>(this._projectManager.ActiveProject.ToBackupDirectorys))
            {
                if (!directoryItem.Key.Equals(directory.FullName, StringComparison.OrdinalIgnoreCase) && directoryItem.Key.Contains(directory.FullName))
                {
                    this._projectManager.ActiveProject.DirectoryRemove(directoryItem.Key);
                }
            }

            foreach (KeyValuePair<string, List<string>> fileItem in new Dictionary<string, List<string>>(this._projectManager.ActiveProject.ToBackupFiles))
            {
                if (fileItem.Key.Contains(directory.FullName))
                {
                    this._projectManager.ActiveProject.ToBackupFiles.Remove(fileItem.Key);
                }
            }
            this._projectManager.ActiveProject.ToggleProjectChanged(this, new EventArgs());
        }
        #endregion
        #endregion
        #endregion
    }
}