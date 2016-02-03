using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Utilities;

namespace TickTick.Utilities.ImageUtility
{
    public class AttachmentRemoteSource
    {

        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private string _projectSid;

        public string ProjectSid
        {
            get { return _projectSid; }
            set { _projectSid = value; }
        }
        private string _taskSid;

        public string TaskSid
        {
            get { return _taskSid; }
            set { _taskSid = value; }
        }
        private string _attachmentSid;

        public string AttachmentSid
        {
            get { return _attachmentSid; }
            set { _attachmentSid = value; }
        }
        private string _localPath;

        public string LocalPath
        {
            get { return _localPath; }
            set { _localPath = value; }
        }
        private string _fileType;// TODO 此处有坑，应该为type类型，而不是简单的string

        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private long _size;

        public long Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
}
