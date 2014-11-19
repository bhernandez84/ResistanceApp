using ResistanceApp.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class GameContext
    {
        public List<Player> Players;
        public List<Player> MissionMembers;
        public List<Vote> Votes;
        public int MaxPlayers;
        public int Leader { get; private set; }
        public int NumberOfPlayers
        {
            get
            {
                try
                {
                    return Players.Count();
                }
                catch
                {
                    return 0;
                }
            }
        }
        public int NumberOfSpies
        {
            get
            {
                return Players.Count(m => m.PlayerRole == Role.Spy);
            }
        }

        public Player GetLeader
        {
            get
            {
                return Players[Leader];
            }
        }
        private GameState State;

        public GameStatus Status
        {
            get
            {
                if (State is OnMissionState)
                {
                    return GameStatus.OnMission;
                }
                if (State is SetupState)
                {
                    return GameStatus.Starting;
                }
                if (State is MissionNominatingState)
                {
                    return GameStatus.MissionTeamSelection;
                }
                if (State is MissionVotingState)
                {
                    return GameStatus.Voting;
                }
                if (State is GameOverState)
                {
                    return GameStatus.Complete;
                }

                return GameStatus.Starting;
            }

        }

        public void SetState(GameState state)
        {
            State = state;
            State.Init(this);
        }
        public GameContext(GameState state, int numPlayers)
        {
            State = state;
            MaxPlayers = numPlayers;
            Leader = -1;
        }
        public Player Join(string playername)
        {
            return State.Join(this, playername);
        }

        public void AddPlayer(Player player)
        {
            if (Players == null)
                Players = new List<Player>();
            if (Players.Any(m => m.Name == player.Name))
            {
                throw new InvalidOperationException("Sorry, that name has been taken. Please select another.");
            }
            SetRole(player);
            Players.Add(player);
        }
        public void SetRole(Player player)
        {

        }
        public void PickMissionMembers(string playerName, string[] missionMembers)
        {
            State.PickMissionMembers(this, missionMembers, playerName);
        }
        public void AddMissionMember(Player player)
        {
            if (MissionMembers == null)
                MissionMembers = new List<Player>();
            MissionMembers.Add(player);
        }
        public Player GetPlayer(string playerName)
        {
            return Players.FirstOrDefault(m => m.Name == playerName);
        }

        public void ChooseLeader()
        {
            if (Leader == (NumberOfPlayers - 1))
            {
                Leader = 0;
            }
            else
                Leader++;
        }

        public bool IsLeader(Player player)
        {
            return (Players.IndexOf(player) == Leader);
        }


        public void Vote(string playername, bool vote)
        {
            Player player = GetPlayer(playername);
            State.Vote(this, player, vote);
        }

        public void AddVote(Vote vote)
        {
            Votes.Add(vote);
        }
        public bool HasPlayerVoted(string playerName)
        {
            return Votes.Any(m => m.Player.Name == playerName);
        }

        public bool? GetVote(string playerName)
        {
            if (HasPlayerVoted(playerName))
            {
                return Votes.FirstOrDefault(m => m.Player.Name == playerName).PlayerVote;
            }
            return null;
        }


        public bool GameFull
        {
            get
            {
                return MaxPlayers == NumberOfPlayers;
            }
        }


    }
}
