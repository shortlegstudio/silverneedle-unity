using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ShortLegStudio.RPG {
	public class BasicStat {
		public int BaseValue { get; protected set; }
		public int TotalValue { get { return BaseValue + SumBasicModifiers; } }
		public IEnumerable<BasicStatModifier> Modifiers { get { return _adjustments; } }
		public IEnumerable<ConditionalStatModifier> ConditionalModifiers { 
			get { return _conditionals; }
		}
		public int SumBasicModifiers { 
			get { 
				return (int)_adjustments.Sum (x => x.Modifier);	
			}
		}
		public int SumConditionalModifiers(string condition) {
			return _conditionals.Where(x => x.Condition == condition).Sum(x => x.Modifier);
		}

		public int ConditionalScore(string conditionName) {
			return TotalValue + SumConditionalModifiers(conditionName);
		}

		public event EventHandler<BasicStatModifiedEventArgs> Modified;

		private IList<BasicStatModifier> _adjustments;
		private IList<ConditionalStatModifier> _conditionals;

		public BasicStat () {
			_adjustments = new List<BasicStatModifier> ();
			_conditionals = new List<ConditionalStatModifier>();
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

		public void AddModifier(ConditionalStatModifier mod) {
			_conditionals.Add(mod);
			Refresh(BaseValue, TotalValue);
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

