using BuildDivide.Core.Cards;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Test
{
    public class DamagePacketTests
    {
        [Fact]
        public void DamagePlayer_Test()
        {
            //Arrange
            var player = new MockPlayer(TestDeckService.CreateValidDeck());
            
            var damagePacket = new DamagePacket();
            
            var initDamageCount= damagePacket.Hit;


            damagePacket.Hit = 1;

            player.TransferCards(player.Deck.Cards.Where(x => x.Trigger != Trigger.BusterCard).ToList(), player.YellowZone, 5);
            player.TransferCards(player.Deck.Cards.Where(x => x.Trigger != Trigger.BusterCard).ToList(), player.RedZone, 5);

            //Act
            player.Trigger(damagePacket);

            //Assert
            {
                //1002-2a
                initDamageCount.ShouldBe(0);

                player.GraveYard.Count.ShouldBe(1);
                player.YellowZone.Count.ShouldBe(4);
                player.RedZone.Count.ShouldBe(5);
            }
        }
    }
}
