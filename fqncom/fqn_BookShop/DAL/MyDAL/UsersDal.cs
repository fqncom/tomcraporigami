using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.DBUtility;

namespace MyBookShop.DAL
{
    public partial class UsersDal
    {
        /// <summary>
        /// 根据loginId判断是否存在该记录
        /// </summary>
        /// <param name="LoginId">输入的loginID</param>
        /// <returns></returns>
        public object CheckIsExistNameByName(string loginId)
        {
            string sql = "select COUNT(*) from Users where LoginId = @LoginId";
            SqlParameter[] parameters = 
            {
				new SqlParameter("@LoginId", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = loginId;

            return DbHelperSQL.GetSingle(sql, parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">增加的数据</param>
        /// <param name="userId">返回的Id</param>
        /// <returns>返回受影响行数</returns>
        public object Add(MyBookShop.Model.Users model, out int userId)
        {
            string sql = "insert into Users(LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId) output inserted.Id values  (@LoginId,@LoginPwd,@Name,@Address,@Phone,@Mail,@UserStateId)";

            SqlParameter[] parameters = {
					new SqlParameter("@LoginId", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,100),
					new SqlParameter("@Mail", SqlDbType.NVarChar,100),
					new SqlParameter("@UserStateId", SqlDbType.Int,4)};
            parameters[0].Value = model.LoginId;
            parameters[1].Value = model.LoginPwd;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Address;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Mail;
            parameters[6].Value = model.UserState.Id;

            userId = Convert.ToInt32(DbHelperSQL.GetSingle(sql, parameters));
            return userId > 0 ? 1 : 0;
        }

        /// <summary>
        /// 根据用户名获取用户密码
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public object GetUserLoginPwdByLoginId(string loginId)
        {
            string sql = "select LoginPwd from Users where UserStateId = 1 and LoginId = @LoginId";
            return DbHelperSQL.GetSingle(sql, new SqlParameter("@LoginId", loginId));
        }

        public Model.Users GetUserInfoByLoginId(string loginId)
        {
            string sql = "select * from Users where LoginId = @LoginId";
            return DataRowToModel(DbHelperSQL.Query(sql, new SqlParameter("@LoginId", loginId)).Tables[0].Rows[0]);
        }
    }
}
