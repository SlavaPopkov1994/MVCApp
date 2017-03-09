using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using BusinessLogic.Interfaces;
using BusinessLogic.Providers;
using System.Globalization;
using MvcApplication1.Helpers;

namespace MvcApplication1.Controllers
{
    public class TournamentController : Controller
    {
        private ITournamentProvider _tournamentProvider = new TournamentProvider();

        [HttpGet]
        public ActionResult Tournament()
        {
            return View(new TournamentViewModel());
        }

        [HttpPost]
        public ActionResult Tournament(TournamentViewModel model)
        {
            var tournament = _tournamentProvider.CreatedTournament(model.TournamentName);
            return View(model);
        }

        [HttpGet]
        public ActionResult AllTournaments()
        {
            var model = new TournamentList();
            
            var tournaments = _tournamentProvider.GetAllTournaments();
            foreach (var tournament in tournaments)
            {
                var tournamentViewModel = new TournamentViewModel
                {
                    IdTournament = tournament.Id_champ,
                    TournamentName = tournament.Title,
                    StartDate = tournament.Date,
                    IsActive = tournament.Is_active,
                    NumberOfRequest = _tournamentProvider.NumberOfRequest(tournament.Id_champ)
                };
                model.TournamentsList.Add(tournamentViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AllTournaments(TournamentList model)
        {
            var tournaments = _tournamentProvider.GetAllTournamentsOrderBy();
            foreach (var tournament in tournaments)
            {
                var tournamentViewModel = new TournamentViewModel
                {
                    IdTournament = tournament.Id_champ,
                    TournamentName = tournament.Title,
                    StartDate = tournament.Date,
                    IsActive = tournament.Is_active,
                    NumberOfRequest = _tournamentProvider.NumberOfRequest(tournament.Id_champ)
                };
                model.TournamentsList.Add(tournamentViewModel);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateRequestForTournament(int tournamentId)
        {
            var User = UserContext.GetCurrentUser();
            if(!_tournamentProvider.CheckAvailabilityOfTournamentRequest(tournamentId, User.Email))
            { 
            var tournaments = _tournamentProvider.CreateTournamentRequest(tournamentId, User.Email);
            }
            return RedirectToAction("AllTournaments", "Tournament");
        }

        public ActionResult StartTournament(int tournamentId)
        {
          
            if(_tournamentProvider.TournamentIsActive(tournamentId))
            { 
            _tournamentProvider.TournamentStart(tournamentId);
            }
            return RedirectToAction("AllTournaments", "Tournament");
        }

        [HttpGet]
        public ActionResult TournamentTable(int tournamentId)
        {
            var model = new PointsPlayerList();
            model.IdTournament = tournamentId;
            var games = _tournamentProvider.GetAllGamesTournament(tournamentId);

            var allEmails = games.Select(g => g.EmailPlayer_1).Distinct().ToList();
            model.Emails = allEmails.ToArray();
            int arrayLength = model.Emails.Length;
            var pointsArray = new PointsViewModel[arrayLength][];
            for (int i = 0; i < arrayLength; i++)
            {
                pointsArray[i] = new PointsViewModel[arrayLength];
            }
            foreach (var game in games)
            {
                var firstEmailIndex = allEmails.IndexOf(game.EmailPlayer_1);
                var secondEmailIndex = allEmails.IndexOf(game.EmailPlayer_2);
                pointsArray[firstEmailIndex][secondEmailIndex] = new PointsViewModel(game.ChampionshipPlayer_1, game.ChampionshipPlayer_2, game.Id_games);
            }
            model.PointsTable = pointsArray;
            return View(model);
        }

        [HttpGet]
        public ActionResult EditingPoints()
        {
            return View(new EditingPointsViewModel());
        }

        [HttpPost]
        public ActionResult EditingPoints(EditingPointsViewModel model, int gameId, int IdChamp)
        {
            model.IdTournament = IdChamp;
            _tournamentProvider.EditingPointsGame(gameId, model.PointsPlayer1, model.PointsPlayer2);
            return View(model);
        }


        public ActionResult TournamentResults(int IdChamp)
        {
            var model = new TournamentResultViewModel();
            var games = _tournamentProvider.GetAllGamesTournament(IdChamp);
            var allEmails = games.Select(g => g.EmailPlayer_1).Distinct().ToList();
            model.Emails = allEmails.ToArray();
            model.IdTournament = IdChamp;
            int arrayLength = model.Emails.Length;
            model.Points = new int[arrayLength];
            for (int r = 0; r < arrayLength; r++)
            {
                model.Points[r] = 0;
            }
            foreach (var game in games)
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    if (game.EmailPlayer_1 == model.Emails[i])
                    {
                        if (game.ChampionshipPlayer_1 > game.ChampionshipPlayer_2)
                        { model.Points[i] = model.Points[i] + 2; }
                        if (game.ChampionshipPlayer_1 == game.ChampionshipPlayer_2)
                        {
                            if (game.ChampionshipPlayer_1 != -1)
                            { model.Points[i] = model.Points[i] + 1; }
                        }
                        break;
                    }
                }
            }
            foreach (var game in games)
            {
                for (int j = 0; j < arrayLength; j++)
                {
                    if (game.EmailPlayer_2 == model.Emails[j])
                    {
                        if (game.ChampionshipPlayer_2 == game.ChampionshipPlayer_1)
                        {
                            if (game.ChampionshipPlayer_2 != -1)
                            { model.Points[j] = model.Points[j] + 1; }
                        }
                        if (game.ChampionshipPlayer_2 > game.ChampionshipPlayer_1)
                        { model.Points[j] = model.Points[j] + 2; }
                        break;
                    }
                }
            }
            for (int s = 0; s < arrayLength; s++)
            {
                for(int q=s+1; q<arrayLength; q++)
                {
                    if (model.Points[s] < model.Points[q])
                    {
                        var temp1 = model.Points[s];
                        var temp2 = model.Emails[s];
                        model.Points[s] = model.Points[q];
                        model.Emails[s] = model.Emails[q];
                        model.Points[q] = temp1;
                        model.Emails[q] = temp2;
                    }
                }
            }
            return View(model);
        }
    }
}
