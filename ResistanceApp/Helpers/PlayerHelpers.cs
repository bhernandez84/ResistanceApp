using ResistanceApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Helpers
{
    public static class PlayerHelpers
    {
        public static void ToSpy(this Player player)
        {
            player.PlayerRole = Models.Enum.Role.Spy;
        }
        public static int GetNumberOfSpiesForGame(int numberOfPlayers)
        {
            switch (numberOfPlayers)
            {
                case 5:
                case 6:
                    return 2;
                case 7:
                case 8:
                    return 3;
                case 9:
                case 10:
                    return 4;
            }
            return 0;
        }
    }
}
