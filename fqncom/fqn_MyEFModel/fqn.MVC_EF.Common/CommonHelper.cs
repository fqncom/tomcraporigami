using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.MVC_EF.Common
{
    public class CommonHelper
    {
        public static string GetPageBar(int pageIndex, int pageCount)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;

            int start = pageIndex - 5;
            start = start < 1 ? 1 : start;
            int end = start + 9;
            end = end > pageCount ? pageCount : end;

            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (pageIndex == i)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href = '?pageIndex={0}'>{0}</a>", i));
                }
            }
            return sb.ToString();
        }

    }
}
