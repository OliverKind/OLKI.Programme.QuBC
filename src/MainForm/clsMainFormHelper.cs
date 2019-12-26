/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
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
        private const ExtendedTreeNode.ExtendedTreeNodeCheckedState DEFAULT_CHECK_STATE = OLKI.Programme.QBC.MainForm.ExtendedTreeNode.ExtendedTreeNodeCheckedState.NotChecked;
        /// <summary>
        /// Default Explorer TreeVie icon
        /// </summary>
        private const int DFAULT_INITIAL_ICON = 0;
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
        /// <param name="irectoryContentList">Specifies the LitView where the content should been listet to</param>
        internal void DirectoryContentListView_ListDirectoryItems(DirectoryInfo directory, ListView irectoryContentList)
        {
            try
            {
                irectoryContentList.Items.Clear();
                foreach (DirectoryInfo Directory in new DirectoryInfo(directory.FullName).GetDirectories().OrderBy(o => o.Name))
                {
                    if ((Tools.CommonTools.DirectoryAndFile.Directory.CheckAccess(Directory) || Settings.Default.ListItems_ShowDirectorysWithoutAccess) && (Settings.Default.ListItems_ShowSystemDirectory || (Directory.Attributes & FileAttributes.System) != FileAttributes.System))
                    {
                        ListViewItem NewItem = new ListViewItem
                        {
                            Checked = this.DirectoryContentListView_GetDirectoryChecked(Directory),
                            ImageIndex = (int)this.ExplorerTreeView_GetCheckStateDirectoryIcon(Directory, 16),
                            Tag = Directory,
                            Text = Directory.Name
                        };
                        NewItem.SubItems[0].Tag = "D_" + Directory.Name;
                        NewItem.SubItems.Add(string.Empty);
                        NewItem.SubItems[1].Tag = null;
                        NewItem.SubItems.Add(Directory.LastWriteTime.ToString());
                        NewItem.SubItems[1].Tag = Directory.LastWriteTime;
                        irectoryContentList.Items.Add(NewItem);
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
                    irectoryContentList.Items.Add(NewItem);
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
        }
        #endregion

        #region ExplorerTreeView
        /// <summary>
        /// Reads the directroy, represents by the specified node, an add sub nodes like the sub directory of the selected directroy to the specified node
        /// </summary>
        /// <param name="parentNode">Specifies the tree node to read an add sub nodes</param>
        internal void ExplorerTreeView_AddSubNotes(ExtendedTreeNode parentNode)
        {
            try
            {
                foreach (DirectoryInfo Directory in new DirectoryInfo(parentNode.DirectoryInfo.FullName).GetDirectories().OrderBy(o => o.Name))
                {
                    try
                    {
                        if ((Tools.CommonTools.DirectoryAndFile.Directory.CheckAccess(Directory) || Settings.Default.ListItems_ShowDirectorysWithoutAccess) && (Settings.Default.ListItems_ShowSystemDirectory || (Directory.Attributes & FileAttributes.System) != FileAttributes.System))
                        {
                            ExtendedTreeNode NewNode = new ExtendedTreeNode(Directory)
                            {
                                BaseImageIndex = 16,
                                ImageVariant = this.ExplorerTreeView_GetCheckStateDirectoryIcon(Directory)
                            };
                            parentNode.Nodes.Add(NewNode);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Print(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        #region ExplorerTreeView_GetCheckStateDirectory
        /// <summary>
        /// Get the check state of a tree view item by a specified directory from project data and returns a unknown as check state if check state can not identified
        /// </summary>
        /// <param name="directory">Specifies the directory to check</param>
        /// <returns>The checked state of the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectory(DirectoryInfo directory)
        {
            return this.ExplorerTreeView_GetCheckStateDirectory(directory.FullName, DEFAULT_CHECK_STATE);
        }
        /// <summary>
        /// Get the check state of a tree view item by a specified directory from project data and returns a specified check state if check state can not identified
        /// </summary>
        /// <param name="directory">Specifies the directory to check</param>
        /// <param name="defaultReturn">Check state to return if check state can not identified</param>
        /// <returns>The checked state of the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectory(DirectoryInfo directory, ExtendedTreeNode.ExtendedTreeNodeCheckedState defaultReturn)
        {
            return this.ExplorerTreeView_GetCheckStateDirectory(directory.FullName, defaultReturn);
        }
        /// <summary>
        /// Get the check state of a tree view item by a specified directory from project data and returns a unknown as check state if check state can not identified
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <returns>The checked state of the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectory(string path)
        {
            return this.ExplorerTreeView_GetCheckStateDirectory(path, DEFAULT_CHECK_STATE);
        }
        /// <summary>
        /// Get the check state of a tree view item by a specified directory from project data and returns a specified check state if check state can not identified
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <param name="defaultReturn">Check state to return if check state can not identified</param>
        /// <returns>The checked state of the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectory(string path, ExtendedTreeNode.ExtendedTreeNodeCheckedState defaultReturn)
        {
            if (this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(path))
            {
                switch (this._projectManager.ActiveProject.ToBackupDirectorys[path])
                {
                    case BackupProject.Project.DirectoryScope.Nothing:
                        return ExtendedTreeNode.ExtendedTreeNodeCheckedState.NotChecked;
                    case BackupProject.Project.DirectoryScope.Selected:
                        return ExtendedTreeNode.ExtendedTreeNodeCheckedState.Intermediate;
                    case BackupProject.Project.DirectoryScope.All:
                        return ExtendedTreeNode.ExtendedTreeNodeCheckedState.Checked;
                    default:
                        return defaultReturn;
                }
            }
            return defaultReturn;
        }
        #endregion

        #region ExplorerTreeView_GetCheckStateDirectoryIcon
        /// <summary>
        /// Get the icon with check state to set for the specified directory in ExplorerTreeView, using a default icon
        /// </summary>
        /// <param name="directory">Specifies the directory to check</param>
        /// <returns>The icon to set in tree view for the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectoryIcon(DirectoryInfo directory)
        {
            return this.ExplorerTreeView_GetCheckStateDirectoryIcon(directory.FullName);
        }
        /// <summary>
        /// Get the icon with check state to set for the specified directory in ExplorerTreeView, using a initial icon
        /// </summary>
        /// <param name="directory">Specifies the directory to check</param>
        /// <returns>The icon to set in tree view for the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectoryIcon(DirectoryInfo directory, int initalIcon)
        {
            return this.ExplorerTreeView_GetCheckStateDirectoryIcon(directory.FullName, initalIcon);
        }
        /// <summary>
        /// Get the icon with check state to set for the specified directory in ExplorerTreeView, using a default icon
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <returns>The icon to set in tree view for the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectoryIcon(string path)
        {
            return this.ExplorerTreeView_GetCheckStateDirectoryIcon(path, DFAULT_INITIAL_ICON);
        }
        /// <summary>
        /// Get the icon with check state to set for the specified directory in ExplorerTreeView, using a initial icon
        /// </summary>
        /// <param name="path">A string that specifies the path of the directory to check</param>
        /// <returns>The icon to set in tree view for the specified directory</returns>
        internal ExtendedTreeNode.ExtendedTreeNodeCheckedState ExplorerTreeView_GetCheckStateDirectoryIcon(string path, int initalIcon)
        {
            return initalIcon + this.ExplorerTreeView_GetCheckStateDirectory(path);
        }
        #endregion

        #region ExplorerTreeView_InitialTreeView
        /// <summary>
        /// Clears the explorer TreeView and add root nodes with syste, drives
        /// </summary>
        internal void ExplorerTreeView_InitialTreeView(TreeView explorerTreeView)
        {
            explorerTreeView.Nodes.Clear();
            foreach (DriveInfo Drive in DriveInfo.GetDrives().OrderBy(o => o.Name))
            {
                ExtendedTreeNode NewNode = new ExtendedTreeNode(new DirectoryInfo(Drive.Name))
                {
                    Text = string.Format("{0} ({1})", new object[] { Drive.IsReady ? Drive.VolumeLabel : string.Empty, Drive.Name })
                };

                switch (Drive.DriveType)
                {
                    case DriveType.Removable:
                        NewNode.BaseImageIndex = 0;
                        break;
                    case DriveType.CDRom:
                        NewNode.BaseImageIndex = 8;
                        break;
                    case DriveType.Network:
                        NewNode.BaseImageIndex = 12;
                        break;
                    default:
                        NewNode.BaseImageIndex = 4;
                        break;
                }
                NewNode.ImageVariant = this.ExplorerTreeView_GetCheckStateDirectoryIcon(Drive.Name);
                explorerTreeView.Nodes.Add(NewNode);
            }
        }
        #endregion

        #region ExplorerTreeView_SelectTreeViewItem
        /// <summary>
        /// Select the TreeView node in explorer TreeView by a specified directroy
        /// </summary>
        /// <param name="directory">A string that specifies the path of the directory to select the tree node in explorer TreeView</param>
        internal void ExplorerTreeView_SelectTreeViewItem(System.IO.DirectoryInfo directory, TreeNodeCollection treeNodes)
        {
            this.ExplorerTreeView_SelectTreeViewItem(directory.FullName, treeNodes);
        }
        /// <summary>
        /// Select the TreeView node in explorer TreeView by a specified directroy
        /// </summary>
        /// <param name="directoryPath">Specifies the directroy to select the TreeView node</param>
        internal void ExplorerTreeView_SelectTreeViewItem(string directoryPath, TreeNodeCollection treeNodes)
        {
            foreach (ExtendedTreeNode TreeNode in treeNodes)
            {
                if (TreeNode.DirectoryInfo.FullName == directoryPath)
                {
                    TreeNode.TreeView.SelectedNode = TreeNode;
                    return;
                }
                else
                {
                    this.ExplorerTreeView_SelectTreeViewItem(directoryPath, (TreeNodeCollection)TreeNode.Nodes);
                }
            }
        }
        #endregion

        /// <summary>
        /// Set the specified ExplorerTreeViewNode an all sub nodes to not checked
        /// </summary>
        /// <param name="treeNode"></param>
        private void ExplorerTreeView_SetTreeViewNodeAndSubNotesToNotChecked(ExtendedTreeNode treeNode)
        {
            treeNode.ImageVariant = ExtendedTreeNode.ExtendedTreeNodeCheckedState.NotChecked;
            foreach (ExtendedTreeNode TreeNode in treeNode.Nodes)
            {
                this.ExplorerTreeView_SetTreeViewNodeAndSubNotesToNotChecked(TreeNode);
            }
        }
        #endregion

        #region Project
        #region Project_AddDirectorysToProjectAndSetTree
        /// <summary>
        /// Add or overwrite the specified directroy to the project, using a default scope and set the associated ExplorerTreeViewNode
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        /// <param name="treeNode">Specifies the ExplorerTreeViewNode associated with the directory</param>
        internal void Project_AddDirectorysToProjectAndSetTree(DirectoryInfo directory, ExtendedTreeNode treeNode)
        {
            this.Project_AddDirectorysToProjectAndSetTree(directory, treeNode, DEFAULT_ADD_DIRECTROY_TO_PROJECT_DIRECTORY_SCOPE, false);
        }
        /// <summary>
        /// Add or overwrite the specified directroy to the project, using a specified scope and set the associated ExplorerTreeViewNode
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        /// <param name="treeNode">Specifies the ExplorerTreeViewNode associated with the directory</param>
        /// <param name="scope">Specifies the scope of the specified directroy to add to project</param>
        internal void Project_AddDirectorysToProjectAndSetTree(DirectoryInfo directory, ExtendedTreeNode treeNode, BackupProject.Project.DirectoryScope scope)
        {
            this.Project_AddDirectorysToProjectAndSetTree(directory, treeNode, scope, true);
        }
        /// <summary>
        /// Add or overwrite the specified directroy to the project, using a specified scope or keep existing scope and set the associated ExplorerTreeViewNode
        /// </summary>
        /// <param name="directory">Specifies the directory to add to project</param>
        /// <param name="treeNode">Specifies the ExplorerTreeViewNode associated with the directory</param>
        /// <param name="scope">Specifies the scope of the specified directroy to add to project</param>
        /// <param name="overwriteExistingScope">Specifies if the scope of the directroy should been overwritte if directroy is allready in project</param>
        private void Project_AddDirectorysToProjectAndSetTree(DirectoryInfo directory, ExtendedTreeNode treeNode, BackupProject.Project.DirectoryScope scope, bool overwriteExistingScope)
        {
            if (directory == null)
            {
                return;
            }
            //Add directory to ToDo list
            if (this._projectManager.ActiveProject.ToBackupDirectorys.ContainsKey(directory.FullName))
            {
                if (this._projectManager.ActiveProject.ToBackupDirectorys[directory.FullName] == BackupProject.Project.DirectoryScope.Nothing)
                {
                    this._projectManager.ActiveProject.ToBackupDirectorys[directory.FullName] = BackupProject.Project.DirectoryScope.Selected;
                }
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
            treeNode.ImageVariant = this.ExplorerTreeView_GetCheckStateDirectoryIcon(directory);

            // Set parents
            this.Project_AddDirectorysToProjectAndSetTree(directory.Parent, (ExtendedTreeNode)treeNode.Parent);
        }
        #endregion

        /// <summary>
        /// Removes the specified directroy and all sub directrories and files from project and set the asspcoated ExplorerTreeViewNodes
        /// </summary>
        /// <param name="directory">Specifies the directroy to remove</param>
        /// <param name="treeNode">Specifies the ExplorerTreeViewNode associated to the specified directroy</param>
        internal void Project_RemoveDirectorysFromBackupAndSetTree(DirectoryInfo directory, ExtendedTreeNode treeNode)
        {
            if (directory == null)
            {
                return;
            }
            this.ExplorerTreeView_SetTreeViewNodeAndSubNotesToNotChecked(treeNode);
            foreach (KeyValuePair<string, QBC.BackupProject.Project.DirectoryScope> directoryItem in new Dictionary<string, BackupProject.Project.DirectoryScope>(this._projectManager.ActiveProject.ToBackupDirectorys).Where(A => A.Key.StartsWith(directory.FullName)))
            {
                this._projectManager.ActiveProject.DirectoryRemove(directoryItem.Key);
            }
            this._projectManager.ActiveProject.Changed = true;
        }
        #endregion
        #endregion
    }
}