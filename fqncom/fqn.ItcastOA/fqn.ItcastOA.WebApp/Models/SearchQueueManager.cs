using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace fqn.ItcastOA.WebApp.Models
{
    public class SearchQueueManager
    {


        #region 同样是单例模式，只是用到了锁来实现

        /// <summary>
        /// 同样是单例模式，只是用到了锁来实现
        /// </summary>
        //public Queue<SearchResultModel> SearchResultModel { get; set; }

        //public void GetInstance()
        //{
        //    if (this.SearchResultModel == null)
        //    {
        //        lock (this)
        //        {
        //            if (this.SearchResultModel == null)
        //            {
        //                this.SearchResultModel = new Queue<SearchResultModel>();
        //            }
        //        }
        //    }
        //}

        #endregion


        /// <summary>
        /// 单例模式，由CLR进行
        /// </summary>
        private static readonly SearchQueueManager searchQueueManager = new SearchQueueManager();

        private SearchQueueManager()
        {

        }
        private Queue<SearchResultModel> SearchResultModel = new Queue<SearchResultModel>();
        public static SearchQueueManager GetInstance()
        {
            return searchQueueManager;
        }

        #region 向队列中添加要更改或则新增的数据

        public void AddOrUpdateIntoQueue(string id, string title, string content)
        {
            this.SearchResultModel.Enqueue(new SearchResultModel()
            {
                Id = id,
                Title = title,
                Content = content,
                QueueStateEnum = QueueStateEnum.Add
            });
        }

        #endregion

        #region 向队列中添加要删除的数据

        public void DeleteFromQueue(string id)
        {
            this.SearchResultModel.Enqueue(new SearchResultModel()
            {
                Id = id,
                QueueStateEnum = QueueStateEnum.Delete
            });
        }

        #endregion

        #region 创建一个线程

        public void StartThread()
        {

            Thread thread =new Thread(CreateThread);
            thread.IsBackground = true;
            thread.Start();
        }


        public void CreateThread()
        {
            while (true)
            {
                if (SearchResultModel.Count > 0)
                {
                    CreateSearchLibrary();
                }
                else
                {
                    Thread.Sleep(5000);
                }
            }
        }

        #endregion


        #region 将队列中的数据保存到本地文件中去
        private void CreateSearchLibrary()
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

            while (SearchResultModel.Count > 0)
            {
                //取一条数据
                Models.SearchResultModel searchResultModel = SearchResultModel.Dequeue();
                //将原有的数据删除
                writer.DeleteDocuments(new Term("id", searchResultModel.Id));
                //判断是否进行的是删除操作
                if (searchResultModel.QueueStateEnum == QueueStateEnum.Delete)
                {
                    continue;//如果删除则继续循环，因为上两行代码已经删除了对应得信息，就不用再次删除了
                }
                //如果不是删除则进行添加
                //string txt = File.ReadAllText(@"D:\传智讲课\1107班\Asp.Net MVC\第十一天\资料\测试文件\" + i + ".txt", System.Text.Encoding.Default);//注意这个地方的编码
                Document document = new Document();//表示一篇文档。
                //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("id", searchResultModel.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("title", searchResultModel.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("content", searchResultModel.Content, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                writer.AddDocument(document);
            }

            writer.Close();//会自动解锁。
            directory.Close();//不要忘了Close，否则索引结果搜不到
        }
        #endregion
    }
}