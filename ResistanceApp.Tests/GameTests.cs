using NUnit.Framework;
using ResistanceApp.Data.Models;
using ResistanceApp.Data.Models.Enum;
using System;



namespace ResistanceApp.Tests
{
    [TestFixture]
    public class GameTests
    {

        [Test]
        public void PlayerCanJoinOpenGame()
        {
            ResistanceGame Game = new ResistanceGame(5);
            Game.Join("ben");
            Assert.AreEqual(Game.NumberOfPlayers, 1);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayersCannotJoinFullGame()
        {
            ResistanceGame Game = new ResistanceGame(5);
            for (int i = 0; i < 7; i++)
            {
                Game.Join("player" + i);
            }
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayersCannotJoinActiveGame()
        {
            ResistanceGame Game = new ResistanceGame(5);
            Game.Join("ben");
            Game.Join("ben2");
            Game.Join("ben3");

            Game.Play();
            Game.Join("ben4");
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayersMustHaveUniqueName()
        {
            ResistanceGame Game = new ResistanceGame(5);
            Game.Join("ben");
            Game.Join("ben");
        }
        [Test]
        public void PlayersCanOnlyVoteOncePerMission()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var currentPlayer = Game.Join("ben");
            Game.Play();
            Game.CallForVote();
            Game.Vote(currentPlayer, true);
            Game.Vote(currentPlayer, false);
            Assert.AreEqual(Game.NumberOfVotes, 1);
            Assert.AreEqual(Game.GetVote("ben"), false);
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayersCanOnlyVoteWhenGameIsInVotingStatus()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var currentPlayer = Game.Join("ben");
            Game.Vote(currentPlayer, true);
        }

        [Test]
        public void WhenMajorityOfPlayersVoteYesMissionBegins()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.Play();
            Game.CallForVote();

            Game.Vote(player1, true);
            Game.Vote(player2, true);
            Game.Vote(player3, true);
            Game.Vote(player4, true);
            Game.Vote(player5, true);

            Assert.AreEqual(Game.Status, GameStatus.OnMission);

        }

        [Test]
        public void WhenMajorityOfPlayersVoteNoNewLeaderChosen()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.Play();
            Game.CallForVote();

            Game.Vote(player1, true);
            Game.Vote(player2, false);
            Game.Vote(player3, false);
            Game.Vote(player4, false);
            Game.Vote(player5, true);

            Assert.AreEqual(Game.Status, GameStatus.MissionTeamSelection);
            Assert.AreEqual(Game.Leader, player2);
        }

    }
}
