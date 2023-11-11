using BuildDivide.Core.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Core.Cards
{
    public class DamagePacket
    {
        public int Hit { get; set; }
        public int HitDealed { get; set; }

        public bool IsDmageFinished => HitDealed >= Hit;
    }
}
