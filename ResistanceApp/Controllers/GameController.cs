using ResistanceApp.Data.Models;
using ResistanceApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResistanceApp.Controllers
{
    public class GameController : Controller
    {
        private GameRepository _gameRepository
        { get; set; }

        private GameRepository GameRepo
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new GameRepository();
                }
                return _gameRepository;
            }
        }

        public JsonResult JoinGame(string playerName, string gameID = null)
        {
            Player player = GameRepo.JoinGame(playerName, gameID);
            return Json(player, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewActiveGames()
        {
            var games = GameRepo.GetActiveGames();
            return Json(games, JsonRequestBehavior.AllowGet);
        }
    }

   
}