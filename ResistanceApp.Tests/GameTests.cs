using NUnit.Framework;
using ResistanceApp.Data.Models;
using ResistanceApp.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using ResistanceApp.Data.Helpers;


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
            Game.Join("ben3");
            Game.Join("ben4");
            Game.Join("ben5");

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

        //Loophole to this rule is mission 4 with more than 8 players.  2 fails are required then
        [Test]
        public void SpiesWinRoundWhenAnyMissionVoteIsFalse()
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


            Game.Vote(player1.Name, false);
            Game.Vote(player2.Name, true);

            Assert.AreEqual(Game.Points.Spies, 1);
        }

        [Test]
        public void ResistanceWinsRoundWhenAllVotesAreTrue()
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

            Assert.AreEqual(Game.Points.Resistance, 1);
        }

       [Test]
        public void NewRoundStartsAfterVotesAreIn()
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

            int currentRound = Game.Round;

            Game.Vote(player1.Name, true);
            Game.Vote(player2.Name, true);

            Assert.AreEqual(Game.Round, currentRound+1);
        }

        [Test]
        public void NumberOfMembersNeededForMissionIsAssigned()
        {
            //5 player game
            int round51 = PlayerHelpers.GetNumberOfPlayersForMission(5, 0);
            int round52 = PlayerHelpers.GetNumberOfPlayersForMission(5, 1);
            int round53 = PlayerHelpers.GetNumberOfPlayersForMission(5, 2);
            int round54 = PlayerHelpers.GetNumberOfPlayersForMission(5, 3);
            int round55 = PlayerHelpers.GetNumberOfPlayersForMission(5, 4);

            Assert.AreEqual(round51, 2);
            Assert.AreEqual(round52, 3);
            Assert.AreEqual(round53, 2);
            Assert.AreEqual(round54, 3);
            Assert.AreEqual(round55, 3);

            //6 player game
            int round61 = PlayerHelpers.GetNumberOfPlayersForMission(6, 0);
            int round62 = PlayerHelpers.GetNumberOfPlayersForMission(6, 1);
            int round63 = PlayerHelpers.GetNumberOfPlayersForMission(6, 2);
            int round64 = PlayerHelpers.GetNumberOfPlayersForMission(6, 3);
            int round65 = PlayerHelpers.GetNumberOfPlayersForMission(6, 4);

            Assert.AreEqual(round61, 2);
            Assert.AreEqual(round62, 3);
            Assert.AreEqual(round63, 3);
            Assert.AreEqual(round64, 3);
            Assert.AreEqual(round65, 4);

            //7 player game
            int round71 = PlayerHelpers.GetNumberOfPlayersForMission(7, 0);
            int round72 = PlayerHelpers.GetNumberOfPlayersForMission(7, 1);
            int round73 = PlayerHelpers.GetNumberOfPlayersForMission(7, 2);
            int round74 = PlayerHelpers.GetNumberOfPlayersForMission(7, 3);
            int round75 = PlayerHelpers.GetNumberOfPlayersForMission(7, 4);

            Assert.AreEqual(round71, 2);
            Assert.AreEqual(round72, 3);
            Assert.AreEqual(round73, 3);
            Assert.AreEqual(round74, 4);
            Assert.AreEqual(round75, 4);

            //8 player game
            int round81 = PlayerHelpers.GetNumberOfPlayersForMission(8, 0);
            int round82 = PlayerHelpers.GetNumberOfPlayersForMission(8, 1);
            int round83 = PlayerHelpers.GetNumberOfPlayersForMission(8, 2);
            int round84 = PlayerHelpers.GetNumberOfPlayersForMission(8, 3);
            int round85 = PlayerHelpers.GetNumberOfPlayersForMission(8, 4);

            Assert.AreEqual(round81, 3);
            Assert.AreEqual(round82, 4);
            Assert.AreEqual(round83, 4);
            Assert.AreEqual(round84, 5);
            Assert.AreEqual(round85, 5);

            //9 player game
            int round91 = PlayerHelpers.GetNumberOfPlayersForMission(9, 0);
            int round92 = PlayerHelpers.GetNumberOfPlayersForMission(9, 1);
            int round93 = PlayerHelpers.GetNumberOfPlayersForMission(9, 2);
            int round94 = PlayerHelpers.GetNumberOfPlayersForMission(9, 3);
            int round95 = PlayerHelpers.GetNumberOfPlayersForMission(9, 4);

            Assert.AreEqual(round91, 3);
            Assert.AreEqual(round92, 4);
            Assert.AreEqual(round93, 4);
            Assert.AreEqual(round94, 5);
            Assert.AreEqual(round95, 5);

            //10 player game
            int round101 = PlayerHelpers.GetNumberOfPlayersForMission(10, 0);
            int round102 = PlayerHelpers.GetNumberOfPlayersForMission(10, 1);
            int round103 = PlayerHelpers.GetNumberOfPlayersForMission(10, 2);
            int round104 = PlayerHelpers.GetNumberOfPlayersForMission(10, 3);
            int round105 = PlayerHelpers.GetNumberOfPlayersForMission(10, 4);

            Assert.AreEqual(round101, 3);
            Assert.AreEqual(round102, 4);
            Assert.AreEqual(round103, 4);
            Assert.AreEqual(round104, 5);
            Assert.AreEqual(round105, 5);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IncorrectMisionMembersThrowsError()
        {
            ResistanceGame Game = new ResistanceGame(5);
            var player1 = Game.Join("ben");
            var player2 = Game.Join("ben2");
            var player3 = Game.Join("ben3");
            var player4 = Game.Join("ben4");
            var player5 = Game.Join("ben5");

            Game.PickMissionMembers("ben", new string[] { "ben" });
        }
    }
}
