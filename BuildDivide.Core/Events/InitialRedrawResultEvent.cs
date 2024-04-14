using BuildDivide.Core.Cards;
using System.Collections.Generic;

namespace BuildDivide.Core.Events
{
    public class InitialRedrawResultEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.InitialRedraw;


        public InitialRedrawResultEvent(List<Card> redrawCards)
        {
            RedrawCards = redrawCards;
        }
        public List<Card> RedrawCards { get; set; } 
    }
}