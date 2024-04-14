using BuildDivide.Core.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildDivide.Core.Events
{
    public interface IGameEvent
    {
        public GameEventType EventType { get; }
    }
}