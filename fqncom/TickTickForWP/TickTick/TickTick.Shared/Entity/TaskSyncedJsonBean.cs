using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Models;

namespace TickTick.Entity
{
    public class TaskSyncedJsonBean
    {
        private List<TasksServer> _added = new List<TasksServer>();

        public List<TasksServer> Added
        {
            get { return _added; }
            set { _added = value; }
        }
        private List<TasksServer> _updated = new List<TasksServer>();

        public List<TasksServer> Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
        private List<TaskSyncedJson> deleted = new List<TaskSyncedJson>();

        public List<TaskSyncedJson> Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        public void AddToDeleted(TaskSyncedJson deleteJson)
        {
            if (deleteJson != null)
            {
                deleted.Add(deleteJson);
            }
        }
        public void AddToUpdated(TasksServer task)
        {
            if (task != null)
            {
                Updated.Add(task);
            }
        }
        public void AddToAdded(TasksServer task)
        {
            if (task != null)
            {
                Added.Add(task);
            }
        }
    }
}
