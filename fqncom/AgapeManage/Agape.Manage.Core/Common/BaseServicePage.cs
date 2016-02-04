using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Reflection;
using Leopard.Util;
using Leopard.Data;
using Agape.Manage.Core.Session;

namespace Agape.Manage.Core.Common
{
    public class BaseServicePage : System.Web.UI.Page
    {
        protected bool m_DefaultResponse;
        protected string m_XmlContent;
        protected string m_ResponseTemplete;
        protected string m_ResponseTemplete2;
        protected string m_TransCode;
        protected string m_SubTransCode;
        protected Encoding m_ClientEncoding;

        protected XReturn m_Return;
        protected MemoryStream m_MemoryStream;
        protected XmlTextWriter m_XmlTextWriter;

        public Encoding ClientEncoding
        {
            get { return m_ClientEncoding; }
        }


        public void Page_PreLoad(object sender, EventArgs e)
        {
            m_TransCode = Request["TransCode"];
            m_SubTransCode = Request["SubTransCode"];
        }

        public void Page_LoadComplete(object sender, EventArgs e)
        {
            // 初始化类变量
            m_Return = new XReturn();
            m_MemoryStream = new MemoryStream();
            m_XmlContent = String.Empty;
            m_ClientEncoding = Encoding.GetEncoding(Response.Charset);

            // 创建XML写入流
            m_XmlTextWriter = new XmlTextWriter(m_MemoryStream, ClientEncoding);
            m_XmlTextWriter.WriteStartDocument();
            m_XmlTextWriter.WriteStartElement("Response");
            m_XmlTextWriter.WriteStartElement("ReturnContent");

            // 调用交易码对应的方法
            if (!String.IsNullOrEmpty(m_TransCode))
            {
                try
                {
                    Type type = this.GetType();
                    MethodInfo mi = type.GetMethod(m_TransCode);
                    m_Return = (XReturn)mi.Invoke(this, null);
                }
                catch (Exception ex)
                {
                    LeopardLog.Error(ex.Message);
                    m_Return.SetError("调用方法[" + m_TransCode + "]失败");
                }
            }
            else
            {
                m_Return.SetError("没有定义交易码");
            }

            m_XmlTextWriter.WriteEndElement();

            // 写入返回信息
            m_XmlTextWriter.WriteStartElement("ReturnInfo");
            m_XmlTextWriter.WriteElementString("ReturnCode", m_Return.ReturnCode);
            m_XmlTextWriter.WriteElementString("ReturnMessage", m_Return.ReturnMessage);

            object objValue;
            string strValue;
            foreach (string strName in m_Return.Values.Keys)
            {
                objValue = m_Return.GetValue(strName);
                strValue = objValue == null ? String.Empty : objValue.ToString();

                m_XmlTextWriter.WriteElementString(strName, strValue);
            }

            m_XmlTextWriter.WriteEndElement();
            m_XmlTextWriter.WriteEndElement();
            m_XmlTextWriter.WriteEndDocument();
            m_XmlTextWriter.Flush();
            m_XmlContent = ClientEncoding.GetString(m_MemoryStream.GetBuffer(), 0, (int)m_MemoryStream.Length);
            m_XmlTextWriter.Close();

            Response.ContentType = "text/xml";
            Response.Clear();
            Response.Write(m_XmlContent);
            Response.End();
        }

        protected bool BasePage_LoadBegin()
        {
            m_XmlContent = String.Empty;
            m_ClientEncoding = Encoding.GetEncoding(Response.Charset);
            m_ResponseTemplete = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Response><ReturnCode>{0:S}</ReturnCode><ReturnMessage>{1:S}</ReturnMessage></Response>";
            m_ResponseTemplete2 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Response><ReturnCode>{0:S}</ReturnCode><ReturnMessage>{1:S}</ReturnMessage><ReturnValue>{2:S}</ReturnValue></Response>";

            m_TransCode = Request["TransCode"];
            m_SubTransCode = Request["SubTransCode"];

            return true;
        }

        protected void BasePage_LoadEnd()
        {
            Response.ContentType = "text/xml";
            Response.Clear();
            Response.Write(m_XmlContent);
            Response.End();
        }

        #region 参数处理
        public bool ExistParameter(string ParameterName)
        {
            return Request[ParameterName] != null;
        }

        protected string GetParameter(string ParameterName)
        {
            return Request[ParameterName];
        }

        /// <summary>
        /// 获取整型参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public int GetIntParameter(string ParameterName, int DefaultValue)
        {
            int TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToInt32(GetParameter(ParameterName));
                }
                catch (Exception e)
                {
                }
            }

            return TempValue;
        }

        /// <summary>
        /// 获取双精度参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public double GetDoubleParameter(string ParameterName, double DefaultValue)
        {
            double TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToDouble(GetParameter(ParameterName));
                }
                catch (Exception e)
                {
                }
            }

            return TempValue;
        }

        /// <summary>
        /// 获取字符串参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public string GetStringParameter(string ParameterName, string DefaultValue)
        {
            string TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                TempValue = GetParameter(ParameterName);
            }

            return TempValue;
        }

        /// <summary>
        /// 获取整型参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="ParameterValue">返回参数值</param>
        /// <returns></returns>
        public bool GetIntParameter(string ParameterName, int DefaultValue, out int ParameterValue)
        {
            bool bReturn = false;
            int TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToInt32(GetParameter(ParameterName));
                    bReturn = true;
                }
                catch (Exception e)
                {
                }
            }

            ParameterValue = TempValue;
            return bReturn;
        }

        /// <summary>
        /// 获取双精度参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="ParameterValue">返回参数值</param>
        /// <returns></returns>
        public bool GetDoubleParameter(string ParameterName, double DefaultValue, out double ParameterValue)
        {
            bool bReturn = false;
            double TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToDouble(GetParameter(ParameterName));
                    bReturn = true;
                }
                catch (Exception e)
                {
                }
            }

            ParameterValue = TempValue;
            return bReturn;
        }

        /// <summary>
        /// 获取字符串参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="ParameterValue">返回参数值</param>
        /// <returns></returns>
        public bool GetStringParameter(string ParameterName, string DefaultValue, out string ParameterValue)
        {
            bool bReturn = false;
            string TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                TempValue = GetParameter(ParameterName);
                bReturn = true;
            }

            ParameterValue = TempValue;
            return bReturn;
        }
  
        /// <summary>
        /// 获取布尔参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="ParameterValue">返回参数值</param>
        /// <returns></returns>
        public bool GetBooleanParameter(string ParameterName, bool DefaultValue, out bool ParameterValue)
        {
            bool bReturn = false;
            bool TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToBoolean(GetParameter(ParameterName));
                    bReturn = true;
                }
                catch (Exception e)
                {
                }
            }

            ParameterValue = TempValue;
            return bReturn;
        }
        #endregion

        #region 导出XML
        /// <summary>
        /// 将数据表以属性形式导出到XML流。
        /// </summary>
        /// <param name="xmlWriter">XML写流</param>
        /// <param name="dt">数据表</param>
        /// <param name="NewElementName">新元素名称</param>
        /// <returns></returns>
        protected XReturn ExportXmlAsAttribute(XmlTextWriter xmlWriter, DataTable dt, string NewElementName)
        {
            string ColumnName, CellValue;
            XReturn xReturn = new XReturn();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xmlWriter.WriteStartElement(NewElementName);

                    foreach (DataColumn dc in dt.Columns)
                    {
                        ColumnName = dc.ColumnName;
                        CellValue = dr[ColumnName].ToString();
                        xmlWriter.WriteAttributeString(ColumnName, CellValue);
                    }

                    xmlWriter.WriteEndElement();
                }
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 将数据行以节点形式导出到XML流。
        /// </summary>
        /// <param name="xmlWriter">XML写流</param>
        /// <param name="dr">数据行</param>
        /// <param name="NewElementName">新元素名称</param>
        /// <returns></returns>
        protected XReturn ExportXmlAsNode(XmlTextWriter xmlWriter, DataRow dr, string NewElementName)
        {
            string ColumnName, CellValue;
            XReturn xReturn = new XReturn();

            if (dr != null)
            {
                if (!String.IsNullOrEmpty(NewElementName))
                {
                    xmlWriter.WriteStartElement(NewElementName);
                }

                foreach (DataColumn dc in dr.Table.Columns)
                {
                    ColumnName = dc.ColumnName;
                    CellValue = dr[ColumnName].ToString();
                    xmlWriter.WriteElementString(ColumnName, CellValue);
                }

                if (!String.IsNullOrEmpty(NewElementName))
                {
                    xmlWriter.WriteEndElement();
                }
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 将数据行以属性形式导出到XML流。
        /// </summary>
        /// <param name="xmlWriter">XML写流</param>
        /// <param name="dr">数据行</param>
        /// <param name="NewElementName">新元素名称</param>
        /// <returns></returns>
        protected XReturn ExportXmlAsAttribute(XmlTextWriter xmlWriter, DataRow dr, string NewElementName)
        {
            string ColumnName, CellValue;
            XReturn xReturn = new XReturn();

            if (dr != null)
            {
                if (!String.IsNullOrEmpty(NewElementName))
                {
                    xmlWriter.WriteStartElement(NewElementName);
                }

                foreach (DataColumn dc in dr.Table.Columns)
                {
                    ColumnName = dc.ColumnName;
                    CellValue = dr[ColumnName].ToString();
                    xmlWriter.WriteAttributeString(ColumnName, CellValue);
                }

                if (!String.IsNullOrEmpty(NewElementName))
                {
                    xmlWriter.WriteEndElement();
                }
            }
 
            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 将数据表以节点形式导出到XML流。
        /// </summary>
        /// <param name="xmlWriter">XML写流</param>
        /// <param name="dt">数据表</param>
        /// <param name="NewElementName">新元素名称</param>
        /// <returns></returns>
        protected XReturn ExportXmlAsNode(XmlTextWriter xmlWriter, DataTable dt, string NewElementName)
        {
            string ColumnName, CellValue;
            XReturn xReturn = new XReturn();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xmlWriter.WriteStartElement(NewElementName);

                    foreach (DataColumn dc in dt.Columns)
                    {
                        ColumnName = dc.ColumnName;
                        CellValue = dr[ColumnName].ToString();
                        xmlWriter.WriteElementString(ColumnName, CellValue);
                    }

                    xmlWriter.WriteEndElement();
                }
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 将实体以属性形式导出到XML流。
        /// </summary>
        /// <param name="xmlWriter">XML写流</param>
        /// <param name="entity">数据实体</param>
        /// <param name="NewElementName">新元素名称</param>
        /// <returns></returns>
        protected XReturn ExportXmlAsAttribute(XmlTextWriter xmlWriter, BaseEntity entity, string NewElementName)
        {
            object objValue;
            string strValue;
            XReturn xReturn = new XReturn();
            Hashtable EntityFields = entity.x.GetFields();

            if (NewElementName != String.Empty)
            {
                xmlWriter.WriteStartElement(NewElementName);
            }

            foreach (string strFieldName in EntityFields.Keys)
            {
                objValue = entity.x.GetValue(strFieldName);
                strValue = objValue == null ? String.Empty : objValue.ToString();

                xmlWriter.WriteAttributeString(strFieldName, strValue);
            }

            if (NewElementName != String.Empty)
            {
                xmlWriter.WriteEndElement();
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 将实体以节点形式导出到XML流。
        /// </summary>
        /// <param name="xmlWriter">XML写流</param>
        /// <param name="entity">数据实体</param>
        /// <param name="NewElementName">新元素名称</param>
        /// <returns></returns>
        protected XReturn ExportXmlAsNode(XmlTextWriter xmlWriter, BaseEntity entity, string NewElementName)
        {
            object objValue;
            string strValue;
            XReturn xReturn = new XReturn();
            Hashtable EntityFields = entity.x.GetFields();

            if (NewElementName != String.Empty)
            {
                xmlWriter.WriteStartElement(NewElementName);
            }

            foreach (string strFieldName in EntityFields.Keys)
            {
                objValue = entity.x.GetValue(strFieldName);
                strValue = objValue == null ? String.Empty : objValue.ToString();

                xmlWriter.WriteElementString(strFieldName, strValue);
            }

            if (NewElementName != String.Empty)
            {
                xmlWriter.WriteEndElement();
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 将返回对象以节点形式导出到XML流。
        /// </summary>
        /// <param name="xmlWriter">XML写流</param>
        /// <param name="xExportReturn">返回对象</param>
        /// <param name="NewElementName">新节点名称</param>
        /// <returns></returns>
        protected XReturn ExportXmlAsNode(XmlTextWriter xmlWriter, XReturn xExportReturn, string NewElementName)
        {
            XReturn xReturn = new XReturn();

            if (NewElementName != String.Empty)
            {
                xmlWriter.WriteStartElement(NewElementName);
            }

            xmlWriter.WriteElementString("ReturnCode", xExportReturn.ReturnCode);
            xmlWriter.WriteElementString("ReturnMessage", xExportReturn.ReturnMessage);

            if (NewElementName != String.Empty)
            {
                xmlWriter.WriteEndElement();
            }

            return xReturn.ReturnSuccess();
        }
        #endregion
    }
    
}
