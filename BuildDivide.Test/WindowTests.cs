using BuildDivide.Core.Games;
using BuildDivide.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Test
{
    public class WindowTests
    {
        [Fact]
        public async Task PlayWindow_Test()
        {
            //Arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var player2 = new MockPlayer(TestDeckService.CreateValidDeck());

            var gameCore = new GameCore(player1, player2);

            gameCore.Preparation();

            var playWindow = new PlayWindow(gameCore);

            //Act
            var playWindowResolve = playWindow.ResolveAsync();

            player1.Pass();

            //Assert
            //TODO: validate pass is working
        }
    }
}
