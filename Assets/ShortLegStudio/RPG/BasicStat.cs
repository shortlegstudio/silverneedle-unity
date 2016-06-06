using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ShortLegStudio.RPG {
	public class BasicStat {
		public int BaseValue { get; protected set; }
		public int TotalValue { get { return BaseValue + SumBasicModifiers; } }
		public IEnumerable<BasicStatModifier> Modifiers { get { return _adjustments; } }
		public int SumBasicModifiers { 
			get { 
				return (int)_adjustments.Sum (x => x.Modifier);	
			}
		}


		public event EventHandler<BasicStatModifiedEventArgs> Modified;

		private IList<BasicStatModifier> _adjustments;

		public BasicStat () {
			_adjustments = new List<BasicStatModifier> ();
		}

		public BasicStat(int baseValue) : this() {
			BaseValue = baseValue;
		}

		public void AddModifier(BasicStatModifier adjustment) {
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

