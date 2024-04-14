namespace BuildDivide.Core.Events
{
    public class DrawEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.Draw;

        public int DrawCount { get; set; }
    }
}