using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn_AbstractFactoryDemo.IDal;
using fqn_AbstractFactoryDemo.Model;

namespace fqn_AbstractFactoryDemo.SqliteDal
{
    public class UserInfoDal : IUserInfoDal
    {
        public List<UserInfo> GetUserList()
        {
            List<UserInfo> list = new List<UserInfo>();
            string sql = "select * from Users";
            DataTable dt = SqliteHelper.SqliteHelper.ExecuteTable(sql);
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
            if (dataRow["autoId"] != null)
            {
                ui.userId = Convert.ToInt32(dataRow["autoId"]);
            }
            if (dataRow["loginId"] != null)
            {
                ui.userName = dataRow["loginId"].ToString();
            }
            if (dataRow["loginPwd"] != null)
            {
                ui.userPwd = dataRow["loginPwd"].ToString();
            }
            return ui;
        }
    }
}
