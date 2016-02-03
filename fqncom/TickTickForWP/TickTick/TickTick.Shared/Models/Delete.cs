using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TickTick.Models
{
    public class Delete
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


    }
}
