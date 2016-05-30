using System;
using System.Collections.Generic;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Repositories {
	public class LanguageYamlGateway : EntityGateway<Language> {
		const string LANGUAGE_YAML_FILE = "Data/languages.yml";
		private IList<Language> Languages;

		public LanguageYamlGateway () {
			LoadFromYaml (FileHelper.OpenYaml (LANGUAGE_YAML_FILE));
		}

		public LanguageYamlGateway(YamlNodeWrapper yaml) {
			LoadFromYaml (yaml);
		}

		public System.Collections.Generic.IEnumerable<Language> All () {
			return Languages;
		}

		private void LoadFromYaml(YamlNodeWrapper yaml) {
			Languages = new List<Language> ();
			foreach (var n in yaml.Children()) {
				Languages.Add (new Language (
					n.GetString ("name"),
					n.GetString ("description")
				));
			}
		}
	}
}

