using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResistanceApp.Data.Models;
using ResistanceApp.Data.Repository;

namespace ResistanceApp.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        protected GameRepository GameRepo = new GameRepository();

        [Test]
        public void JoiningAGameReturnsAReferenceToThatGame()
        {
            var player = GameRepo.JoinGame("nardo",null);

            var game = GameRepo.GetGame(player.GameID);

            Assert.NotNull(game);
            Assert.AreNotEqual(player.GameID, Guid.Empty);
        }
    }
}
