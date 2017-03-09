using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class TournamentViewModel
    {
        public string TournamentName { get; set; }
        public DateTime StartDate { get; set; }
        public int IdTournament { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfRequest { get; set; }
         
    }
}