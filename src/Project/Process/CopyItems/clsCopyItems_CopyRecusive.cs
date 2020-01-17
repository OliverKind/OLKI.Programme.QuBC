/*
 * QBC- QuickBackupCreator
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Copy item recusive to backup or to resotre
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
using System.Linq;
using System.Windows.Forms;

namespace OLKI.Programme.QBC.BackupProject.Process
{
    /// <summary>
    /// Provides tools to write logfiles
    /// </summary>
    internal partial class CopyItems
    {
        #region Methodes
        #region CopyRecusive
        /// <summary>
        /// Copy recusive all elements in directory an subdirectory, sepending of the scope
        /// </summary>
        /// <param name="copyMode">The copy mode</param>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private ProcessException.ExceptionLevel CopyRecusive(CopyItems.CopyMode copyMode, string sourceDirectory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            return this.CopyRecusive(copyMode, new DirectoryInfo(sourceDirectory), scope, worker, e, out exception);
        }

        /// <summary>
        /// Copy recusive all elements in directory an subdirectory, sepending of the scope
        /// </summary>
        /// <param name="copyMode">The copy mode</param>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="scope">Scope of the directory</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private ProcessException.ExceptionLevel CopyRecusive(CopyItems.CopyMode copyMode, DirectoryInfo sourceDirectory, Project.DirectoryScope scope, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            exception = null;
            DirectoryCreator CreateDirectory = new DirectoryCreator();
            DirectoryInfo TargetDirectory = null; ;
            try
            {
                // Get Target Directory
                TargetDirectory = new DirectoryInfo(CreateDirectory.GetTargetFullName(copyMode, sourceDirectory, this._project.Settings));

                // Exit if cancelation pending
                if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }

                //Check for existing directory
                if (!sourceDirectory.Exists)
                {
                    this._progress.Exception.Exception = new Exception(Stringtable._0x000C, null);
                    worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

                    return ProcessException.ExceptionLevel.Slight;
                }

                // Create target directory, stop creating directory if directory can't created withoud excpetion
                switch (CreateDirectory.TargetDirectory(sourceDirectory, TargetDirectory, worker, this._progress, out exception, this))
                {
                    case ProcessException.ExceptionLevel.Critical:
                        return ProcessException.ExceptionLevel.Critical;
                    case ProcessException.ExceptionLevel.Slight:
                        return ProcessException.ExceptionLevel.Slight;
                    default:
                        //No exception, nothing to do
                        break;
                }

                // Report Progress
                this._progress.DirectroyFiles.ElemenName = sourceDirectory.FullName;
                worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);

                //Copy Files
                switch (scope)
                {
                    case Project.DirectoryScope.All:
                        if (this.CopyAllFiles(sourceDirectory, TargetDirectory, worker, e, out exception) == ProcessException.ExceptionLevel.Critical) return ProcessException.ExceptionLevel.Critical;

                        // Copy files in sub directorys
                        foreach (DirectoryInfo NextSourceDirectory in sourceDirectory.GetDirectories().OrderBy(o => o.Name))
                        {
                            // Check for abbort
                            if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }
                            if (this.CopyRecusive(copyMode, NextSourceDirectory, scope, worker, e, out exception) == ProcessException.ExceptionLevel.Critical) return ProcessException.ExceptionLevel.Critical;
                        }
                        break;
                    case Project.DirectoryScope.Nothing:
                        if (this.CopyNoFiles(sourceDirectory, TargetDirectory, worker, e, out exception) == ProcessException.ExceptionLevel.Critical) return ProcessException.ExceptionLevel.Critical;
                        break;
                    case Project.DirectoryScope.Selected:
                        if (this.CopySelectedFiles(sourceDirectory, TargetDirectory, worker, e, out exception) == ProcessException.ExceptionLevel.Critical) return ProcessException.ExceptionLevel.Critical;
                        break;
                    default:
                        throw new ArgumentException("CopyItems->CopyRecusive->Invalid value", nameof(scope));
                }

                //Copy attributes from source to target
                if (Properties.Settings.Default.Copy_DirectoryAttributes && !Tools.CommonTools.DirectoryAndFile.Path.IsDrive(sourceDirectory)) HandleAttributes.Direcotry.Set(TargetDirectory, sourceDirectory.Attributes);

                // Report Progress
                this._progress.TotalDirectories.ActualValue++;
                worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);

                return ProcessException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                this._progress.Exception = new ProcessException
                {
                    Description = Stringtable._0x000D,
                    Exception = ex,
                    Level = ProcessException.ExceptionLevel.Critical,
                    Source = sourceDirectory.FullName,
                    Target = TargetDirectory.FullName
                };
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

                return ProcessException.ExceptionLevel.Critical;
            }
        }
        #endregion

        #region Copy by scope
#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        /// <summary>
        /// Copy all files in directroy 
        /// </summary>
        /// <param name="sourceDirectory">Source directroy to copy</param>
        /// <param name="targetDirectory">Target directroy for copy files</param>
        /// <param name="worker">BackgroundWorker for count</param>
        /// <param name="e">Provides data for the BackgroundWorker</param>
        /// <param name="exception">Exception of the process</param>
        /// <returns>Exception level of the copy process</returns>
        private ProcessException.ExceptionLevel CopyAllFiles(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            exception = null;

            // Search for files in selected directory
            this._progress.DirectroyFiles.ActualValue = 0;
            this._progress.DirectroyFiles.MaxValue = sourceDirectory.GetFiles().Count();
            foreach (FileInfo FileItem in sourceDirectory.GetFiles().OrderBy(o => o.Name))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }

                //Count up Files an Bytes
                if (this.CopyFile(FileItem, targetDirectory, worker, e, out exception) == ProcessException.ExceptionLevel.Critical) return ProcessException.ExceptionLevel.Critical;

                //Report Progress
                this._progress.DirectroyFiles.ActualValue++;
                worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);
            }
            return ProcessException.ExceptionLevel.NoException;
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
        private ProcessException.ExceptionLevel CopyNoFiles(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            //Absolutly nothing to do
            exception = null;
            return ProcessException.ExceptionLevel.NoException;
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
        private ProcessException.ExceptionLevel CopySelectedFiles(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            exception = null;

            //Leafe if the key didn't exists or if no files are selected
            if (!this._project.ToBackupFiles.ContainsKey(sourceDirectory.FullName) || this._project.ToBackupFiles[sourceDirectory.FullName].Count == 0) return ProcessException.ExceptionLevel.NoException;

            // Search for files in selected directory
            this._progress.DirectroyFiles.ActualValue = 0;
            this._progress.DirectroyFiles.MaxValue = this._project.ToBackupFiles[sourceDirectory.FullName].Count;
            foreach (FileInfo FileItem in sourceDirectory.GetFiles().OrderBy(o => o.Name))
            {
                // Check for abbort
                if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }

                //Count up Files an Bytes if file is selected
                if (this._project.ToBackupFiles[sourceDirectory.FullName].Contains(FileItem.FullName))
                {
                    if (this.CopyFile(FileItem, targetDirectory, worker, e, out exception) == ProcessException.ExceptionLevel.Critical) return ProcessException.ExceptionLevel.Critical;
                    if (worker.CancellationPending) { e.Cancel = true; return ProcessException.ExceptionLevel.NoException; }
                }

                //Report Progress
                this._progress.DirectroyFiles.ActualValue++;
                worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);
            }
            return ProcessException.ExceptionLevel.NoException;
        }
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
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
        private ProcessException.ExceptionLevel CopyFile(FileInfo sourceFile, DirectoryInfo targetDirectory, BackgroundWorker worker, DoWorkEventArgs e, out Exception exception)
        {
            this._progress.FileBytes.ActualValue = 0;
            this._progress.FileBytes.ElemenName = sourceFile.FullName;
            this._progress.FileBytes.MaxValue = sourceFile.Length;

            worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);

            FileInfo TargetFile = new FileInfo(targetDirectory.FullName + @"\" + sourceFile.Name);
            try
            {
                if (TargetFile.Exists)
                {
                    HandleExistingFiles.CheckResult HandleFile = HandleExistingFiles.GetOverwriteByAction(sourceFile, TargetFile, Properties.Settings.Default.Copy_FileExisitngAddTextDateFormat, this._project.Settings.Common.ExisitingFiles.HandleExistingItem, this._project.Settings.Common.ExisitingFiles.AddTextToExistingFile, false, out exception);
                    if (HandleFile.FormResult == DialogResult.Cancel)
                    {
                        worker.ReportProgress((int)ProcControle.ProcessStep.Cancel, ProcControle.FORCE_REPORTING_FLAG);
                        e.Cancel = true;
                        worker.CancelAsync();
                        return ProcessException.ExceptionLevel.NoException;
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
                            worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);
                            return ProcessException.ExceptionLevel.Medium;
                        case HandleExistingFiles.ExistingFile.Overwrite:
                        case HandleExistingFiles.ExistingFile.Rename:
                            //Nothing speceial to do
                            break;
                        case HandleExistingFiles.ExistingFile.Skip:
                            this._progress.TotalBytes.ActualValue += sourceFile.Length;
                            this._progress.TotalFiles.ActualValue++;
                            worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);
                            return ProcessException.ExceptionLevel.NoException;
                    }
                }

                if (!this.CopyFileBufferd(sourceFile, TargetFile, worker, e, out exception))
                {
                    return this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Medium);
                }
                else
                {
                    this._progress.TotalFiles.ActualValue++;
                }

                //Report Progress
                worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);
                return ProcessException.ExceptionLevel.NoException;
            }
            catch (Exception ex)
            {
                exception = ex;
                ProcessException.ExceptionLevel ReturnLevel = this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Medium);
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x000E,
                    Exception = ex,
                    Level = ReturnLevel,
                    Source = sourceFile.FullName,
                    Target = TargetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);
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
            int BufferSize = (int)Math.Pow(2, 19);
            BinaryReader Reader = null;
            BinaryWriter Writer = null;
            try
            {
                // Remove target file attributes
                this.CopyFileRemoveMetadata(sourceFile, targetFile, worker, out exception);

                if (!this.CopyFileOpenSource(sourceFile, targetFile, BufferSize, out Reader, worker, out exception)) return false; ;
                if (!this.CopyFileCreateTarget(sourceFile, targetFile, BufferSize, out Writer, worker, out exception)) return false;
                if (!this.CopyFileWriteDate(sourceFile, targetFile, BufferSize, Reader, Writer, worker, e, out exception)) return false;

                if (!this.CopyFileWriteMetadata(sourceFile, targetFile, worker, out exception)) return false;
                if (worker.CancellationPending) { e.Cancel = true; return true; }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x000F,
                    Exception = ex,
                    Level = ProcessException.ExceptionLevel.Critical,
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);
                return false;
            }
            finally
            {
                Writer.Dispose();
                Reader.Dispose();
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
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x0010,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Medium),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

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
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x0011,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Medium),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

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
                    worker.ReportProgress((int)ProcControle.ProcessStep.Copy_Busy);

                    if (worker.CancellationPending) { e.Cancel = true; return false; }
                }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x0012,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Medium),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

                return false;
            }
            finally
            {
                reader.Dispose();
                writer.Dispose();
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
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x0013,
                    Exception = exception,
                    Level = this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Slight),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

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
                if (Properties.Settings.Default.Copy_FileAttributes && !HandleAttributes.File.Set(targetFile, sourceFile.Attributes, out exception))
                {
                    ProcessException Exception = new ProcessException
                    {
                        Description = Stringtable._0x0014,
                        Exception = exception,
                        Level = this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Slight),
                        Source = sourceFile.FullName,
                        Target = targetFile.FullName
                    };
                    this._progress.Exception = Exception;
                    worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);
                }
                targetFile.IsReadOnly = sourceFile.IsReadOnly;

                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                ProcessException Exception = new ProcessException
                {
                    Description = Stringtable._0x0014,
                    Exception = ex,
                    Level = this.GetFullDiscExceptionReturnCode(exception, ProcessException.ExceptionLevel.Slight),
                    Source = sourceFile.FullName,
                    Target = targetFile.FullName
                };
                this._progress.Exception = Exception;
                worker.ReportProgress((int)ProcControle.ProcessStep.Exception, ProcControle.FORCE_REPORTING_FLAG);

                return false;
            }
        }
        #endregion

        /// <summary>
        /// Check if the exception is a full disc exception an return a critical exception level, otherwise return the alternative exception level
        /// </summary>
        /// <param name="exception">Exception to check the exception code for full disc exception</param>
        /// <param name="alternateLevel">Exceptionlevel to return if it is not an full disc exception</param>
        /// <returns>Critical exception level if exception is an full disc exception, otherweise return the alternative definded exception level</returns>
        public ProcessException.ExceptionLevel GetFullDiscExceptionReturnCode(Exception exception, ProcessException.ExceptionLevel alternateLevel)
        {
            if (exception != null && System.Runtime.InteropServices.Marshal.GetHRForException(exception) == EXCEPTION_FULL_DISC) return ProcessException.ExceptionLevel.Critical;
            return alternateLevel;
        }
        #endregion
    }
}