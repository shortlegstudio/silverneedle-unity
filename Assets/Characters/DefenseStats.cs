using UnityEngine;
using System.Collections;

namespace ShortLegStudio.RPG.Characters {
	
	public class DefenseStats  {
		const int BASE_ARMOR_CLASS = 10;

		private AbilityScores Abilities;
		private SizeStats Size;

		public DefenseStats(AbilityScores abilityScores, SizeStats size) {
			Abilities = abilityScores;	
			Size = size;
		}

		public int ArmorClass() {
			return BASE_ARMOR_CLASS 
				+ Abilities.GetModifier (AbilityScoreTypes.Dexterity)
				+ Size.SizeModifier;
		}

		public int TouchArmorClass() {
			return BASE_ARMOR_CLASS 
				+ Abilities.GetModifier (AbilityScoreTypes.Dexterity)
				+ Size.SizeModifier;
		}

		public int FlatFootedArmorClass() {
			return BASE_ARMOR_CLASS
				+ Size.SizeModifier;
		}
	}
}
