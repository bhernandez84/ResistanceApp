using ResistanceApp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class MissionNominatingState : GameState
    {
        protected int LeaderProposalCounter
        { get; set; }

        public int NumberOfMembersForMission
        { get; set; }

        public override void PickMissionMembers(GameContext context, string[] playerNames, string sendingPlayer)
        {
            Player currentPlayer = context.GetPlayer(sendingPlayer);
            if (!context.IsLeader(currentPlayer))
            {
                throw new InvalidOperationException("Only the leader can nominate mission members");
            }
            else
            {
                if (playerNames.Length != NumberOfMembersForMission)
                {
                    throw new InvalidOperationException("Incorrect number of members nominated for this mission");
                }
                foreach (var player in playerNames)
                {
                    Player missionPlayer = context.GetPlayer(player);
                    if (missionPlayer == null)
                    {
                        throw new NullReferenceException("That player was not found!");
                    }
                    context.AddMissionMember(missionPlayer);
                }
                context.SetState(new MissionVotingState(LeaderProposalCounter));
            }

        }
        public override void Init(GameContext context)
        {
            context.ChooseLeader();
            NumberOfMembersForMission = PlayerHelpers.GetNumberOfPlayersForMission(context.NumberOfPlayers,
                context.Round);
        }
        public MissionNominatingState(int leaderCounter = 1)
        {
            LeaderProposalCounter = leaderCounter;
        }
    }
}
