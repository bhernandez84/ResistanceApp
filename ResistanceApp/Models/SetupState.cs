using ResistanceApp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class SetupState : GameState
    {

        public override Player Join(GameContext context, string playerName)
        {
            if (context.GameFull)
            {
                throw new InvalidOperationException("Sorry, this game is full.");
            }
            Player player = new Player(playerName);
            context.AddPlayer(player);
            if (context.GameFull)
            {
                AssignRoles(context);
                context.SetState(new MissionNominatingState());
            }
            return player;
        }
        protected void AssignRoles(GameContext context)
        {
            int numberOfSpies = PlayerHelpers.GetNumberOfSpiesForGame(context.NumberOfPlayers);
            int[] spies = new int[numberOfSpies];
            Random random = new Random();
            for (int i = 0; i < numberOfSpies; i++)
            {
                int spy = random.Next(0, context.NumberOfPlayers);
                if (!spies.Contains(spy))
                {
                    spies[i] = spy;
                }
                else
                {
                    i--;
                }
            }
            foreach (int spyIndex in spies)
            {
                context.Players[spyIndex].ToSpy();
            }
        }
    }
}
