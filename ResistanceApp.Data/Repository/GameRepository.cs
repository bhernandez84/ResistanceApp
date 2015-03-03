using ResistanceApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Repository
{
    public class GameRepository
    {
        protected List<ResistanceGame> Games
        { get; set; }

        public Player JoinGame(string playerName, string gameID)
        {
            Guid myGameGuid;

            if (Guid.TryParse(gameID, out myGameGuid))
            {
                ResistanceGame game = GetGame(myGameGuid);
                if (game != null)
                {
                    return game.Join(playerName);
                }
            }
            ResistanceGame randonGame = GetRandomGameToJoin();
            return randonGame.Join(playerName);
        }
        public ResistanceGame GetRandomGameToJoin()
        {
            if (Games == null || !Games.Any(m => m.Status == Data.Models.Enum.GameStatus.Starting))
            {
                return CreateGame();
            }
            else
            {
                return Games.FirstOrDefault(m => m.Status == Data.Models.Enum.GameStatus.Starting);
            }
        }
        public ResistanceGame GetGame(Guid gameId)
        {
            return Games.FirstOrDefault(m => m.GameID == gameId);
        }

        protected IEnumerable<ResistanceGame> GetGames(Func<ResistanceGame, bool> predicate)
        {
            return Games.Where(predicate);
        }

        public IEnumerable<ResistanceGame> GetActiveGames()
        {
            return GetGames(m => m.Status != Models.Enum.GameStatus.Complete);
        }

        public ResistanceGame CreateGame()
        {
            ResistanceGame game = new ResistanceGame(5);
            game.GameID = Guid.NewGuid();
            Games.Add(game);
            return game;
        }
        public GameRepository()
        {
            Games = new List<ResistanceGame>();
        }
    }
}
