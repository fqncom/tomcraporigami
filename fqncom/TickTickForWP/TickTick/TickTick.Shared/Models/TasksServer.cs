using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;

namespace TickTick.Models
{
    public class TasksServer
    {
        private String _id;
        [JsonProperty("id")]
        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _projectId;

        public String ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }
        private string _kind;

        // TODO 已经废弃
        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }
        private long? _sortOrder;

        public long? SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
        private String _title;

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private String _content;

        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }
        private DateTime? _dueDate;

        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }
        private String _timeZone;

        public String TimeZone
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }
        private List<Reminder> _reminders;

        public List<Reminder> Reminders
        {
            get { return _reminders; }
            set { _reminders = value; }
        }

        private String _reminder;

        public String Reminder
        {
            get { return _reminder; }
            set { _reminder = value; }
        }
        private DateTime? _repeatFirstDate;

        public DateTime? RepeatFirstDate
        {
            get { return _repeatFirstDate; }
            set { _repeatFirstDate = value; }
        }
        private String _repeatFlag;

        public String RepeatFlag
        {
            get { return _repeatFlag; }
            set { _repeatFlag = value; }
        }
        private DateTime? _completedTime;

        
        public DateTime? CompletedTime
        {
            get { return _completedTime; }
            set { _completedTime = value; }
        }
        private String _repeatTaskId;

        public String RepeatTaskId
        {
            get { return _repeatTaskId; }
            set { _repeatTaskId = value; }
        }
        private int _priority;

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        private int _status = 0;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private List<ChecklistItem> _items;

        public List<ChecklistItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }
        private List<long> _userIds = new List<long>();

        public List<long> UserIds
        {
            get { return _userIds; }
            set { _userIds = value; }
        }
        private DateTime _modifiedTime;

        public DateTime ModifiedTime
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        private long _etimestamp;

        public long Etimestamp
        {
            get { return _etimestamp; }
            set { _etimestamp = value; }
        }
        private String _etag;

        public String Etag
        {
            get { return _etag; }
            set { _etag = value; }
        }
        private int _deleted = 0;

        public int Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }
        private DateTime _createdTime;

        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { _createdTime = value; }
        }
        private long _creator;

        public long Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }
        private DateTime? _remindTime;

        public DateTime? RemindTime
        {
            get { return _remindTime; }
            set { _remindTime = value; }
        }
        private Location _location;

        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }
        private String _repeatFrom;

        public String RepeatFrom
        {
            get { return _repeatFrom; }
            set { _repeatFrom = value; }
        }
        private HashSet<String> _tags;

        public HashSet<String> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
        private List<Attachment> _attachments;

        public List<Attachment> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }
        private int? _commentCount;

        public int? CommentCount
        {
            get { return _commentCount; }
            set { _commentCount = value; }
        }
        private long? _assignee;

        public long? Assignee
        {
            get { return _assignee; }
            set { _assignee = value; }
        }

        private bool _isAllDay;
        
        public bool IsAllDay
        {
            get
            {
                return DueDate != null && _isAllDay;
            }
            set { _isAllDay = value; }
        }
        //@JsonIgnore
        //private TaskSource source;
    }
}
