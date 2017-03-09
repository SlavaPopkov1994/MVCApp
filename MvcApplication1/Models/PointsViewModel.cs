using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class PointsViewModel
    {
        public PointsViewModel(int pointsPlayer1, int pointsPlayer2, int idGame)
        {
            PointsPlayer1 = pointsPlayer1;
            PointsPlayer2 = pointsPlayer2;
            IdGame = idGame;
        }
        public int PointsPlayer1 { get; set; }
        public int PointsPlayer2 { get; set; }
        public int IdGame { get; set; }
    }
}
