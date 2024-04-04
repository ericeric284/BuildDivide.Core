using BuildDivide.Core.Cards;
using BuildDivide.Core.Games;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Test
{
    public class GameCoreTests
    {

        [Fact]
        public void PreparationTest()
        {
            //arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var player2 = new MockPlayer(TestDeckService.CreateValidDeck());

            var gameCore = new GameCore(player1, player2);

            //action
            gameCore.Preparation();

            //assert
            gameCore.Player1.YellowZone.Count.ShouldBe(5);
            gameCore.Player1.RedZone.Count.ShouldBe(5);
            gameCore.Player1.EnergyZone.Count.ShouldBe(2);
            gameCore.Player1.Hand.Count.ShouldBe(5);

            gameCore.Player2.YellowZone.Count.ShouldBe(5);
            gameCore.Player2.RedZone.Count.ShouldBe(5);
            gameCore.Player2.EnergyZone.Count.ShouldBe(2);
            gameCore.Player2.Hand.Count.ShouldBe(5);
        }

        [Fact]
        public void StandPhaseTest()
        {
            //arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var player2 = new MockPlayer(TestDeckService.CreateValidDeck());

            var gameCore = new GameCore(player1, player2);

            //action
            gameCore.Preparation();

            gameCore.ProcessCurrentPhase();

            //assert
            gameCore.CurrentGamePhase.Type.ShouldBe(GamePhaseType.Stand);

            gameCore.TurnPlayer.EnergyZone.ShouldAllBe(x => x.IsSatnding);

            gameCore.TurnPlayer.Field.All(x => x.IsSatnding).ShouldBe(true);

            gameCore.TurnPlayer.Territory.IsSatnding.ShouldBe(true);
        }

        [Fact]
        public void DrawPhaseTest()
        {
            //arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var player2 = new MockPlayer(TestDeckService.CreateValidDeck());

            var gameCore = new GameCore(player1, player2);

            //action
            gameCore.Preparation();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            //assert
            gameCore.CurrentGamePhase.Type.ShouldBe(GamePhaseType.Draw);

            gameCore.TurnPlayer.Hand.Count.ShouldBe(5);
        }

        [Fact]
        public void MainPhaseTest()
        {
            //arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var player2 = new MockPlayer(TestDeckService.CreateValidDeck());

            var gameCore = new GameCore(player1, player2);

            //action
            gameCore.Preparation();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            //assert
            gameCore.CurrentGamePhase.Type.ShouldBe(GamePhaseType.Main);

            gameCore.TurnPlayer.Hand.Count.ShouldBe(5);
        }

        [Fact]
        public void AttackPhaseTest()
        {
            //arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var player2 = new MockPlayer(TestDeckService.CreateValidDeck());

            var gameCore = new GameCore(player1, player2);

            //action
            gameCore.Preparation();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            //assert
            gameCore.CurrentGamePhase.Type.ShouldBe(GamePhaseType.Attack);
        }

        [Fact]
        public void EndPhaseTest()
        {
            //arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var player2 = new MockPlayer(TestDeckService.CreateValidDeck());

            var gameCore = new GameCore(player1, player2);

            //action
            gameCore.Preparation();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            //assert
            gameCore.CurrentGamePhase.Type.ShouldBe(GamePhaseType.End);
        }
    }
}
