using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Models;

namespace TickTick.Entity
{
    public class TaskReminder
    {
        public TaskReminder() { }
        public TaskReminder(TaskReminder src)
        {
            Id = src.Id;
            Sid = src.Sid;
            UserId = src.UserId;
            TaskId = src.TaskId;
            TaskSid = src.TaskSid;
            Duration = new TickTickDuration(src.GetDurationString());
        }

        private DateTime _remindTime;

        public DateTime RemindTime
        {
            get { return _remindTime; }
            set { _remindTime = value; }
        }

        private int _id;
        [PrimaryKey]
        [AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _sid;

        public String Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }

        private String _userId;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

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

        private TickTickDuration _duration;
        [Ignore]
        public TickTickDuration Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public String GetDurationString()
        {
            if (Duration == null)
            {
                return null;
            }
            return Duration.ToString();
        }
        public void SetDuration(String durationStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(durationStr))
                {
                    _duration = new TickTickDuration(durationStr);
                    return;
                }
            }
            catch (Exception e)
            {
                //Log.e("TaskReminder", "To TickTickDuration failed, durationStr = " + durationStr);
            }
            _duration = null;
        }
    }
}
