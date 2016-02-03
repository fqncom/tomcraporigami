using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class Comment : BaseEntity
    {

        //private int _id;

        //public int Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}
        private string _taskSId;

        public string TaskSId
        {
            get { return _taskSId; }
            set { _taskSId = value; }
        }
        private string _projectSid;

        public string ProjectSid
        {
            get { return _projectSid; }
            set { _projectSid = value; }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _ownerSid;

        public string OwnerSid
        {
            get { return _ownerSid; }
            set { _ownerSid = value; }
        }
    }
}
