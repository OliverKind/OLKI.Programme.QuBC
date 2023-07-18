﻿/*
 * QuBC - QuickBackupCreator
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Copy item recursive to backup or to resotre
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
using System.Linq;
using System.Windows.Forms;

namespace OLKI.Programme.QuBC.src.Project.Task
{
    /// <summary>
    /// Provides tools to write logfiles
    /// </summary>
    internal partial class CopyItems
    {
        #region Constants
        /// <summary>
        /// Buffer size while writing files
        /// </summary>
        private const int WRITE_BUFFER_SIZE = 524288;
        #endregion

        #region Methodes
        #region CopyRecursive
        /// <summary>
        /// Copy recursive all elements in directory an subdirectory, sepending of the scope
        /// </summary>
        /// <param name="copyMode">The copy mode</param>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for copy</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private TaskException.ExceptionLevel CopyRecursive(CopyItems.CopyMode copyMode, string sourceDirectory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            return this.CopyRecursive(copyMode, new DirectoryInfo(sourceDirectory), scope, worker, e, out exception);
        }

        /// <summary>
        /// Copy recursive all elements in directory an subdirectory, sepending of the scope
        /// </summary>
        /// <param name="copyMode">The copy mode</param>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for copy</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private TaskException.ExceptionLevel CopyRecursive(CopyItems.CopyMode copyMode, DirectoryInfo sourceDirectory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            exception = null;
            DirectoryCreator CreateDirectory = new DirectoryCreator();
            DirectoryInfo TargetDirectory = null; ;
            try
            {
                // Get Target Directory
                TargetDirectory = new DirectoryInfo(CreateDirectory.GetTargetFullName(copyMode, sourceDirectory, this._project.Settings));

                // Exit if cancelation pending
                if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }

                //Check for existing directory
                if (!sourceDirectory.Exists)
                {
                    this._progress.Exception.Exception = new Exception(Properties.Stringtable._0x000C, null);
                    this._progress.Exception.Level = TaskException.ExceptionLevel.Slight;
                    this._progress.Exception.Source = sourceDirectory.FullName;
                    this._progress.Exception.Target = TargetDirectory.FullName;
                    worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                    return TaskException.ExceptionLevel.Slight;
                }

                // Create target directory, stop creating directory if directory can't created withoud excpetion
                switch (CreateDirectory.TargetDirectory(sourceDirectory, TargetDirectory, worker, this._progress, out exception, this))
                {
                    case TaskException.ExceptionLevel.Critical:
                        return TaskException.ExceptionLevel.Critical;
                    case TaskException.ExceptionLevel.Medium:
                        return TaskException.ExceptionLevel.Medium;
                    case TaskException.ExceptionLevel.Slight:
                        return TaskException.ExceptionLevel.Slight;
                    default:
                        //No exception, nothing to do
                        break;
                }

                // Report Progress
                this._progress.DirectroyFiles.ElemenName = sourceDirectory.FullName;
                this._progress.FileBytes.MaxValue = null;
                worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));

                //Copy Files
                switch (scope)
                {
                    case Project.DirectoryScope.All:
                        if (this.CopyAllFiles(sourceDirectory, TargetDirectory, worker, e, out exception) == TaskException.ExceptionLevel.Critical) return TaskException.ExceptionLevel.Critical;

                        // Copy files in sub directorys if there is access to the directory
                        if (OLKI.Toolbox.DirectoryAndFile.Directory.CheckAccess(sourceDirectory))
                        {
                            foreach (DirectoryInfo NextSourceDirectory in sourceDirectory.GetDirectories().OrderBy(o => o.Name))
                            {
                                // Check for abbort
                                if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }
                                if (this.CopyRecursive(copyMode, NextSourceDirectory, scope, worker, e, out exception) == TaskException.ExceptionLevel.Critical) return TaskException.ExceptionLevel.Critical;
                            }
                        }
                        break;
                    case Project.DirectoryScope.Nothing:
                        if (this.CopyNoFiles(sourceDirectory, TargetDirectory, worker, e, out exception) == TaskException.ExceptionLevel.Critical) return TaskException.ExceptionLevel.Critical;
                        break;
                    case Project.DirectoryScope.Selected:
                        if (this.CopySelectedFiles(sourceDirectory, TargetDirectory, worker, e, out exception) == TaskException.ExceptionLevel.Critical) return TaskException.ExceptionLevel.Critical;
                        break;
                    default:
                        throw new ArgumentException("CopyItems->CopyRecursive->Invalid value", nameof(scope));
                }

                //Copy attributes from source to target
                if (this._project.Settings.Common.CopyDirectoryProperties && !Toolbox.DirectoryAndFile.Path.IsDrive(sourceDirectory) && !OLKI.Toolbox.DirectoryAndFile.Path.IsDrive(TargetDirectory)) HandleAttributes.Direcotry.Set(TargetDirectory, sourceDirectory.Attributes);

                // Report Progress
                this._progress.TotalDirectories.ActualValue++;
                worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));

                return TaskException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                this._progress.Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x000D,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Critical,
                    Source = sourceDirectory.FullName,
                    Target = (TargetDirectory != null ? TargetDirectory.FullName : "")
                };
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return TaskException.ExceptionLevel.Critical;
            }
        }
        #endregion

        #region Copy by scope
        /// <summary>
        /// Copy all files in directroy 
        /// </summary>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="targetDirectory">Target directroy for copy files</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private TaskException.ExceptionLevel CopyAllFiles(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            try
            {
                exception = null;

                // Search for files in selected directory
                this._progress.DirectroyFiles.ActualValue = 0;
                this._progress.DirectroyFiles.MaxValue = sourceDirectory.GetFiles().Count();
                foreach (FileInfo FileItem in sourceDirectory.GetFiles().OrderBy(o => o.Name))
                {
                    // Check for abbort
                    if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }

                    //Count up Files an Bytes
                    if (this.CopyFile(FileItem, targetDirectory, worker, e, out exception) == TaskException.ExceptionLevel.Critical) return TaskException.ExceptionLevel.Critical;

                    //Report Progress
                    this._progress.DirectroyFiles.ActualValue++;
                    worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));
                }
                return TaskException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                this._progress.Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x000D,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Medium,
                    Source = sourceDirectory.FullName,
                    Target = targetDirectory.FullName
                };
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return TaskException.ExceptionLevel.Medium;
            }
        }

        /// <summary>
        /// Copy no  files
        /// </summary>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="targetDirectory">Target directroy for copy files</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private TaskException.ExceptionLevel CopyNoFiles(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            //Absolutly nothing to do
            try
            {
                exception = null;

                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }
                return TaskException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                this._progress.Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x000D,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Critical,
                    Source = sourceDirectory.FullName,
                    Target = targetDirectory.FullName
                };
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return TaskException.ExceptionLevel.Medium;
            }
        }

        /// <summary>
        /// Copy all selected files in directroy
        /// </summary>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="targetDirectory">Target directroy for copy files</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private TaskException.ExceptionLevel CopySelectedFiles(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            try
            {
                exception = null;

                //Leafe if the key didn't exists or if no files are selected
                if (!this._project.ToBackupFiles.ContainsKey(sourceDirectory.FullName) || this._project.ToBackupFiles[sourceDirectory.FullName].Count == 0) return TaskException.ExceptionLevel.NoException;

                // Search for files in selected directory
                this._progress.DirectroyFiles.ActualValue = 0;
                this._progress.DirectroyFiles.MaxValue = this._project.ToBackupFiles[sourceDirectory.FullName].Count;
                foreach (FileInfo FileItem in sourceDirectory.GetFiles().OrderBy(o => o.Name))
                {
                    // Check for abbort
                    if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }

                    //Count up Files an Bytes if file is selected
                    if (this._project.ToBackupFiles[sourceDirectory.FullName].Contains(FileItem.FullName))
                    {
                        if (this.CopyFile(FileItem, targetDirectory, worker, e, out exception) == TaskException.ExceptionLevel.Critical) return TaskException.ExceptionLevel.Critical;
                        if (worker.CancellationPending) { e.Cancel = true; return TaskException.ExceptionLevel.NoException; }
                    }

                    //Report Progress
                    this._progress.DirectroyFiles.ActualValue++;
                    worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));
                }
                return TaskException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                this._progress.Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x000D,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Critical,
                    Source = sourceDirectory.FullName,
                    Target = targetDirectory.FullName
                };
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return TaskException.ExceptionLevel.Medium;
            }
        }
        #endregion

        #region Copy file buffered
        /// <summary>
        /// Copya file from target to source
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetDirectory">Target directroy for copy the files to</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private TaskException.ExceptionLevel CopyFile(FileInfo sourceFile, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            this._progress.FileBytes.ActualValue = 0;
            this._progress.FileBytes.ElemenName = sourceFile.FullName;
            this._progress.FileBytes.MaxValue = sourceFile.Length;

            worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));

            FileInfo TargetFile = new FileInfo(targetDirectory.FullName + @"\" + sourceFile.Name);
            try
            {
                if (TargetFile.Exists)
                {
                    HandleExistingFiles.CheckResult HandleFile = HandleExistingFiles.GetOverwriteByAction(sourceFile, TargetFile, Properties.Settings.Default.Copy_FileExisitngAddTextDateFormat, this._project.Settings.Common.ExisitingFiles.HandleExistingItem, this._project.Settings.Common.ExisitingFiles.AddTextToExistingFile, false, out exception, this._mainForm, System.Security.Cryptography.SHA256.Create());
                    if (HandleFile.FormResult == DialogResult.Cancel)
                    {
                        worker.ReportProgress((int)TaskControle.TaskStep.Cancel, new ProgressState(true));
                        e.Cancel = true;
                        worker.CancelAsync();
                        return TaskException.ExceptionLevel.NoException;
                    }
                    else if (HandleFile.RememberAction)
                    {
                        this._project.Settings.Common.ExisitingFiles.HandleExistingItem = HandleFile.SelectedAction;
                        this._project.Settings.Common.ExisitingFiles.AddTextToExistingFile = HandleFile.AddText;
                    }

                    //Handle overwrite state
                    switch (HandleFile.OverwriteFile)
                    {
                        case HandleExistingFiles.ExistingFile.Exception:
                            worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));
                            return TaskException.ExceptionLevel.Medium;
                        case HandleExistingFiles.ExistingFile.Overwrite:
                        case HandleExistingFiles.ExistingFile.Rename:
                            //Nothing speceial to do
                            break;
                        case HandleExistingFiles.ExistingFile.Skip:
                            this._progress.TotalBytes.ActualValue += sourceFile.Length;
                            this._progress.TotalFiles.ActualValue++;
                            worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));
                            return TaskException.ExceptionLevel.NoException;
                    }
                }

                if (!this.CopyFileBufferd(sourceFile, TargetFile, worker, e, out exception))
                {
                    return this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Medium);
                }
                else
                {
                    this._progress.TotalFiles.ActualValue++;
                }

                //Report Progress
                worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));
                return TaskException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                TaskException.ExceptionLevel ReturnLevel = this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Medium);
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x000E,
                    Exception = ex,
                    Level = ReturnLevel,
                    Source = sourceFile.FullName,
                    Target = TargetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));
                return ReturnLevel;
            }
        }

        /// <summary>
        /// Copy a file with in bufferd parts
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        public bool CopyFileBufferd(FileInfo sourceFile, FileInfo targetFile, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            BinaryReader Reader = null;
            BinaryWriter Writer = null;
            try
            {
                // Remove target file attributes
                this.CopyFileRemoveMetadata(sourceFile, targetFile, worker, out exception);

                if (!this.CopyFileOpenSource(sourceFile, targetFile, WRITE_BUFFER_SIZE, out Reader, worker, out exception)) return false; ;
                if (!this.CopyFileCreateTarget(sourceFile, targetFile, WRITE_BUFFER_SIZE, out Writer, worker, out exception)) return false;
                if (!this.CopyFileWriteDate(sourceFile, targetFile, WRITE_BUFFER_SIZE, Reader, Writer, worker, e, out exception)) return false;

                if (!this.CopyFileWriteMetadata(sourceFile, targetFile, worker, out exception)) return false;
                if (worker.CancellationPending) { e.Cancel = true; return true; }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x000F,
                    Exception = ex,
                    Level = TaskException.ExceptionLevel.Critical,
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));
                return false;
            }
            finally
            {
                if (Writer != null) Writer.Dispose();
                if (Reader != null) Reader.Dispose();
            }
        }

        /// <summary>
        /// Open source file to copy a file with in bufferd parts
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <param name="bufferSize">Buffer size for copy files with in bufferd parts</param>
        /// <param name="reader">File reader to read files</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private bool CopyFileOpenSource(FileInfo sourceFile, FileInfo targetFile, int bufferSize, out BinaryReader reader, BackgroundWorker worker, out Exception exception)
        {
            exception = null;
            try
            {
                FileStream FileStream = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize);
                reader = new BinaryReader(FileStream);
                return true;
            }
            catch (Exception ex)
            {
                reader = null;
                exception = ex;
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0010,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Medium),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return false;
            }
        }

        /// <summary>
        /// Create an open target file to copy a file with in bufferd parts
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <param name="bufferSize">Buffer size for copy files with in bufferd parts</param>
        /// <param name="writer">File writer to write data to target file</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private bool CopyFileCreateTarget(FileInfo sourceFile, FileInfo targetFile, int bufferSize, out BinaryWriter writer, BackgroundWorker worker, out Exception exception)
        {
            exception = null;
            try
            {
                FileStream FileStream = new FileStream(targetFile.FullName, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize);
                writer = new BinaryWriter(FileStream);
                return true;
            }
            catch (Exception ex)
            {
                writer = null;
                exception = ex;
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0011,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Medium),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return false;
            }
        }

        /// <summary>
        /// Write data to target file, to copy a file with in bufferd parts
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <param name="bufferSize">Buffer size for copy files with in bufferd parts</param>
        /// <param name="reader">File reader to read files</param>
        /// <param name="writer">File writer to write data to target file</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private bool CopyFileWriteDate(FileInfo sourceFile, FileInfo targetFile, int bufferSize, BinaryReader reader, BinaryWriter writer, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            exception = null;
            try
            {
                int read = 0;
                byte[] FileDataPart = new byte[bufferSize];

                while ((read = reader.Read(FileDataPart, 0, bufferSize)) > 0)
                {
                    writer.Write(FileDataPart, 0, read);

                    this._progress.FileBytes.ActualValue += read;
                    this._progress.TotalBytes.ActualValue += read;
                    worker.ReportProgress((int)TaskControle.TaskStep.Copy_Busy, new ProgressState(this._progress));

                    if (worker.CancellationPending) { e.Cancel = true; return false; }
                }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0012,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Medium),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return false;
            }
            finally
            {
                if (writer != null) writer.Dispose();
                if (reader != null) reader.Dispose();
            }
        }

        /// <summary>
        /// Remove Metadate from target file, if target already exists
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to remove metadate</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private bool CopyFileRemoveMetadata(FileInfo sourceFile, FileInfo targetFile, BackgroundWorker worker, out Exception exception)
        {
            exception = null;
            if (!targetFile.Exists) return false;

            if (!HandleAttributes.File.Remove(targetFile, out exception))
            {
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0013,
                    Exception = exception,
                    Level = this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Slight),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return false;
            }
            return true;
        }

        /// <summary>
        /// Write Metadate to target file
        /// </summary>
        /// <param name="sourceFile">Source file to copy metadata</param>
        /// <param name="targetFile">Target file to write metadate</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private bool CopyFileWriteMetadata(FileInfo sourceFile, FileInfo targetFile, BackgroundWorker worker, out Exception exception)
        {
            exception = null;
            try
            {
                targetFile.CreationTime = sourceFile.CreationTime;
                targetFile.LastAccessTime = sourceFile.LastAccessTime;
                targetFile.LastWriteTime = sourceFile.LastWriteTime;
                if (this._project.Settings.Common.CopyFileProperties && !HandleAttributes.File.Set(targetFile, sourceFile.Attributes, out exception))
                {
                    TaskException Exception = new TaskException
                    {
                        Description = Properties.Stringtable._0x0014,
                        Exception = exception,
                        Level = this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Slight),
                        Source = sourceFile.FullName,
                        Target = targetFile.FullName
                    };
                    this._progress.Exception = Exception;
                    worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));
                }
                targetFile.IsReadOnly = sourceFile.IsReadOnly;

                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                TaskException Exception = new TaskException
                {
                    Description = Properties.Stringtable._0x0014,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, TaskException.ExceptionLevel.Slight),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)TaskControle.TaskStep.Exception, new ProgressState(this._progress, true));

                return false;
            }
        }
        #endregion

        /// <summary>
        /// Check if the exception is a full disc exception an return a critical exception level, otherwise return the alternative exception level
        /// </summary>
        /// <param name="exception">Exception to check the exception code for full disc exception</param>
        /// <param name="alternativeLevel">Exceptionlevel to return if it is not an full disc exception</param>
        /// <returns>Critical exception level if exception is an full disc exception, otherweise return the alternative definded exception level</returns>
        public TaskException.ExceptionLevel GetFullDiscExceptionReturnCode(Exception exception, TaskException.ExceptionLevel alternativeLevel)
        {
            if (exception != null && System.Runtime.InteropServices.Marshal.GetHRForException(exception) == EXCEPTION_FULL_DISC) return TaskException.ExceptionLevel.Critical;
            return alternativeLevel;
        }
        #endregion
    }
}