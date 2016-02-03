using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Models
{
    public class SignUserInfo
    {
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
        private String _username;

        public String Username
        {
            get { return _username; }
            set { _username = value; }
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
        private Boolean _needSubscribe;

        public Boolean NeedSubscribe
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
    }
}
