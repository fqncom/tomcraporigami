using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class HtmlProperty
    {
        public static MvcHtmlString GetSpanString(this HtmlHelper txt, string str)
        {
            return new MvcHtmlString("<span style='color:red' font-size:'30px'>" + str + "</span>");
        }

    }
}