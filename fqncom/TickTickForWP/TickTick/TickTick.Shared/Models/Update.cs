using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Models
{
    public class Update
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private String projectId;

        public String ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }
        private long sortOrder;

        public long SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }
        private String title;

        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        private String content;

        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private DateTime? dueDate;

        public DateTime? DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        private String timeZone;

        public String TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }
        private String reminder;

        public String Reminder
        {
            get { return reminder; }
            set { reminder = value; }
        }
        private DateTime? repeatFirstDateTime;

        public DateTime? RepeatFirstDateTime
        {
            get { return repeatFirstDateTime; }
            set { repeatFirstDateTime = value; }
        }
        private String repeatFlag;

        public String RepeatFlag
        {
            get { return repeatFlag; }
            set { repeatFlag = value; }
        }
        private DateTime? _completedTime;

        public DateTime? CompletedTime
        {
            get { return _completedTime; }
            set { _completedTime = value; }
        }
        private String repeatTaskId;

        public String RepeatTaskId
        {
            get { return repeatTaskId; }
            set { repeatTaskId = value; }
        }
        private int priority;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        private int status = 0;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private List<ChecklistItem> _checklistItems;

        public List<ChecklistItem> ChecklistItems
        {
            get { return _checklistItems; }
            set { _checklistItems = value; }
        }
        private HashSet<long> userIds = new HashSet<long>();

        public HashSet<long> UserIds
        {
            get { return userIds; }
            set { userIds = value; }
        }
        private DateTime modifiedTime;

        public DateTime ModifiedTime
        {
            get { return modifiedTime; }
            set { modifiedTime = value; }
        }
        private long etimestamp;

        public long Etimestamp
        {
            get { return etimestamp; }
            set { etimestamp = value; }
        }
        private String etag;

        public String Etag
        {
            get { return etag; }
            set { etag = value; }
        }
        private int deleted = 0;

        public int Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        private DateTime createdTime;

        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
        private long creator;

        public long Creator
        {
            get { return creator; }
            set { creator = value; }
        }
        private DateTime? remindTime;

        public DateTime? RemindTime
        {
            get { return remindTime; }
            set { remindTime = value; }
        }
        private Location _location;

        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }
        private String repeatFrom;

        public String RepeatFrom
        {
            get { return repeatFrom; }
            set { repeatFrom = value; }
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
        private int? commentCount;

        public int? CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; }
        }
        private long? assignee;

        public long? Assignee
        {
            get { return assignee; }
            set { assignee = value; }
        }
        private int _userCount;

        public int UserCount
        {
            get { return _userCount; }
            set { _userCount = value; }
        }
        private string _kind;

        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }
        private bool _isOwner;

        public bool IsOwner
        {
            get { return _isOwner; }
            set { _isOwner = value; }
        }



        //@JsonIgnore
        //private TaskSource source;
    }
}
