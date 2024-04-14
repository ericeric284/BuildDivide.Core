namespace BuildDivide.Core.Events
{
    public class ShuffleEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.Shuffle;
    }
}