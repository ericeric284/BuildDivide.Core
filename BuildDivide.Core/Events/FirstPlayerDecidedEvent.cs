namespace BuildDivide.Core.Events
{
    public class FirstPlayerDecidedEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.PlayerDecided;

        public bool IsNotifedPlayerFirst { get; set; }
    }
}