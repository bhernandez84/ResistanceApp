using NUnit.Framework;
using ResistanceApp.Data.Models;
using ResistanceApp.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;


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
            var player2 = Game.Join("ben2");

            Game.Play();
            Game.PickMissionMembers("ben", new string[] { "ben", "ben2" });
            
            Game.Vote(currentPlayer.Name, true);
            Game.Vote(currentPlayer.Name, false);
            Assert.AreEqual(Game.NumberOfVotes, 1);
            Assert.AreEqual(Game.GetVote("ben"), false);
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayersCanOnlyVoteWhenGameIsInVotingStatus()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var currentPlayer = Game.Join("ben");
            Game.Vote(currentPlayer.Name, true);
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

            Game.PickMissionMembers("ben", new string[] { "ben", "ben2" });
            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, true);
            Game.Vote(player3.Name, true);
            Game.Vote(player4.Name, true);
            Game.Vote(player5.Name, true);

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

            var leader = Game.Leader;
            Game.PickMissionMembers("ben", new string[] { "ben", "ben2" });            

            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, false);
            Game.Vote(player3.Name, false);
            Game.Vote(player4.Name, false);
            Game.Vote(player5.Name, true);

            Assert.AreEqual(Game.Status, GameStatus.MissionTeamSelection);
            Assert.AreEqual(Game.Leader, player2);
            Assert.AreNotEqual(Game.Leader, leader);
        }

        [Test]
        public void IfNoPlayerSubmitsAnApprovedTeamGameIsOver()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.PickMissionMembers("ben", new string[] { "ben", "ben2" });

            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, false);
            Game.Vote(player3.Name, false);
            Game.Vote(player4.Name, false);
            Game.Vote(player5.Name, true);
            Game.PickMissionMembers("ben2", new string[] { "ben", "ben2" });

            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, false);
            Game.Vote(player3.Name, false);
            Game.Vote(player4.Name, false);
            Game.Vote(player5.Name, true);
            Game.PickMissionMembers("ben3", new string[] { "ben", "ben2" });

            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, false);
            Game.Vote(player3.Name, false);
            Game.Vote(player4.Name, false);
            Game.Vote(player5.Name, true);
            Game.PickMissionMembers("ben4", new string[] { "ben", "ben2" });

            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, false);
            Game.Vote(player3.Name, false);
            Game.Vote(player4.Name, false);
            Game.Vote(player5.Name, true);
            Game.PickMissionMembers("ben5", new string[] { "ben", "ben2" });

            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, false);
            Game.Vote(player3.Name, false);
            Game.Vote(player4.Name, false);
            Game.Vote(player5.Name, true);


            Assert.AreEqual(Game.Status, GameStatus.Complete);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OnlyLeaderCanNominateMissionMembers()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.PickMissionMembers("ben2", new string[] { "ben", "ben2" });
            
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoDuplicatePeopleAllowedOnMissions()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.PickMissionMembers("ben1", new string[] { "ben1", "ben1" });
        }

        [Test]
        public void SpiesAreAssignedCorrectNumber()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Assert.AreEqual(Game.NumberOfSpies, 2);
        }
        [Test]
        public void SpiesCanSeeOtherSpies()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");    
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            List<Player> AllPlayers = new System.Collections.Generic.List<Player>();
            AllPlayers.Add(player1);
            AllPlayers.Add(player2);
            AllPlayers.Add(player3);
            AllPlayers.Add(player4);
            AllPlayers.Add(player5);

            Player firstSpy = AllPlayers.FirstOrDefault(m => m.PlayerRole == Role.Spy);
            Player firstResistance = AllPlayers.FirstOrDefault(m => m.PlayerRole == Role.Resistance);

            var spies = Game.ShowSpies(firstSpy.Name);
            var spiesForResistance = Game.ShowSpies(firstResistance.Name);

            Assert.AreNotEqual(spies, spiesForResistance);
            Assert.Greater(spies.Count(), spiesForResistance.Count());
            Assert.AreEqual(spies.Count(), 2);
           
            
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OnlyPlayersOnMissionCanVoteDuringMissionRound()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.PickMissionMembers("ben", new string[] { "ben", "ben2" });
            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, true);
            Game.Vote(player3.Name, true);
            Game.Vote(player4.Name, true);
            Game.Vote(player5.Name, true);

            //test voting while onmission state
            Game.Vote(player3.Name, true);
        }

        [Test]
        public void RoundEndsWhenAllMissionPlayersHaveVoted()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.PickMissionMembers("ben", new string[] { "ben", "ben2" });
            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, true);
            Game.Vote(player3.Name, true);
            Game.Vote(player4.Name, true);
            Game.Vote(player5.Name, true);


            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, true);

            Assert.AreEqual(Game.Status, GameStatus.Complete);

        }

        
    }
}
