using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace fqn_AbstractFactoryDemo.SqlHelper
{
    public class SqlHelper
    {
        /// <summary>
        /// 定义连接字符串
        /// </summary>
        private static readonly string serverPath = ConfigurationManager.ConnectionStrings["serverPath"].ConnectionString;
        /// <summary>
        /// 增删改操作的方法
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, CommandType cmdType, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(serverPath))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    cmd.CommandType = cmdType;
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        conn.Dispose();
                        throw ex;
                    }
                }
            }
        }
        /// <summary>
        /// 重载增删改操作的方法，默认sql语句是text类型
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] paras)
        {
            return ExecuteNonQuery(sql, CommandType.Text, paras);
        }
        /// <summary>
        /// 查询操作方法
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回执行结果的首行首列</returns>
        public static object ExecuteScalar(string sql, CommandType cmdType, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(serverPath))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    cmd.CommandType = cmdType;
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        conn.Dispose();
                        throw ex;
                    }
                }
            }
        }
        /// <summary>
        /// 重载查询操作的方法，默认sql语句是text类型
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回执行结果的首行首列</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] paras)
        {
            return ExecuteScalar(sql, CommandType.Text, paras);
        }
        /// <summary>
        /// 数据库读取操作的方法
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType, params SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(serverPath);
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                cmd.CommandType = cmdType;
                try
                {
                    conn.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 重载数据库读取操作的方法，默认sql语句是text类型
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] paras)
        {
            return ExecuteReader(sql, CommandType.Text, paras);
        }
        /// <summary>
        /// 数据库查表操作的方法
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable ExecuteTable(string sql, CommandType cmdType, params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(serverPath))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
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
        /// <summary>
        /// 重载数据库查表操作的方法，默认sql语句是text类型
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable ExecuteTable(string sql, params SqlParameter[] paras)
        {
            return ExecuteTable(sql, CommandType.Text, paras);
        }
        /// <summary>
        /// 数据库查表操作的方法
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回DataSet对象</returns>
        public static DataSet ExecuteDataSet(string sql, CommandType cmdType, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(serverPath))
            {
                using (SqlCommand cmd = new SqlCommand(sql,conn))
                {
                    if (paras!=null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    cmd.CommandType = cmdType;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        try
                        {
                            conn.Open();
                            sda.Fill(ds);
                            return ds;
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            conn.Dispose();
                            throw ex;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 数据库查表操作的方法，默认sql语句是text类型
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">传入的参数</param>
        /// <returns>返回DataSet对象</returns>
        public static DataSet ExecuteDataSet(string sql, params SqlParameter[] paras)
        {
            return ExecuteDataSet(sql, CommandType.Text, paras);
        }

    }
}
