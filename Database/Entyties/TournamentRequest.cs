using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Database.Entyties
{
     [Table(Name = "[TournamentRequest]")]
    public class TournamentRequest
    {
        [Column]
        public int Id_champ { get; set; }

        [Column]
        public string Email { get; set; }

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id_tournamentRequest { get; set; }
    }
}
