using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Models
{
   public class ProjectProfiles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsOwner { get; set; }
        public bool InAll { get; set; }
        public long SortOrder { get; set; }
        public string SortType { get; set; }
        public int? UserCount { get; set; }
        public string Etag { get; set; }
        public DateTime? ModifiedTime { get; set; } 
    }
}
