using BuildDivide.Core.Cards;
using System.Collections.Generic;

namespace BuildDivide.Core.Events
{
    public class InitialDrawEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.InitialDraw;

        public InitialDrawEvent(List<Card> initialDrawCards)
        {
            InitialDrawCards = initialDrawCards;
        }

        public List<Card> InitialDrawCards { get; set; }
    }
}