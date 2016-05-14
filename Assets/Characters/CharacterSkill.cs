using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSkill {
		private Skill skill;
		private CharacterSheet character;
		public int Score { get; private set; }
		public bool AbleToUse { get; private set; }
		public int Ranks { get; private set; }
		public bool ClassSkill { get; set; }
		public IList<SkillAdjustment> Adjustments { get; private set; }


		public CharacterSkill(Skill baseSkill, CharacterSheet charSheet) {
			skill = baseSkill;
			character = charSheet;
			character.Modified += Character_Modified;

			RefreshCharacter ();

		}

		void Character_Modified (object sender, CharacterSheetEventArgs e) {
			RefreshCharacter ();
		}

		void RefreshCharacter() {
			Adjustments = new List<SkillAdjustment> ();
			ClassSkill = character.IsClassSkill(skill.Name);

			foreach (var adj in character.FindSkillAdjustments (skill.Name)) {
				AddAdjustment (adj);
			}

			CalculateScore ();
		}

		public int CalculateScore() {
			var val = 0;
			if (skill.TrainingRequired && Ranks == 0) {
				val = int.MinValue;
				AbleToUse = false;
			} else {
				val += character.Abilities.GetModifier (skill.Ability);
				val += Ranks;
				if (Ranks > 0 && ClassSkill)
					val += 3;
				AbleToUse = true;
			}
			val += TotalAdjustments ();

			Score = val;
			return Score;
		}

		public int TotalAdjustments() {
			return Adjustments.Sum(x => x.Modifier);
		}

		public void AddRank() {
			Ranks++;
			CalculateScore ();
		}

		public string Name { 
			get { return skill.Name; }
		}

		public void AddAdjustment(SkillAdjustment adjustment) {
			if (adjustment.SkillName == Name) {
				Adjustments.Add (adjustment);
				CalculateScore ();
			}
		}


	}
}