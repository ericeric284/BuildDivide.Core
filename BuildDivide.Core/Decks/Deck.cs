using BuildDivide.Core.Cards;

namespace BuildDivide.Core.Decks
{
	public class Deck
	{
		public Card Territory { get; set; }
        public List<Card> Cards { get; } = new List<Card>();
        public int BusterCount => Cards.Where(x => x.Trigger == Trigger.BusterCard).Count();
		public int ShotCount => Cards.Where(x => x.Trigger == Trigger.ShotCard).Count();


		public void AddCard(Card card)
		{
			Cards.Add(card);
		}

        public void AddCards(List<Card> cards)
        {
            Cards.AddRange(cards);
        }

        public void Shuffle()
        {
            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int k = new Random().Next(n + 1);
                Card card = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = card;
            }
        }


        public bool Validate()
		{
			if (Cards.Count < 40 || Cards.Count > 50)
			{
				return false;
			}
			if (BusterCount != 12 || ShotCount > 12)
			{
				return false;
			}

			foreach (Card card in Cards)
			{
				int count = Cards.Count(c => c.CardName == card.CardName);
				if (count > 4)
				{
					return false;
				}
			}
			return true;
		}
	}
}