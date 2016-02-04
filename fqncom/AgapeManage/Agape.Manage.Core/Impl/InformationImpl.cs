using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Cache;

namespace Agape.Manage.Core.Impl
{
    public class InformationImpl
    {
        #region 文章接口
        /// <summary>
        /// 查询文章数量。
        /// </summary>
        /// <param name="ArticleCategoryID">文章类型ID</param>
        /// <param name="IssueOperatorID">发布者ID</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static XReturn QueryArticleCount(int ArticleCategoryID, int IssueOperatorID, string FromDate, string ToDate, int Status)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ArticleCategoryID != 0) strWhere += string.Format(" and a.ArticleCategoryID={0:D}", ArticleCategoryID);
            if (IssueOperatorID != 0) strWhere += string.Format(" and a.IssueOperatorID={0:D}", IssueOperatorID);
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select count(*) from INF_Article a where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询文章列表。
        /// </summary>
        /// <param name="ArticleCategoryID">文章类型ID</param>
        /// <param name="IssueOperatorID">发布者ID</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns></returns>
        public static XReturn QueryArticleList(int ArticleCategoryID, int IssueOperatorID, string FromDate, string ToDate, int Status, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ArticleCategoryID != 0) strWhere += string.Format(" and a.ArticleCategoryID={0:D}", ArticleCategoryID);
            if (IssueOperatorID != 0) strWhere += string.Format(" and a.IssueOperatorID={0:D}", IssueOperatorID);
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select a.*,b.ArticleCategoryName,c.OperatorName as IssueOperatorName from INF_Article a" +
                    " left join INF_ArticleCategory b on b.ArticleCategoryID=a.ArticleCategoryID" +
                    " left join LPD_Operator c on c.OperatorID=a.IssueOperatorID" +
                    " where {0:S} order by a.IssueDateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }

        /// <summary>
        /// 查询文章。
        /// </summary>
        /// <param name="ArticleID">文章ID</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns></returns>
        public static XReturn QueryArticle(int ArticleID, out DataTable ReturnTable)
        {
            string strSql;

            strSql = "select a.*,b.ArticleCategoryName,c.OperatorName as IssueOperatorName from INF_Article a" +
                    " left join INF_ArticleCategory b on b.ArticleCategoryID=a.ArticleCategoryID" +
                    " left join LPD_Operator c on c.OperatorID=a.IssueOperatorID" +
                    " where ArticleID={0:D}";
            strSql = string.Format(strSql, ArticleID);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 保存文章。
        /// </summary>
        /// <param name="Article">文章对象</param>
        /// <returns></returns>
        public static XReturn SaveArticle(INF_Article Article)
        {
            if (Article.ArticleID == 0)
            {
                return Article.x.Insert();
            }
            else
            {
                return Article.x.UpdateByIdentity();
            }
        }

        /// <summary>
        /// 删除文章。
        /// </summary>
        /// <param name="ArticleID">文章ID</param>
        /// <returns></returns>
        public static XReturn DeleteArticle(int ArticleID)
        {
            INF_Article Article = new INF_Article();
            Article.ArticleID = ArticleID;
            return Article.x.DeleteByIdentity();
        }

        /// <summary>
        /// 查询文章类型列表。
        /// </summary>
        /// <param name="ArticleCategoryName">文章类型名称</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryArticleCategoryList(string ArticleCategoryName, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ArticleCategoryName != String.Empty) strWhere += string.Format(" and ArticleCategoryName like '%{0:S}%'", ArticleCategoryName);

            strSql = "select * from INF_ArticleCategory where {0:S} order by ArticleCategoryName desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 查询排行榜文章列表。
        /// </summary>
        /// <param name="ArticleCategoryID">文章类型ID</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns></returns>
        public static XReturn QueryArticleProductRankingList(int ArticleCategoryID, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ArticleCategoryID != 0) strWhere += string.Format(" and a.ArticleCategoryID={0:D}", ArticleCategoryID);

            strSql = "select top 10 a.*,b.ArticleCategoryName,c.OperatorName as IssueOperatorName from INF_Article a" +
                    " left join INF_ArticleCategory b on b.ArticleCategoryID=a.ArticleCategoryID" +
                    " left join LPD_Operator c on c.OperatorID=a.IssueOperatorID" +
                    " where {0:S} order by a.IssueDateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }
        #endregion
    }
}
