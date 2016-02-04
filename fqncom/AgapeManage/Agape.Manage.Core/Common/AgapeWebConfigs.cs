using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;

namespace Agape.Manage.Core.Common
{
    public class AgapeManageConfigs
    {
        #region 静态部分
        private static AgapeManageConfigs m_Instance;

        static AgapeManageConfigs()
        {
            m_Instance = null;
        }

        public static AgapeManageConfigs Current
        {
            get { return GetInstance(); }
        }

        public static AgapeManageConfigs GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new AgapeManageConfigs();
            }
            return m_Instance;
        }
        #endregion

        public string ImageServerPath;

        public string Alipay_Partner;
        public string Alipay_Key;
        public string Alipay_SellerEmail;

        public AgapeManageConfigs()
        {
            ImageServerPath = String.Empty;
        }

        /// <summary>
        /// 加载爱家贝网站配置
        /// </summary>
        /// <returns></returns>
        public XReturn LoadConfigs()
        {
            XReturn xReturn = new XReturn();

            ImageServerPath = LeopardConfigs.ReadClientIniValue("Web", "ImageServerPath");

            Alipay_Partner = LeopardConfigs.ReadClientIniValue("Alipay", "Partner");
            Alipay_Key = LeopardConfigs.ReadClientIniValue("Alipay", "Key");
            Alipay_SellerEmail = LeopardConfigs.ReadClientIniValue("Alipay", "SellerEmail");

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 获取商品图片URL
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="PictureSize">图片尺寸类型</param>
        /// <returns></returns>
        public string GetProductPictureUrl(string ProductNo, string PictureSize)
        {
            return GetProductPictureUrl(ProductNo, ProductNo, "jpg", PictureSize);
        }

        /// <summary>
        /// 获取商品图片URL
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="FileType">文件类型</param>
        /// <param name="PictureSize">图片尺寸类型</param>
        /// <returns></returns>
        public string GetProductPictureUrl(string ProductNo, string FileName, string FileType, string PictureSize)
        {
            return string.Format("{0:S}productpic/{1:S}/{2:S}{4:S}.{3:S}", ImageServerPath, ProductNo.Trim(), FileName.Trim(), FileType.Trim(), PictureSize);
        }
    }
}
