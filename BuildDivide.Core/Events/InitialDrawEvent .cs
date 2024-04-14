using BuildDivide.Core.Cards;
using System.Collections.Generic;

namespace BuildDivide.Core.Events
{
    public class InitialDrawEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.InitialDraw;

        public InitialDrawEvent(List<Card> redrawCards)
        {
            RedrawCards = redrawCards;
        }

        public List<Card> RedrawCards { get; set; }
    }
}