using System;

namespace ShortLegStudio.RPG.Characters {
	public class OffenseStats : IStatTracker {
		const string CMD_STAT_NAME = "CMD";
		const string CMB_STAT_NAME = "CMB";

		public BasicStat BaseAttackBonus { get; private set; }
		private AbilityScores AbilityScores { get; set; }
		private SizeStats Size { get; set; }
		private BasicStat CMD { get; set; }
		private BasicStat CMB { get; set; }


		public OffenseStats (AbilityScores scores, SizeStats size) {
			BaseAttackBonus = new BasicStat ();
			CMD = new BasicStat(10);
			CMB = new BasicStat();
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
			
			return CMB.TotalValue + BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Strength) - Size.SizeModifier;
		}

		public int CombatManueverDefense() {
			return CMD.TotalValue + BaseAttackBonus.TotalValue
				+ AbilityScores.GetModifier (AbilityScoreTypes.Strength)
				+ AbilityScores.GetModifier (AbilityScoreTypes.Dexterity)
				- Size.SizeModifier;
		}

		public void ProcessModifier(IModifiesStats statModifier) {
			foreach (var m in statModifier.Modifiers) {
				switch (m.StatName) {
					case CMD_STAT_NAME:
						CMD.AddModifier(m);
						break;
					case CMB_STAT_NAME:
						CMB.AddModifier(m);
						break;
				}
			}
		}
	}
}

