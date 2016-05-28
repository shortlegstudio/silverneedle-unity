using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class Prerequisites : List<Prerequisite> {
		string[] PREREQ_KEYS = { "ability", "race", "feat", "classfeature", "proficiency" , 
			"casterlevel", "baseattackbonus", "classlevel", "skillranks" };
		public Prerequisites() {
		}

		public Prerequisites(YamlNodeWrapper yaml) {
			ParseYaml (yaml);
		}

		public bool Qualified(CharacterSheet sheet) {
			if (this.Count == 0)
				return true;
			
			return this.All (x => x.Qualified (sheet));
		}

		private void ParseYaml(YamlNodeWrapper yaml) {
			foreach (var prereq in yaml.Children()) {
				Prerequisite newreq = null;

				foreach (var key in PREREQ_KEYS) {
					var val = prereq.GetStringOptional(key);
					if (val != null) {
						switch(key) {
						case "ability":
							newreq = new AbilityPrerequisite (val);
							break;
						case "baseattackbonus":
							newreq = new BaseAttackBonusPrerequisite (val);
							break;
						case "casterlevel":
							newreq = new CasterLevelPrerequisite (val);
							break;
						case "classfeature":
							newreq = new ClassFeaturePrerequisite(val);
							break;
						case "classlevel":
							newreq = new ClassLevelPrerequisite(val);
							break;
						case "feat":
							newreq = new FeatPrerequisite (val);
							break;
						case "proficiency":
							newreq = new ProficiencyPrerequisite (val);
							break;
						case "race":
							newreq = new RacePrerequisite (val);
							break;
						case "skillranks":
							newreq = new SkillRankPrerequisite (val);
							break;

						
						}
						break;
					}
				}
					
				if (newreq != null)
					Add(newreq);
			}
		}

	}

	public abstract class Prerequisite {
		public abstract bool Qualified (CharacterSheet character);
	}

	public class AbilityPrerequisite : Prerequisite {
		public AbilityScoreTypes Ability { get; set; }
		public int Minimum { get; set; }

		public AbilityPrerequisite(string req) {
			var vals = req.Split (' ');
			Ability = AbilityScore.GetType (vals [0]);
			Minimum = int.Parse (vals [1]);
		}

		public override bool Qualified (CharacterSheet character)
		{
			return character.Abilities.GetScore(Ability) >= Minimum;
		}
	}

	public class RacePrerequisite : Prerequisite {
		public string Race { get; set; }

		public RacePrerequisite(string value) {
			Race = value;
		}

		public override bool Qualified (CharacterSheet character)
		{
			return false;
		}
	}

	public class FeatPrerequisite : Prerequisite {
		public string Feat { get; set; }

		public FeatPrerequisite(string value) {
			Feat = value;
		}

		public override bool Qualified (CharacterSheet character)
		{
			return false;
		}
	}

	public class ClassFeaturePrerequisite : Prerequisite {
		public string ClassFeature { get; set; }

		public ClassFeaturePrerequisite(string value) {
			ClassFeature = value;
		}

		public override bool Qualified (CharacterSheet character) {
			return false;
		}
	}

	public class ProficiencyPrerequisite : Prerequisite {
		public string Proficiency { get; set; }

		public ProficiencyPrerequisite(string value) {
			Proficiency = value;
		}

		public override bool Qualified (CharacterSheet character) {
			return false;
		}
	}

	public class CasterLevelPrerequisite : Prerequisite {
		public string CasterLevel { get; set; }

		public CasterLevelPrerequisite(string value) {
			CasterLevel = value;
		}

		public override bool Qualified (CharacterSheet character) {
			return false;
		}
	}

	public class BaseAttackBonusPrerequisite : Prerequisite {
		public string AttackBonus { get; set; }

		public BaseAttackBonusPrerequisite(string value) {
			AttackBonus = value;
		}

		public override bool Qualified (CharacterSheet character) {
			return false;
		}
	}

	public class ClassLevelPrerequisite: Prerequisite {
		public string Class { get; set; }
		public int Minimum { get; set; }

		public ClassLevelPrerequisite(string req) {
			var vals = req.Split (' ');
			Class = vals [0];
			Minimum = int.Parse (vals [1]);
		}

		public override bool Qualified (CharacterSheet character)
		{
			return false;
		}
	}


	public class SkillRankPrerequisite : Prerequisite {
		public string SkillName { get; set; }
		public int Minimum { get; set; }

		public SkillRankPrerequisite(string req) {
			var vals = req.Split (' ');
			SkillName = vals [0];
			Minimum = int.Parse (vals [1]);
		}

		public override bool Qualified (CharacterSheet character)
		{
			return false;
		}
	}

}