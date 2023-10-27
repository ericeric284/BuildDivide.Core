using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Core.Cards
{
	public struct Cost
	{
		public Cost(Color color, int costAmount)
		{
			Color = color;
			this.CostAmount = costAmount;
		}

		public Color Color { get; set; }
		public int CostAmount { get; set; }
	}
}
