using ResistanceApp.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class Player
    {   
        public string Name;
        public Role PlayerRole;
        public Player() { }

        public Player(string playerName)
        {
            Name = playerName;
        }

    }
}
