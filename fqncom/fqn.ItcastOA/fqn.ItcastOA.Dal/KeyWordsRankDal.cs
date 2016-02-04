using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.IDal;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.Dal
{
   public partial class KeyWordsRankDal:BaseSqlServerDal<KeyWordsRank>,IKeyWordsRankDal
   {
       public IEnumerable<KeyWordsRank> SelectHotEntities(string keyWord)
       {
           string sql = "select top 10 * from KeyWordsRank where KeyWords like @KeyWords order by SearchCount desc";
           return this.ExecuteQuerySql<KeyWordsRank>(sql, new SqlParameter("@KeyWords", keyWord + "%"));
       }


       public int ClearAllKeyWordsRank()
       {
           string sql = "truncate table KeyWordsRank";
           return this.ExecuteNonQuerySql(sql);
       }


       public int AddDetailCountToRankTable()
       {
           string sql = "insert into KeyWordsRank select NEWID(),KeyWords,COUNT(*) from SearchDetails where DATEDIFF(DAY,SearchDateTime,GETDATE())<=7 group by KeyWords";
           return this.ExecuteNonQuerySql(sql);
       }
   }
}
