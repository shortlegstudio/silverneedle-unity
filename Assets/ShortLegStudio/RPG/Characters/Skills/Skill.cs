using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Skills {
	public class Skill {
		//Static Values

		public string Name { get; set; }
		public AbilityScoreTypes Ability { get; set; }
		public bool TrainingRequired { get; set; }
		public string Description { get; set; }

		public Skill(string name, AbilityScoreTypes baseAbility, bool trainingRequired) {
			Name = name;
			Ability = baseAbility;
			TrainingRequired = trainingRequired;
		}

		public override string ToString ()
		{
			return string.Format ("[Skill: Name={0}, Ability={1}, Trained={2}]", Name, Ability, TrainingRequired);
		}
	}
}