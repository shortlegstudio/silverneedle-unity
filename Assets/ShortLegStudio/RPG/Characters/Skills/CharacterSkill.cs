using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace ShortLegStudio.RPG.Characters.Skills {
	public class CharacterSkill : BasicStat {
		public Skill Skill { get; private set; }
		private AbilityScore _baseScore;
		public bool AbleToUse { 
			get {
				return !Skill.TrainingRequired || Ranks > 0;
			} 
		}
		public int Ranks { get { return BaseValue; } }
		public bool ClassSkill { get; set; }

		public CharacterSkill(Skill baseSkill, AbilityScore baseScore, bool isClassSkill) {
			Skill = baseSkill;
			_baseScore = baseScore;
			ClassSkill = isClassSkill;
		}

		public int Score() {
			//I can't do this...
			if (!AbleToUse)
				return int.MinValue;
			
			var val = Ranks;

			//Base Ability Score
			val += _baseScore.BaseModifier;

			//Class Skill
			if (Ranks > 0 && ClassSkill)
				val += 3;

			//Other Bonuses
			val += SumBasicModifiers;
			return val;
		}

		public void AddRank() {
			BaseValue++;
			//Total Hack job
			OnModified(BaseValue - 1, TotalValue - 1);
		}

		public string Name { 
			get { return Skill.Name; }
		}

	}
}