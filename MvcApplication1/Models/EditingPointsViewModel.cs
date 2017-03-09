using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class EditingPointsViewModel
    {
        public int IdTournament { get; set; }
        public int IdGames { get; set; }
        public int PointsPlayer1 { get; set; }
        public int PointsPlayer2 { get; set; }
    }
}