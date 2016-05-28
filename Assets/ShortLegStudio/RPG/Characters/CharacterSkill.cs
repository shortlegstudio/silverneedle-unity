using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSkill {
		public Skill Skill { get; private set; }
		private AbilityScore _baseScore;
		public bool AbleToUse { get; private set; }
		public int Ranks { get; private set; }
		public bool ClassSkill { get; set; }
		public IList<SkillAdjustment> Adjustments { get; private set; }


		public CharacterSkill(Skill baseSkill, AbilityScore baseScore, bool isClassSkill) {
			Skill = baseSkill;
			_baseScore = baseScore;
			ClassSkill = isClassSkill;
			Adjustments = new List<SkillAdjustment> ();
			UpdateAbleToUse ();
		}

		public int Score() {

			//I can't do this...
			if (!AbleToUse)
				return int.MinValue;
			
			var val = 0;

			//Base Ability Score
			val += _baseScore.BaseModifier;

			//Number of Ranks
			val += Ranks;

			//Class Skill
			if (Ranks > 0 && ClassSkill)
				val += 3;

			//Other Bonuses
			val += TotalAdjustments ();
			return val;
		}

		public int TotalAdjustments() {
			return (int)Adjustments.Sum(x => x.Modifier);
		}

		public void AddRank() {
			Ranks++;
			UpdateAbleToUse ();
		}

		public string Name { 
			get { return Skill.Name; }
		}

		public void AddAdjustment(SkillAdjustment adjustment) {
			if (adjustment.SkillName == Name) {
				Adjustments.Add (adjustment);
			}
		}

		private void UpdateAbleToUse() {
			AbleToUse = !Skill.TrainingRequired || Ranks > 0;
		}

	}
}