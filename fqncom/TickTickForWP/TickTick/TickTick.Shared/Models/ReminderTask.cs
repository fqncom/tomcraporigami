using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using TickTick.Models;

namespace TickTick.Models
{
    public class ReminderTask
    {
        public static readonly int TYPE_REMINDER_TIME = 0;
        public static readonly int TYPE_REMINDER_LOCATION = 1;
        private readonly User _user;

        private long _taskListId;

        public long TaskListId
        {
            get { return _taskListId; }
            set { _taskListId = value; }
        }
        private bool _completed;

        public bool Completed
        {
            get { return _completed; }
            set { _completed = value; }
        }
        private DateTime _reminderTime;

        public DateTime ReminderTime
        {
            get { return _reminderTime; }
            set { _reminderTime = value; }
        }
        private DateTime _taskDate;

        public DateTime TaskDate
        {
            get { return _taskDate; }
            set { _taskDate = value; }
        }
        private bool _hasReminder;

        public bool HasReminder
        {
            get { return _hasReminder; }
            set { _hasReminder = value; }
        }
        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private String _notes;

        public String Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private Location Location = null;
        private DateTime? FiredTime = null;
        private String _projectName;

        public String ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }
        private long ReminderId = TickTick.Enums.Constants.EntityIdentifie.DEFAULT_NEW_ID;
        //private Identity identity;

        public bool IsCompleted()
        {
            return Completed;
        }
    }
}
