using BuildDivide.Core.Cards;
using BuildDivide.Core.Decks;
using Shouldly;

namespace BuildDivide.Test
{
	public class DeckTests
	{
		[Fact]
		public void ValidDeck_Test()
		{
			Deck deck = TestDeckService.CreateValidDeck();

			deck.Cards.Count.ShouldBe(40);
			deck.Validate().ShouldBe(true);
		}
		[Fact]
		public void BusterIsNot12_Test()
		{
			Deck deck = TestDeckService.CreateValidDeck();

			var card = new Unit("buster", new List<Cost>());
			card.Trigger = Trigger.BusterCard;
			deck.AddCard(card);

			deck.Validate().ShouldBe(false);
		}
		[Fact]
		public void TooManyShot_Test()
		{
			Deck deck = TestDeckService.CreateValidDeck();

			var card = new Unit("Shot", new List<Cost>());
			card.Trigger = Trigger.ShotCard;
			deck.AddCard(card);

			deck.Validate().ShouldBe(false);
		}
	}
}