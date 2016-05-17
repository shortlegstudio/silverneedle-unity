using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.RPG.Characters {
	public class BasicStat {
		public int BaseValue { get; private set; }
		public int TotalValue { get { return BaseValue + SumAdjustments; } }
		public IEnumerable<BasicStatAdjustment> Adjustments { get { return _adjustments; } }
		public int SumAdjustments { get; private set; }

		private IList<BasicStatAdjustment> _adjustments;

		public event EventHandler<EventArgs> Modified;

		public BasicStat () {
			_adjustments = new List<BasicStatAdjustment> ();
		}

		public BasicStat(int baseValue) : this() {
			BaseValue = baseValue;
		}

		public void AddAdjustment(BasicStatAdjustment adjustment) {
			_adjustments.Add (adjustment);
			Refresh ();
		}

		public void SetValue(int val) {
			BaseValue = val;
			Refresh ();
		}

		protected virtual void Refresh() {
			SumAdjustments = _adjustments.Sum (x => x.Modifier);
			OnModified ();
		}

		protected void OnModified() {
			if (Modified != null) {
				Modified(this, new EventArgs());
			}
		}
	}

	public class BasicStatAdjustment {
		public int Modifier { get; set; }
		public string Reason { get; set; }

		public BasicStatAdjustment() { }

		public BasicStatAdjustment(int mod, string reas) {
			Modifier = mod;
			Reason = reas;
		}
	}

}

