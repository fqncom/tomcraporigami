using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Models
{
    public class SignUserInfo
    {

        public readonly static String LOCAL_MODE = "local";
        public readonly static String LOCAL_MODE_ID = "local_id";

        private String _token;

        public String Token
        {
            get { return _token; }
            set { _token = value; }
        }
        private String _userId;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private String _userCode;

        public String UserCode
        {
            get { return _userCode; }
            set { _userCode = value; }
        }
        private String _userName;

        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private bool _isPro;

        public bool IsPro
        {
            get { return _isPro; }
            set { _isPro = value; }
        }
        private DateTime _proEndDate;

        public DateTime ProEndDate
        {
            get { return _proEndDate; }
            set { _proEndDate = value; }
        }
        private String _subscribeType;

        public String SubscribeType
        {
            get { return _subscribeType; }
            set { _subscribeType = value; }
        }
        private bool _needSubscribe;

        public bool NeedSubscribe
        {
            get { return _needSubscribe; }
            set { _needSubscribe = value; }
        }
        private String _inboxId;

        public String InboxId
        {
            get { return _inboxId; }
            set { _inboxId = value; }
        }
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
            return new User { Name = "临时用户", Id = User.LOCAL_MODE_ID };
        }
    }
}
