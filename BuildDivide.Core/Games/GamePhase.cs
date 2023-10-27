namespace BuildDivide.Core.Games
{
    public enum GamePhaseType
	{
		Stand,
		Draw,
		Main,
		Attack,
		End
	}

	public interface IGamePhase
	{
		public GamePhaseType Type { get; }

		public void Action(GameCore gameCore);
	}

    public class StandPhase : IGamePhase
    {
		public GamePhaseType Type => GamePhaseType.Stand;

        public void Action(GameCore gameCore)
        {
            //702-1
			gameCore.TurnPlayer.StandAll();

            //702-2
            //TODO: handle trigger when turn start

            //702-3
            //TODO: hanlde 1103 play window
        }
    }
    public class DrawPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.Draw;

        public void Action(GameCore gameCore)
        {
            throw new NotImplementedException();
        }
    }
    public class MainPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.Main;

        public void Action(GameCore gameCore)
        {
            throw new NotImplementedException();
        }
    }
    public class AttackPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.Attack;

        public void Action(GameCore gameCore)
        {
            throw new NotImplementedException();
        }
    }
    public class EndPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.End;

        public void Action(GameCore gameCore)
        {
            throw new NotImplementedException();
        }
    }
}