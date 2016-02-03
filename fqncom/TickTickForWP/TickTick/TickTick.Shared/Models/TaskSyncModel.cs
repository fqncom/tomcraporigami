using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;

namespace TickTick.Models
{
    public class TaskSyncModel
    {
        private TaskSyncBean _taskSyncBean = new TaskSyncBean();

        public TaskSyncBean TaskSyncBean
        {
            get { return _taskSyncBean; }
            set { _taskSyncBean = value; }
        }
        private TaskSyncedJsonBean _taskSyncedJsonBean = new TaskSyncedJsonBean();

        public TaskSyncedJsonBean TaskSyncedJsonBean
        {
            get { return _taskSyncedJsonBean; }
            set { _taskSyncedJsonBean = value; }
        }
        private LocationSyncBean _locationSyncBean = new LocationSyncBean();

        public LocationSyncBean LocationSyncBean
        {
            get { return _locationSyncBean; }
            set { _locationSyncBean = value; }
        }
        private AttachmentSyncBean _attachmentSyncBean = new AttachmentSyncBean();

        public AttachmentSyncBean AttachmentSyncBean
        {
            get { return _attachmentSyncBean; }
            set { _attachmentSyncBean = value; }
        }

        public void AddDeletedForeverTask(Tasks deleteTask)
        {
            TaskSyncBean.AddToDeletedForever(deleteTask);
        }
        public void AddDeletedTaskSyncedJson(TaskSyncedJson deletedJson)
        {
            TaskSyncedJsonBean.AddToDeleted(deletedJson);
        }

        public void AddDeletedInTrashTask(Tasks deleteTask)
        {
            TaskSyncBean.AddToDeletedInTrash(deleteTask);
        }
        public void AddUpdatingTask(Tasks updating)
        {
            TaskSyncBean.AddToUpdating(updating);
        }
        public void AddUpdatedTask(Tasks update)
        {
            TaskSyncBean.AddToUpdated(update);
        }
        public void AddUpdatedTaskSyncedJson(TasksServer serviceTask)
        {
            TaskSyncedJsonBean.AddToUpdated(serviceTask);
        }
        public void AddAddedTaskSyncedJson(TasksServer serviceTask)
        {
            TaskSyncedJsonBean.AddToAdded(serviceTask);
        }
        public void AddUpdateLocation(Location update)
        {
            LocationSyncBean.AddUpdateLocation(update);
        }
        public void AddAllAddedAttachments(List<Attachment> addeds)
        {
            AttachmentSyncBean.AddAllAddeds(addeds);
        }
        public void AddAddedTask(Tasks add)
        {
            TaskSyncBean.AddToAdded(add);
        }
    }
}
