/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
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

using OLKI.Programme.QBC.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.MainForm
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
        private const BackupProject.Project.DirectoryScope DEFAULT_ADD_DIRECTROY_TO_PROJECT_DIRECTORY_SCOPE = BackupProject.Project.DirectoryScope.Selected;
        /// <summary>
        /// Specifies the default scope to set for a directroy that has been added to project, because an sub directroy of this directroy was added to the project
        /// </summary>
        private const BackupProject.Project.DirectoryScope DEFAULT_ADD_DIRECTROY_TO_PROJECT_SCOPE_IF_NOT_IN_PROJECT = QBC.BackupProject.Project.DirectoryScope.Selected;
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
            return this.DirectoryContentListView_GetDirectoryScope(path) != BackupProject.Project.DirectoryScope.Nothing;
        }
        #endregion

        #region DirectoryContentListView_GetDirectoryScope
        /// <summary>
        /// Get the Scope of the specified directory from project data
        /// </summary>
        /// <param name="directory">Specifies the directory to check</param>
        /// <returns>The scope of  the specified directory</returns>
        internal BackupProject.Project.DirectoryScope DirectoryContentListView_GetDirectoryScope(System.IO.DirectoryInfo directory)
        {
            return this.DirectoryContentListView_GetDirectoryScope(directory.FullName);
        }
        /// <summary>
        /// Get the Scope of the specified directory from project data
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <returns>The scope of  the specified directory</returns>
        internal BackupProject.Project.DirectoryScope DirectoryContentListView_GetDirectoryScope(string path)
        {
            if (this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(path))
            {
                return this._projectManager.ActiveProject.ToBackupDirectorys[path];
            }
            return BackupProject.Project.DirectoryScope.Nothing;
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
            return false;
        }
        #endregion

        /// <summary>
        /// List the sub directories and files of the specified directroy to the directroy contenct list
        /// </summary>
        /// <param name="directory">Specifies the directory to lust sub directories and files</param>
        /// <param name="directoryContentList">Specifies the LitView where the content should been listet to</param>
        internal void DirectoryContentListView_ListDirectoryItems(DirectoryInfo directory, ListView directoryContentList)
        {
            try
            {
                directoryContentList.BeginUpdate();
                directoryContentList.Items.Clear();

                foreach (DirectoryInfo Directory in new DirectoryInfo(directory.FullName).GetDirectories().OrderBy(o => o.Name))
                {
                    if ((Tools.CommonTools.DirectoryAndFile.Directory.CheckAccess(Directory) || Settings.Default.ListItems_ShowWithoutAccess) && (Settings.Default.ListItems_ShowSystem || (Directory.Attributes & FileAttributes.System) != FileAttributes.System))
                    {
                        ListViewItem NewItem = new ListViewItem
                        {
                            Checked = this.DirectoryContentListView_GetDirectoryChecked(Directory),
                            ImageIndex = 16 + (int)this.DirectoryContentListView_GetCheckState(Directory.FullName),
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
                    ListViewItem NewItem = new ListViewItem
                    {
                        Checked = this.DirectoryContentListView_GetFileChecked(directory, File),
                        ImageIndex = 20,
                        Tag = File,
                        Text = File.Name
                    };
                    NewItem.SubItems[0].Tag = "F_" + File.Name;
                    NewItem.SubItems.Add(Tools.CommonTools.DirectoryAndFile.FileSize.Convert(File));
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
                System.Diagnostics.Debug.Print(ex.Message);
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
                    case BackupProject.Project.DirectoryScope.Nothing:
                        return ExtendedTreeNode.CheckedState.NotChecked;
                    case BackupProject.Project.DirectoryScope.Selected:
                        return ExtendedTreeNode.CheckedState.Intermediate;
                    case BackupProject.Project.DirectoryScope.All:
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
        /// <param name="treeNode">Specifies the ExplorerTreeViewNode associated with the directory</param>
        internal void Project_AddDirectorysToProject(DirectoryInfo directory)
        {
            this.Project_AddDirectorysToProject(directory, DEFAULT_ADD_DIRECTROY_TO_PROJECT_DIRECTORY_SCOPE, false);
        }
        /// <summary>
        /// Add or overwrite the specified directroy to the project, using a specified scope and set the associated ExplorerTreeViewNode
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        /// <param name="treeNode">Specifies the ExplorerTreeViewNode associated with the directory</param>
        /// <param name="scope">Specifies the scope of the specified directroy to add to project</param>
        internal void Project_AddDirectorysToProject(DirectoryInfo directory, BackupProject.Project.DirectoryScope scope)
        {
            this.Project_AddDirectorysToProject(directory, scope, true);
        }
        /// <summary>
        /// Add or overwrite the specified directroy to the project, using a specified scope or keep existing scope and set the associated ExplorerTreeViewNode
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        /// <param name="treeNode">Specifies the ExplorerTreeViewNode associated with the directory</param>
        /// <param name="scope">Specifies the scope of the specified directroy to add to project</param>
        /// <param name="overwriteExistingScope">Specifies if the scope of the directroy should been overwritte if directroy is allready in project</param>
        private void Project_AddDirectorysToProject(DirectoryInfo directory, BackupProject.Project.DirectoryScope scope, bool overwriteExistingScope)
        {
            if (directory == null) return;

            if (this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(directory.FullName))
            {
                //Add directory to ToDo list
                this._projectManager.ActiveProject.ToBackupDirectorys[directory.FullName] = BackupProject.Project.DirectoryScope.Selected;
            }
            else
            {
                //Add directory to ToDo list and set "todo" to "Selected" by default
                this._projectManager.ActiveProject.DirectoryAdd(directory, DEFAULT_ADD_DIRECTROY_TO_PROJECT_SCOPE_IF_NOT_IN_PROJECT);
            }

            // Overwrite scope if requested, otherwise keep old scope
            if (overwriteExistingScope)
            {
                this._projectManager.ActiveProject.ToBackupDirectorys[directory.FullName] = scope;
            }

            // Set parents
            this.Project_AddDirectorysToProject(directory.Parent);
        }
        #endregion

        #region Project_RemoveDirectorysFromBackup
        /// <summary>
        /// Removes the specified directroy and all sub directrories and files from project and set the asspcoated ExplorerTreeViewNodes
        /// </summary>
        /// <param name="directory">Specifies the directroy to remove</param>
        internal void Project_RemoveDirectorysFromBackup(DirectoryInfo directory)
        {
            foreach (KeyValuePair<string, QBC.BackupProject.Project.DirectoryScope> directoryItem in new Dictionary<string, BackupProject.Project.DirectoryScope>(this._projectManager.ActiveProject.ToBackupDirectorys).Where(A => A.Key.StartsWith(directory.FullName)))
            {
                this._projectManager.ActiveProject.DirectoryRemove(directoryItem.Key);
            }
            this._projectManager.ActiveProject.Changed = true;
        }
        #endregion
        #endregion
        #endregion
    }
}