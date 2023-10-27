namespace BuildDivide.Core.Cards
{
    public class Territory: Card
	{
        public Territory(string cardName, List<Cost> costs) : base(cardName, CardType.Terrirtory, costs)
        {
            
        }
        public bool IsOpen { get; set; }

    }
}