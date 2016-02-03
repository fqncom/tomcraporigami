using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Models
{
    public class MoveProject
    {
        private String _fromProjectId;

        public String FromProjectId
        {
            get { return _fromProjectId; }
            set { _fromProjectId = value; }
        }
        private String _toProjectId;

        public String ToProjectId
        {
            get { return _toProjectId; }
            set { _toProjectId = value; }
        }
        private String _taskId;

        public String TaskId
        {
            get { return _taskId; }
            set { _taskId = value; }
        }
        private long _sortOrder;

        public long SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
    }
}
