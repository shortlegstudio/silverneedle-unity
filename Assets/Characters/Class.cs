using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	public class Class {
		public string Name { get; set; }
		public IList<string> Skills { get; set; }
		public int SkillPoints { get; set; }
		public DiceSides HitDice { get; set; }

		public Class() {
			Skills = new List<string> ();
		}

		public bool IsClassSkill(string name) {
			return Skills.Any (x => x == name);
		}

		public void AddClassSkill(string name) {
			if (!IsClassSkill(name)) 
				Skills.Add (name);
		}

		public static IList<Class> LoadFromYaml(YamlNodeWrapper yaml) {
			var classes = new List<Class> ();

			foreach (var node in yaml.Children()) {
				var cls = new Class ();
				cls.Name = node.GetString ("name"); 
				cls.SkillPoints = node.GetInteger ("skillpoints");

				cls.HitDice = DiceStrings.ParseSides (node.GetString ("hitdice"));

				//Get the Skills for this class
				var skills = node.GetNode ("skills").Children();
				foreach (var s in skills) {
					cls.AddClassSkill (s.Value);
				}

				classes.Add (cls);
			}

			return classes;
		}

		public static IList<Class> GetClasses() {
			if (_Classes == null) {
				var yaml = FileHelper.OpenYaml (CLASS_DATA_FILE);
				_Classes = LoadFromYaml (yaml);
			}

			return _Classes;
		}

		//Static Values
		const string CLASS_DATA_FILE = "Data/classes.yml";
		static IList<Class> _Classes;

	}
}