using System;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Characters;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Gateways {
	public class TraitYamlGateway : EntityGateway<Trait> {
		//Static Values
		const string TRAIT_DATA_FILE = "Data/traits.yml";
		private IList<Trait> _traits;

		public TraitYamlGateway() {
			LoadFromYaml(FileHelper.OpenYaml(TRAIT_DATA_FILE));
		}

		public TraitYamlGateway(YamlNodeWrapper yaml) {
			LoadFromYaml(yaml);
		}

		public System.Collections.Generic.IEnumerable<Trait> All() {
			return _traits;
		}

		private void LoadFromYaml(YamlNodeWrapper yaml) {
			_traits = new List<Trait> ();

			foreach (var traitNode in yaml.Children()) {
				var trait = new Trait ();
				trait.Name = traitNode.GetString ("name"); 
				ShortLog.Debug ("Loading Trait: " + trait.Name);
				trait.Description = traitNode.GetString ("description");

				//Get Any skill Modifiers if they exist
				var modifiers = traitNode.GetNodeOptional("modifiers");
				if (modifiers != null) {
					var mods = ParseStatModifiersYaml.ParseYaml(modifiers, string.Format("{0} (trait)", trait.Name));
					foreach (var m in mods) {
						trait.Modifiers.Add(m);
					}
				}

				_traits.Add (trait);
			}
		}
	}
}

