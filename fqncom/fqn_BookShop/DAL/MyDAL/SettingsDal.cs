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
    public partial class SettingsDal
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MyBookShop.Model.Settings GetModel(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Name,Value from Settings ");
            strSql.Append(" where Name=@name");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = name;

            MyBookShop.Model.Settings model = new MyBookShop.Model.Settings();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

    }
}
