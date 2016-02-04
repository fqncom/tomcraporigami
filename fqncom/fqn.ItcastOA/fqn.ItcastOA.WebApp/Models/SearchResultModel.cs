using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fqn.ItcastOA.Model.Enum;

namespace fqn.ItcastOA.WebApp.Models
{
    public class SearchResultModel:SearchResultViewModel
    {
        public QueueStateEnum QueueStateEnum { get; set; }
    }
}