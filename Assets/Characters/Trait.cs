﻿using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class Trait {
		//Static Values
		const string TRAIT_DATA_FILE = "Data/traits.yml";
		static IList<Trait> _Traits = new List<Trait>();

		public string Name { get; set; }
		public string Description { get; set; }

		public static IList<Trait> LoadFromYaml(YamlNodeWrapper yaml) {
			var traits = new List<Trait> ();

			foreach (var traitNode in yaml.Children()) {
				var trait = new Trait ();
				trait.Name = traitNode.GetString ("name"); 
				Debug.Log ("Loading Trait: " + trait.Name);
				trait.Description = traitNode.GetString ("description");
				traits.Add (trait);
			}

			return traits;
		}

		public static Trait GetTrait(string name) {
			return GetTraits().First (x => x.Name == name); 
		}

		public static IList<Trait> GetTraits() {
			
			if (_Traits == null || _Traits.Count == 0) {
				var yaml = FileHelper.OpenYaml (TRAIT_DATA_FILE);
				_Traits = LoadFromYaml (yaml);
				Debug.Log ("Loaded Traits: " + _Traits.Count);
			}
			return _Traits;
		}

		public static void SetTraits(IList<Trait> traits) {
			_Traits = traits;
		}
	}


}