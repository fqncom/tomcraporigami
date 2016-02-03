using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Models
{
    public class SyncResult
    {

        private bool _remoteTaskChanged = false;

        public bool RemoteTaskChanged
        {
            get { return _remoteTaskChanged; }
            set { _remoteTaskChanged = value; }
        }

        private bool _remoteProjectChanged = false;

        public bool RemoteProjectChanged
        {
            get { return _remoteProjectChanged; }
            set { _remoteProjectChanged = value; }
        }
        private bool _pushedLocalChanges = false;

        public bool PushedLocalChanges
        {
            get { return _pushedLocalChanges; }
            set { _pushedLocalChanges = value; }
        }

        private bool _remoteUserInfoChanged = false;

        public bool RemoteUserInfoChanged
        {
            get { return _remoteUserInfoChanged; }
            set { _remoteUserInfoChanged = value; }
        }
        private bool _notificationCountChanged = false;

        public bool NotificationCountChanged
        {
            get { return _notificationCountChanged; }
            set { _notificationCountChanged = value; }
        }
        private bool _userProfileChanged;

        public bool UserProfileChanged
        {
            get { return _userProfileChanged; }
            set { _userProfileChanged = value; }
        }

        public bool HasChanged()
        {
            return RemoteProjectChanged || RemoteTaskChanged || PushedLocalChanges
                    || RemoteUserInfoChanged || NotificationCountChanged || UserProfileChanged;
        }
        public void SetPushedLocalChanges(bool pushedLocalChanges)
        {
            this.PushedLocalChanges = pushedLocalChanges;
        }



        #region android代码
        //private boolean remoteProjectChanged = false;

        //private boolean remoteTaskChanged = false;

        //private boolean pushedLocalChanges = false;

        //private boolean remoteUserInfoChanged = false;

        //private boolean notificationCountChanged = false;

        //private boolean userProfileChanged = false;

        //public void setPushedLocalChanges(boolean pushedLocalChanges)
        //{
        //    this.pushedLocalChanges = pushedLocalChanges;
        //}

        //public boolean hasChanged()
        //{
        //    return remoteProjectChanged || remoteTaskChanged || pushedLocalChanges
        //            || remoteUserInfoChanged || notificationCountChanged || userProfileChanged;
        //}


        //public void setNotificationCountChanged(boolean notificationCountChanged)
        //{
        //    this.notificationCountChanged = notificationCountChanged;
        //}

        //public void setUserProfileChanged(boolean userProfileChanged)
        //{
        //    this.userProfileChanged = userProfileChanged;
        //}

        //public void setRemoteProjectChanged(boolean remoteProjectChanged)
        //{
        //    this.remoteProjectChanged = remoteProjectChanged;
        //}

        //public boolean isRemoteTaskChanged()
        //{
        //    return remoteTaskChanged;
        //}

        //public void setRemoteTaskChanged(boolean remoteTaskChanged)
        //{
        //    this.remoteTaskChanged = remoteTaskChanged;
        //}
        #endregion
    }
}
