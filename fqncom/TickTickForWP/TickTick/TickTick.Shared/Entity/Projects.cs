using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Enums;

namespace TickTick.Entity
{
    public partial class Projects : BaseEntity
    {
        #region fqn自定义属性
        ///// <summary>
        ///// 分类Id
        ///// </summary>
        //private string _id;

        //[PrimaryKey]
        //[AutoIncrement]
        //public string Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        ///// <summary>
        ///// 分类名
        ///// </summary>
        //private string _projectName;

        //public string ProjectName
        //{
        //    get { return _projectName; }
        //    set { _projectName = value; }
        //} 
        #endregion

        //private string _id;

        //public string Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private bool _isOwner = true;

        public bool IsOwner
        {
            get { return _isOwner; }
            set { _isOwner = value; }
        }
        private string _color;

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
        private bool _inAll;

        public bool InAll
        {
            get { return _inAll; }
            set { _inAll = value; }
        }
        private long _sortOrder;

        public long SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
        private string _sortType;

        /// <summary>
        /// 排序类型，===========未设置默认值
        /// </summary>
        public string SortType
        {
            get { return _sortType; }
            set { _sortType = value; }
        }
        private int _userCount = 1;

        public int UserCount
        {
            get { return _userCount; }
            set { _userCount = value; }
        }
        //private string _etag;

        //public string Etag
        //{
        //    get { return _etag; }
        //    set { _etag = value; }
        //}
        //private DateTime _modifiedTime;

        //public DateTime ModifiedTime
        //{
        //    get { return _modifiedTime; }
        //    set { _modifiedTime = value; }
        //}
        /// <summary>
        /// 关闭标识，默认为不关闭，使用枚举
        /// </summary>
        private int _closed = ModelStatusEnum.CLOSED_NO;

        public int Closed
        {
            get { return _closed; }
            set { _closed = value; }
        }

        public bool IsLocalAdded()
        {
            if (Deleted != ModelStatusEnum.DELETED_NO)
            {
                return false;
            }
            return Status == ModelStatusEnum.SYNC_NEW || (Status == ModelStatusEnum.SYNC_UPDATE && !HasSynced());
        }
        public bool IsShowInAll()
        {
            return ShowInAll;
        }

        #region fqn添加的属性
        //private string _sId;

        //public string SId
        //{
        //    get { return _sId; }
        //    set { _sId = value; }
        //}
        //private string _userId;

        //public string UserId
        //{
        //    get { return _userId; }
        //    set { _userId = value; }
        //}
        private int _defaultProject;

        public int DefaultProject
        {
            get { return _defaultProject; }
            set { _defaultProject = value; }
        }
        private bool _showInAll;

        public bool ShowInAll
        {
            get { return _showInAll; }
            set { _showInAll = value; }
        }
        //private DateTime _createTime;

        //public DateTime CreateTime
        //{
        //    get { return _createTime; }
        //    set { _createTime = value; }
        //}
        ///// <summary>
        ///// 删除标识，默认不删除，可以使用枚举
        ///// </summary>
        //private int _deleted = ModelStatusEnum.DELETED_NO;

        //public int Deleted
        //{
        //    get { return _deleted; }
        //    set { _deleted = value; }
        //}
        private int _status = ModelStatusEnum.SYNC_NEW;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 是否需要推送提醒？默认是不需要，可以设置枚举
        /// </summary>
        private int _needPullTasks = ModelStatusEnum.PROJECT_DONT_NEED_PULL_TASKS;

        public int NeedPullTasks
        {
            get { return _needPullTasks; }
            set { _needPullTasks = value; }
        }
        public bool IsLocalUpdated()
        {
            if (this.Deleted != ModelStatusEnum.DELETED_NO)
            {
                return false;
            }
            return this.Status == ModelStatusEnum.SYNC_UPDATE && HasSynced();
        }
        public bool HasSynced()
        {
            return !string.IsNullOrEmpty(this.Etag);
        }
        public bool IsClosed()
        {
            return Closed == ModelStatusEnum.CLOSED_YES;
        }
        public bool IsLocalDeleted()
        {
            if (!HasSynced() || Status == ModelStatusEnum.SYNC_DONE)
            {
                return false;
            }

            return Deleted == ModelStatusEnum.DELETED_TRASH;
        }


        #endregion
    }
}
