using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class Trait {
		//Static Values
		const string TRAIT_DATA_FILE = "Data/traits.yml";
		static IList<Trait> _Traits;

		public string Name { get; set; }
		public string Description { get; set; }

		public static IList<Trait> LoadFromYaml(YamlNodeWrapper yaml) {
			var traits = new List<Trait> ();

			foreach (var traitNode in yaml.Children()) {
				var trait = new Trait ();
				trait.Name = traitNode.GetString ("name"); 
				trait.Description = traitNode.GetString ("description");
				traits.Add (trait);
			}

			return traits;
		}

		public static IList<Trait> GetTraits() {
			if (_Traits == null) {
				var yaml = FileHelper.OpenYaml (TRAIT_DATA_FILE);
				_Traits = LoadFromYaml (yaml);
			}

			return _Traits;
		}
	}


}