using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace 数据库连接
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 数据库连接练习：windows 身份验证模式登入
            //string serverPath = "Date Source = fqn-pc;Initial Catalog = nononodeleteimportant;Integrated Security = true;";
            //string serverPath = "Data Source = fqn-pc;Initial Catalog = nononodeleteimportant;Integrated Security = true;";
            //string serverPath = "Data Source = fqn-pc;Initial Catalog = nononodeleteimportant;Integrated Security = true;";

            //int count = -1;
            //using (SqlConnection conn = new SqlConnection(serverPath))
            //{
            //    string sql = "update tblscore set tsid = 10 where tsid = 20";
            //    using (SqlCommand cmd = new SqlCommand(sql,conn))
            //    {
            //        conn.Open();
            //        count = cmd.ExecuteNonQuery();
            //    }
            //}
            //Console.WriteLine(count>0?"成功":"失败");

            

            #endregion

            #region 数据库连接练习，使用SQL Server身份验证登入

            //string serverPath = "Data Source = fqn-pc;Initial Catalog = nononodeleteimportant;User ID = sa; Password = 199210622;";
            //int count = -1;
            //using (SqlConnection conn = new SqlConnection(serverPath))
            //{
            //    string sql = "update tblscore set tsid = 20 where tsid = 10";
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        conn.Open();
            //        count = cmd.ExecuteNonQuery();
            //    }
            //}
            //Console.WriteLine(count > 0 ? "成功了" : "失败了");

            #endregion


            Console.ReadKey();

        }
    }
}
