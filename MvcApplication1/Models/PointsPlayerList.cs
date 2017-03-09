using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class PointsPlayerList
    {
        public string[] Emails { get; set; }
        public PointsViewModel[][] PointsTable { get; set; }
        public int IdTournament { get; set; }
        
    }
}
