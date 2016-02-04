using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.WebApp.Models;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Search.Function;
using Lucene.Net.Store;

namespace fqn.ItcastOA.WebApp.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        private IBll.IBooksBll BooksBll { get; set; }
        private IBll.ISearchDetailsBll SearchDetailsBll { get; set; }

        private IBll.IKeyWordsRankBll KeyWordsRankBll { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 搜索内容
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchContent()
        {
            if (Request["btnCreate"] != null)
            {
                return Redirect("/Search/GenrateSearchLibrary");
            }

            string indexPath = ConfigurationManager.AppSettings["LuceneNetPath"];
            string searchStr = Request["txtSearchContent"] ?? "";

            if (searchStr == "")
            {
                return View("Index");
            }
            //将用户搜索的词加入热词库
            SearchDetailsBll.AddEntity(new SearchDetails()
            {
                Id = Guid.NewGuid(),
                KeyWords = searchStr,
                SearchDateTime = DateTime.Now
            });

            List<string> stringList = Common.SearchHelper.ChangeStringToSegment(searchStr);
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件
            PhraseQuery query = new PhraseQuery();
            foreach (string word in stringList)//先用空格，让用户去分词，空格分隔的就是词“计算机   专业”
            {
                query.Add(new Term("content", word));
            }
            //query.Add(new Term("body","语言"));--可以添加查询条件，两者是add关系.顺序没有关系.
            // query.Add(new Term("body", "大学生"));
            //query.Add(new Term("body", searchStr));//body中含有kw的文章
            query.SetSlop(100);//多个查询条件的词之间的最大距离.在文章中相隔太远 也就无意义.（例如 “大学生”这个查询条件和"简历"这个查询条件之间如果间隔的词太多也就没有意义了。）
            //TopScoreDocCollector是盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(query, null, collector);//根据query查询条件进行查询，查询结果放入collector容器
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;//得到所有查询结果中的文档的ID,GetTotalHits():表示总条数   TopDocs(300, 20);//表示得到300（从300开始），到320（结束）的文档内容.
            //可以用来实现分页功能
            //this.listBox1.Items.Clear();
            List<SearchResultViewModel> list = new List<SearchResultViewModel>();
            for (int i = 0; i < docs.Length; i++)
            {
                //
                //搜索ScoreDoc[]只能获得文档的id,这样不会把查询结果的Document一次性加载到内存中。降低了内存压力，需要获得文档的详细内容的时候通过searcher.Doc来根据文档id来获得文档的详细内容对象Document.
                int docId = docs[i].doc;//得到查询结果文档的id（Lucene内部分配的id）
                Document doc = searcher.Doc(docId);//找到文档id对应的文档详细信息
                list.Add(new SearchResultViewModel()
                {
                    Id = doc.Get("id"),
                    Title = doc.Get("title"),
                    Content = Common.SearchHelper.ChangeStringToHighLight(searchStr, doc.Get("content"))
                });

            }
            ViewData["list"] = list;
            ViewData["searchContent"] = searchStr;
            return View("Index");
        }

        /// <summary>
        /// 创建索引库
        /// </summary>
        /// <returns></returns>
        public ActionResult GenrateSearchLibrary()
        {
            int rowCount = 0;
            List<Books> list = BooksBll.SelectEntities<Books>(b => true, out rowCount).ToList();

            Common.SearchHelper.CreateSearchLibrary(list);
            return Content("success");
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTheHotWords()
        {
            string keyWord = Request["term"] ?? "";
            if (keyWord == "")
            {
                return View("Index");
            }
            var keyWordsRank = KeyWordsRankBll.SelectHotEntities(keyWord);
            List<string> result = (from k in keyWordsRank
                                   select k.KeyWords).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
