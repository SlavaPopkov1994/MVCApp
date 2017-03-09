using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entyties
{
    [Table(Name = "Championships")]
    public class Championship
    {
        [Column(IsPrimaryKey = true, Name = "Id_champ", IsDbGenerated=true)]
        public int Id_champ { get; set; }

        [Column(Name = "Title")]
        public string Title { get; set; }

        [Column(Name = "Date")]
        public DateTime Date { get; set; }

        [Column(Name = "Is_active")]
        public bool Is_active { get; set; }
    }
}
