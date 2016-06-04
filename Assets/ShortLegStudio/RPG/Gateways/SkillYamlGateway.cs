using System;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Enchilada;
using System.Collections;

namespace ShortLegStudio.RPG.Gateways {
	public class SkillYamlGateway : EntityGateway<Skill>
	{
		const string SKILL_DATA_FILE = "Data/skills.yml";
		IList<Skill> Skills; 

		public SkillYamlGateway() {
			LoadFromYaml (FileHelper.OpenYaml (SKILL_DATA_FILE));
		}

		public SkillYamlGateway (YamlNodeWrapper yaml) { 
			LoadFromYaml (yaml);
		}

		public IEnumerable<Skill> All() {
			return Skills;
		}

		private void LoadFromYaml(YamlNodeWrapper yaml) {
			Skills = new List<Skill> ();

			foreach (var skillNode in yaml.Children()) {
				var skill = new Skill (
					skillNode.GetString ("name"),
					(AbilityScoreTypes) System.Enum.Parse(
						typeof(AbilityScoreTypes), 
						skillNode.GetString ("ability"), 
						true),
					skillNode.GetString ("trained") == "yes"
				);
				skill.Description = skillNode.GetString ("description");
				Skills.Add (skill);
			}

		}
	}
}

