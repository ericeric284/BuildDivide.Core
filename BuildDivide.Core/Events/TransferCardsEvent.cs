using BuildDivide.Core.Cards;
using System.Collections.Generic;

namespace BuildDivide.Core.Events
{
    public class TransferCardsEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.TransferCards;

        public List<Card> cards { get; }
        public CardPosition From { get; }
        public CardPosition To { get; }

        public TransferCardsEvent(List<Card> cards, CardPosition from, CardPosition to)
        {
            this.cards = cards;
            From = from;
            To = to;
        }


    }
}