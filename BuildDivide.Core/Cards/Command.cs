namespace BuildDivide.Core.Cards
{
    public class Command : Card
    {
        public int PlayCost { get; set; }
		public Command(string cardName, List<Cost> costs) : base(cardName, CardType.Command, costs)
		{
        }
    }
}