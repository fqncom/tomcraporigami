using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Models
{
    public class SyncProjectBean
    {
        private List<ProjectProfiles> _update = new List<ProjectProfiles>();

        public List<ProjectProfiles> Update
        {
            get { return _update; }
            set { _update = value; }
        }
        private List<String> _delete = new List<string>();

        public List<String> Delete
        {
            get { return _delete; }
            set { _delete = value; }
        }
        private List<ProjectProfiles> _add = new List<ProjectProfiles>();

        public List<ProjectProfiles> Add
        {
            get { return _add; }
            set { _add = value; }
        }

    }
}
