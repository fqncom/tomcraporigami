using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TickTick.Enums;

namespace TickTick.Entity
{
    public class BaseEntity : INotifyPropertyChanged
    {
        private int _deleted = ModelStatusEnum.DELETED_NO;

        public int Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        /**
         * task or calendar event
         */
        private int _type;

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public bool IsMoveToTrash()
        {
            return this.Deleted == ModelStatusEnum.DELETED_TRASH;
        }

        //private static readonly long serialVersionUID = 2532569449574898519L;

        public static class OrderStepData
        {
            public static readonly long STEP = 1L << 38;
        }

        private int _id;
        [PrimaryKey]
        [AutoIncrement]
        [JsonIgnore]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _sId;

        [JsonProperty("id")]
        public String SId
        {
            get { return _sId; }
            set { _sId = value; }
        }
        private String _userId;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private DateTime _createdTime;

        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { _createdTime = value; }
        }
        private DateTime _modifiedTime;

        public DateTime ModifiedTime
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        private String _etag;

        public String Etag
        {
            get { return _etag; }
            set { _etag = value; }
        }
        /** 由Google账户下合并过来的Id **/
        private String _googleId;

        public String GoogleId
        {
            get { return _googleId; }
            set { _googleId = value; }
        }
        /** temp of merge data,used for googleId is null **/
        private long _localId;

        public long LocalId
        {
            get { return _localId; }
            set { _localId = value; }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性变化
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 设置属性变化，内部通知修改
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="current">当前属性</param>
        /// <param name="value">新设置的值</param>
        /// <param name="propertyName">属性名称</param>
        public void SetProperty<T>(ref T current, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(current, value))
            {
                return;
            }
            current = value;
            // 通知修改 
            OnPropertyChanged(propertyName);
        }
        #endregion
    }
}
