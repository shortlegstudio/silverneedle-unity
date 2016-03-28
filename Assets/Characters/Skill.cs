using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class Skill {
		public enum SkillTypes {
			Acrobatics,
			Bluff,
			Climb,
			Diplomacy,
			DisableDevice,
			Heal,
			KnowledgeArcana
		}

		//Static Values
		const string SKILL_DATA_FILE = "Data/skills.yml";
		static IList<Skill> _Skills;

		public string Name { get; set; }
		public AbilityScoreTypes Ability { get; set; }
		public bool Trained { get; set; }

		public static IList<Skill> LoadFromYaml(YamlNodeWrapper yaml) {
			var skills = new List<Skill> ();

			foreach (var skillNode in yaml.Children()) {
				var skill = new Skill ();
				skill.Name = skillNode.GetValue ("name"); 
				skill.Ability = (AbilityScoreTypes) System.Enum.Parse(
					typeof(AbilityScoreTypes), 
					skillNode.GetValue ("ability"), 
					true);
				skill.Trained = skillNode.GetValue ("trained") == "yes";
				Debug.Log (skill.ToString ());
				skills.Add (skill);
			}

			return skills;
		}

		public static IList<Skill> GetSkills() {
			if (_Skills == null) {
				var yaml = FileHelper.OpenYaml (SKILL_DATA_FILE);
				_Skills = LoadFromYaml (yaml);
			}

			return _Skills;
		}

		public override string ToString ()
		{
			return string.Format ("[Skill: Name={0}, Ability={1}, Trained={2}]", Name, Ability, Trained);
		}
	}
}