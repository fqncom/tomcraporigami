using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Common;

namespace Agape.Manage.Core.Cache
{
    public class BatchCache
    {
        #region 静态部分
        private static BatchCache m_Instance;

        static BatchCache()
        {
            m_Instance = null;
        }

        public static BatchCache Current
        {
            get { return GetInstance(); }
        }

        public static BatchCache GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new BatchCache();
            }
            return m_Instance;
        }
        #endregion

        private SLS_Batch m_Batch;
        private string m_BatchSeparateHour;
        private ArrayList m_BatchHours;
        private object m_Lock;

        public BatchCache()
        {
            m_Batch = new SLS_Batch();
            m_BatchHours = new ArrayList();
            m_Lock = new object();
        }

        /// <summary>
        /// 载入。
        /// </summary>
        /// <returns></returns>
        public XReturn LoadCache()
        {
            lock (m_Lock)
            {
                XReturn xSubReturn;
                XReturn xReturn = new XReturn();

                m_BatchSeparateHour = LeopardConfigs.ReadLeopardIniValue("Batch", "SeparateHour");
                if (m_BatchSeparateHour == String.Empty) m_BatchSeparateHour = "6,14";
                LeopardLog.Info("SeparateHour=" + m_BatchSeparateHour);

                m_BatchHours.Clear();
                string[] strTempArray = m_BatchSeparateHour.Split(',');
                foreach (string strTemp in strTempArray)
                {
                    m_BatchHours.Add(Int32.Parse(strTemp));
                }

                xSubReturn = CheckBatch();
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError("检查批次失败");
                }

                return xReturn.ReturnSuccess();
            }
        }

        /// <summary>
        /// 获取当前批次ID。
        /// </summary>
        /// <returns></returns>
        public int GetCurrentBatchID()
        {
            lock (m_Lock)
            {
                XReturn xSubReturn;

                xSubReturn = CheckBatch();
                if (xSubReturn.IsUnSuccess())
                {
                    LeopardLog.Error("检查批次失败");
                    return 0;
                }

                return m_Batch.BatchID;
            }
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <returns></returns>
        public XReturn ConfirmOrder(int OrderID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            
            // 查询订单记录
            SLS_Order Order = new SLS_Order();
            Order.OrderID = OrderID;
            xSubReturn = Order.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, string.Format("订单记录[{0:D}]不存在", OrderID));
            }

            m_Batch.ConfirmOrderCount++;
            m_Batch.ConfirmTotalQuantity += Order.TotalQuantity;
            m_Batch.ConfirmTotalAmount += Order.TotalAmount;
            xSubReturn = m_Batch.x.UpdateByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "更新批次确认信息失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 检查批次
        /// </summary>
        /// <returns></returns>
        private XReturn CheckBatch()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            if (m_Batch != null && m_Batch.BatchNo != String.Empty)
            {
                DateTime Now = DateTime.Now;
                if (Now.CompareTo(m_Batch.ToTime) < 0)
                {
                    return xReturn.ReturnSuccess();
                }
                else
                {
                    xSubReturn = ConfirmCurrentBatch();
                    if (xSubReturn.IsUnSuccess())
                    {
                        return xReturn.ReturnError(xSubReturn, "确认当前批次失败");
                    }
                }
            }

            xSubReturn = LoadCurrentBatch();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "创建新批次失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 载入当前批次。
        /// </summary>
        /// <returns></returns>
        private XReturn LoadCurrentBatch()
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            DateTime Now = DateTime.Now;
            m_Batch.x.ResetEntity();

            if (m_BatchHours.Count == 0)
            {
                return xReturn.ReturnError("没有设置批次时段");
            }

            if (Now.Hour < (int)m_BatchHours[0])
            {
                m_Batch.BatchOrder = m_BatchHours.Count;
                m_Batch.BatchDate = DateTimeUtil.GetShortDateString(Now.AddDays(-1));
            }
            else
            {
                for (int i = m_BatchHours.Count - 1; i >= 0; i--)
                {
                    if (Now.Hour >= (int)m_BatchHours[i])
                    {
                        m_Batch.BatchOrder = i + 1;
                        break;
                    }
                }
                m_Batch.BatchDate = DateTimeUtil.GetShortDateString(Now);
            }

            m_Batch.BatchNo = string.Format("{0:S}-{1:D}", m_Batch.BatchDate, m_Batch.BatchOrder);

            strSql = string.Format("select isnull(BatchID,0) from SLS_Batch where BatchNo='{0:S}'", m_Batch.BatchNo);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询已经存在批次失败");
            }
            int BatchID = xSubReturn.GetIntValue();

            if (BatchID > 0)
            {
                m_Batch.BatchID = BatchID;
                xSubReturn = m_Batch.x.SelectByIdentity();
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "查询已经存在的批次失败");
                }
            }
            else
            {
                m_Batch.FromTime = new DateTime(Now.Year, Now.Month, Now.Day, (int)m_BatchHours[m_Batch.BatchOrder - 1], 0, 0);
                if (m_Batch.BatchOrder == m_BatchHours.Count)
                {
                    m_Batch.ToTime = new DateTime(Now.Year, Now.Month, Now.Day, (int)m_BatchHours[0], 0, 0).AddDays(1);
                }
                else
                {
                    m_Batch.ToTime = new DateTime(Now.Year, Now.Month, Now.Day, (int)m_BatchHours[m_Batch.BatchOrder], 0, 0);
                }

                m_Batch.Status = (int)EBatchStatus.BatchActive;
                xSubReturn = m_Batch.x.Insert();
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "插入批次失败");
                }
            }

            strSql = string.Format("update SLS_Batch set Status={2:D} where BatchID!={0:D} and Status={1:D}", m_Batch.BatchID, (int)EBatchStatus.BatchActive, (int)EBatchStatus.OrderConfirm);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "更新状态异常的批量状态失败");
            }

            LeopardLog.Info(string.Format("创建当前批次[{0:S}]完成", m_Batch.BatchNo));

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 确认当前批次
        /// </summary>
        /// <returns></returns>
        private XReturn ConfirmCurrentBatch()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            if (m_Batch.BatchID > 0 && m_Batch.Status == (int)EBatchStatus.BatchActive)
            {
                m_Batch.Status = (int)EBatchStatus.OrderConfirm;
                xSubReturn = m_Batch.x.UpdateByIdentity();
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "更新当前批次状态失败");
                }

                LeopardLog.Info(string.Format("确认当前批次[{0:S}]完成", m_Batch.BatchNo));
            }
            
            return xReturn.ReturnSuccess();
        }
    }
}
