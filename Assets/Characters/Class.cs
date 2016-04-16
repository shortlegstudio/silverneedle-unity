using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
			//Craft, Profession, and Perform are special cases 
			//All skills in that group are considered class skills 
			// in this case 
			//
			// Truncate any skill names like 'Craft (Food)' -> Craft
			// Profession (Farmer) => Profession
			// etc...
			var pattern = "(\\(.*\\))";
			var skillName = Regex.Replace (name, pattern, string.Empty).Trim();
			return Skills.Any (x => x == skillName);
		}

		public void AddClassSkill(string name) {
			if (!IsClassSkill (name))
				Skills.Add (name);
			else
				Debug.Log ("Not adding class skill as it already is there: " + name);
		}

		public static IList<Class> LoadFromYaml(YamlNodeWrapper yaml) {
			var classes = new List<Class> ();

			foreach (var node in yaml.Children()) {
				var cls = new Class ();
				cls.Name = node.GetString ("name"); 
				Debug.Log ("Loading Class: " + cls.Name);
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