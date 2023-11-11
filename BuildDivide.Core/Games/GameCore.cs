using BuildDivide.Core.Cards;
using BuildDivide.Core.Decks;
using BuildDivide.Core.Windows;

namespace BuildDivide.Core.Games
{
	//TODO: make player abstract class
    public abstract class Player
	{
		public Deck Deck { get; set; }
		public Card Territory { get; set; }
		public List<Card> Hand { get; set; }
		public List<Card> YellowZone { get; set; }
		public List<Card> RedZone { get; set; }
		public List<Card> EnergyZone { get; set; }
		public List<Card> Field { get; set; }
		public List<Card> GraveYard { get; set; }

        public Player(Deck deck)
        {
            this.Deck = deck;
            this.Hand = new List<Card>();
			this.YellowZone = new List<Card>();
			this.RedZone = new List<Card>();
			this.EnergyZone = new List<Card>();
            this.Field = new List<Card>();
			this.GraveYard = new List<Card>();
			this.Territory = deck.Territory;
        }

        public void RedrawCard()
		{
			var tempZone = Hand.ToList();

			Hand.Clear();
			DrawCards(5);
			Deck.AddCards(tempZone);

            Deck.Shuffle();
        }

		public Card DrawCard()
		{
			Card card;

			if (Deck.Cards.Any())
			{
				//Draw from deck
				card = TransferCard(Deck.Cards, Hand);
			}
			else
			{
				//Draw from life
				if (YellowZone.Any())
				{
					card = TransferCard(YellowZone, Hand);
				}
				else if (RedZone.Any())
				{
					card = TransferCard(RedZone, Hand);
				}
				else
				{
					//TODO:handle player lose, throw custom exception indicate player lose?
					throw new Exception("No cards are avalible");
				}
			}

			return card;
		}

		public List<Card> DrawCards(int count)
		{
			var cardList = new List<Card>();

			for (int i = 0; i < count; i++)
			{
				cardList.Add(DrawCard());
			}

			return cardList;
		}

		public Card TransferCard(List<Card> source, List<Card> target)
		{
			if (!source.Any())
			{
				throw new Exception("No cards are avalible");
			}

			Card card = source[0];
			target.Add(card);
			source.RemoveAt(0);

			return card;
		}
        public Card TransferCard(Stack<Card> source, List<Card> target)
        {
            if (!source.Any())
            {
                throw new Exception("No cards are avalible");
            }

            Card card = source.Pop();
            target.Add(card);

            return card;
        }

        public List<Card> TransferCards(List<Card> source, List<Card> target, int transferCount)
		{
			var cardList = new List<Card>();

			for (var i = 0; i < transferCount; i++)
			{
				cardList.Add(TransferCard(source, target));
			}

			return cardList;
		}

        public void StandAll()
		{
            foreach (var energy in EnergyZone)
            {
				energy.IsSatnding = true;
            }

			foreach(var unit in Field)
			{
				unit.IsSatnding = true;
            }

			Territory.IsSatnding = true;
        }

		public void Trigger(DamagePacket damagePacket)
		{
			Card triggeredCard;

            //1003-2
            while (!damagePacket.IsDmageFinished)
            {
                if (YellowZone.Count > 0)
                {
                    triggeredCard = YellowZone.Last();
					YellowZone.Remove(triggeredCard);
                }
                else if (RedZone.Count > 0)
                {
                    triggeredCard = RedZone.Last();
                    RedZone.Remove(triggeredCard);
                }
                else
                {
                    //1003-2a Player lost TODO
                    throw new NotImplementedException();
                }

				damagePacket.HitDealed++;

                if (triggeredCard.Trigger == Cards.Trigger.BusterCard)
                {
					//1003-2c
					damagePacket.Hit++;
                }
                else if (triggeredCard.Trigger == Cards.Trigger.ShotCard)
                {
					//1003-2d
                    //TODO: handle shot card cost if avalible (511-2)
					//TODO: handle effect of card
                    //TODO: 1003.2.d-i
                }

                //1003-2b & 1003-2c & 1003-2d
                GraveYard.Add(triggeredCard);
            }
		}

		public virtual Task HandleEvent(GameEvent ev)
		{
			return Task.CompletedTask;
		}

		public abstract Task<PlayWindowActionType> ResolvePlayWindowActionAsync(PlayWindow playWindow);
	}

	
	public interface GameState
	{
		GamePhaseType Phase { get; set; }

		Task DoActionAsync();
	}


	public class GameCore
	{
        public Player Player1 { get; }
        public Player Player2 { get; }

		public Player TurnPlayer { get; private set; }

        public Player NonTurnPlayer => TurnPlayer == Player1 ? Player2 : Player1;

        public GameCore(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
        

		public int Turn { get; private set; }

        public void Preparation()
        {
            Turn = 1;

			DecideFirstPlayer();

            Player1.Deck.Shuffle();
            Player2.Deck.Shuffle();

            PlaceTerritoryCard(Player1);
            PlaceTerritoryCard(Player2);


            //TODO: handle redraw decision somehow
            DrawInitialHand(Player1, () => false);
            DrawInitialHand(Player2, () => false);

            PlaceLife(Player1);
            PlaceLife(Player2);

            PlaceEnergy(Player1);
            PlaceEnergy(Player2);

			this.CurrentGamePhase = new StandPhase();
            CurrentGamePhase.Action(this);
        }

        public void EnterNextPhase()
        {
            ChangeToNextPhase();
			CurrentGamePhase.Action(this);

            void ChangeToNextPhase()
            {
                switch (CurrentGamePhase.Type)
                {
                    case GamePhaseType.Stand:
						CurrentGamePhase = new StandPhase();
                        break;

                    case GamePhaseType.Draw:
                        CurrentGamePhase = new DrawPhase();
                        break;

                    case GamePhaseType.Main:
                        CurrentGamePhase = new MainPhase();
                        break;

                    case GamePhaseType.Attack:
                        CurrentGamePhase = new AttackPhase();
                        break;

                    case GamePhaseType.End:
						CurrentGamePhase = new EndPhase();
                        break;

                    default:
                        throw new InvalidOperationException("Invalid game phase.");
                }
            }
        }

        public IGamePhase CurrentGamePhase { get; private set; }

        #region prepartion



        private void PlaceTerritoryCard(Player Board)
		{
			// Place the territory card on the back side (character side) in the "territory" position
		}

		private void DecideFirstPlayer()
		{
			//TODO: decide first player, using provider from outside
            TurnPlayer = Player1;
        }

		private void DrawInitialHand(Player player, Func<bool> shouldRedraw)
		{
			// If you don't like the card you drew, you can "redraw your hand" only once.
			player.DrawCards(5);

			// Check if player wants to redraw hand
			if (shouldRedraw())
			{
				player.RedrawCard();
			}
		}

		private void PlaceLife(Player player)
		{
			// From the top of the deck, place 5 cards in the yellow zone
			// and 5 cards in the red zone, for a total of 10 cards face down in the life zone.

			player.TransferCards(player.Deck.Cards, player.YellowZone, 5);
			player.TransferCards(player.Deck.Cards, player.RedZone, 5);
		}

		private void PlaceEnergy(Player player)
		{
			// From the top of the deck, place two cards face up in the energy zone.
			player.TransferCards(player.Deck.Cards, player.EnergyZone, 2);
		}

		#endregion prepartion
    }
}