using Database.Entyties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ChampionshipDatabaseContext : DataContext
    {
        private const string ConnectionStringName = "ChamionshipConnection";

        public ChampionshipDatabaseContext()
            : base(GetConnectionString())
        {
        }

        public Table<User> Users;
        public Table<Championship> Championchips;
        public Table<TournamentRequest> TournamentRequests;
        public Table<Game> Games;

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }
    }
}
