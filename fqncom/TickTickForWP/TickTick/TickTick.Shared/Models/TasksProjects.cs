using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Models
{
    public class TasksProjects
    {

        private String _taskId;

        public String TaskId
        {
            get { return _taskId; }
            set { _taskId = value; }
        }
        private String _projectId;

        public String ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }
        private long _sortOrder;

        public long SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }

        public TasksProjects() { }

        public TasksProjects(String projectId, String taskId)
        {
            this.ProjectId = projectId;
            this.TaskId = taskId;
        }

        public TasksProjects(String projectId, String taskId, long sortOrder)
        {
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.SortOrder = sortOrder;
        }
    }
}
