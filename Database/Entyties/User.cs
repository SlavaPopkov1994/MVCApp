using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entyties
{
    [Table(Name = "Users")]
    public class User
    {
        [Column]
        public string Surname { get; set; }

        [Column]
        public string Name { get; set; }

        [Column(Name = "Middle_name")]//указываем, если имя изменилось
        public string MiddleName { get; set; }

        [Column(IsPrimaryKey = true)]
        public string Email { get; set; }

        [Column]
        public string Phone { get; set; }

        [Column]
        public string Comments { get; set; }

        [Column]
        public string PhotoURL { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public bool IsAdmin { get; set; }
    }
}
