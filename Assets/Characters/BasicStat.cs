using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.RPG.Characters {
	public abstract class BasicStat {
		public int BaseValue { get; private set; }
		public int TotalValue { get { return BaseValue + SumAdjustments; } }
		public IList<BasicStatAdjustment> Adjustments { get; private set; }
		public int SumAdjustments { get; private set; }

		public BasicStat () {
			Adjustments = new List<BasicStatAdjustment> ();
		}

		public BasicStat(int baseValue) : this() {
			BaseValue = baseValue;
		}

		public void AddAdjustment(BasicStatAdjustment adjustment) {
			Adjustments.Add (adjustment);
			Refresh ();
		}

		public void SetValue(int val) {
			BaseValue = val;
			Refresh ();
		}

		protected virtual void Refresh() {
			SumAdjustments = Adjustments.Sum (x => x.Modifier);
		}
	}

	public class BasicStatAdjustment {
		public int Modifier { get; set; }
		public string Reason { get; set; }
	}

}

