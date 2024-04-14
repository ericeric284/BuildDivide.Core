using BuildDivide.Core.Decks;
using BuildDivide.Core.Events;
using BuildDivide.Core.Games;
using BuildDivide.Core.Windows;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Test
{
    internal class MockPlayer : Player
    {
        TaskCompletionSource<PlayWindowActionType> PlayWindowTsc { get; set; }

        public MockPlayer(Deck deck) : base(deck)
        {
        }

        public override Task<PlayWindowActionType> ResolvePlayWindowActionAsync(PlayWindow playWindow)
        {
            if (PlayWindowTsc != null)
            {
                throw new InvalidOperationException("PlayWindow Task already existed");
            }

            PlayWindowTsc = new TaskCompletionSource<PlayWindowActionType>();

            return PlayWindowTsc.Task;
        }

        public void Pass()
        {
            if (PlayWindowTsc == null)
            {
                throw new InvalidOperationException("No playWindow is existed");
            }

            PlayWindowTsc.SetResult(PlayWindowActionType.Pass);
        }

        public override Task NotifyFirstPlayerDecidedAsync(FirstPlayerDecidedEvent ev)
        {
            return Task.CompletedTask;
        }

        public override Task<bool> NotifyInitialDrawAsync(InitialDrawEvent ev)
        {
            return Task.FromResult(true);
        }

        public override Task NotifyRedrawResult(InitialRedrawResultEvent ev)
        {
            ev.RedrawCards.Count.ShouldBe(5);
            return Task.CompletedTask;
        }

        public override Task NotifyShuffleEventAsync(ShuffleEvent ev)
        {
            return Task.CompletedTask;
        }

        public override Task NotifyLifePlaced(InitialPlaceLifeEvent ev)
        {
            return Task.CompletedTask;
        }

        public override Task NotifyTransferCardsAsync(TransferCardsEvent ev)
        {
            return Task.CompletedTask;
        }
    }
}
