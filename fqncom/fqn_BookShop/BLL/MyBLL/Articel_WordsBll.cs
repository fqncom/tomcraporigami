using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBookShop.BLL
{
    public partial class Articel_WordsBll
    {
        /// <summary>
        /// 拿到所有的禁止词
        /// </summary>
        /// <returns></returns>
        public bool CheckIsForbidWord(string msg)
        {
            object cacheObj = Common.CacheHelper.GetCache("ForbidWord");
            List<string> list;
            if (cacheObj != null)
            {
                list = cacheObj as List<string>;
                Common.CacheHelper.SetCache("ForbidWord", list);
            }
            else
            {
                list = dal.GetAllForbidList();
            }
            string regex = string.Join("|", list.ToArray());
            //MatchCollection matches = Regex.Matches(msg, regex);//数据库中存在一条数据带*号。所以会匹配到=="动 * 态* 网"
            return Regex.IsMatch(msg, regex);
        }

        /// <summary>
        /// 拿到所有的禁止词
        /// </summary>
        /// <returns></returns>
        public bool CheckIsModWord(string msg)
        {
            object cacheObj = Common.CacheHelper.GetCache("ModWord");
            List<string> list;
            if (cacheObj != null)
            {
                list = cacheObj as List<string>;
                Common.CacheHelper.SetCache("ModWord", list);
            }
            else
            {
                list = dal.GetAllModList();
            }

            string regex = string.Join("|", list.ToArray());
            regex = regex.Replace(@"\", @"\\").Replace("{2}", ".{0,2}");
            return Regex.IsMatch(msg, regex);
        }

        /// <summary>
        /// 拿到所有的替换词
        /// </summary>
        /// <returns></returns>
        public string CheckIsReplaceWord(string msg)
        {
            object cacheOnj = Common.CacheHelper.GetCache("ReplaceWord");
            List<Model.Articel_Words> list;
            if (cacheOnj != null)
            {
                list = cacheOnj as List<Model.Articel_Words>;
            }
            else
            {
                list = dal.GetAllReplaceWordList();
                Common.CacheHelper.SetCache("ReplaceWord", list);
            }

            //第一种方案
            //Model.Articel_Words articelWords = list.Find(aw => Regex.IsMatch(msg, aw.WordPattern + "+"));
            //return articelWords == null ? msg : msg.Replace(articelWords.WordPattern, articelWords.ReplaceWord);
            //第二种方案
            foreach (Model.Articel_Words articelWords in list)
            {
                msg = msg.Replace(articelWords.WordPattern, articelWords.ReplaceWord);
            }
            return msg;
            //第三种方案Dictionary
            //第四种方案javascriptSerializer + 匿名类型

        }
    }
}
