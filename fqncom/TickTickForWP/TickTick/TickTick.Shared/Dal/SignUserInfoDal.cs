using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Models;

namespace TickTick.Dal
{
    public class SignUserInfoDal : BaseDal<User>
    {
        /// <summary>
        /// 查询最近登入且未注销的本地用户
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetLocalLastSignUserInfo()
        {
            var conn = await CreateTableAsync();
            return await conn.Table<User>().Where(s => !s.IsLogOut).OrderByDescending(s => s.LastLocalLoginTime).FirstOrDefaultAsync();
        }
    }
}
