using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.WebApp.Models;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            #region 字典队列测试数据
            Model.Books model = new Model.Books();
            model.AurhorDescription = "sdhflkasjflaksdjf";
            model.Author = "老赵";
            model.CategoryId = 1;
            model.Clicks = 1;
            model.ContentDescription = "老赵的私人生活是......???";
            model.EditorComment = "adfsadfsadf";
            model.ISBN = "39u582349";
            model.PublishDate = DateTime.Now;
            model.PublisherId = 72;
            model.Title = "私生活";
            model.TOC = "aaaaaaaaaaaaaaaa";
            model.UnitPrice = 22.3m;
            model.WordsCount = 1234;

            SearchQueueManager.GetInstance().AddOrUpdateIntoQueue(model.ISBN, model.Title, model.ContentDescription);
            #endregion


            return Content("ok");
        }

    }
}
