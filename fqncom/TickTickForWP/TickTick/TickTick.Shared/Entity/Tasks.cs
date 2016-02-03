using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TickTick.Enums;
using TickTick.Helper;
using TickTick.Models;

namespace TickTick.Entity
{
    public class Tasks : BaseEntity
    {

        #region fqn自定义测试属性

        ///// <summary>
        ///// 任务Id
        ///// </summary>
        //private int _id;

        //[PrimaryKey]
        //[AutoIncrement]
        //public int Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        ///// <summary>
        ///// 任务名
        ///// </summary>
        //private string _taskName;

        //public string TaskName
        //{
        //    get { return _taskName; }
        //    set { _taskName = value; }
        //}

        ///// <summary>
        ///// 所属分类名
        ///// </summary>
        //private int _projectId;

        //public int ProjectId
        //{
        //    get { return _projectId; }
        //    set { _projectId = value; }
        //} 
        #endregion

        #region 来自android源代码的字段，稍有修改


        //private static readonly long SerialVersionUID = -4021780575068256460L;
        //public static readonly Table table = new Table(Task2Field.TABLE_NAME, Task2Field.values(),
        //        Task2Field.modifiedTime, Task2Field.createdTime);
        //public static readonly VirtualTable virtualTable = new VirtualTable(Task2VirtualField.TABLE_NAME,
        //        Task2VirtualField.values());
        // TODO 发布时，改String值
        public static readonly int TASK = 1;
        public static readonly int CALENDAR = 2;

        //private String _id;

        //[PrimaryKey]
        //[AutoIncrement]
        //public String Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        //private string _sId;

        //public string SId
        //{
        //    get { return _sId; }
        //    set { _sId = value; }
        //}
        //private string _userId;

        //public string UserId
        //{
        //    get { return _userId; }
        //    set { _userId = value; }
        //}
        private bool _isAllDay;

        public bool IsAllDay
        {
            get
            {
                return DueDate != null && _isAllDay;
            }
            set { _isAllDay = value; }
        }


        private string _projectId;

        public string ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
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
        private List<TaskReminder> _reminders;
        [Ignore]
        public List<TaskReminder> Reminders
        {
            get { return _reminders; }
            set { _reminders = value; }
        }

        //private List<ChecklistItem> _items;
        //[Ignore]
        //public List<ChecklistItem> Items
        //{
        //    get
        //    {
        //        if (_items == null)
        //        {
        //            _items = new List<ChecklistItem>();
        //        }
        //        return _items;
        //    }
        //    set { _items = value; }
        //}
        //private Collection<long> _userIds = new HashSet();

        //private DateTime? _modifiedTime;

        //public DateTime? ModifiedTime
        //{
        //    get { return _modifiedTime; }
        //    set { _modifiedTime = value; }
        //}
        private long _etimestamp;

        public long Etimestamp
        {
            get { return _etimestamp; }
            set { _etimestamp = value; }
        }
        //private String _etag;

        //public String Etag
        //{
        //    get { return _etag; }
        //    set { _etag = value; }
        //}
        //private int _deleted = 0;

        //public int Deleted
        //{
        //    get { return _deleted; }
        //    set { _deleted = value; }
        //}
        //private DateTime? _createdTime;

        //public DateTime? CreatedTime
        //{
        //    get { return _createdTime; }
        //    set { _createdTime = value; }
        //}
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
        [Ignore]
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
        //private Set<String> _tags;
        //private List<Attachment> _attachments;

        private long? _assignee = TickTick.Enums.Constants.Removed.ASSIGNEE;

        public long? Assignee
        {
            get { return _assignee; }
            set { _assignee = value; }
        }
        //@JsonIgnore
        //private TaskSource _source;

        private int _taskStatus;

        public int TaskStatus
        {
            get { return _taskStatus; }
            set 
            {
                //SetProperty(ref _taskStatus, value);
                _taskStatus = value; 
            }
        }
        private bool _isOwner;

        public bool IsOwner
        {
            get { return _isOwner; }
            set { _isOwner = value; }
        }

        private int _userCount;

        public int UserCount
        {
            get { return _userCount; }
            set { _userCount = value; }
        }

        private Projects _projects;
        [Ignore]
        public Projects Projects
        {
            get { return _projects; }
            set { _projects = value; }
        }
        private string _kind;

        //该字段被抛弃，使用checklist.count来判断
        //[SQLite.Ignore]
        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        private DateTime? _snoozeRemindTime;

        public DateTime? SnoozeRemindTime
        {
            get
            {
                //if (IsRepeatTask())
                //{
                //    //_reminderTime = _reminderTime.Value.Add();
                //}
                return _snoozeRemindTime;
            }
            set { _snoozeRemindTime = value; }
        }

        private bool _hasAttachment;

        public bool HasAttachment
        {
            get { return _hasAttachment; }
            set { _hasAttachment = value; }
        }


        private List<ChecklistItem> _checklistItems = new List<ChecklistItem>();
        [Ignore]
        public List<ChecklistItem> ChecklistItems
        {
            get { return _checklistItems; }
            set { _checklistItems = value; }
        }

        private List<Attachment> _attachments = new List<Attachment>();
        [Ignore]
        public List<Attachment> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }

        private HashSet<String> _tags = new HashSet<String>();
        [Ignore]
        public HashSet<String> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        private int? _commentCount = 0;

        public int? CommentCount
        {
            get { return _commentCount; }
            set { _commentCount = value; }
        }
        ///**
        // * task or calendar event======为了重构，将此属性放置父类中去
        // */
        //private int _type;

        //public int Type
        //{
        //    get { return _type; }
        //    set { _type = value; }
        //}
        /**
     * 用于方便查询repeat任务是否需要近期安排Reminder
     */
        private DateTime? _repeatReminderTime;

        public DateTime? RepeatReminderTime
        {
            get { return _repeatReminderTime; }
            set { _repeatReminderTime = value; }
        }

        public bool HasAssignee()
        {
            return Assignee > 0 && Assignee != Constants.Removed.ASSIGNEE;
        }

        public bool IsDeletedForever()
        {
            return Deleted == ModelStatusEnum.DELETED_FOREVER;
        }

        private string _projectSid;

        public string ProjectSid
        {
            get { return _projectSid; }
            set { _projectSid = value; }
        }
        public bool IsRepeatTask()
        {
            return DueDate != null && !string.IsNullOrEmpty(RepeatFlag);
        }
        public void SetContentByItemsInner()
        {
            if (ChecklistItems.Count <= 0)
            {
                return;
            }
            this.Content = GetCompositeContent();
        }
        private String GetCompositeContent()
        {
            StringBuilder content = new StringBuilder();
            List<ChecklistItem> subTasks = ChecklistItems;
            if (subTasks.Count <= 0)
            {
                // 如果只有一个默认产生的空item，则清除
                return content.ToString();
            }
            bool isFirst = true;
            foreach (var item in subTasks)
            {
                if (!isFirst)
                {
                    content.Append("\r\n");
                }
                else
                {
                    isFirst = false;
                }
                content.Append(item.Title);
            }
            return content.ToString();
        }
        public void SetTagsInner()
        {
            // TODO 此处有坑，由于不知道转换方式，所以没实现
            //Tags = TagUtils.parseTags(toCompositeNote(title, content));
        }
        public bool IsChecklistMode()
        {
            // 抛弃kind字段
            return this.Kind == Constants.Kind.CHECKLIST;
            //return this.ChecklistItems.Count > 0;
        }
        public bool HasLocation()
        {
            return Location != null;
        }
        public bool HasReminder()
        {
            //return !string.IsNullOrEmpty(Reminder) && Reminder.StartsWith("TRIGGER")
            //        && this.SnoozeRemindTime != null;
            if (this.Reminders == null)
            {
                return false;
            }
            return this.Reminders.Count > 0;
        }
        public bool IsReminder()
        {
            return TaskHelper.IsRemindTask(this).Equals(TaskRemindStatus.VALID);
            //if (DueDate == null || SnoozeRemindTime == null)
            //{
            //    return false;
            //}
            //return HasReminder() && TaskStatus == Constants.TaskStatus.UNCOMPLETED && (SnoozeRemindTime.Value > System.DateTime.UtcNow || IsRepeatTask());
        }
        public void SetRepeatAlertTime()
        {
            if (IsRepeatTask() && HasReminder() && TaskStatus == Constants.TaskStatus.UNCOMPLETED)
            {
                this.RepeatReminderTime = SnoozeRemindTime;
            }
            else
            {
                this.RepeatReminderTime = null;
            }
        }
        [Ignore]
        public bool IsCompleted
        {
            get { return this.TaskStatus == Constants.TaskStatus.COMPLETED; }
        }
        public bool IsUncompleted()
        {
            return TaskStatus == Constants.TaskStatus.UNCOMPLETED;
        }

        #endregion


    }
}
