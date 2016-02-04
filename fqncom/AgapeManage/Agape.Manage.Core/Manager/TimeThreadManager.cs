using System.Net.Mail;
using System.Web;
using System.Data.SqlClient;
using Leopard.Util;
using Leopard.Data;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.Web.Security;
using System.Timers;
using Leopard.Cache;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Cache;
using Agape.Manage.Core.Impl;

namespace Agape.Manage.Core.Manager
{
    public class TimeThreadManager
    {
        #region 静态部分
        private static TimeThreadManager m_Instance;

        static TimeThreadManager()
        {
            m_Instance = null;
        }

        public static TimeThreadManager Current
        {
            get { return GetInstance(); }
        }

        public static TimeThreadManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new TimeThreadManager();
            }
            return m_Instance;
        }
        #endregion

        protected Timer m_BatchTimer;
        protected Timer m_StatTimer;

        public TimeThreadManager()
        {
        }

        /// <summary>
        /// 启动管理器
        /// </summary>
        public void StartManager()
        {
            // 启动批次定时器
            m_BatchTimer = new Timer();
            m_BatchTimer.Interval = 1000 * 60;
            m_BatchTimer.Enabled = true;
            m_BatchTimer.Elapsed += new ElapsedEventHandler(BatchTimeEvent);

            // 启动统计定时器
            m_StatTimer = new Timer();
            m_StatTimer.Interval = 1000 * 60 * 60;
            m_StatTimer.Enabled = true;
            m_StatTimer.Elapsed += new ElapsedEventHandler(StatTimeEvent);

            LeopardLog.Info("启动时间任务管理器成功");
        }

        /// <summary>
        /// 结束管理器
        /// </summary>
        public void EndManager()
        {
            m_BatchTimer.Enabled = false;
            m_BatchTimer.Close();

            m_StatTimer.Enabled = false;
            m_StatTimer.Close();

            LeopardLog.Info("结束时间任务管理器成功");
        }

        private static void BatchTimeEvent(object source, ElapsedEventArgs e)
        {
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;

            int BatchID = BatchCache.Current.GetCurrentBatchID();
            if (BatchID == 0)
            {
                LeopardLog.Error("获取当前批次ID失败");
            }
        }

        private static void StatTimeEvent(object source, ElapsedEventArgs e)
        {
            XReturn xSubReturn;
            int nHour = e.SignalTime.Hour;
            int nMinute = e.SignalTime.Minute;
            int nSecond = e.SignalTime.Second;
            string SummaryDate = DateTimeUtil.GetShortDateString();

            if (nHour == 0)
            {
                LeopardLog.Info("提交统计商品每日汇总任务");
                xSubReturn = StatImpl.SubmitStatProductDaySummary(SummaryDate);
                if (xSubReturn.IsUnSuccess())
                {

                }
                LeopardLog.Info("完成统计商品每日汇总任务");
            }
        }
    }
}
