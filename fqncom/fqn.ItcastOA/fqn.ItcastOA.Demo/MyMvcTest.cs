using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace fqn.ItcastOA.Demo
{
   public  class MyMvcTest:BaseController
    {

       public string Index()
       {
           return "this is a test page";
       }
    }
}
