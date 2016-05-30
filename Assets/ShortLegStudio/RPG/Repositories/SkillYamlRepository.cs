using System;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Enchilada;
using System.Collections;

namespace ShortLegStudio.RPG.Repositories {
	public class SkillYamlRepository : EntityGateway<Skill>
	{
		const string SKILL_DATA_FILE = "Data/skills.yml";
		IList<Skill> Skills; 

		public SkillYamlRepository() {
			LoadFromYaml (FileHelper.OpenYaml (SKILL_DATA_FILE));
		}

		public SkillYamlRepository (YamlNodeWrapper yaml) { 
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

