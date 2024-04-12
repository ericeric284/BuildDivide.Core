using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildDivide.Core.Games
{
    public interface IGameEvent
    {
        public GameEventType EventType { get; }
    }

    public enum GameEventType
    {
        Shuffle,
        Draw,
        Play,
        Discard,
        Energy,
        Attack,
        Damage,
        Heal,
        EndTurn,
        EndGame
    }

    public class ShuffleEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.Shuffle;
    }

}