using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class TournamentList
    {
        public TournamentList()
        {
            TournamentsList = new List<TournamentViewModel>();
        }
        
        public List<TournamentViewModel> TournamentsList { get; set; }
        
    }
}