using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class Race {
		//Static Values
		const string RACE_DATA_FILE = "Data/races.yml";
		static IList<Race> _Races;

		public string Name { get; set; }

		public static IList<Race> LoadFromYaml(YamlNodeWrapper yaml) {
			var races = new List<Race> ();

			foreach (var raceNode in yaml.Children()) {
				var race = new Race ();
				race.Name = raceNode.GetValue ("name"); 
				var abilityModifiers = raceNode.GetNode ("abilities");

				foreach (var ability in abilityModifiers.ChildrenToDictionary()) {
					//Debug.Log (ability);
				}
				races.Add (race);
			}

			return races;
		}

		public static IList<Race> GetRaces() {
			if (_Races == null) {
				var yaml = FileHelper.OpenYaml (RACE_DATA_FILE);
				_Races = LoadFromYaml (yaml);
			}

			return _Races;
		}
	}


}