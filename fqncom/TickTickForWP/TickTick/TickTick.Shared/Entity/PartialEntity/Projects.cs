using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TickTick.Enums;

namespace TickTick.Entity
{
    public partial class Projects
    {
        private int _tasksCount;
        [JsonIgnore]
        [Ignore]
        public int TasksCount
        {
            get { return _tasksCount; }
            set { _tasksCount = value; }
        }

        private bool _isIntelligentProjects = false;
        
        /// <summary>
        /// 是否是智能清单
        /// </summary>
        [Ignore]
        [JsonIgnore]
        public bool IsIntelligentProjects
        {
            get { return _isIntelligentProjects; }
            set { _isIntelligentProjects = value; }
        }

        public IntelligentProjectsTypeEnum IntelligentProjectsTypeEnum { get; set; }

        public static Projects GetDefaultProjects()
        {
            return new Projects { Name = "收集箱", IsIntelligentProjects = true, IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsInboxList, SId = App.SignUserInfo.InboxId }; //SId = App.SignUserInfo.InboxId };// TODO 如果是未登入用户，如何处理
        }

    }
}
