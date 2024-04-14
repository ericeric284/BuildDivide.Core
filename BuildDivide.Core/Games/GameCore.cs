using BuildDivide.Core.Events;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace BuildDivide.Core.Games
{


    public class GameCore
	{
        private readonly InitialPlayerDecider initialPlayerDecider;

        public Player Player1 { get; }
        public Player Player2 { get; }

		public Player TurnPlayer { get; private set; }

        public Player NonTurnPlayer => TurnPlayer == Player1 ? Player2 : Player1;

        public GameCore(Player player1, Player player2, InitialPlayerDecider initialPlayerDecider)
        {
            Player1 = player1;
            Player2 = player2;
            this.initialPlayerDecider = initialPlayerDecider;
        }
        

		public int Turn { get; private set; }

        public async Task PreparationAsync()
        {
            Turn = 1;

            await DecideFirstPlayerAsync();

            await ShuffleDeck();
            
            await DrawInitialHandAsync();

            await PlaceLifeAsync();

            await PlaceEnergyAsync(Player1);
            await PlaceEnergyAsync(Player2);

            this.CurrentGamePhase = new StandPhase();
            await CurrentGamePhase.ActionAsync(this);


            async Task DecideFirstPlayerAsync()
            {
                TurnPlayer = initialPlayerDecider.DecideFirstPlayer(Player1, Player2);

                var t1 = Player1.NotifyFirstPlayerDecidedAsync(new FirstPlayerDecidedEvent()
                {
                    IsNotifedPlayerFirst = TurnPlayer == Player1
                });

                var t2 = Player2.NotifyFirstPlayerDecidedAsync(new FirstPlayerDecidedEvent()
                {
                    IsNotifedPlayerFirst = TurnPlayer == Player2
                });

                await Task.WhenAll(t1, t2);
            }

            async Task ShuffleDeck()
            {
                Player1.Deck.Shuffle();
                Player2.Deck.Shuffle();

                //Notify shuffle event
                {
                    var t1 = Player1.NotifyShuffleEventAsync(new ShuffleEvent());
                    var t2 = Player2.NotifyShuffleEventAsync(new ShuffleEvent());
                    await Task.WhenAll(t1, t2);
                }
            }

            async Task DrawInitialHandAsync()
            {
                {
                    var p1DrawCards = Player1.DrawCards(5);
                    var p2DrawCards = Player2.DrawCards(5);

                    //Notify draw event
                    var t1 = Player1.NotifyInitialDrawAsync(new InitialDrawEvent(p1DrawCards));
                    var t2 = Player2.NotifyInitialDrawAsync(new InitialDrawEvent(p2DrawCards));
                    await Task.WhenAll(t1, t2);

                    bool p1RequestRedraw = t1.Result;
                    bool p2RequestRedraw = t2.Result;

                    await HandleRedraw(p1RequestRedraw, p2RequestRedraw);
                }

                async Task HandleRedraw(bool p1RequestRedraw, bool p2RequestRedraw)
                {
                    var list = new List<Task>();
                    if (p1RequestRedraw)
                    {
                        var redrawnCards = Player1.RedrawCard();
                        list.Add(Player1.NotifyRedrawResult(new InitialRedrawResultEvent(redrawnCards)));
                    }

                    if (p2RequestRedraw)
                    {
                        var redrawnCards = Player1.RedrawCard();
                        list.Add(Player2.NotifyRedrawResult(new InitialRedrawResultEvent(redrawnCards)));
                    }

                    await Task.WhenAll(list);
                }
            }
        }

        public void EnterNextPhase()
        {
            switch (CurrentGamePhase.Type)
            {
                case GamePhaseType.Stand:
                    CurrentGamePhase = new DrawPhase();
                    break;

                case GamePhaseType.Draw:
                    CurrentGamePhase = new MainPhase();
                    break;

                case GamePhaseType.Main:
                    CurrentGamePhase = new AttackPhase();
                    break;

                case GamePhaseType.Attack:
                    CurrentGamePhase = new EndPhase();
                    break;

                case GamePhaseType.End:
                    CurrentGamePhase = new DrawPhase();
                    break;

                default:
                    throw new InvalidOperationException("Invalid game phase.");
            }
        }

		public void ProcessCurrentPhase()
		{
            CurrentGamePhase.ActionAsync(this);
        }

        public IGamePhase CurrentGamePhase { get; private set; }

        #region prepartion

		private async Task PlaceLifeAsync()
		{
            // From the top of the deck, place 5 cards in the yellow zone
            // and 5 cards in the red zone, for a total of 10 cards face down in the life zone.

            Player1.TransferCards(Player1.Deck.Cards, Player1.YellowZone, 5);
            Player1.TransferCards(Player1.Deck.Cards, Player1.RedZone, 5);

            Player2.TransferCards(Player2.Deck.Cards, Player2.YellowZone, 5);
            Player2.TransferCards(Player2.Deck.Cards, Player2.RedZone, 5);

            var t1 = Player1.NotifyLifePlaced(new InitialPlaceLifeEvent());
            var t2 = Player2.NotifyLifePlaced(new InitialPlaceLifeEvent());
            await Task.WhenAll(t1, t2);
        }

		private async Task PlaceEnergyAsync(Player player)
		{
            // From the top of the deck, place two cards face up in the energy zone.
            var cards = player.TransferCards(player.Deck.Cards, player.EnergyZone, 2);

            var t1 = Player1.NotifyTransferCardsAsync(new TransferCardsEvent(cards, Cards.CardPosition.Deck, Cards.CardPosition.EnergyZone));
            var t2 = Player2.NotifyTransferCardsAsync(new TransferCardsEvent(cards, Cards.CardPosition.Deck, Cards.CardPosition.EnergyZone));
            await Task.WhenAll(t1, t2);
        }

		#endregion prepartion
    }

    public interface InitialPlayerDecider
    {
        Player DecideFirstPlayer(Player player1, Player player2);
    }

    public class CoinFlipPlayerDecider : InitialPlayerDecider
    {
        public Player DecideFirstPlayer(Player player1, Player player2)
        {
            return new Random().Next(2) == 0 ? player1 : player2;
        }
    }
}