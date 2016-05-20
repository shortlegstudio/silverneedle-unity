using UnityEngine;
using System.Collections;

namespace ShortLegStudio.RPG.Characters {
	
	public class DefenseStats  {
		const int BASE_ARMOR_CLASS = 10;

		private AbilityScores Abilities;

		public DefenseStats(AbilityScores abilityScores) {
			Abilities = abilityScores;	
		}

		public int ArmorClass() {
			return BASE_ARMOR_CLASS +
				Abilities.GetModifier (AbilityScoreTypes.Dexterity);
		}

		public int TouchArmorClass() {
			return BASE_ARMOR_CLASS +
				Abilities.GetModifier (AbilityScoreTypes.Dexterity);
		}

		public int FlatFootedArmorClass() {
			return BASE_ARMOR_CLASS;
		}
	}
}
