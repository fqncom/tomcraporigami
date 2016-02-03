using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Models;

namespace TickTick.Bll
{
    public class UserProfileBll : BaseBll<UserProfile>
    {
        private UserProfileDal UserProfileDal = new UserProfileDal();

        protected override void SetCurrentDal()
        {
            CurrentDal = UserProfileDal;
        }




        public async Task<UserProfile> GetLastOneUserProfileInfoByUserId(string userId)
        {
            return await UserProfileDal.GetLastOneUserProfileInfoByUserId(userId);
        }

        public async  Task SaveUpdateUserProfile(UserProfile userProfile)
        {
            await UserProfileDal.UpdateUserProfile(userProfile);
        }
    }
}
