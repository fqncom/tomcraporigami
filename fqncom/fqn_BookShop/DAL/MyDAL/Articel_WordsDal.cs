using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.DBUtility;

namespace MyBookShop.DAL
{
    public partial class Articel_WordsDal
    {
        /// <summary>
        /// 拿到所有的禁止词
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllForbidList()
        {
            List<string> list = new List<string>();
            string sql = "select WordPattern from Articel_Words where IsForbid = 1";
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(sql))
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add( sdr.GetString(0));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 拿到所有的审查词
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllModList()
        {
            List<string> list = new List<string>();
            string sql = "select WordPattern from Articel_Words where IsMod = 1";
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(sql))
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr.GetString(0));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 拿到所有的替换词
        /// </summary>
        /// <returns></returns>
        public List<Model.Articel_Words> GetAllReplaceWordList()
        {
            List<Model.Articel_Words> list = new List<Model.Articel_Words>();
            string sql = "select WordPattern,ReplaceWord from Articel_Words where IsMod = 0 and IsForbid = 0";
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(sql))
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new Model.Articel_Words()
                        {
                            ReplaceWord = sdr.GetString(1),
                            WordPattern = sdr.GetString(0)
                        });
                    }
                }
            }
            return list;
        }

    }
}
