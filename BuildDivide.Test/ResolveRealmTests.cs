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
    public class ResolveRealmTests
    {
        [Fact]
        public void ResolveRealm_Test()
        {
            //arrange
            var player1 = new MockPlayer(TestDeckService.CreateValidDeck());

            var resolveRealm = new ResolveRealm();

            //act
            resolveRealm.Push(new ResolveRealmStackModel()
            {
                Effect = new TestDrawCardEffect(),
                SourcePlayer = player1
            });

            resolveRealm.StackCount.ShouldBe(1);

            resolveRealm.Resolve();

            player1.Hand.Count.ShouldBe(1);

            resolveRealm.StackCount.ShouldBe(0);
        }
    }
}
