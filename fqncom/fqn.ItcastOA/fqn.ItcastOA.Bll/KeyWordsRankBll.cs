using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.IBll;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.Bll
{
    public partial class KeyWordsRankBll : BaseBll<KeyWordsRank>, IKeyWordsRankBll
    {

        public List<KeyWordsRank> SelectHotEntities(string keyWord)
        {
            return this.GetDbSession.KeyWordsRankDal.SelectHotEntities(keyWord).ToList();
        }

        public bool ClearAllKeyWordsRank()
        {
            return this.GetDbSession.KeyWordsRankDal.ClearAllKeyWordsRank() > 0;
        }

        public bool AddDetailCountToRankTable()
        {
            return this.GetDbSession.KeyWordsRankDal.AddDetailCountToRankTable() > 0;
        }
    }
}
