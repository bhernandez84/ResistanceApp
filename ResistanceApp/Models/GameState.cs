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

    public class SetupState : GameState
    {

       public override Player Join(GameContext context, string playerName){
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

    public class MissionNominatingState : GameState {
        protected int LeaderProposalCounter
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
        }
        public MissionNominatingState(int leaderCounter = 1){
            LeaderProposalCounter = leaderCounter;
        }


    }
    public class MissionVotingState : GameState
    {
        protected int LeaderProposalCounter
        { get; set; }
        public override void Init(GameContext context)
        {
            context.Votes = new List<Vote>();
        }
        public override void Vote(GameContext context, Player player, bool vote){
            if (!context.HasPlayerVoted(player.Name))
            {
                context.AddVote(new Vote(player, vote));
            }
            else {
                var playerVote = context.Votes.FirstOrDefault(m => m.Player.Name == player.Name);
                playerVote.PlayerVote = vote;
            }
            if (context.Votes.Count() == context.NumberOfPlayers)
            {
                ResolveVotes(context);
            }
        }
        public override void ResolveVotes(GameContext context){
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

    public class OnMissionState : GameState
    {

    }

    public class GameOverState : GameState {}
}
