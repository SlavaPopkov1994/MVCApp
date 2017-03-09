using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class TournamentResultViewModel
    {
      
        public int IdTournament { get; set; }
        public string[] Emails { get; set; }
        public int[] Points { get; set; }
    }
}