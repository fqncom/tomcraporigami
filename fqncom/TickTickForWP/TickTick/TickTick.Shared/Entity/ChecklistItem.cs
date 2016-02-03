using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TickTick.Enums;

namespace TickTick.Entity
{
    public class ChecklistItem:INotifyPropertyChanged
    {
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
        private String _title;

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private int _checked;

        public int Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }



        private long? _sortOrder;

        public long? SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
        private String _oldTitle;

        public String OldTitle
        {
            get { return _oldTitle; }
            set { _oldTitle = value; }
        }

        private int _status = ModelStatusEnum.SYNC_NEW;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _viewHashCode;

        public int ViewHashCode
        {
            get { return _viewHashCode; }
            set { _viewHashCode = value; }
        }
        [Ignore]
        public bool IsChecked
        {
            get
            {
                return Checked == ModelStatusEnum.COMPLETED;
            }
        }
        public bool IsDeletedForever()
        {
            return Deleted == ModelStatusEnum.DELETED_FOREVER;
        }

        private int _deleted;

        public int Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        private int _id;
        [PrimaryKey]
        [AutoIncrement]
        [JsonIgnore]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _sId;

        [JsonProperty("id")]
        public String SId
        {
            get { return _sId; }
            set { _sId = value; }
        }
        private String _userId;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private DateTime _createdTime;

        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { _createdTime = value; }
        }
        private DateTime _modifiedTime;

        public DateTime ModifiedTime
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        private String _etag;

        public String Etag
        {
            get { return _etag; }
            set { _etag = value; }
        }
        /** 由Google账户下合并过来的Id **/
        private String _googleId;

        public String GoogleId
        {
            get { return _googleId; }
            set { _googleId = value; }
        }
        /** temp of merge data,used for googleId is null **/
        private long _localId;

        public long LocalId
        {
            get { return _localId; }
            set { _localId = value; }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
