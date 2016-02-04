using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBookShop
{
    public partial class BookShowHtmlGenerate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string transCode = Request["TransCode"] ?? "";
            if (transCode == "")
            {
                return;
            }
            //string bookId = Request["BookId"] ?? "-1";
            if (transCode == "GenerateHtml")
            {
                GenerateHtml();
            }
            else if (transCode == "AddSensitiveCode")
            {
                string sensitiveCode = Request["SensitiveCode"] ?? "";
                AddSensitiveCode(sensitiveCode);
            }
        }

        //添加敏感词
        private void AddSensitiveCode(string sensitiveCode)
        {
            string[] lines = sensitiveCode.Trim().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] words = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                Model.Articel_Words aw = new Model.Articel_Words();
                aw.WordPattern = words[0];
                if (words[1] == "{BANNED}")
                {
                    aw.IsForbid = true;
                }
                if (words[1] == "{MOD}")
                {
                    aw.IsMod = true;
                }
                if (words[1] != "{BANNED}" && words[1] != "{MOD}")
                {
                    aw.ReplaceWord = words[1];
                }
                BLL.Articel_WordsBll bll = new BLL.Articel_WordsBll();
                bll.Add(aw);
            }
            //清除缓存
            Common.CacheHelper.DeleteCache("ModWord");
            Common.CacheHelper.DeleteCache("ForbidWord");
            Common.CacheHelper.DeleteCache("ReplaceWord");
            Response.Write("success");
            Response.End();
        }

        //生成商品html静态页
        public void GenerateHtml()
        {
            List<Model.Books> list = new BLL.BooksBll().GetModelList("");
            string textHtml = Common.CommonTools.ReadFileGetAllText("/Master/BookShowTemplate.html");

            foreach (Model.Books book in list)
            {
                string bookHtml = textHtml.Replace("$title", book.Title)
                     .Replace("$author", book.Author)
                     .Replace("$descriptionAuthor", book.AurhorDescription)
                     .Replace("$ISBN", book.ISBN)
                     .Replace("$unitPrice", book.UnitPrice.ToString())
                     .Replace("$content", book.ContentDescription)
                     .Replace("$BookId", book.Id.ToString());
                string dir = "/" + book.PublishDate.Year + "/" + book.PublishDate.Month + "/";
                Directory.CreateDirectory(Request.MapPath(dir));
                File.WriteAllText(Request.MapPath(dir + book.ISBN + ".html"), bookHtml, Encoding.UTF8);
            }
            Response.Write("success");
            Response.End();
        }
    }
}