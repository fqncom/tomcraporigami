using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace fqn_AbstractFactoryDemo.AccessDal
{
    public class OleHelper
    {
        /// <summary>
        /// 定义连接字符串
        /// </summary>
        private static readonly string serverPath = ConfigurationManager.ConnectionStrings["serverPath"].ConnectionString;

        /// <summary>
        /// 数据库查表操作的方法
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable ExecuteTable(string sql, CommandType cmdType, params OleDbParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(serverPath))
            {
                using (OleDbDataAdapter sda = new OleDbDataAdapter(sql, conn))
                {
                    if (paras != null)
                    {
                        sda.SelectCommand.Parameters.AddRange(paras);
                    }
                    sda.SelectCommand.CommandType = cmdType;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
}