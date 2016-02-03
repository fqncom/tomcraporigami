using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Entity;
using Windows.UI.Xaml;

namespace TickTick.Manager
{
    public class TickTickAccountManager
    {
        //private TickTickApplicationBase _application;
        private UserBll UserBll = new UserBll();
        //private UserProfileBll UserProfileBll = new UserProfileBll();
        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                //InitialCurrentUser();// TODO 寻找更好的解决方案
                return _currentUser;
            }
            set { _currentUser = value; }
        }
        public async Task SetCheckpoint(String id, long checkpoint)
        {
            User user = await UserBll.GetUserById(id);
            if (user != null)
            {
                user.Checkpoint = checkpoint;
                await UpdateCurrentUserCache(user);
                await UserBll.UpdateUserToDB(user);
            }
        }
        private async Task UpdateCurrentUserCache(User user)
        {
            if (CurrentUser==null)
            {
                await InitialCurrentUser();
            }
            if (string.Equals(user.Sid, CurrentUser.Sid))
            {
                CurrentUser = user;
            }
        }

        public async Task<User> GetAccountById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            if (string.Equals(User.LOCAL_MODE_ID, userId))
            {
                return await InstantLocalUser();
            }
            return await UserBll.GetActiveUserById(userId);
        }
        public async Task<User> InstantLocalUser()
        {
            return await UserBll.InstantLocalUser();
        }
        public async Task InitialCurrentUser()// 同 GetCurrentUser
        {
            if (_currentUser == null || _currentUser.UserProfile == null)
            {
                _currentUser = await UserBll.GetCurrentUser();
                if (_currentUser == null)
                {
                    _currentUser = await InstantLocalUser();
                }
            }
        }

    }
}
