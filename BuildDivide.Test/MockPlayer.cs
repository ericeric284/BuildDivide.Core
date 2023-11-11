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
        public MockPlayer(Deck deck) : base(deck)
        {
        }

        public override Task<PlayWindowActionType> ResolvePlayWindowActionAsync(PlayWindow playWindow)
        {
            return Task.FromResult(PlayWindowActionType.Pass);   
        }
    }
}
