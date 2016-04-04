using UnityEngine;
using System.Collections;

namespace ShortLegStudio.RPG.Characters {
	public class CharacterSkill {
		private Skill skill;
		private CharacterSheet character;
		public int Score { get; private set; }
		public bool AbleToUse { get; private set; }

		public CharacterSkill(Skill baseSkill, CharacterSheet charSheet) {
			skill = baseSkill;
			character = charSheet;
			CalculateScore ();
		}

		public int CalculateScore() {
			var val = 0;
			if (skill.TrainingRequired) {
				val = int.MinValue;
				AbleToUse = false;
			} else {
				val += character.GetAbilityModifier (skill.Ability);
				AbleToUse = true;
			}

			Score = val;

			return Score;
		}
	}
}