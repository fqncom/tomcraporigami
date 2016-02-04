using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBookShop
{
    public partial class Index : System.Web.UI.Page
    {
        protected int CurrentPageIndex { get; set; }
        protected int CurrentPageCount { get; set; }
        protected string PageBarString { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int pageIndex = 1;
            int pageSize = 5;
            int categoryId = 1;
            
            //if (string.IsNullOrEmpty(Request["hidePageIndex"]))//第一次请求进来，加载默认首页
            //{
            //    pageIndex = 1;
            //    LoadAllBooksInfo(pageIndex, pageSize, categoryId);
            //    return;
            //}
            ////简单上下页切换
            //if (!string.IsNullOrEmpty(Request["PrePage"]))
            //{
            //    pageIndex = Convert.ToInt32(Request["hidePageIndex"] ?? "-1");
            //    LoadAllBooksInfo(--pageIndex, pageSize, categoryId);
            //}
            //if (!string.IsNullOrEmpty(Request["NextPage"]))
            //{
            //    pageIndex = Convert.ToInt32(Request["hidePageIndex"] ?? "-1");
            //    LoadAllBooksInfo(++pageIndex, pageSize, categoryId);
            //}
            pageIndex = Convert.ToInt32(Request["pageIndex"] ?? "1");
            LoadAllBooksInfo(pageIndex, pageSize, categoryId);
        }

        /// <summary>
        /// 加载指定类型的书本信息
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页面显示条数</param>
        /// <param name="categoryId">指定类型</param>
        private void LoadAllBooksInfo(int pageIndex, int pageSize, int categoryId)
        {
            BLL.BooksBll bll = new BLL.BooksBll();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;

            int pageCount = bll.GetPageCountByCategoryId(categoryId, pageSize);
            this.CurrentPageCount = pageCount;

            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            this.CurrentPageIndex = pageIndex;

            List<Model.Books> list = bll.GetModelListByPage(pageIndex, pageSize, categoryId);
            Repeater1.DataSource = list;
            
            Repeater1.DataBind();

            //加载页面跳转按钮
            this.PageBarString = Common.CommonTools.GetPageBarString(pageIndex, pageCount);
        }

        /// <summary>
        /// 切割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        public string CutString(string str, int len)
        {
            if (str.Length < len)
            {
                len = str.Length;
            }
            return str = str.Substring(0, len);
        }
    }
}