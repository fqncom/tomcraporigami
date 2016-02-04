using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.IDal
{
    public partial interface IKeyWordsRankDal : IBaseDal<KeyWordsRank>
    {
        IEnumerable<KeyWordsRank> SelectHotEntities(string keyWord);

        int ClearAllKeyWordsRank();

        int AddDetailCountToRankTable();
    }
}
