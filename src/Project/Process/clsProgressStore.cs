/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OLKI.Programme.QBC.BackupProject.Process
{
    public class ProgressStore
    {
        #region Properties
        public ProgressElement TotalFiles { get; set; } = new ProgressElement();
        public ProgressElement TotalDirectories { get; set; } = new ProgressElement();
        public ProgressElement TotalBytes { get; set; } = new ProgressElement();
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
        public ProgressElement DirectroyFiles { get; set; } = new ProgressElement();
        public ProgressElement FileBytes { get; set; } = new ProgressElement();

        public ProcessException Exception { get; set; } = new ProcessException();
        #endregion

        #region Methodes
        public void Initial()
        {
            this.TotalBytes = new ProgressElement();
            this.TotalDirectories = new ProgressElement();
            this.TotalFiles = new ProgressElement();
            this.DirectroyFiles = new ProgressElement();
        }
        #endregion

        #region Subclasses
        public class ProgressElement
        {
            #region Properties
            public long? ActualValue { get; set; } = null;
            public long? MaxValue { get; set; } = null;
            public string ElemenName { get; set; } = string.Empty;

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