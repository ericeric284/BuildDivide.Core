using BuildDivide.Core.Cards;
using System;
using System.Threading.Tasks;

namespace BuildDivide.Core.Games
{
    public class PlayCardAction : IPlayerAction
    {
        public Card Card { get; }
        public PlayCardAction(Card card)
        {
            Card = card;
        }

        public Task DoActionAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsAvailible()
        {
			return true;
        }
    }
}