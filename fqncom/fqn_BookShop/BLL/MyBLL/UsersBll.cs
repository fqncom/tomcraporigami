using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShop.BLL
{
    public partial class UsersBll
    {
        /// <summary>
        /// 根据loginId判断是否存在该记录
        /// </summary>
        /// <param name="LoginId">输入的loginID</param>
        /// <returns>返回是否存在</returns>
        public bool CheckIsExistNameByName(string loginId)
        {
            return Convert.ToInt32(dal.CheckIsExistNameByName(loginId)) > 0;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">增加的数据</param>
        /// <param name="userId">返回的Id</param>
        /// <returns>返回受影响行数</returns>
        public int Add(MyBookShop.Model.Users model, out int userId)
        {
            return Convert.ToInt32(dal.Add(model, out userId));
        }

        /// <summary>
        /// 根据用户名获取用户密码
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public object GetUserLoginPwdByLoginId(string loginId)
        {
            return dal.GetUserLoginPwdByLoginId(loginId);
        }

        public bool CheckUserMailByLoginId(string loginId, string loginMail)
        {
            Model.Users user = dal.GetUserInfoByLoginId(loginId);
            if (user != null)
            {
                return user.Mail.Equals(loginMail);
            }
            return false;
        }

        /// <summary>
        /// 根据用登录名获取用户
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public Model.Users GetModelByLoginId(string loginId)
        {
            return dal.GetUserInfoByLoginId(loginId);
        }

    }
}
