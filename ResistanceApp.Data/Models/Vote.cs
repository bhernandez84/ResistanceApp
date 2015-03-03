using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class Vote
    {
       public  Player Player;
       public  bool PlayerVote;

       public Vote() { }

       public Vote(Player player, bool vote)
       {
           Player = player;
           PlayerVote = vote;
       }
    }
}
