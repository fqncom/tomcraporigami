
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using Windows.Storage;

namespace TickTick.Dal
{
    public class UserDal : BaseDal<User>
    {


        #region IBaseDal<User> 成员

        /// <summary>
        /// 异步创建person数据库，若存在则不重复创建
        /// </summary>
        /// <returns>返回链接对象</returns>
        //public async Task<SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    var conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
        //    await conn.CreateTableAsync<User>();
        //    return conn;
        //}

        //public async Task<List<User>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    var conn = await CreateTableAsync();
        //    return await conn.QueryAsync<User>(sql, paras);
        //}

        //public Task<int> DeleteData(User t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(User t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<User> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<User>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(User t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<User> t)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        public async Task<User> GetUserByID(String userID)
        {
            var conn = await CreateTableAsync();
            //return await conn.Table<User>().Where((u) => u.Id == userID).FirstOrDefaultAsync();
            return await conn.Table<User>().Where((u) => u.Sid == userID).FirstOrDefaultAsync();

            //StringBuilder selection = new StringBuilder();
            //selection.append(UserField._id.name()).append(" = ?");
            //String[] selectionArgs = new String[] {
            //    userID
            //};
            //    List<User> user = getAllUsers(selection.toString(), selectionArgs);
            //    return user.isEmpty() ? null : user.get(0);
        }
        public async Task<bool> UpdateUser(User user)
        {
            if (LoggerHelper.IS_LOG_ENABLED)
            {
                await LoggerHelper.LogToAllChannels(null, "UpdateUser.user = " + user.ToString());
            }
            var conn = await CreateTableAsync();
            user.ModifiedTime = DateTime.UtcNow;
            return await conn.UpdateAsync(user) > 0;

            //    ContentValues values = makeBaseContentValues(user);
            //    StringBuilder whereClause = new StringBuilder();
            //    whereClause.append(UserField._id.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        user.getId()
            //};
            //    int ret = table.update(values, whereClause.toString(), whereArgs, dbHelper);
            //    return ret > 0;
        }
        /// <summary>
        /// 查询最近登入且未注销的本地用户
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetLocalLastSignUserInfo()
        {
            var conn = await CreateTableAsync();
            return await conn.Table<User>().OrderByDescending(s => s.LastLocalLoginTime).FirstOrDefaultAsync();
        }

        public async Task<User> GetActiveUserById(string userId)
        {
            var queryResult = (await ExecuteAsyncQueryTable()).Where(u => u.Sid == userId && u.Activity == ModelStatusEnum.ACCOUNT_ACTIVE);
            return await queryResult.FirstOrDefaultAsync();

            //    StringBuilder selection = new StringBuilder();
            //    selection.append(UserField._id.name()).append(" = ? and ").append(UserField.activity.name())
            //            .append(" = ?");
            //    String[] selectionArgs = new String[] {
            //        userId, Status.ACCOUNT_ACTIVE + ""
            //};
            //    List<User> user = getAllUsers(selection.toString(), selectionArgs);
            //    return user.isEmpty() ? null : user.get(0);
        }

        public async Task<User> GetUserByActivity(int activity)
        {
            var queryResult = (await ExecuteAsyncQueryTable()).Where(u => u.Activity == activity);
            return await queryResult.FirstOrDefaultAsync();
            //    StringBuffer selection = new StringBuffer();
            //    selection.append(UserField.activity.name()).append(" = ?");
            //    String[] selectionArgs = new String[] {
            //        activity + ""
            //};
            //    List<User> user = getAllUsers(selection.toString(), selectionArgs);
            //    return user.isEmpty() ? null : user.get(0);
        }
    }
}
