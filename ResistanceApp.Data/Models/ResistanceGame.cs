﻿using ResistanceApp.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceApp.Data.Models
{


    public class ResistanceGame
    {
        public Guid GameID
        { get; set; }

        public DateTime DateStarted
        { get; set; }

        GameContext Context;
        public int NumberOfPlayers
        {
            get
            {
                return Context.NumberOfPlayers;
            }
        }

        public int NumberOfSpies
        {
            get
            {
                return Context.NumberOfSpies;
            }
        }
        public int NumberOfVotes
        {
            get
            {
                return Context.VoteCount();
            }
        }

        public int Round
        { get { return Context.Round; } }

        public bool? GetVote(string playerName)
        {
            return Context.GetVote(playerName);
        }

        public GameStatus Status
        {
            get { return Context.Status; }
        }
        public Player Leader
        {
            get { return Context.GetLeader; }
        }

        public Score Points
        {
            get { return Context.Points; }
        }
        #region Constructors

        public ResistanceGame(GameState state) {
            Context =  new GameContext(state,5);
        }

        public ResistanceGame(int numPlayers)
        {
            Context = new GameContext(new SetupState(), numPlayers);
        }

        #endregion

        #region Properties

        #endregion
        #region Methods

        public void Play()
        {
            Context.Start();
        }

        public Player Join(string playername)
        {
            Player player =  Context.Join(playername);
            player.GameID = GameID;
            return player;
        }

        public void Vote(string playername, bool vote)
        {
            Context.Vote(playername, vote);
        }

        public void PickMissionMembers(string sendingPlayer, string[] missionMembers)
        {
            Context.PickMissionMembers(sendingPlayer, missionMembers);
        }

        public IEnumerable<Player> ViewPlayers(string sendingPlayer)
        {
            return Context.GetPlayers(sendingPlayer);
        }

        public List<Player> ShowSpies(string sendingPlayer)
        {
            var players = Context.GetPlayers(sendingPlayer);
            return players.Where(m => m.PlayerRole == Role.Spy).ToList();
        }
       
       #endregion

    }
}
