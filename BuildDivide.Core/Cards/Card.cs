using System.Collections.Generic;

namespace BuildDivide.Core.Cards
{
    public abstract class Card
    {
        public string CardName { get; set; }
        public CardType CardType { get; set; }
        public List<Cost> Costs { get; set; }
		public Trigger Trigger { get; set; }

		public bool IsSatnding { get; set; }

		public Card(string cardName, CardType cardType, List<Cost> costs)
		{
			CardName = cardName;
			CardType = cardType;
			Costs = costs;
		}
	}
}