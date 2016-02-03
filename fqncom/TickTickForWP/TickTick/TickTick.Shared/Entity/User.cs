using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Enums;

namespace TickTick.Entity
{
    public class User
    {
        #region fqn自定义属性

        ///// <summary>
        ///// 用户Id，主键，自增
        ///// </summary>
        // private int _id;

        // public int Id
        // {
        //     get { return _id; }
        //     set { _id = value; }
        // }

        ///// <summary>
        ///// 用户名
        ///// </summary>
        // private string _userName;

        // public string UserName
        // {
        //     get { return _userName; }
        //     set { _userName = value; }
        // }


        ///// <summary>
        ///// 用户状态
        ///// </summary>
        // private UserStatus _userStatus;

        // public UserStatus UserStatus
        // {
        //     get { return _userStatus; }
        //     set { _userStatus = value; }
        // } 
        #endregion

        public readonly static String LOCAL_MODE = "local";
        public readonly static String LOCAL_MODE_ID = "local_id";
        private int _id;
        [PrimaryKey]
        [AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _sid;
        [JsonProperty("userId")]
        public String Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private String _userName;

        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private String _password;

        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private String _authToken;
        [JsonProperty("token")]
        public String AuthToken
        {
            get { return _authToken; }
            set { _authToken = value; }
        }
        private int _accountType;

        public int AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }
        private long _checkpoint;

        public long Checkpoint
        {
            get { return _checkpoint; }
            set { _checkpoint = value; }
        }
        private long _settingsBackupPoint;

        public long SettingsBackupPoint
        {
            get { return _settingsBackupPoint; }
            set { _settingsBackupPoint = value; }
        }
        private long _listBackupPoint;

        public long ListBackupPoint
        {
            get { return _listBackupPoint; }
            set { _listBackupPoint = value; }
        }
        private long _taskBackupPoint;

        public long TaskBackupPoint
        {
            get { return _taskBackupPoint; }
            set { _taskBackupPoint = value; }
        }
        private int _activity = ModelStatusEnum.ACCOUNT_FREEZE;

        public int Activity
        {
            get { return _activity; }
            set { _activity = value; }
        }
        private int _wake = ModelStatusEnum.SLEEP;

        public int Wake
        {
            get { return _wake; }
            set { _wake = value; }
        }
        private DateTime _modifiedTime;

        public DateTime ModifiedTime
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        private DateTime _createdTime;

        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { _createdTime = value; }
        }
        private int _isDisabled = ModelStatusEnum.ACCOUNT_DISABLED_NO;

        public int IsDisabled
        {
            get { return _isDisabled; }
            set { _isDisabled = value; }
        }

        private DateTime _proEndTime;
        [JsonProperty("proEndDate")]
        public DateTime ProEndTime
        {
            get { return _proEndTime; }
            set { _proEndTime = value; }
        }
        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private String _domain;

        public String Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }
        private String _avatar;

        public String Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        private String _subscribeType;

        public String SubscribeType
        {
            get { return _subscribeType; }
            set { _subscribeType = value; }
        }

        /**
         * 一般都为null，只有通过Google账户登陆注册GTasks账户时有效 *
         */
        private String _requestToken;

        public String RequestToken
        {
            get { return _requestToken; }
            set { _requestToken = value; }
        }

        private UserProfile _userProfile;
        [Ignore]
        public UserProfile UserProfile
        {
            get { return _userProfile; }
            set { _userProfile = value; }
        }
        private int _proType = Constants.AccountType.ACCOUNT_TYPE_FREE;

        public int ProType
        {
            get { return _proType; }
            set { _proType = value; }
        }

        //Server端对User的标示，更适合对外暴露
        private String _userCode;

        public String UserCode
        {
            get { return _userCode; }
            set { _userCode = value; }
        }

        public bool IsPro()
        {
            return this.ProType == Constants.AccountType.ACCOUNT_TYPE_PREMIUM;
        }
        public bool IsLocalMode()
        {
            return this.AccountType == Constants.AccountType.ACCOUNT_TYPE_LOCAL_MODE;
        }
        private string _inboxId;

        public string InboxId
        {
            get { return _inboxId; }
            set { _inboxId = value; }
        }


        #region fqn 自定义属性
        /// <summary>
        /// 本地数据，无需同步
        /// </summary>
        private DateTime _lastLocalLoginTime;
        [JsonIgnore]
        public DateTime LastLocalLoginTime
        {
            get { return _lastLocalLoginTime; }
            set { _lastLocalLoginTime = value; }
        }
        private bool _isLogOut;
        [JsonIgnore]
        public bool IsLogOut
        {
            get { return _isLogOut; }
            set { _isLogOut = value; }
        }

        /// <summary>
        /// 创建临时用户
        /// </summary>
        /// <returns></returns>
        public static User GetDefaultSignUserInfo()
        {
            return new User { UserName = "临时用户", Sid = User.LOCAL_MODE_ID };
        }
        #endregion
    }
}
