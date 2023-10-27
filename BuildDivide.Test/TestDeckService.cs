using BuildDivide.Core.Cards;
using BuildDivide.Core.Decks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Test
{
    public static class TestDeckService
    {
        public static Deck CreateValidDeck()
        {
            var deck = new Deck();

            //Add 12 buster cards
            for (var i = 1; i <= 12; i++)
            {
                var unit = new Unit($"Test{i}", new List<Cost>());
                unit.Trigger = Trigger.BusterCard;
                deck.AddCard(unit);
            }

            //Add 12 shot cards
            for (var i = 13; i <= 24; i++)
            {
                var unit = new Unit($"Test{i}", new List<Cost>());
                unit.Trigger = Trigger.ShotCard;
                deck.AddCard(unit);
            }

            //Add 16 normal cards
            for (var i = 25; i <= 40; i++)
            {
                var unit = new Unit($"Test{i}", new List<Cost>());
                unit.Trigger = Trigger.None;
                deck.AddCard(unit);
            }

            deck.Territory = new Territory("TestTerritory", new List<Cost>()
            {
                new Cost()
                {
                    Color = Color.Black,
                    CostAmount = 5
                }
            });

            return deck;
        }
    }
}
