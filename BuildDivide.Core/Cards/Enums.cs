using System;

namespace BuildDivide.Core.Cards
{
    public enum Attribute { Unit, Ace }
	public enum Trigger { None, BusterCard, ShotCard }
	public enum PlayingTiming { Normal, Quick }
	public enum CardType { Unit, Command ,Terrirtory}

	[Flags]
	public enum Color
	{
		ColorLess = 1,
		Black = 2,
		Blue = 4,
		White = 8,
		Red = 16,
		BlackBlue = Black | Blue,
		BlackRed = Black | Red,
		BlueWhite = Blue | White,
		WhiteRed = White | Red
	}
}