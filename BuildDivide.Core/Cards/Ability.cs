using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Core.Cards
{
    /// <summary>
    /// 能力
    /// </summary>
    public abstract class Ability
    {
        public AbilityType Type { get; set; }

        public string? OriginalText { get; set; }

        /// <summary>
        /// Checks rules and costs, and returns whether this ability can be activated.
        /// </summary>
        /// <returns></returns>
        public abstract bool CanBeActivated();

        /// <summary>
        /// Executes this ability.
        /// </summary>
        public abstract void Excecute();
    }

    public enum AbilityType
    {
        /// <summary>
        /// 起動能力
        /// </summary>
        Activation,
        /// <summary>
        /// 自動能力
        /// </summary>
        Automatic,
        /// <summary>
        /// 永続能力
        /// </summary>
        Permanent
    }
}
