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

		public event EventHandler<BasicStatModifiedEventArgs> Modified;

		public BasicStat () {
			_adjustments = new List<BasicStatAdjustment> ();
		}

		public BasicStat(int baseValue) : this() {
			BaseValue = baseValue;
		}

		public void AddAdjustment(BasicStatAdjustment adjustment) {
			var oldBase = BaseValue;
			var oldTotal = TotalValue;
			_adjustments.Add (adjustment);
			Refresh (oldBase, oldTotal);
		}

		public void SetValue(int val) {
			var oldBase = BaseValue;
			var oldTotal = TotalValue;
			BaseValue = val;
			Refresh (oldBase, oldTotal);
		}

		protected virtual void Refresh(int oldBase, int oldTotal) {
			SumAdjustments = _adjustments.Sum (x => x.Modifier);
			OnModified (oldBase, oldTotal);
		}

		protected void OnModified(int oldBase, int oldTotal) {
			if (Modified != null) {
				Modified(this, 
					new BasicStatModifiedEventArgs(
						oldBase,
						BaseValue,
						oldTotal,
						TotalValue
				));
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

	public class BasicStatModifiedEventArgs : EventArgs {
		public int OldBaseValue;
		public int NewBaseValue;
		public int OldTotalValue;
		public int NewTotalValue;

		public BasicStatModifiedEventArgs(
			int oldBase,
			int newBase,
			int oldTotal,
			int newTotal) {
			OldBaseValue = oldBase;
			NewBaseValue = newBase;
			OldTotalValue = oldTotal;
			NewTotalValue = newTotal;
		}
	}

}

