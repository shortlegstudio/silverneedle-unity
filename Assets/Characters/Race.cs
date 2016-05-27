using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	public class Race {
		//Static Values
		const string RACE_DATA_FILE = "Data/races.yml";
		static IList<Race> _Races;

		public string Name { get; set; }
		public IList<AbilityScoreAdjustment> AbilityModifiers { get; private set;  }
		public IList<string> Traits { get; private set; }
		public IEnumerable<string> KnownLanguages { get { return _knownLanguages; } }
		public IEnumerable<string> AvailableLanguages { get { return _availableLanguages; } }
		public CharacterSize SizeSetting { get; set; }
		public Cup HeightRange { get; set; }
		public Cup WeightRange { get; set; }

		private IList<string> _availableLanguages;
		private IList<string> _knownLanguages;

		public Race() {
			AbilityModifiers = new List<AbilityScoreAdjustment> ();
			Traits = new List<string> ();
			_availableLanguages = new List<string> ();
			_knownLanguages = new List<string> ();
		}

		public void AddKnownLanguage(string languageName) {
			_knownLanguages.Add (languageName);
		}

		public void AddAvailableLanguage(string languageName) {
			_availableLanguages.Add (languageName);
		}

		public static IList<Race> LoadFromYaml(YamlNodeWrapper yaml) {
			var races = new List<Race> ();

			foreach (var raceNode in yaml.Children()) {
				var race = new Race ();
				race.Name = raceNode.GetString ("name"); 
				ShortLog.Debug ("Loading Race: " + race.Name);
				race.SizeSetting = (CharacterSize)System.Enum.Parse (typeof(CharacterSize), raceNode.GetString ("size"));
				race.HeightRange = DiceStrings.ParseDice (raceNode.GetString ("height"));
				race.WeightRange = DiceStrings.ParseDice (raceNode.GetString ("weight"));

				var abilities = raceNode.GetNode ("abilities");
				foreach (var ability in abilities.ChildrenToDictionary()) {
					var modifier = new AbilityScoreAdjustment ();
					modifier.Reason = "Racial Trait";
					modifier.Modifier = int.Parse (ability.Value);
					//Special case is races that can choose
					if (string.Compare (ability.Key, "choose", true) == 0) {
						modifier.RacialChose = true;
					} else {
						modifier.ability = AbilityScore.GetType (ability.Key);
					}
					race.AbilityModifiers.Add (modifier);

					//Debug.Log (ability);
				}

				var traits = raceNode.GetNode ("traits");
				foreach (var trait in traits.Children()) {
					race.Traits.Add (trait.Value);
				}

				var languages = raceNode.GetNode ("languages");
				race._knownLanguages = languages.GetCommaStringOptional ("known");
				race._availableLanguages = languages.GetCommaStringOptional ("available");
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