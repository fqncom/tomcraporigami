using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Models;

namespace TickTick.Entity
{
    public class Reminder
    {
        private String _id;

        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _trigger;

        public String Trigger
        {
            get { return _trigger; }
            set { _trigger = value; }
        }
        
    }
}
