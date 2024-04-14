namespace BuildDivide.Core.Events
{
    public class InitialPlaceEnergyEvent : IGameEvent
    {
        public GameEventType EventType => GameEventType.InitialPlaceLife;
    }
}