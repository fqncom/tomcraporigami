using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Models
{
   public class SyncBean
    {

        public long CheckPoint { get; set; }
        public SyncTaskBean SyncTaskBean { get; set; }
        public List<ProjectProfiles> ProjectProfiles { get; set; }
        public SyncTaskOrderBean SyncTaskOrderBean { get; set; }
        public string InboxId { get; set; } 
    }
}
