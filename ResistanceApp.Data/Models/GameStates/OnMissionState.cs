﻿using ResistanceApp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class OnMissionState : GameState
    {

        public override void Init(GameContext context)
        {
            context.Votes = new List<Vote>();
        }
        public override void Vote(GameContext context, Player player, bool vote)
        {
            if (context.MissionMembers.Contains(player) && !context.HasPlayerVoted(player.Name))
            {
                context.AddVote(new Vote(player, vote));
                if (context.Votes.Count() == context.MissionMembers.Count())
                {
                    context.ResolveMissionVote();
                    context.SetState(new GameOverState());
                }
            }
            else
            {
                base.Vote(context, player, vote);
            }
        }
    }
}
