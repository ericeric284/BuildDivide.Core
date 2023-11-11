using BuildDivide.Core.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Core.Cards
{
    public class ResolveRealm
    {
        private Stack<ResolveRealmStackModel> stackModel = new Stack<ResolveRealmStackModel>();

        public int StackCount => stackModel.Count;

        public void Push(ResolveRealmStackModel model)
        {
            stackModel.Push(model);
        }

        public void Resolve()
        {
            //TODO:
            //513-1
            //513-2
            //513-3
            var model = stackModel.Pop();

            model.Effect.Excecute(model.SourcePlayer);
        }
    }
    public class ResolveRealmStackModel
    {
        public Effect Effect { get; set; }
        public Player SourcePlayer { get; set; }
    }
}
