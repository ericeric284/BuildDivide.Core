using BuildDivide.Core.Cards;
using BuildDivide.Core.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Test
{
    public class EffectTests
    {
        [Fact]
        public void DrawCardEffect_Test()
        {
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var drawCardEffect = new TestDrawCardEffect();

            drawCardEffect.Excecute(player1);

            Assert.Equal(1, player1.Hand.Count);
        }
    }

    public class TestDrawCardEffect : Effect
    {
        public override EffectType Type => EffectType.SingleUse;

        public override void Excecute(Player player)
        {
            player.DrawCard();
        }
    }   
}
