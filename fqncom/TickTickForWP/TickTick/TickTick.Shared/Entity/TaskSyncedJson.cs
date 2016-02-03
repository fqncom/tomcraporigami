using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class TaskSyncedJson : BaseEntity
    {
        //private long _id;

        //public long Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}
        private string _taskSID;

        public string TaskSID
        {
            get { return _taskSID; }
            set { _taskSID = value; }
        }
        //private string _userID;

        //public string UserID
        //{
        //    get { return _userID; }
        //    set { _userID = value; }
        //}
        private string _jsonString;

        public string JsonString
        {
            get { return _jsonString; }
            set { _jsonString = value; }
        }
    }
}
