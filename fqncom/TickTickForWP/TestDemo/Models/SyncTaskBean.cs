using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Models
{
   public  class SyncTaskBean
    {
        public List<Update> Update { get; set; }
        public List<Delete> Delete { get; set; }
        public List<Add> Add { get; set; } 
    }
}
