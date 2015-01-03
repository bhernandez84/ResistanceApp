using ResistanceApp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class MissionVotingState : GameState
    {
        protected int LeaderProposalCounter
        { get; set; }
        public override void Init(GameContext context)
        {
            context.Votes = new List<Vote>();
        }
        public override void Vote(GameContext context, Player player, bool vote)
        {
            if (!context.HasPlayerVoted(player.Name))
            {
                context.AddVote(new Vote(player, vote));
            }
            else
            {
                var playerVote = context.Votes.FirstOrDefault(m => m.Player.Name == player.Name);
                playerVote.PlayerVote = vote;
            }
            if (context.Votes.Count() == context.NumberOfPlayers)
            {
                ResolveVotes(context);
            }
        }
        public override void ResolveVotes(GameContext context)
        {
            int majority = context.NumberOfPlayers % 2 == 1 ? (context.NumberOfPlayers + 1) / 2 : context.NumberOfPlayers / 2;
            bool doesMajorityApprove = context.Votes.Count(m => m.PlayerVote) > majority;
            if (doesMajorityApprove)
            {
                context.SetState(new OnMissionState());
            }
            else
            {
                if (LeaderProposalCounter == context.NumberOfPlayers)
                {
                    context.SetState(new GameOverState());
                }
                else
                {
                    LeaderProposalCounter++;
                    context.SetState(new MissionNominatingState(LeaderProposalCounter));
                }
            }
        }

        public MissionVotingState(int leaderCounter = 1)
        {

            LeaderProposalCounter = leaderCounter;
        }
    }
}
