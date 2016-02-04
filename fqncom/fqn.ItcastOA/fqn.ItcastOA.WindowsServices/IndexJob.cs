using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace fqn.ItcastOA.WindowsServices
{
    public class IndexJob : IJob
    {

        IBll.IKeyWordsRankBll bll = new Bll.KeyWordsRankBll();
        public void Execute(JobExecutionContext context)
        {
            bll.ClearAllKeyWordsRank();

            bll.AddDetailCountToRankTable();
        }
    }
}
