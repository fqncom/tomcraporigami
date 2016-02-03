using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TickTick.Enums;
using Windows.Globalization;
using SQLite;

namespace TickTick.Entity
{
    public class UserProfile
    {
        //#region fqn自定义属性
        //private int _tableId;
        ///// <summary>
        ///// 表的自增列，使用
        ///// </summary>
        //[PrimaryKey]
        //[AutoIncrement]
        //public int TableId
        //{
        //    get { return _tableId; }
        //    set { _tableId = value; }
        //}

        //#endregion
        private long _id;
        [PrimaryKey]
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _userId;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private int _isShowTodayList;

        public int IsShowTodayList
        {
            get { return _isShowTodayList; }
            set { _isShowTodayList = value; }
        }
        private int _isShow7DaysList;

        public int IsShow7DaysList
        {
            get { return _isShow7DaysList; }
            set { _isShow7DaysList = value; }
        }

        private bool _isShowAllList;

        public bool IsShowAllList
        {
            get { return _isShowAllList; }
            set { _isShowAllList = value; }
        }


        private String _defaultReminderTime;

        public String DefaultReminderTime
        {
            get { return _defaultReminderTime; }
            set { _defaultReminderTime = value; }
        }
        private String _dailyReminderTime;

        public String DailyReminderTime
        {
            get { return _dailyReminderTime; }
            set { _dailyReminderTime = value; }
        }
        private int _meridiemType;

        public int MeridiemType
        {
            get { return _meridiemType; }
            set { _meridiemType = value; }
        }
        private int _startDayWeek = 0;

        public int StartDayWeek
        {
            get { return _startDayWeek; }
            set { _startDayWeek = value; }
        }
        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private String _etag;

        public String Etag
        {
            get { return _etag; }
            set { _etag = value; }
        }
        //private SortType _sortTypeOfAllProject = SortType.DUE_DATE;

        //public SortType SortTypeOfAllProject
        //{
        //    get { return _sortTypeOfAllProject; }
        //    set { _sortTypeOfAllProject = value; }
        //}
        //private SortType _sortTypeOfInbox = SortType.USER_ORDER;

        //public SortType SortTypeOfInbox
        //{
        //    get { return _sortTypeOfInbox; }
        //    set { _sortTypeOfInbox = value; }
        //}

        private int _isShowCompletedList = ModelStatusEnum.NO;

        public int IsShowCompletedList
        {
            get { return _isShowCompletedList; }
            set { _isShowCompletedList = value; }
        }

        private bool _isShowTagsList = false;

        public bool IsShowTagsList
        {
            get { return _isShowTagsList; }
            set { _isShowTagsList = value; }
        }
        private bool _isShowScheduledList = false;

        public bool IsShowScheduledList
        {
            get { return _isShowScheduledList; }
            set { _isShowScheduledList = value; }
        }
        private bool _isShowTrashList = false;

        public bool IsShowTrashList
        {
            get { return _isShowTrashList; }
            set { _isShowTrashList = value; }
        }

        private bool _isFakeEmail = false;

        public bool IsFakeEmail
        {
            get { return _isFakeEmail; }
            set { _isFakeEmail = value; }
        }
        private DateTime _createTIme;

        public DateTime CreateTime
        {
            get { return _createTIme; }
            set { _createTIme = value; }
        }
        private DateTime _modifiedTime;

        public DateTime ModifiedTime
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }



        /// <summary>
        /// 创建默认userProfile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserProfile CreateDefaultUserProfile(String userId)
        {
            UserProfile profile = new UserProfile();
            profile.Id = Constants.EntityIdentifie.DEFAULT_USER_PROFILE_ID;
            profile.UserId = userId;
            profile.IsShowTodayList = 1;
            profile.IsShow7DaysList = 0;
            profile.DefaultReminderTime = "-1";
            //profile.DailyReminderTime = DateUtils.removeDailyReminderTimeZone("09:00");
            profile.MeridiemType = Constants.MeridiemType.TYPE_24_HOUR;//==========此处处理日期留坑
            //DateFormat.is24HourFormat(TickTickApplicationBase.StaticApplication) ?
            //    Constants.MeridiemType.TYPE_24_HOUR
            //    :
            //    Constants.MeridiemType.TYPE_AM_PM;
            //profile.StartDayWeek = new DateTimeFormatInfo().FirstDayOfWeek;
            profile.Status = ModelStatusEnum.SYNC_NEW;
            profile.IsShowCompletedList = ModelStatusEnum.NO;
            profile.IsShowTagsList = false;
            profile.IsShowScheduledList = true;
            profile.IsShowTrashList = false;
            //profile.SortTypeOfAllProject = SortType.DUE_DATE;
            //profile.SortTypeOfInbox = SortType.USER_ORDER;
            return profile;
        }
    }
}
