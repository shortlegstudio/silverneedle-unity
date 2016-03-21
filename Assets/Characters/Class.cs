using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters {
	public class Class {
		public string Name { get; set; }



		public static IList<Class> LoadFromYaml(YamlNodeWrapper yaml) {
			var classes = new List<Class> ();

			foreach (var node in yaml.Children()) {
				var cls = new Class ();
				cls.Name = node.GetValue ("name"); 

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