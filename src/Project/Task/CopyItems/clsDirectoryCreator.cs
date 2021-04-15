/*
 * QuBC - QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2021
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

using OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscTaskControle;
using OLKI.Toolbox.DirectoryAndFile;
using System;
using System.ComponentModel;
using System.IO;

namespace OLKI.Programme.QuBC.src.Project.Task
{
    /// <summary>
    /// Provides tools to create target directorys
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
        public TaskException.ExceptionLevel TargetDirectory(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, ProgressStore progress, out Exception exception, CopyItems copyItemClass)
        {
            exception = null;
            try
            {
                if (!targetDirectory.Exists && !OLKI.Toolbox.DirectoryAndFile.Path.IsDrive(targetDirectory))
                {
                    targetDirectory.Create();
                    HandleAttributes.Direcotry.Remove(targetDirectory);
                }
                return TaskException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x001D,
                    Exception = ex,
                    Level = copyItemClass.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Medium),
                    Source = sourceDirectory.FullName,
                    Target = targetDirectory.FullName
                };
                progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(progress, true));

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
                    return this.BuildTargetFullName(RootSegment, DriveNameSegment, SourceSegment);
                case CopyItems.CopyMode.Restore:
                    DirectoryInfo Source = new DirectoryInfo(projectSettings.ControleRestore.Directory.Path);
                    if (projectSettings.ControleRestore.Directory.CreateDriveDirectroy)
                    {
                        RootSegment = sourceDirectory.FullName.Substring(Source.FullName.Length + 1);
                        return OLKI.Toolbox.DirectoryAndFile.Path.Repair(RootSegment.Insert(1, @":\"));
                    }
                    else
                    {
                        SourceSegment = "";
                        if (sourceDirectory.FullName.Length > Source.FullName.Length + 1) SourceSegment = sourceDirectory.FullName.Substring(Source.FullName.Length + 1);
                        RootSegment = projectSettings.ControleRestore.Directory.RestoreTargetPath;
                        RootSegment += @"\" + SourceSegment;
                        return OLKI.Toolbox.DirectoryAndFile.Path.Repair(RootSegment);
                    }
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
        private string BuildTargetFullName(string rootSegment, string driveNameSegment, string sourceSegment)
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