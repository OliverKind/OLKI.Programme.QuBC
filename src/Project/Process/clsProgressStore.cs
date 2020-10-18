/*
 * QBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
 * 
 * Desctiption:
 * A class to store the progess of an backup or restore process
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

namespace OLKI.Programme.QBC.src.Project.Process
{
    /// <summary>
    /// A class to store the progess of an backup or restore process
    /// </summary>
    public class ProgressStore
    {
        #region Properties
        /// <summary>
        /// Total files counted or copied
        /// </summary>
        public ProgressElement TotalFiles { get; set; } = new ProgressElement();

        /// <summary>
        /// Total directories counted or copied
        /// </summary>
        public ProgressElement TotalDirectories { get; set; } = new ProgressElement();
        /// <summary>
        /// Total bytes counted or copied
        /// </summary>
        public ProgressElement TotalBytes { get; set; } = new ProgressElement();

        /// <summary>
        /// Total bytes counted or copied
        /// </summary>
        public ProgressElement TotalItems
        {
            get
            {
                ProgressElement TotalItems = new ProgressElement
                {
                    ActualValue = this.TotalDirectories.ActualValue + TotalFiles.ActualValue,
                    MaxValue = this.TotalDirectories.MaxValue + TotalFiles.MaxValue
                };
                return TotalItems;
            }
        }

        /// <summary>
        /// Files in the actual copied directroy
        /// </summary>
        public ProgressElement DirectroyFiles { get; set; } = new ProgressElement();

        /// <summary>
        /// Bytes of the actual copied file
        /// </summary>
        public ProgressElement FileBytes { get; set; } = new ProgressElement();

        /// <summary>
        /// Exception while copy or count a item
        /// </summary>
        public ProcessException Exception { get; set; } = new ProcessException();
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new ProgressStore
        /// </summary>
        public ProgressStore()
        {
            this.TotalBytes = new ProgressElement();
            this.TotalDirectories = new ProgressElement();
            this.TotalFiles = new ProgressElement();
            this.DirectroyFiles = new ProgressElement();
        }

        /// <summary>
        /// Clone the ProgressStore
        /// </summary>
        /// <returns>The cloned ProgressStore</returns>
        public ProgressStore Clone()
        {
            ProgressStore ThisClone = (ProgressStore)this.MemberwiseClone();
            return ThisClone;
        }
        #endregion

        #region Subclasses
        /// <summary>
        /// A class to store the progess data of a single element
        /// </summary>
        public class ProgressElement
        {
            #region Properties
            /// <summary>
            /// Actual counted or copied value, Bytes for example
            /// </summary>
            public long? ActualValue { get; set; } = null;

            /// <summary>
            /// Maximum counted or copied value, Bytes for example
            /// </summary>
            public long? MaxValue { get; set; } = null;

            /// <summary>
            /// The name of the actual element, Filename for example
            /// </summary>
            public string ElemenName { get; set; } = string.Empty;

            /// <summary>
            /// The percentage value of the actual copy progress, Byte of an file for example
            /// </summary>
            public int Percentage
            {
                get
                {
                    int Percentage = 0;
                    if (this.MaxValue != null && this.ActualValue != null)
                    {
                        Percentage = (int)OLKI.Tools.CommonTools.Matehmatics.Percentages((long)this.ActualValue, (long)this.MaxValue);
                    }
                    if (Percentage > 100) Percentage = 100;
                    return Percentage;
                }
            }
            #endregion
        }
        #endregion
    }
}