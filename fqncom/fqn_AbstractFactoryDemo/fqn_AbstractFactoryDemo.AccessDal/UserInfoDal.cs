using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn_AbstractFactoryDemo.IDal;
using fqn_AbstractFactoryDemo.Model;

namespace fqn_AbstractFactoryDemo.AccessDal
{
    public class UserInfoDal : IUserInfoDal
    {
        public List<UserInfo> GetUserList()
        {
            List<UserInfo> list = new List<UserInfo>();
            string sql = "select * from UserInfo";
            DataTable dt = OleHelper.ExecuteTable(sql,CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    list.Add(RowToModel(dataRow));
                }
            }
            return list;
        }

        private UserInfo RowToModel(DataRow dataRow)
        {
            UserInfo ui = new UserInfo();
            if (dataRow["userId"] != null)
            {
                ui.userId = Convert.ToInt32(dataRow["userId"]);
            }
            if (dataRow["userName"] != null)
            {
                ui.userName = dataRow["userName"].ToString();
            }
            if (dataRow["userPwd"] != null)
            {
                ui.userPwd = dataRow["userPwd"].ToString();
            }
            return ui;
        }
    }
}
