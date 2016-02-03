using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class Items
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        private int _id;

        [PrimaryKey]
        [AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 项目名
        /// </summary>
        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        /// <summary>
        /// 所属任务名
        /// </summary>
        private int _taskId;

        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value; }
        }

    }
}
