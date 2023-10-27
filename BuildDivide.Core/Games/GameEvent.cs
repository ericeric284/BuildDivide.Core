namespace BuildDivide.Core.Games
{
    public abstract class GameEvent
	{

	}

    public class ShuffleEvent : GameEvent
    {
        public ShuffleEvent(Player player)
        {
            Player = player;
        }
        public Player Player { get; set; }
    }

}