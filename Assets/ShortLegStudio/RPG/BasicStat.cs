using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Text;
using ShortLegStudio.RPG.Characters; 	//TODO: Move ModifierString out of this namespace

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
			conditionalModifiers = new List<ConditionalStatModifier>();
		}

		public BasicStat(int baseValue) : this() {
			BaseValue = baseValue;
		}

		public void AddModifier(BasicStatModifier adjustment) {
			//Check for conditionalModifer
			//HACK: This is a hack job I think. Shouldn't check type to get proper behavior
			if (adjustment is ConditionalStatModifier) {
				AddModifier((ConditionalStatModifier)adjustment);
				return;
			}

			var oldBase = BaseValue;
			var oldTotal = TotalValue;
			_adjustments.Add (adjustment);
			Refresh (oldBase, oldTotal);
		}

		public void AddModifier(ConditionalStatModifier conditional) {
			conditionalModifiers.Add(conditional);
		}

		public void SetValue(int val) {
			var oldBase = BaseValue;
			var oldTotal = TotalValue;
			BaseValue = val;
			Refresh (oldBase, oldTotal);
		}

		public IEnumerable<string> GetConditions() {
			return conditionalModifiers.GroupBy(x => x.Condition).Select(x => x.Key);
		}

		public int GetConditionalScore(string cond) {
			var conditions = conditionalModifiers.Where(x => x.Condition == cond);
			return TotalValue + (int)conditions.Sum(x => x.Modifier);
		}

		public string ToString(string statName) {
			var sb = new StringBuilder();
			sb.AppendFormat("{0} {1}", statName, TotalValue.ToModifierString());

			var mods = GetConditions().Select(
				x => string.Format("{0} {1}", GetConditionalScore(x).ToModifierString(), x)
			);

			if (mods.Count() > 0) {
				sb.AppendFormat(" ({0})",
					string.Join(",", mods.ToArray())
				);
			}
			return sb.ToString();
		}

		public override string ToString() {
			return ToString("");
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


		//First Hack job approach to handling conditional modifiers
		private IList<ConditionalStatModifier> conditionalModifiers;

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

