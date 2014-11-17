using ResistanceApp.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{
    public class ResistanceGame
    {
        List<Player> Players;
        List<Vote> Votes;
        int MaxPlayers;
        public GameStatus Status { get; private set; }
        
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

        public int NumberOfVotes
        {
            get
            {
                try
                {
                    return Votes.Count();
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int Leader
        {
            get;
            private set;
        }

        public int ProposedMissionCount
        { get; private set; }
        #region Constructors

        public ResistanceGame() { }

        public ResistanceGame(int numPlayers)
        {
            MaxPlayers = numPlayers;
            Status = GameStatus.Starting;
        }

        #endregion

        #region Methods

        public Player Join(string playername)
        {
            if(Status != GameStatus.Starting)
            {
                throw new InvalidOperationException("Sorry, this game is in progress and cannot be joined.");
            }
            if (MaxPlayers == NumberOfPlayers)
            {
                throw new InvalidOperationException("Sorry, this game is full.");
            }
            Player player = new Player(playername);
            AddPlayer(player);
            return player;
        }

        protected void AddPlayer(Player player)
        {
            if (Players == null)
                Players = new List<Player>();
            if (Players.Any(m => m.Name == player.Name))
            {
                throw new InvalidOperationException("Sorry, that name has been taken. Please select another.");
            }
            Players.Add(player);
        }
        public void Play() {
            Status = GameStatus.Active;
        }

        protected void ChooseLeader()
        {
            if (Leader == (NumberOfPlayers - 1))
            {
                Leader = 0;
            }
            else
                Leader++;
        }

        
        
        public void CallForVote()
        {
            Votes = new List<Vote>();
            Status = GameStatus.Voting;
        }

        public void Vote(Player player, bool vote)
        {
            if (Status != GameStatus.Voting)
            {
                throw new InvalidOperationException("Voting has not begun!");
            }
            if (!HasPlayerVoted(player.Name))
            {
                AddVote(new Vote(player, vote));
            }
            else {
                var playerVote = Votes.FirstOrDefault(m => m.Player.Name == player.Name);
                playerVote.PlayerVote = vote;
            }
            if (Votes.Count() == NumberOfPlayers)
            {
                ResolveVotes();
            }
                
        }

        protected void AddVote(Vote vote)
        {
            Votes.Add(vote);
        }
        protected bool HasPlayerVoted(string playerName)
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

        protected void ResolveVotes()
        {
            int majority = NumberOfPlayers % 2 == 1 ? (NumberOfPlayers+1)/ 2 : NumberOfPlayers / 2;
            bool doesMajorityApprove = Votes.Count(m => m.PlayerVote) > majority;
            if (doesMajorityApprove)
            {
                StartMission();
            }
            
        }

        protected void StartMission()
        {
            Status = GameStatus.OnMission;
        }
        #endregion

    }
}
