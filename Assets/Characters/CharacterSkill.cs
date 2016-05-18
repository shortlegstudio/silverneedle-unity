using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSkill {
		public Skill Skill { get; private set; }
		private AbilityScore _baseScore;
		public int Score { get; private set; }
		public bool AbleToUse { get; private set; }
		public int Ranks { get; private set; }
		public bool ClassSkill { get; set; }
		public IList<SkillAdjustment> Adjustments { get; private set; }


		public CharacterSkill(Skill baseSkill, AbilityScore baseScore, bool isClassSkill) {
			Skill = baseSkill;
			_baseScore = baseScore;
			_baseScore.Modified += AbilityModified;
			ClassSkill = isClassSkill;
			Adjustments = new List<SkillAdjustment> ();
			CalculateScore ();
		}

		void AbilityModified (object sender, BasicStatModifiedEventArgs e) {
			CalculateScore ();
		}

		public int CalculateScore() {
			var val = 0;
			if (Skill.TrainingRequired && Ranks == 0) {
				val = int.MinValue;
				AbleToUse = false;
			} else {
				val += _baseScore.BaseModifier;
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
			get { return Skill.Name; }
		}

		public void AddAdjustment(SkillAdjustment adjustment) {
			if (adjustment.SkillName == Name) {
				Adjustments.Add (adjustment);
				CalculateScore ();
			}
		}


	}
}