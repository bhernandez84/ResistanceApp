using ResistanceApp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public abstract class GameState
    {

        public virtual void Init(GameContext context)
        {

        }
        public virtual void Vote(GameContext context, Player player, bool vote)
        {
            throw new InvalidOperationException("Voting has not begun!");
        }

        public virtual void PickMissionMembers(GameContext context, string[] playerNames, string sendingPlayer)
        {
            throw new InvalidOperationException("Game has not started yet!");
        }

        void SendMessage(GameContext context, string fromPlayer, string toPlayer, string message)
        {
            
        }

        public virtual Player Join(GameContext context, string playerName)
        {
            throw new InvalidOperationException("Sorry, you cannot join at this time.");
        }

        void ChooseLeader(GameContext context)
        {
            throw new NotImplementedException();
        }

        void CallForVote(GameContext context)
        {
            throw new NotImplementedException();
        }

        void GetVote(GameContext context)
        {
            throw new NotImplementedException();
        }
        public virtual void ResolveVotes(GameContext context)
        { }

    }
}
