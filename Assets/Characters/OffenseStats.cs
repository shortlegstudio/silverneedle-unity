using System;

namespace ShortLegStudio.RPG.Characters {
	public class OffenseStats
	{
		public BasicStat BaseAttackBonus { get; private set; }
		private AbilityScores AbilityScores { get; set; }
		private SizeStats Size { get; set; }

		public OffenseStats (AbilityScores scores, SizeStats size) {
			BaseAttackBonus = new BasicStat ();
			AbilityScores = scores;
			Size = size;
		}

		public int MeleeAttackBonus() {
			return BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Strength) + Size.SizeModifier;
		}

		public int RangeAttackBonus() {
			return BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Dexterity) + Size.SizeModifier;
		}

		public int CombatManueverBonus() {
			
			return BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Strength) - Size.SizeModifier;
		}

		public int CombatManueverDefense() {
			return 10 + BaseAttackBonus.TotalValue
				+ AbilityScores.GetModifier (AbilityScoreTypes.Strength)
				+ AbilityScores.GetModifier (AbilityScoreTypes.Dexterity)
				- Size.SizeModifier;
		}
	}
}

