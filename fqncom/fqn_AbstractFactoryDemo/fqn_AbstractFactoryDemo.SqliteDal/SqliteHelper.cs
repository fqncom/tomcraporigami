using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
namespace fqn_AbstractFactoryDemo.SqliteHelper
{
    public class SqliteHelper
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
        public static DataTable ExecuteTable(string sql, params SQLiteParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(serverPath))
            {
                using (SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn))
                {
                    if (paras != null)
                    {
                        sda.SelectCommand.Parameters.AddRange(paras);
                    }
                    sda.Fill(dt);
                }
            }
            return dt;
        }

    }
}
