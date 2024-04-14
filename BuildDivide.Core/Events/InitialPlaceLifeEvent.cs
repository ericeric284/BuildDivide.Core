namespace BuildDivide.Core.Events
{
    public class InitialPlaceLifeEvent: IGameEvent
    {
        public GameEventType EventType => GameEventType.InitialPlaceLife;
    }
}