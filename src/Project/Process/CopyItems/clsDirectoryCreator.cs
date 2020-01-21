/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Create directorys
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

using OLKI.Programme.QBC.MainForm.Usercontroles.uscProcControle;
using OLKI.Programme.QBC.Properties;
using OLKI.Tools.CommonTools.DirectoryAndFile;
using System;
using System.ComponentModel;
using System.IO;

namespace OLKI.Programme.QBC.BackupProject.Process
{
    /// <summary>
    /// Provides tools to write logfiles
    /// </summary>
    internal class DirectoryCreator
    {
        #region Methodes
        /// <summary>
        /// Create the target dirextroy for an backup or to restpore
        /// </summary>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="targetDirectory">Target directroy to create</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="progress">Progress element for the process</param>
        /// <param name="exception">Exception of the process</param>
        /// <param name="copyItemClass">CcopyItem to use the GetFullDiscExceptionReturnCode function</param>
        /// <returns>Exception level of the copy process</returns>
        public ProcessException.ExceptionLevel TargetDirectory(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, ProgressStore progress, out Exception exception, CopyItems copyItemClass)
        {
            exception = null;
            try
            {
                if (!targetDirectory.Exists && !Tools.CommonTools.DirectoryAndFile.Path.IsDrive(targetDirectory))
                {
                    targetDirectory.Create();
                    HandleAttributes.Direcotry.Remove(targetDirectory);
                }
                return ProcessException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x001D,
                    Exception = ex,
                    Level = copyItemClass.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Slight),
                    Source = sourceDirectory.FullName,
                    Target = targetDirectory.FullName
                };
                progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

                return Exception.Level;
            }
        }

        /// <summary>
        /// Get the corresponding target path for target directroy, depending of the given source directroy and mode
        /// </summary>
        /// <param name="copyMode">Mode to get the target path</param>
        /// <param name="sourceDirectory">Source directroy to get the target forc</param>
        /// <param name="projectSettings">Settings of the project</param>
        /// <returns>Full target name for the directroy to create</returns>
        public string GetTargetFullName(CopyItems.CopyMode copyMode, DirectoryInfo sourceDirectory, Settings.Settings projectSettings)
        {
            string RootSegment;
            string DriveNameSegment;
            string SourceSegment;
            switch (copyMode)
            {
                case CopyItems.CopyMode.Backup:
                    RootSegment = projectSettings.ControleBackup.Directory.Path;
                    DriveNameSegment = projectSettings.ControleBackup.Directory.CreateDriveDirectroy ? sourceDirectory.Root.FullName.Remove(1, 2) : "";
                    SourceSegment = sourceDirectory.FullName.Remove(0, sourceDirectory.Root.FullName.Length);
                    return this.BuildTargetFullNameResult(RootSegment, DriveNameSegment, SourceSegment);
                case CopyItems.CopyMode.Restore:
                    //TODO: ADD CODE --> in future version to restore Backup
                    //Use drive letter or use settings
                    RootSegment = "";
                    DriveNameSegment = "";
                    SourceSegment = "";
                    return RootSegment + DriveNameSegment + SourceSegment;
                default:
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// Build up the target path, of severel path segments
        /// </summary>
        /// <param name="rootSegment">Root backup target</param>
        /// <param name="driveNameSegment">Source drive name</param>
        /// <param name="sourceSegment">Source path witoud drive name</param>
        /// <returns>Combined target path</returns>
        private string BuildTargetFullNameResult(string rootSegment, string driveNameSegment, string sourceSegment)
        {
            string Result = "";
            Result += rootSegment;
            Result += driveNameSegment.Length > 0 ? @"\" + driveNameSegment : "";
            Result += sourceSegment.Length > 0 ? @"\" + sourceSegment : "";

            return Result;
        }
        #endregion
    }
}