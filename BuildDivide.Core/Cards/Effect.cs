using BuildDivide.Core.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Core.Cards
{
    /// <summary>
    /// 効果
    /// </summary>
    public abstract class Effect
    {
        public abstract EffectType Type { get; }
        public abstract void Excecute(Player activator);
    }

    public enum EffectType
    {
        /// <summary>
        /// 単発効果
        /// </summary>
        SingleUse,
        /// <summary>
        /// 持続効果
        /// </summary>
        Continuous,
        /// <summary>
        /// 置換効果
        /// </summary>
        Swapping
    }
}
