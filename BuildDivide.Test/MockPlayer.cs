using BuildDivide.Core.Decks;
using BuildDivide.Core.Games;
using BuildDivide.Core.Windows;
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
    }
}
