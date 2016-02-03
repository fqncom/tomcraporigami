using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class Limits
    {
        public static readonly long DEFAULT_FILE_SIZE_LIMIT_FREE = 5 * 1024 * 1024;
        public static readonly long DEFAULT_FILE_SIZE_LIMIT_PRO = 5 * 1024 * 1024;
        public static readonly long DEFAULT_FILE_COUNT_DAILY_LIMIT_FREE = 1;
        public static readonly long DEFAULT_FILE_COUNT_DAILY_LIMIT_PRO = 99;
        public static readonly long DEFAULT_ATTACH_TASK_COUNT = 20;

        private static readonly long DEFAULT_INVALID_ID = -1;

        private long _id = DEFAULT_INVALID_ID;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _projectNumber;

        public int ProjectNumber
        {
            get { return _projectNumber; }
            set { _projectNumber = value; }
        }
        private int _projectTaskNumber;

        public int ProjectTaskNumber
        {
            get { return _projectTaskNumber; }
            set { _projectTaskNumber = value; }
        }
        private int _subTaskNumber;

        public int SubTaskNumber
        {
            get { return _subTaskNumber; }
            set { _subTaskNumber = value; }
        }
        private int _shareUserNumber;

        public int ShareUserNumber
        {
            get { return _shareUserNumber; }
            set { _shareUserNumber = value; }
        }
        private int _accountType;

        public int AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }
        private long _fileSizeLimit = DEFAULT_FILE_SIZE_LIMIT_FREE;

        public long FileSizeLimit
        {
            get { return _fileSizeLimit; }
            set { _fileSizeLimit = value; }
        }
        private long _fileCountDailyLimit = DEFAULT_FILE_COUNT_DAILY_LIMIT_FREE;

        public long FileCountDailyLimit
        {
            get { return _fileCountDailyLimit; }
            set { _fileCountDailyLimit = value; }
        }
        private long _taskAttachCount = DEFAULT_ATTACH_TASK_COUNT;

        public long TaskAttachCount
        {
            get { return _taskAttachCount; }
            set { _taskAttachCount = value; }
        }
    }
}
