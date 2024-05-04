using System.Collections.Generic;

namespace BuildDivide.Core.Cards
{
    public class UnknownCard : Card
	{
        public static Card Create => new UnknownCard();

        private UnknownCard() : base("Unknown", CardType.Unknown, new List<Cost>())
		{
        }
    }
}