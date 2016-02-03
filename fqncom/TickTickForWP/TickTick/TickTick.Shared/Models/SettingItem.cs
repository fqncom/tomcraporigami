using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Enums;

namespace TickTick.Models
{
    public class SettingItem
    {
        /// <summary>
        /// 设置项的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设置项类型
        /// </summary>
        public SettingItemTypeEnum SettingItemType { get; set; }
    }
}
