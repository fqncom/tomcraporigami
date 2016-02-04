using System;
using System.Collections.Generic;
using System.Text;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Common;

namespace Agape.Manage.Core.Util
{
    public class WebUtil
    {
        /// <summary>
        /// 提交WEB请求
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <returns></returns>
        public static string PostWebRequest(string Url)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.Default))
                {
                    return reader.ReadToEnd();
                }
            }

            catch (System.Exception ex)
            {
                LeopardLog.Error(ex.Message);
            }

            return String.Empty;
        }
    }
}
