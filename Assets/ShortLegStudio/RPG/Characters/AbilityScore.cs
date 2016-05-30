using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	public class AbilityScore : BasicStat {
		public AbilityScoreTypes Name { get; set; }
		public int BaseModifier {
			get {
				return CalculateModifier (BaseValue);
			}
		}
		public int TotalModifier {
			get {
				return CalculateModifier (TotalValue);
			}
		}

		public AbilityScore() : base() { } 
		public AbilityScore(AbilityScoreTypes type, int val) : base(val) {
			Name = type;
		}

		public override string ToString () {
			return string.Format ("[AbilityScore: Name={0}, Adjustments={1}, BaseValue={2}, BaseModifier={3}, TotalValue={4}, TotalModifier={5}, SumAdjustments={6}]", Name, Adjustments, BaseValue, BaseModifier, TotalValue, TotalModifier, SumAdjustments);
		}

		public static AbilityScoreTypes GetType(string name) {
			return (AbilityScoreTypes)System.Enum.Parse (typeof(AbilityScoreTypes), name, true);
		}

		public static int CalculateModifier(int val) {
			return val / 2 - 5;
		}
	}

	public enum AbilityScoreTypes {
		Strength,
		Dexterity,
		Constitution,
		Intelligence,
		Wisdom,
		Charisma
	}

	public class AbilityScoreAdjustment : BasicStatAdjustment {
		public bool RacialChose;
		public AbilityScoreTypes ability;
	}
}