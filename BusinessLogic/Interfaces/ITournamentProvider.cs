using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entyties;

namespace BusinessLogic.Interfaces
{
    public interface ITournamentProvider
    {
        Championship CreatedTournament(string TournamentName );
        IEnumerable<Championship> GetAllTournaments();
        IEnumerable<Championship>  GetAllTournamentsOrderBy();
        int NumberOfRequest(int IdTournament);

        bool CheckAvailabilityOfTournamentRequest(int IdTournament, string Email);
        TournamentRequest CreateTournamentRequest(int IdTournament, string Email);

        bool TournamentIsActive(int TournamentId);
        void TournamentStart(int IdTournament);

        IEnumerable<Game> GetAllGamesTournament(int IdTournament);
        Game EditingPointsGame(int GameId, int PointsPlayer1, int PointsPlayer2);

       
    }
}

