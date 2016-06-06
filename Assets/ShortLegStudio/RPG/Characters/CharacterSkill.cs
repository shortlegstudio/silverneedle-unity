using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSkill {
		public Skill Skill { get; private set; }
		private AbilityScore _baseScore;
		private BasicStat _skillStats;

		public bool AbleToUse { 
			get {
				return !Skill.TrainingRequired || Ranks > 0;
			} 
		}
		public int Ranks { get { return _skillStats.BaseValue; } }
		public bool ClassSkill { get; set; }

		public CharacterSkill(Skill baseSkill, AbilityScore baseScore, bool isClassSkill) {
			Skill = baseSkill;
			_baseScore = baseScore;
			ClassSkill = isClassSkill;
			_skillStats = new BasicStat();
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
			val += _skillStats.SumBasicModifiers;
			return val;
		}


		public void AddRank() {
			_skillStats.SetValue(_skillStats.BaseValue+1);
		}

		public void AddModifier(SkillModifier mod) {
			_skillStats.AddModifier(mod);
		}


		public string Name { 
			get { return Skill.Name; }
		}

	}
}