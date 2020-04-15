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
    public class ExtendedTreeNode : System.Windows.Forms.TreeNode
    {
        #region Constants
        /// <summary>
        /// Specifies the default variant of the Icon for this TreeNode
        /// </summary>
        private const CheckedState DEFAULT_IMAGE_VARIANT = CheckedState.Question;
        /// <summary>
        /// Specifies the default  base icon of this TreeNode
        /// </summary>
        private const int DEFAULT_BASE_ICON = -1;
        /// <summary>
        /// The format if a directroy is a drive
        /// </summary>
        private const string DRIVE_NAME_FORMAT = @"{0} ({1})";
        /// <summary>
        /// Text to display for dummy tree nodes
        /// </summary>
        private const string DUMMY_IDENTIFIER = @"\\DUMMY//";
        #endregion

        #region Enums
        /// <summary>
        /// Represents the check state variant of a ExtendedTreeNode icon
        /// </summary>
        public enum CheckedState
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
        /// Specifies the variant of the Icon for this TreeNode
        /// </summary>
        private CheckedState _imageVariant = DEFAULT_IMAGE_VARIANT;
        /// <summary>
        /// Get or set the variant of the Icon for this TreeNode
        /// </summary>
        public CheckedState ImageVariant
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
        /// Indicades if the node is an dummy node, for more sub nodes at an paren note
        /// </summary>
        private readonly bool _isDummy = false;

        /// <summary>
        /// Get if the node is an dummy node, for more sub nodes at an paren note
        /// </summary>
        public bool IsDummy
        {
            get
            {
                return this._isDummy;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new ExtendedTreeNode with a specified directroy
        /// </summary>
        /// <param name="directoryInfo">Specifies the directroy, associated to the tree node</param>
        public ExtendedTreeNode(DirectoryInfo directoryInfo) : this(directoryInfo, false)
        {
        }

        /// <summary>
        /// Initialise a new ExtendedTreeNode with a specified directroy
        /// </summary>
        /// <param name="directoryInfo">Specifies the directroy, associated to the tree node</param>
        /// <param name="isDummy">Specifies if the ExtendedTreeNode is used as dummy</param>
        public ExtendedTreeNode(DirectoryInfo directoryInfo, bool isDummy)
        {
            this._directoryInfo = directoryInfo;
            this._isDummy = isDummy;
            base.Text = this.GetNodeText(directoryInfo, isDummy);
        }

        /// <summary>
        /// Create the text of the TreeNode
        /// </summary>
        /// <param name="directoryInfo">The directory to create the name from</param>
        /// <param name="isDummy">Is the directroy a dummy directory</param>
        /// <returns>The text to name the directroy</returns>
        private string GetNodeText(DirectoryInfo directoryInfo, bool isDummy)
        {
            // Node is dummy
            if (isDummy) return DUMMY_IDENTIFIER;

            // Node is drive
            if (Tools.CommonTools.DirectoryAndFile.Path.IsDrive(directoryInfo))
            {
                DriveInfo Drive = new DriveInfo(directoryInfo.FullName);
                return string.Format(DRIVE_NAME_FORMAT, new object[] { Drive.IsReady ? Drive.VolumeLabel : string.Empty, Drive.Name });
            }

            // Node is directory
            return directoryInfo.Name;
        }
        #endregion
    }
}