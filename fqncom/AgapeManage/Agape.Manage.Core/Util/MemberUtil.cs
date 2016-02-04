using System;
using System.Collections.Generic;
using System.Text;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Common;

namespace Agape.Manage.Core.Util
{
    public class MemberUtil
    {
        /// <summary>
        /// 获取完全地址。
        /// </summary>
        /// <param name="MemberAddress">会员地址对象</param>
        /// <returns></returns>
        public static string GetMemberFullAddress(BSC_MemberAddress MemberAddress)
        {
            return LeopardFactory.GetAreaInfoCache().GetAreaName(MemberAddress.Province) + LeopardFactory.GetAreaInfoCache().GetAreaName(MemberAddress.City) + LeopardFactory.GetAreaInfoCache().GetAreaName(MemberAddress.District) + MemberAddress.Detail;
        }
    }
}
