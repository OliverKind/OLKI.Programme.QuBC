/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
 * License:     LGPL
 * 
 * Desctiption:
 * Explorer TreeView for the application
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.src.MainForm
{
    /// <summary>
    /// Explorer TreeView for the application
    /// </summary>
    public partial class ExplorerTreeView : TreeView
    {
        #region Constants
        /// <summary>
        /// Should sub nodes been set if the image variant for a special node was changed
        /// </summary>
        private const bool SET_IMAGE_VARIANT_FOR_SUBNODES = false;
        #endregion

        #region Properties
        /// <summary>
        /// Directroylist with all directroys with states
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, Project.Project.DirectoryScope> DirectoryList { get; set; }

        /// <summary>
        /// Get or set if subdirectries shold add to an TreeView node, if it ist expanded
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("Shold subdirectries add to an TreeView node, if it ist expanded")]
        [DefaultValue(true)]
        [DisplayName("GetSubDirectoriesOnAfterExpandTreeNode")]
        public bool GetSubDirectoriesOnAfterExpandTreeNode { get; set; } = true;

        /// <summary>
        /// Get and set if system directories should been listed
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("Shold system directories been shown")]
        [DefaultValue(true)]
        [DisplayName("ShowSystemDirectories")]
        public bool ShowSystemDirectories { get; set; } = true;

        /// <summary>
        /// Tree Node that was selected at last
        /// </summary>
        private ExtendedTreeNode _lastSelectedNode;

        /// <summary>
        /// Get the Tree Node that was selected at last
        /// </summary>
        [Browsable(false)]
        public ExtendedTreeNode LastSelectedNode
        {
            get
            {
                return this._lastSelectedNode;
            }
        }

        /// <summary>
        /// Get and set if hidden directories should been listed
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("Shold hidden directories been shown")]
        [DefaultValue(true)]
        [DisplayName("ShowHiddenDirectories")]
        public bool ShowHiddenDirectories { get; set; } = true;

        /// <summary>
        /// Get and set if directories withoud access should been listed
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("Shold directories withoud access been shown")]
        [DefaultValue(true)]
        [DisplayName("ShowDirectoriesWithoutAccess")]
        public bool ShowDirectoriesWithoutAccess { get; set; } = true;
        #endregion

        #region Event override
        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            if (!this.GetSubDirectoriesOnAfterExpandTreeNode || e.Node == null)
            {
                base.OnAfterExpand(e);
                return;
            }
            this.LoadSubDirectories((ExtendedTreeNode)e.Node);
            base.OnAfterExpand(e);
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            this._lastSelectedNode = (ExtendedTreeNode)this.SelectedNode;
            base.OnAfterSelect(e);
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Create a new ExplorerTreeView instance
        /// </summary>
        public ExplorerTreeView()
        {
        }

        /// <summary>
        /// Clear treeview and add drive nodes
        /// </summary>
        public void LoadDrives()
        {
            this.Nodes.Clear();

            // Add root nodes
            foreach (DriveInfo Drive in DriveInfo.GetDrives().OrderBy(d => d.Name))
            {
                ExtendedTreeNode NewNode = new ExtendedTreeNode(new DirectoryInfo(Drive.Name));

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
                this.CreateDummyForSubDirectories(NewNode);
                this.Nodes.Add(NewNode);
            }
        }

        /// <summary>
        /// Check if the directroy shold been get for the TreeView
        /// </summary>
        /// <param name="Directory">Directroy to check if it should been shown in TreeView</param>
        /// <returns>True, if directroy shold been shown in TreeView</returns>
        private bool CheckShowDirectroyInTreeView(DirectoryInfo Directory)
        {
            // Check for directories withoud access
            if ((!OLKI.Tools.CommonTools.DirectoryAndFile.Directory.CheckAccess(Directory) && !this.ShowDirectoriesWithoutAccess)) return false;

            // Check for system directrories
            if ((Directory.Attributes & FileAttributes.System) == FileAttributes.System && !this.ShowSystemDirectories) return false;

            // Check for hidden directories
            if ((Directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden && !this.ShowHiddenDirectories) return false;

            // Return true if there is no reason not to show the directory
            return true;
        }

        /// <summary>
        /// Create dummy nodes if a node has not loaded sub directories
        /// </summary>
        /// <param name="treeNode"></param>
        private void CreateDummyForSubDirectories(ExtendedTreeNode treeNode)
        {
            try
            {
                if (treeNode.DirectoryInfo.Exists && treeNode.DirectoryInfo.GetDirectories().Length > 0)
                {
                    treeNode.Nodes.Add(new ExtendedTreeNode(treeNode.DirectoryInfo, true));
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Load sub directories for a directorie to TreeNode
        /// </summary>
        /// <param name="treeNode">TreeNode to load subdirectories</param>
        public void LoadSubDirectories(ExtendedTreeNode treeNode)
        {
            if (treeNode.IsDummy || !treeNode.DirectoryInfo.Exists) return;

            treeNode.Nodes.Clear();
            try
            {
                ExtendedTreeNode NewNode;
                foreach (DirectoryInfo Directory in treeNode.DirectoryInfo.GetDirectories().OrderBy(d => d.Name))
                {
                    if (this.CheckShowDirectroyInTreeView(Directory))
                    {
                        NewNode = new ExtendedTreeNode(Directory);
                        this.CreateDummyForSubDirectories(NewNode);
                        NewNode.BaseImageIndex = 16;
                        this.SetImageVariant(NewNode);
                        treeNode.Nodes.Add(NewNode);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        #region SearchTreeNode
        /// <summary>
        /// Search the tree node, that is corresponding that match to the directory
        /// </summary>
        /// <param name="directroy">Directroy to search the tree nodes</param>
        public ExtendedTreeNode SearchTreeNode(DirectoryInfo directroy)
        {
            return this.SearchTreeNode(directroy, this.Nodes);
        }
        /// <summary>
        /// Search the tree node, that is corresponding that match to the directory
        /// </summary>
        /// <param name="directroy">Directroy to search the tree nodes</param>
        /// <param name="nodes">TreeNodes to search fir the node</param>
        private ExtendedTreeNode SearchTreeNode(DirectoryInfo directroy, TreeNodeCollection nodes)
        {
            ExtendedTreeNode ReturnNode = null;
            foreach (ExtendedTreeNode TreeNode in nodes)
            {
                if (TreeNode.DirectoryInfo.FullName == directroy.FullName && !TreeNode.IsDummy)
                {
                    ReturnNode = TreeNode;
                }
                else if (TreeNode.Nodes.Count > 0)
                {
                    ReturnNode = this.SearchTreeNode(directroy, TreeNode.Nodes);
                }
                if (ReturnNode != null) break;
            }
            return ReturnNode;
        }
        #endregion

        #region SetImageVariant
        /// <summary>
        /// Set the image variant for all nodes in the TreeView, automatically by the directory list
        /// </summary>
        public void SetImageVariant()
        {
            this.SetImageVariantRecursive(this.Nodes);
        }
        /// <summary>
        /// Set the image variant for the TreeNode, that match to the given directroy path
        /// </summary>
        /// <param name="directroy">Directroy to search for to set the ImageVariant</param>
        public void SetImageVariant(DirectoryInfo directroy)
        {
            this.SetImageVariant(directroy, SET_IMAGE_VARIANT_FOR_SUBNODES);
        }
        /// <summary>
        /// Set the image variant for the TreeNode, that match to the given directroy path
        /// </summary>
        /// <param name="directroy">Directroy to search for to set the ImageVariant</param>
        /// <param name="setSubNodes">Specifies if SubNodes should been set too</param>
        public void SetImageVariant(DirectoryInfo directroy, bool setSubNodes)
        {
            ExtendedTreeNode TreeNode = this.SearchTreeNode(directroy, this.Nodes);
            if (TreeNode != null)
            {
                this.SetImageVariant(TreeNode);
                if (setSubNodes) this.SetImageVariantRecursive(TreeNode.Nodes);
            }
        }
        /// <summary>
        /// Set the image variant of the specified TreeNode, automatically by the directory list
        /// </summary>
        /// <param name="treeNode">TreeNode to search an set the image variant</param>
        public void SetImageVariant(ExtendedTreeNode treeNode)
        {
            if (treeNode.IsDummy) return;

            ExtendedTreeNode.CheckedState ImageVariant = ExtendedTreeNode.CheckedState.NotChecked; ;
            if (this.DirectoryList == null) this.DirectoryList = new Dictionary<string, Project.Project.DirectoryScope>();

            foreach (KeyValuePair<string, Project.Project.DirectoryScope> Directroy in this.DirectoryList)
            {
                if (Directroy.Key == treeNode.DirectoryInfo.FullName && !treeNode.IsDummy)
                {
                    switch (Directroy.Value)
                    {
                        case Project.Project.DirectoryScope.All:
                            ImageVariant = ExtendedTreeNode.CheckedState.Checked;
                            break;
                        case Project.Project.DirectoryScope.Nothing:
                            ImageVariant = ExtendedTreeNode.CheckedState.NotChecked;
                            break;
                        case Project.Project.DirectoryScope.Selected:
                            ImageVariant = ExtendedTreeNode.CheckedState.Intermediate;
                            break;
                        default:
                            ImageVariant = ExtendedTreeNode.CheckedState.Question;
                            break;
                    }
                    break;  // Exit foreach, because directroy was found
                }
            }
            this.SetImageVariant(treeNode, ImageVariant);
        }
        /// <summary>
        /// Set the specified ImageVariant to the specified TreeNode
        /// </summary>
        /// <param name="treeNode">TreeNode to set the ImageVariant</param>
        /// <param name="imageVariant">ImageVariant to set to TreeNode</param>
        public void SetImageVariant(ExtendedTreeNode treeNode, ExtendedTreeNode.CheckedState imageVariant)
        {
            if (treeNode.IsDummy) return;
            treeNode.ImageVariant = imageVariant;
        }

        /// <summary>
        /// Set the image variant of the specified TreeNodes and all sub nodes, automatically by the directory list
        /// </summary>
        /// <param name="nodes">TreeNodes to search an set the image variant</param>
        private void SetImageVariantRecursive(TreeNodeCollection nodes)
        {
            foreach (ExtendedTreeNode TreeNode in nodes)
            {
                if (!TreeNode.IsDummy) this.SetImageVariant(TreeNode);
                if (TreeNode.Nodes.Count > 0) this.SetImageVariantRecursive(TreeNode.Nodes);
            }
        }
        #endregion
        #endregion
    }
}