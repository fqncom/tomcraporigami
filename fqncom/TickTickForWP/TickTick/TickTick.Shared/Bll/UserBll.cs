using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Bll
{
    public class UserBll : BaseBll<User>
    {

        /// <summary>
        /// 用户dal层对象
        /// </summary>
        private UserDal UserDal = new UserDal();
        private UserProfileDal UserProfileDal = new UserProfileDal();



        public async Task<User> InstantLocalUser()
        {
            User user = new User();
            //user.Id = User.LOCAL_MODE_ID;
            user.Sid = User.LOCAL_MODE_ID;
            user.UserName = User.LOCAL_MODE;
            user.AccountType = Constants.AccountType.ACCOUNT_TYPE_LOCAL_MODE;
            UserProfile profile = await UserProfileDal.GetUserProfileByUserId(User.LOCAL_MODE_ID);
            user.UserProfile = profile ?? UserProfile.CreateDefaultUserProfile(User.LOCAL_MODE_ID);
            return user;
        }
        public async Task<User> GetUserById(String userId)
        {
            User user = await UserDal.GetUserByID(userId);
            return await AddWithUserProfile(user);
        }
        private async Task<User> AddWithUserProfile(User user)
        {
            if (user != null)
            {
                //UserProfile profile = await UserProfileDal.GetUserProfileByUser(user.Id);
                //profile = profile == null ? UserProfile.CreateDefaultUserProfile(user.Id) : profile;

                UserProfile profile = await UserProfileDal.GetUserProfileByUser(user.Sid);
                profile = profile == null ? UserProfile.CreateDefaultUserProfile(user.Sid) : profile;
                user.UserProfile = profile;
            }
            return user;
        }
        public async Task UpdateUserToDB(User user)
        {
            if (!user.IsLocalMode())
            {
                await UserDal.UpdateUser(user);
                await SaveUserProfile(user);
            }
        }
        private async Task SaveUserProfile(User user)
        {
            //UserProfile profile = await UserProfileDal.GetUserProfileByUser(user.Id);
            UserProfile profile = await UserProfileDal.GetUserProfileByUser(user.Sid);
            if (profile == null)
            {
                if (user.UserProfile == null)
                {
                    //UserProfile newProfile = await UserProfileDal.CreateUserProfile(UserProfile.CreateDefaultUserProfile(user.Id));
                    UserProfile newProfile = await UserProfileDal.CreateUserProfile(UserProfile.CreateDefaultUserProfile(user.Sid));
                    user.UserProfile = newProfile;
                }
                else
                {
                    UserProfile newProfile = await UserProfileDal.CreateUserProfile(user.UserProfile);
                    user.UserProfile = newProfile;
                }
            }
            else
            {
                if (user.UserProfile != null)
                {
                    await UserProfileDal.UpdateUserProfile(user.UserProfile);
                }
            }
        }
        public async Task InsertUserInfo(User user)
        {
            await UserDal.InsertAsync(user);
        }

        protected override void SetCurrentDal()
        {
            CurrentDal = UserDal;
        }

        public async Task UpdateUserLoginTime(User user)
        {
            user.LastLocalLoginTime = DateTime.UtcNow;
            user.Activity = ModelStatusEnum.ACCOUNT_ACTIVE;
            var realUser = await UserDal.GetUserByID(user.Sid);
            if (realUser == null)
            {
                await this.InsertUserInfo(user);
            }
            else
            {
                await this.UpdateUserToDB(user);
            }
        }

        public async Task<User> GetLocalLastSignUserInfo()
        {
            return await UserDal.GetLocalLastSignUserInfo();
        }

        public async Task<User> GetActiveUserById(string userId)
        {
            User user = await UserDal.GetActiveUserById(userId);
            return await AddWithUserProfile(user);
        }
        public async Task<User> GetCurrentUser()
        {
            User user = await UserDal.GetUserByActivity(ModelStatusEnum.ACCOUNT_ACTIVE);
            if (user == null)
            {
                return await InstantLocalUser();
            }
            else
            {
                return await AddWithUserProfile(user);
            }
        }
    }
}
