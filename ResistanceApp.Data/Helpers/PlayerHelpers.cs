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

        public static int GetNumberOfPlayersForMission(int numberOfPlayers, int roundNumber)
        {
            switch (numberOfPlayers)
            {
                case 5:
                    switch (roundNumber)
                    {
                        case 0:
                            return 2;
                        case 1:
                            return 3;
                        case 2:
                            return 2;
                        case 3:
                            return 3;
                        case 4:
                            return 3;
                        default:
                            return 0;
                    }
                case 6:
                    switch (roundNumber)
                    {
                        case 0:
                            return 2;
                        case 1:
                        case 2:
                        case 3:
                            return 3;
                        case 4:
                            return 4;
                        default:
                            return 0;
                    }
                case 7:
                    switch (roundNumber)
                    {
                        case 0:
                            return 2;
                        case 1:
                        case 2:
                            return 3;
                        case 3:
                        case 4:
                            return 4;
                        default:
                            return 0;
                    }
                case 8:
                case 9:
                case 10:
                    switch (roundNumber)
                    {
                        case 0:
                            return 3;
                        case 1:
                        case 2:
                            return 4;
                        case 3:
                        case 4:
                            return 5;
                        default:
                            return 0;
                    }
                default:
                    return 0;
            }

        }
    }
}
