using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Dal
{
    public class UserProfileDal : BaseDal<UserProfile>
    {
        #region IBaseDal<UserProfile> 成员

        //public Task<SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<UserProfile>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<UserProfile>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(UserProfile t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(UserProfile t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<UserProfile> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(UserProfile t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<UserProfile> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        /// <summary>
        /// 返回用户设置
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserProfile> GetUserProfileByUserId(string userId)
        {
            var conn = await CreateTableAsync();
            return await conn.Table<UserProfile>().Where((u) => u.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task<UserProfile> GetUserProfileByUser(String userId)
        {
            var conn = await CreateTableAsync();
            return await conn.Table<UserProfile>().Where((u) => u.UserId == userId).FirstOrDefaultAsync();

            //    StringBuffer selection = new StringBuffer();
            //    selection.append(UserProfileField.user_id.name()).append(" = ?");
            //    String[] selectionArgs = new String[] {
            //        userId + ""
            //};
            //    ArrayList<UserProfile> userProfiles = getAllUserProfiles(selection.toString(),
            //            selectionArgs);
            //    return userProfiles.isEmpty() ? null : userProfiles.get(0);
        }
        public async Task<UserProfile> CreateUserProfile(UserProfile profile)
        {
            var conn = await CreateTableAsync();
            profile.CreateTime = DateTime.UtcNow;
            await conn.InsertAsync(profile);
            return profile;

            //ContentValues values = makeCommonValues(profile);
            //long id = table.create(values, dbHelper);
            //profile.setId(id);
            //return profile;
        }
        public async Task<bool> UpdateUserProfile(UserProfile profile)
        {
            var conn = await CreateTableAsync();
            profile.ModifiedTime = DateTime.UtcNow;
            return await conn.UpdateAsync(profile) > 0;

            //    ContentValues values = makeCommonValues(profile);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(UserProfileField._id.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        profile.getId() + ""
            //};
            //    int ret = table.update(values, whereClause.toString(), whereArgs, dbHelper);
            //    return ret > 0;
        }

        public async Task<UserProfile> GetLastOneUserProfileInfoByUserId(string userId)
        {
            var conn = await CreateTableAsync();
            
            return (await conn.Table<UserProfile>().Where(u => u.UserId == userId).ToListAsync()).LastOrDefault();

        }
    }
}
