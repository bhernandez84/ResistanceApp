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
        #region Game Data

        public Player Join(string playername)
        {
            return CurrentState.Join(this, playername);
        }

        public int Round
        {
            get
            {
                return Points.Resistance + Points.Spies;
            }
        }

        public Score Points
        { get; set; }

        public void Start()
        {
            SetState(new MissionNominatingState());
        }

        public int MaxPlayers;

        public bool GameFull
        {
            get
            {
                return MaxPlayers == NumberOfPlayers;
            }
        }

        private GameState CurrentState;

        public GameStatus Status
        {
            get
            {
                if (CurrentState is OnMissionState)
                {
                    return GameStatus.OnMission;
                }
                if (CurrentState is SetupState)
                {
                    return GameStatus.Starting;
                }
                if (CurrentState is MissionNominatingState)
                {
                    return GameStatus.MissionTeamSelection;
                }
                if (CurrentState is MissionVotingState)
                {
                    return GameStatus.Voting;
                }
                if (CurrentState is GameOverState)
                {
                    return GameStatus.Complete;
                }

                return GameStatus.Starting;
            }

        }

        public void SetState(GameState state)
        {
            CurrentState = state;
            CurrentState.Init(this);
        }

        public GameContext(GameState state, int numPlayers)
        {
            CurrentState = state;
            MaxPlayers = numPlayers;
            Leader = -1;
            Points = new Score();
        }

        #endregion

        #region Player Info

        public List<Player> Players;

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

        public void AddPlayer(Player player)
        {
            if (Players == null)
                Players = new List<Player>();
            if (Players.Any(m => m.Name == player.Name))
            {
                throw new InvalidOperationException("Sorry, that name has been taken. Please select another.");
            }
            Players.Add(player);
        }

        public Player GetPlayer(string playerName)
        {
            return Players.FirstOrDefault(m => m.Name == playerName);
        }

        public IEnumerable<Player> GetPlayers(string playerName)
        {
            var currentPlayer = GetPlayer(playerName);
            if (currentPlayer.PlayerRole == Role.Spy)
            {
                return GetListOfPlayersForSpies();
            }
            return GetListOfPlayersMaskRoles();
        }
        protected IEnumerable<Player> GetListOfPlayersForSpies()
        {
            return Players;
        }
        protected IEnumerable<Player> GetListOfPlayersMaskRoles()
        {
            var resistancePlayers = Players.Select(m => new Player() { Name = m.Name, PlayerRole = Role.Resistance });
            return resistancePlayers;
        }

        #endregion

        #region Leader

        public int Leader { get; private set; }


        public Player GetLeader
        {
            get
            {
                return Players[Leader];
            }
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

        #endregion

        #region Mission info

        public List<Player> MissionMembers;

        public void PickMissionMembers(string playerName, string[] missionMembers)
        {
            CurrentState.PickMissionMembers(this, missionMembers, playerName);
        }

        public void AddMissionMember(Player player)
        {
            if (MissionMembers == null)
                MissionMembers = new List<Player>();
            MissionMembers.Add(player);
        }

        #endregion

        #region Votes
        public List<Vote> Votes;

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

        public void Vote(string playername, bool vote)
        {
            Player player = GetPlayer(playername);
            CurrentState.Vote(this, player, vote);
        }

        public void AddVote(Vote vote)
        {
            Votes.Add(vote);
        }

        public void ResolveMissionVote()
        {
            if (Votes.Any(m => m.PlayerVote == false))
            {
                Points.Spies++;
            }
            else
            {
                Points.Resistance++;
            }
        }

        #endregion
    }
}