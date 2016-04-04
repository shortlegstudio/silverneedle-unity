using UnityEngine;
using System.Collections;

namespace ShortLegStudio.RPG.Characters {
	public class CharacterSkill {
		private Skill skill;
		private CharacterSheet character;
		public int Score { get; private set; }
		public bool AbleToUse { get; private set; }
		public int Ranks { get; private set; }
		public bool ClassSkill { get; set; }

		public CharacterSkill(Skill baseSkill, CharacterSheet charSheet) {
			skill = baseSkill;
			character = charSheet;
			CalculateScore ();
		}

		public int CalculateScore() {
			var val = 0;
			if (skill.TrainingRequired && Ranks == 0) {
				val = int.MinValue;
				AbleToUse = false;
			} else {
				val += character.GetAbilityModifier (skill.Ability);
				val += Ranks;
				if (Ranks > 0 && ClassSkill)
					val += 3;
				AbleToUse = true;
			}

			Score = val;
			return Score;
		}

		public void AddRank() {
			Ranks++;
			CalculateScore ();
		}

		public string Name { 
			get { return skill.Name; }
		}
	}
}