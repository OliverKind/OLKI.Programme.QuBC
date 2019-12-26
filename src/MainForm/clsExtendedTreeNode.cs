/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Enhanced the TreeViewNode with an directroy info and and an semi automatic icon
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

using System.IO;

namespace OLKI.Programme.QBC.MainForm
{
    /// <summary>
    /// Enhanced the TreeViewNode with an directroy info and and an half automatic icon
    /// </summary>
    internal class ExtendedTreeNode : System.Windows.Forms.TreeNode
    {
        #region Constants
        /// <summary>
        /// Specifies the default variant of the Icon for this TreeNode
        /// </summary>
        private const ExtendedTreeNodeCheckedState DEFAULT_IMAGE_VARIANT = ExtendedTreeNodeCheckedState.Question;
        /// <summary>
        /// Specifies the default  base icon this TreeNode
        /// </summary>
        private const int DEFAULT_BASE_ICON = -1;
        #endregion

        #region Enums
        /// <summary>
        /// Represents the check state variant of a TreeNodePlus icon
        /// </summary>
        public enum ExtendedTreeNodeCheckedState
        {
            /// <summary>
            /// Check state icon is an questionmark icon
            /// </summary>
            Question = 0,
            /// <summary>
            /// Check state icon is an not checked icon
            /// </summary>
            NotChecked = 1,
            /// <summary>
            /// Check state icon is an checked icon
            /// </summary>
            Checked = 2,
            /// <summary>
            /// Check state icon is an intermediate icon
            /// </summary>
            Intermediate = 3,
        }
        #endregion

        #region Properties
        /// <summary>
        /// Specifies the base icon of this TreeNode
        /// </summary>
        private int _baseImageIndex = DEFAULT_BASE_ICON;
        /// <summary>
        /// Get or set the base icon of this TreeNode
        /// </summary>
        public int BaseImageIndex
        {
            get
            {
                return this._baseImageIndex;
            }
            set
            {
                this._baseImageIndex = value;
                base.ImageIndex = (int)(this._baseImageIndex + this._imageVariant);
                base.SelectedImageIndex = base.ImageIndex;
            }
        }

        /// <summary>
        /// Specifies the information of the directroy associated with this TreeNode
        /// </summary>
        private readonly DirectoryInfo _directoryInfo = null;
        /// <summary>
        /// Get the information of the directroy associated with this TreeNode
        /// </summary>
        public DirectoryInfo DirectoryInfo
        {
            get
            {
                return this._directoryInfo;
            }
        }

        /// <summary>
        /// Specifies the variant of the Icon for this TreeNode
        /// </summary>
        private ExtendedTreeNodeCheckedState _imageVariant = DEFAULT_IMAGE_VARIANT;
        /// <summary>
        /// Get or set the variant of the Icon for this TreeNode
        /// </summary>
        public ExtendedTreeNodeCheckedState ImageVariant
        {
            get
            {
                return this._imageVariant;
            }
            set
            {
                this._imageVariant = value;
                base.ImageIndex = (int)(this._baseImageIndex + this._imageVariant);
                base.SelectedImageIndex = base.ImageIndex;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new TreeNodePlus with a specified directroy
        /// </summary>
        /// <param name="directoryInfo">Specifies the directroy, associated to the tree node</param>
        public ExtendedTreeNode(DirectoryInfo directoryInfo)
        {
            this._directoryInfo = directoryInfo;
            base.Text = this._directoryInfo.FullName.Substring(this._directoryInfo.FullName.LastIndexOf(@"\") + 1);
        }
        #endregion
    }
}