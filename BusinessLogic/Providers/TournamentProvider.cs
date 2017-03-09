using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entyties;
using Database;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Providers
{
    public class TournamentProvider : ITournamentProvider
    {
        private ChampionshipDatabaseContext _context = new ChampionshipDatabaseContext();
        public Championship CreatedTournament(string tournamentName)
        {
            var championship = new Championship
            {
                Title = tournamentName,
                Date = DateTime.Today,
                Is_active = true

            };

            _context.Championchips.InsertOnSubmit(championship);
            _context.SubmitChanges();
            return championship;
        }

        public IEnumerable<Championship> GetAllTournaments()
        {
            return _context.Championchips.OrderBy(t => t.Title);
        }

        public IEnumerable<Championship> GetAllTournamentsOrderBy()
        {
            return _context.Championchips.OrderBy(t => t.Date);
        }

        public int NumberOfRequest(int idTournament)
        {
            var tournamentsRequests = _context.TournamentRequests.Where(t => t.Id_champ == idTournament);
            int NumberOfRequest=0;
            foreach(var tournamentsRequest in tournamentsRequests )
            {
                NumberOfRequest = NumberOfRequest + 1;
            }
            return NumberOfRequest;
        }

        public bool CheckAvailabilityOfTournamentRequest(int IdTournament, string Email)
        {
            var chamionship = _context.Championchips.FirstOrDefault(u => int.Equals(u.Id_champ, IdTournament));
            if (chamionship.Is_active)
            {
                var tournamentsRequests = _context.TournamentRequests.Where(t => t.Id_champ == IdTournament);
                foreach (var tournamentsRequest in tournamentsRequests)
                {
                    if (tournamentsRequest.Email == Email)
                    { return true; }
                }
                return false;
            }
            return true;
        }

        public TournamentRequest CreateTournamentRequest(int idTournament, string email)
        {
            var tournamentRequest = new TournamentRequest
            {
                Id_champ = idTournament,
                Email = email
            };
            _context.TournamentRequests.InsertOnSubmit(tournamentRequest);
            _context.SubmitChanges();
            return tournamentRequest;
        }

        public bool TournamentIsActive(int tournamentId)
        {
            var chamionship = _context.Championchips.FirstOrDefault(u => int.Equals(u.Id_champ, tournamentId));
            return chamionship.Is_active;

        }

        public void TournamentStart(int idTournament)
        {
            var tournamentsRequests = _context.TournamentRequests.Where(t => t.Id_champ == idTournament);
            foreach (var tournamentsRequest in tournamentsRequests)
            {
                foreach (var otherTournamentsRequest in tournamentsRequests)
                {
                    if (otherTournamentsRequest != tournamentsRequest)
                    {
                        var game = new Game();
                        game.IdChamp = idTournament;
                        game.EmailPlayer_1 = tournamentsRequest.Email;
                        game.EmailPlayer_2 = otherTournamentsRequest.Email;
                        game.ChampionshipPlayer_1 = -1;
                        game.ChampionshipPlayer_2 = -1;
                        _context.Games.InsertOnSubmit(game);
                    }
                }
            }
            var championship = _context.Championchips.FirstOrDefault(u => int.Equals(u.Id_champ, idTournament));
            championship.Is_active = false;
            _context.SubmitChanges();
        }

        public IEnumerable<Game> GetAllGamesTournament(int idTournament)
        {
            return _context.Games.Where(t => t.IdChamp == idTournament);
        }

        public Game EditingPointsGame(int GameId, int PointsPlayer1, int PointsPlayer2)
        {
            var game = _context.Games.FirstOrDefault(u => int.Equals(u.Id_games, GameId));
            game.ChampionshipPlayer_1 = PointsPlayer1;
            game.ChampionshipPlayer_2 = PointsPlayer2;
            _context.SubmitChanges();
            return game;
        }
    }
}
