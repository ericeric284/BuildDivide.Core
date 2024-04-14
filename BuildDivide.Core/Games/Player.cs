using BuildDivide.Core.Cards;
using BuildDivide.Core.Decks;
using BuildDivide.Core.Events;
using BuildDivide.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<Card> RedrawCard()
		{
			var tempZone = Hand.ToList();

			Hand.Clear();
			var drawnCards = DrawCards(5);
			Deck.AddCards(tempZone);

            Deck.Shuffle();

            return drawnCards;
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

		//TODO: need to return from and to
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

		public virtual Task HandleEvent(IGameEvent ev)
		{
			return Task.CompletedTask;
		}
        public virtual Task<T> HandleEvent<T>(IGameEvent ev) where T: new()
		{
            return Task.FromResult(new T());
        }

        public abstract Task<PlayWindowActionType> ResolvePlayWindowActionAsync(PlayWindow playWindow);


		public void CheckAvalibleOptions()
		{

		}

        public abstract Task NotifyFirstPlayerDecidedAsync(FirstPlayerDecidedEvent ev);

		/// <summary>
		/// Player draws their initial hand
		/// </summary>
		/// <param name="ev"></param>
		/// <returns>Does player wants to redraw</returns>
        public abstract Task<bool> NotifyInitialDrawAsync(InitialDrawEvent ev);

		/// <summary>
		/// Player redraws their hand
		/// </summary>
		/// <param name="ev"></param>
		/// <returns></returns>
        public abstract Task NotifyRedrawResult(InitialRedrawResultEvent ev);
        public abstract Task NotifyShuffleEventAsync(ShuffleEvent ev);

		public abstract Task NotifyLifePlaced(InitialPlaceLifeEvent ev);
        public abstract Task NotifyTransferCardsAsync(TransferCardsEvent ev);
    }
}