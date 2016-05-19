using System;

namespace ShortLegStudio.RPG.Characters {
	public class OffenseStats
	{
		public BasicStat BaseAttackBonus { get; private set; }
		private AbilityScores AbilityScores { get; set; }

		public OffenseStats (AbilityScores scores) {
			BaseAttackBonus = new BasicStat ();
			AbilityScores = scores;
		}

		public int MeleeAttackBonus() {
			return BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Strength);
		}

		public int RangeAttackBonus() {
			return BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Dexterity);
		}
	}
}

