using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Common;
using TickTick.Enums;
using TickTick.Utilities.ImageUtility;

namespace TickTick.Entity
{
    public class Attachment : BaseEntity
    {
        //private static readonly long SerialVersionUID = -6938721321363992256L;

        private int _taskId;

        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value; }
        }
        private String _taskSid;

        public String TaskSid
        {
            get { return _taskSid; }
            set { _taskSid = value; }
        }
        /**
         * Absolutely Path
         */
        private String _localPath;

        public String LocalPath
        {
            get { return _localPath; }
            set { _localPath = value; }
        }
        private long? _size;

        public long? Size
        {
            get { return _size; }
            set { _size = value; }
        }
        private String _fileName;

        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private string _fileType;//此处有坑，是string类型，不是fileType

        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        private String _description;

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private String _otherData;

        public String OtherData
        {
            get { return _otherData; }
            set { _otherData = value; }
        }
        private int _upDown = ModelStatusEnum.UP_DOWN_DONE;

        public int UpDown
        {
            get { return _upDown; }
            set { _upDown = value; }
        }
        private AttachmentRemoteSource _remoteSource = null;
        [Ignore]
        public AttachmentRemoteSource RemoteSource
        {
            get { return _remoteSource; }
            set { _remoteSource = value; }
        }
        private int _syncErrorCode = Constants.SyncFileErrorCode.NO_ERROR;

        public int SyncErrorCode
        {
            get { return _syncErrorCode; }
            set { _syncErrorCode = value; }
        }

        private string _refId;

        public string RefId
        {
            get { return _refId ?? SId; }//此处注意
            set { _refId = value; }
        }

        private String _referAttachmentSid;

        public String ReferAttachmentSid
        {
            get { return _referAttachmentSid; }
            set { _referAttachmentSid = value; }
        }
        /**
         * Use for detector if manual uploading
         */
        private bool _manualUploading = false;

        public bool ManualUploading
        {
            get { return _manualUploading; }
            set { _manualUploading = value; }
        }

        private int _status = ModelStatusEnum.SYNC_NEW;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public async Task InitDownloadStatus()
        {
            UpDown = await FileLimiter.IsOverSyncFileSize(Size) ? ModelStatusEnum.UP_DOWN_UNDOWNLOAD : ModelStatusEnum.UP_DOWN_NEED_TO_DOWNLOAD;

        }
    }
}
