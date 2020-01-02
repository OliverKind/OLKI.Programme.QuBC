/*
 * Filename:      clsBackgroundWorkerResponse.cs
 * Created:       2017-06-11
 * Last modified: 2017-06-11
 * Copyright:     Oliver Kind - 2017
 * 
 * File Content:
 * - Properties
 * - Methodes
 *  1. Clone
 *  2. Helpers
 *   a. StartBackup
 * - SubClasses
 *  # WorkerException - Descripes an exception, occours during the backup progress
 *   - Properties
 *   - Methodes
 *    1. WorkerException - Constructor
 *    2. Clone
 *  # SizeCopyData - Descripes the state of copieng data in all bytes an copied bytes
 *   - Properties
 *   - Methodes
 *    1. SizeCopyData - Constructor
 *    
 * Desctiption:
 * Provides tools to report the progress of an backup progress
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OLKI.Programme.QBC.BackupProject
{
    /// <summary>
    /// Provides tools to report the progress of an backup progress
    /// </summary>
    internal class BackupResponse
    {
        #region Properties
        #region Clean Project
        /// <summary>
        /// Specifies the number of all items to check in cleaning project step
        /// </summary>
        private long _clean_ItemsToCheck = 0;
        /// <summary>
        /// Get or set the number of all items to check in cleaning project step
        /// </summary>
        public long Clean_ItemsToCheck
        {
            get
            {
                return this._clean_ItemsToCheck;
            }
            set
            {
                this._clean_ItemsToCheck = value;
            }
        }

        /// <summary>
        /// Specifies the actual number of checked items while cleaning project step
        /// </summary>
        private long _clean_ItemsChecked = 0;
        /// <summary>
        /// Get or set the actual number of checked items while cleaning project step
        /// </summary>
        public long Clean_ItemsChecked
        {
            get
            {
                return this._clean_ItemsChecked;
            }
            set
            {
                this._clean_ItemsChecked = value;
            }
        }
        #endregion

        #region Count Items
        /// <summary>
        /// Specifies the number of all counted direcotrys to copy
        /// </summary>
        public long _count_AllDirec = 0;
        /// <summary>
        /// Get or set the number of all counted direcotrys to copy
        /// </summary>
        public long Count_AllDirec
        {
            get
            {
                return this._count_AllDirec;
            }
            set
            {
                this._count_AllDirec = value;
            }
        }

        /// <summary>
        /// Specifies the number of all counted files to copy
        /// </summary>
        private long _count_AllFiles = 0;
        /// <summary>
        /// Get or set the number of all counted files to copy
        /// </summary>
        public long Count_AllFiles
        {
            get
            {
                return this._count_AllFiles;
            }
            set
            {
                this._count_AllFiles = value;
            }
        }

        /// <summary>
        /// Specifies the number of all counted bytes to copy
        /// </summary>
        private long _count_AllBytes = 0;
        /// <summary>
        /// Get or set the number of all counted bytes to copy
        /// </summary>
        public long Count_AllBytes
        {
            get
            {
                return this._count_AllBytes;
            }
            set
            {
                this._count_AllBytes = value;
            }
        }

        /// <summary>
        /// Specifies the path of the actualy counted directory
        /// </summary>
        public string _count_ActDireName = string.Empty;
        /// <summary>
        /// Get or set the path of the actualy counted directory
        /// </summary>
        public string Count_ActDireName
        {
            get
            {
                return this._count_ActDireName;
            }
            set
            {
                this._count_ActDireName = value;
            }
        }

        /// <summary>
        /// Specifies the path of the actualy counted file
        /// </summary>
        private string _count_ActFileName = string.Empty;
        /// <summary>
        /// Get or set the path of the actualy counted file
        /// </summary>
        public string Count_ActFileName
        {
            get
            {
                return this._count_ActFileName;
            }
            set
            {
                this._count_ActFileName = value;
            }
        }

        /// <summary>
        /// Get or set number of all counted items, directorys and files
        /// </summary>
        public long Count_AllItems
        {
            get
            {
                return this.Count_AllDirec + this.Count_AllFiles;
            }
        }
        #endregion

        #region Copy Itmes
        // Backup progres, all
        /// <summary>
        /// Specifies the start time of the copy progress
        /// </summary>
        private DateTime _copy_Startime = DateTime.Now;
        /// <summary>
        /// Get or set the start time of the copy progress
        /// </summary>
        public DateTime Copy_Startime
        {
            get
            {
                return this._copy_Startime;
            }
            set
            {
                this._copy_Startime = value;
            }
        }

        /// <summary>
        /// Specifies the number of all copied directories
        /// </summary>
        private long _copy_AllDire = 0;
        /// <summary>
        /// Get or set the number of all copied directories
        /// </summary>
        public long Copy_AllDire
        {
            get
            {
                return this._copy_AllDire;
            }
            set
            {
                this._copy_AllDire = value;
            }

        }

        /// <summary>
        /// Specifies the number of all copied files
        /// </summary>
        private long _copy_AllFile = 0;
        /// <summary>
        /// Get or set the number of all copied files
        /// </summary>
        public long Copy_AllFile
        {
            get
            {
                return this._copy_AllFile;
            }
            set
            {
                this._copy_AllFile = value;
            }
        }

        /// <summary>
        /// Specifies the number of all copied bytes
        /// </summary>
        private long _copy_AllByte = 0;
        /// <summary>
        /// Get or set the number of all copied bytes
        /// </summary>
        public long Copy_AllByte
        {
            get
            {
                return this._copy_AllByte;
            }
            set
            {
                this._copy_AllByte = value;
            }
        }

        /// <summary>
        /// Get the number of all copied items
        /// </summary>
        public long Copy_AllItems
        {
            get
            {
                return this.Copy_AllDire + this.Copy_AllFile;
            }
        }

        /// <summary>
        /// Get the number of all items left to copy
        /// </summary>
        public long Copy_LeftItems
        {
            get
            {
                return this.Count_AllItems - this.Copy_AllItems;
            }
        }

        // Actual Directory and name
        /// <summary>
        /// Specifies the path of the actualy copied directory
        /// </summary>
        public string _copy_ActDireName = string.Empty;
        /// <summary>
        /// Get or set the path of the actualy copied directory
        /// </summary>
        public string Copy_ActDireName
        {
            get
            {
                return this._copy_ActDireName;
            }
            set
            {
                this._copy_ActDireName = value;
            }
        }

        /// <summary>
        /// Specifies the path of the actualy copied file
        /// </summary>
        private string _copy_ActFileName = string.Empty;
        /// <summary>
        /// Get or set the path of the actualy copied file
        /// </summary>
        public string Copy_ActFileName
        {
            get
            {
                return this._copy_ActFileName;
            }
            set
            {
                this._copy_ActFileName = value;
            }
        }

        /// <summary>
        /// Specifies the number of all files to copy in actual directory
        /// </summary>
        private long _copy_ActDireAllFiles = 0;
        /// <summary>
        /// Get or set the number of all files to copy in actual directory
        /// </summary>
        public long Copy_ActDireAllFiles
        {
            get
            {
                return this._copy_ActDireAllFiles;
            }
            set
            {
                this._copy_ActDireAllFiles = value;
            }
        }

        /// <summary>
        /// Specifies the number of copied files in actual directory
        /// </summary>
        private long _copy_ActDireActFiles = 0;
        /// <summary>
        /// Get or set the number of copied files in actual directory
        /// </summary>
        public long Copy_ActDireActFiles
        {
            get
            {
                return this._copy_ActDireActFiles;
            }
            set
            {
                this._copy_ActDireActFiles = value;
            }
        }

        /// <summary>
        /// Specifies the number of all bytes to copy in actual file
        /// </summary>
        private long _copy_ActFileAllByte = 0;
        /// <summary>
        /// Get or set the number of all bytes to copy in actual file
        /// </summary>
        public long Copy_ActFileAllByte
        {
            get
            {
                return this._copy_ActFileAllByte;
            }
            set
            {
                this._copy_ActFileAllByte = value;
            }
        }

        /// <summary>
        /// Specifies the number of copied bytes in actual file
        /// </summary>
        private long _copy_ActFileByte = 0;
        /// <summary>
        /// Get or set the number of copied bytes in actual file
        /// </summary>
        public long Copy_ActFileByte
        {
            get
            {
                return this._copy_ActFileByte;
            }
            set
            {
                this._copy_ActFileByte = value;
            }
        }
        #endregion

        #region Exception
        /// <summary>
        /// Specifies an exception, if an exception occours during the backup progress
        /// </summary>
        internal WorkerException _exception = null;
        /// <summary>
        /// Get or set an exception, if an exception occours during the backup progress
        /// </summary>
        public WorkerException Exception
        {
            get
            {
                return this._exception;
            }
            set
            {
                this._exception = value;
            }
        }
        #endregion
        #endregion

        #region Methodes
        /// <summary>
        /// Creates an copy of the actual progress instance
        /// </summary>
        /// <returns>Copy of the actual progress instance</returns>
        internal BackupResponse Clone()
        {
            BackupResponse ThisClone = (BackupResponse)this.MemberwiseClone();
            if (ThisClone.Exception != null)
            {
                ThisClone.Exception = this.Exception.Clone();
            }
            return ThisClone;
        }
        #endregion

        #region SubClasses
        /// <summary>
        /// Descripes an exception, occours during the backup progress
        /// </summary>
        public class WorkerException
        {
            #region Properties
            /// <summary>
            /// Specifies the exception occours during the backup progress
            /// </summary>
            public Exception _exception = null;
            /// <summary>
            /// Get or set the exception occours during the backup progress
            /// </summary>
            public Exception Exception
            {
                get
                {
                    return this._exception;
                }
                set
                {
                    this._exception = value;
                }
            }

            /// <summary>
            /// Specifies the path of the item while processing the exception occours
            /// </summary>
            private string _sourcePath = string.Empty;
            /// <summary>
            /// Get or set the path of the item while processing the exception occours
            /// </summary>
            public string SourcePath
            {
                get
                {
                    return this._sourcePath;
                }
                set
                {
                    this._sourcePath = value;
                }
            }

            /// <summary>
            /// Specifies the destination path of the item while processing the exception occours
            /// </summary>
            private string _targetPath = string.Empty;
            /// <summary>
            /// Get or set the destination path of the item while processing the exception occours
            /// </summary>
            public string TargetPath
            {
                get
                {
                    return this._targetPath;
                }
                set
                {
                    this._targetPath = value;
                }
            }
            #endregion

            #region Methodes
            /// <summary>
            /// Creates a new instance of an exception during the backup progress
            /// </summary>
            /// <param name="exception">Specifies the exception occours during the backup progress</param>
            /// <param name="sourcePath">Specifies the path of the item while processing the exception occours</param>
            /// <param name="targetPath">Specifies the destination path of the item while processing the exception occours</param>
            public WorkerException(Exception exception, string sourcePath, string targetPath)
            {
                this.Exception = exception;
                this.SourcePath = sourcePath;
                this.TargetPath = targetPath;
            }

            /// <summary>
            /// Creates an copy of the actual exception instance
            /// </summary>
            /// <returns>Copy of the actual exception instance</returns>
            public WorkerException Clone()
            {
                return (WorkerException)this.MemberwiseClone();
            }
            #endregion
        }

        /// <summary>
        /// Descripes the state of copieng data in all bytes an copied bytes
        /// </summary>
        internal class SizeCopyData
        {
            #region Properties
            /// <summary>
            /// Specifies the number of all bytes to copy. Set -1 if not given. Set -1 if not given
            /// </summary>
            private long _count_AllBytes = -1;
            /// <summary>
            /// Get or set the number of all bytes to copy. Set -1 if not given
            /// </summary>
            internal long Count_AllBytes
            {
                get
                {
                    return this._count_AllBytes;
                }
                set
                {
                    this._count_AllBytes = value;
                }
            }

            /// <summary>
            /// Specifies the number of copied bytes. Set -1 if not given. Set -1 if not given
            /// </summary>
            private long _copy_Byte = -1;
            /// <summary>
            /// Get or set the number of copied bytes. Set -1 if not given
            /// </summary>
            internal long Copy_Byte
            {
                get
                {
                    return this._copy_Byte;
                }
                set
                {
                    this._copy_Byte = value;
                }
            }
            #endregion

            #region Methodes
            /// <summary>
            /// Creates a new instance of the copieng data progress with non given over all and current bytes
            /// </summary>
            internal SizeCopyData()
                : this(-1, -1)
            {
            }
            /// <summary>
            /// Creates a new instance of the copieng data progress with a specified number of over all and copied bytes
            /// </summary>
            /// <param name="count_AllBytes">Specifies the number of all bytes to copy. Set -1 if not given. Set -1 if not given</param>
            /// <param name="copy_AllByte">Specifies the number of copied bytes. Set -1 if not given. Set -1 if not given</param>
            internal SizeCopyData(long count_AllBytes, long copy_AllByte)
            {
                this._count_AllBytes = count_AllBytes;
                this._copy_Byte = copy_AllByte;
            }
            #endregion
        }
        #endregion
    }
}