using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;

namespace TickTick.Models
{
    public class FrameTransitionParam
    {
        public Projects Projects { get; set; }
        public Tasks Tasks { get; set; }
        public SettingItem SettingItem { set; get; }

        public User SignUserInfo { get; set; }

        public int? TasksIdFromToast { get; set; }

        public Tasks NewTasks { get; set; }
        public Tasks UpdateTasks { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
