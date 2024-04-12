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
        private static MockPlayer player1;
        private static MockPlayer player2;
        private static GameCore gameCore;

        public GameCoreTests()
        {
            player1 = new MockPlayer(TestDeckService.CreateValidDeck());
            player2 = new MockPlayer(TestDeckService.CreateValidDeck());
            gameCore = new GameCore(player1, player2);

            gameCore.GameEvent.Subscribe(async x =>
            {
                //simulate process time
                await Task.Delay(10);
                
            });
        }

        [Fact]
        public async Task PreparationTest()
        {
            //arrange

            //action
            await gameCore.PreparationAsync();

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
        public async Task StandPhaseTest()
        {
            //arrange

            //action
            await gameCore.PreparationAsync();

            gameCore.ProcessCurrentPhase();

            //assert
            gameCore.CurrentGamePhase.Type.ShouldBe(GamePhaseType.Stand);

            gameCore.TurnPlayer.EnergyZone.ShouldAllBe(x => x.IsSatnding);

            gameCore.TurnPlayer.Field.All(x => x.IsSatnding).ShouldBe(true);

            gameCore.TurnPlayer.Territory.IsSatnding.ShouldBe(true);
        }

        [Fact]
        public async Task DrawPhaseTest()
        {
            //arrange

            //action
            await gameCore.PreparationAsync();
            gameCore.ProcessCurrentPhase();

            gameCore.EnterNextPhase();
            gameCore.ProcessCurrentPhase();

            //assert
            gameCore.CurrentGamePhase.Type.ShouldBe(GamePhaseType.Draw);

            gameCore.TurnPlayer.Hand.Count.ShouldBe(5);
        }

        [Fact]
        public async Task MainPhaseTest()
        {
            //arrange

            //action
            await gameCore.PreparationAsync();
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
        public async Task AttackPhaseTest()
        {
            //arrange

            //action
            await gameCore.PreparationAsync();
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
        public async Task EndPhaseTest()
        {
            //arrange

            //action
            await gameCore.PreparationAsync();
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
