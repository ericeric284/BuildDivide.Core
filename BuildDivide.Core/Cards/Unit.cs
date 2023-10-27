namespace BuildDivide.Core.Cards
{
    public class Unit : Card
    {
        public int PlayCost { get; set; }
        public PlayingTiming PlayTiming { get; set; }
        public int Power { get; set; }
        public int Hits { get; set; }
        public string? Titles { get; set; }
        public string? FlavorText { get; set; }
        public List<Attribute> Attributes { get; set; }

        public int ReceviedDamage { get; set; }

		public Unit(string cardName, List<Cost> costs) : base(cardName, CardType.Unit, costs)
		{
        }
    }
}