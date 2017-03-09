using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entyties
{
    [Table(Name = "Games")]
    public class Game
    {
        [Column(Name = "Id_games", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id_games { get; set; }

        [Column(Name = "Id_champ")]
        public int IdChamp { get; set; }

        [Column(Name = "Email_player_1")]
        public string EmailPlayer_1 { get; set; }

        [Column(Name = "Email_player_2")]
        public string EmailPlayer_2 { get; set; }

        [Column(Name = "Points_ championship_player_1")]
        public int ChampionshipPlayer_1 { get; set; }

        [Column(Name = "Points_ championship_player_2")]
        public int ChampionshipPlayer_2 { get; set; }
    }
}
