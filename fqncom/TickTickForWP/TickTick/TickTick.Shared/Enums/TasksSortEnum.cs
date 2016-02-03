using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Enums
{
    public enum TasksSortEnum
    {
        /// <summary>
        /// 自定义排序
        /// </summary>
        Custom_Sort = 0,
        /// <summary>
        /// 日期排序
        /// </summary>
        DateTime_Sort = 1,
        /// <summary>
        /// 标题排序
        /// </summary>
        Title_Sort = 2,
        /// <summary>
        /// 优先级排序
        /// </summary>
        Priorities_Sort = 3
    }
}
