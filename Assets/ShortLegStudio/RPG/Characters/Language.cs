using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

namespace ShortLegStudio.RPG.Characters {
	public class Language {
		
		public string Name { get; set; }
		public string Description { get; set; }

		public Language () { }
		public Language(string name, string desc) {
			Name = name;
			Description = desc;
		}


		// Static Methods
		private static IList<Language> _languages;
		const string LANGUAGE_YAML_FILE = "Data/languages.yml";
		public static IList<Language> LoadFromYaml(YamlNodeWrapper yaml) {
			var list = new List<Language> ();
			foreach (var n in yaml.Children()) {
				list.Add (new Language (
					n.GetString ("name"),
					n.GetString ("description")
				));
			}
			return list;
		}

		public static IList<Language> GetLanguages() {
			if (_languages == null || _languages.Count == 0) {
				_languages = LoadFromYaml (FileHelper.OpenYaml (LANGUAGE_YAML_FILE));
			}
			return _languages;
		}
	}
}

