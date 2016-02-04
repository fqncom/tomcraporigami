using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using fqn.ItcastOA.Model;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using PanGu;

namespace fqn.ItcastOA.Common
{
    public class SearchHelper
    {

        /// <summary>
        /// 将字符串经过盘古分词之后返回字符串集合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> ChangeStringToSegment(string str)
        {
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            List<string> list = new List<string>();
            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }
            return list;
        }

        public static string ChangeStringToHighLight(string keyWord, string content)
        {
            //创建HTMLFormatter,参数为高亮单词的前后缀 
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");  //创建 Highlighter ，输入HTMLFormatter 和 盘古分词对象Semgent 
            PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            //设置每个摘要段的字符数 
            highlighter.FragmentSize = 50;
            //获取最匹配的摘要段 
            return highlighter.GetBestFragment(keyWord, content);
        }

        /// <summary>
        /// 创建索引库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void CreateSearchLibrary(List<Books> list)
        {
            string indexPath = WebConfigurationManager.AppSettings["LuceneNetPath"];//@"D:\---vs_projects\lucenedir";//注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());//指定索引文件(打开索引目录) FS指的是就是FileSystem
            bool isUpdate = IndexReader.IndexExists(directory);//IndexReader:对索引进行读取的类。该语句的作用：判断索引库文件夹是否存在以及索引特征文件是否存在。
            if (isUpdate)
            {
                //同时只能有一段代码对索引库进行写操作。当使用IndexWriter打开directory时会自动对索引库文件上锁。
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁（提示一下：如果我现在正在写着已经加锁了，但是还没有写完，这时候又来一个请求，那么不就解锁了吗？这个问题后面会解决）
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。

            foreach (Books item in list)
            {
                //先删除再去添加
                writer.DeleteDocuments(new Term("id", item.Id.ToString()));
                //string txt = File.ReadAllText(@"D:\传智讲课\1107班\Asp.Net MVC\第十一天\资料\测试文件\" + i + ".txt", System.Text.Encoding.Default);//注意这个地方的编码
                Document document = new Document();//表示一篇文档。
                //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("id", item.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("title", item.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("content", item.ContentDescription, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                writer.AddDocument(document);
            }

            writer.Close();//会自动解锁。
            directory.Close();//不要忘了Close，否则索引结果搜不到
        }


    }
}
