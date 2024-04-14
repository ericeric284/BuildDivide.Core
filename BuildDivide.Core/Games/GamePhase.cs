﻿using BuildDivide.Core.Windows;
using System.Threading.Tasks;

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

		public Task ActionAsync(GameCore gameCore);
	}

    public class StandPhase : IGamePhase
    {
		public GamePhaseType Type => GamePhaseType.Stand;

        public Task ActionAsync(GameCore gameCore)
        {
            //702-1
			gameCore.TurnPlayer.StandAll();

            //702-2
            //TODO: handle start turn ability tirgger

            //702-3
            var window = new PlayWindow(gameCore);

            return Task.CompletedTask;
        }
    }
    public class DrawPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.Draw;

        public Task ActionAsync(GameCore gameCore)
        {
            //703-1
            //TODO: draw phase start trigger

            //703-2
            //TODO: handle 1103 play window

            //703-3
            if (gameCore.Turn != 1)
            {
                gameCore.TurnPlayer.DrawCard();
            }

            //703-4
            //TODO: handle 1103 play window

            return Task.CompletedTask;
        }
    }
    public class MainPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.Main;

        public Task ActionAsync(GameCore gameCore)
        {
            //704-1
            //TODO: start phase trigger

            //704-2
            //TODO: handle 1103 play window

            return Task.CompletedTask;
        }
    }
    public class AttackPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.Attack;

        public Task ActionAsync(GameCore gameCore)
        {
            //TODO: Attack phase

            return Task.CompletedTask;
        }
    }
    public class EndPhase : IGamePhase
    {
        public GamePhaseType Type => GamePhaseType.End;

        public Task ActionAsync(GameCore gameCore)
        {
            //706-1
            //TODO: turn end trigger & end phase start trigger

            //706-2
            //TODO: handle 1103 play window

            //706-3
            //TODO: reset all unit damage

            //706-4
            //TODO: Remove effect that works only in this turn

            //706-5
            //TODO: If player has too much card in hand, discard card

            //706-6
            //TODO: If there are multipel territory on territory zone and field, remove all territory except one

            //706-7
            //TODO: if there's 1303 or 1205-2,return to 706-2,otherwise end turn and start next turn

            return Task.CompletedTask;
        }
    }
}