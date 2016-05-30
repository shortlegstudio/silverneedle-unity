using System;
using System.Collections.Generic;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Repositories
{
	public class RaceYamlRepository : EntityGateway<Race>
	{
		const string RACE_DATA_FILE = "Data/races.yml";
		IList<Race> Races;

		public IEnumerable<Race> All () {
			return Races;
		}

		public RaceYamlRepository() {
			LoadFromYaml (FileHelper.OpenYaml (RACE_DATA_FILE));
		}

		public RaceYamlRepository(YamlNodeWrapper yaml) {
			LoadFromYaml (yaml);
		}

		private void LoadFromYaml(YamlNodeWrapper yaml) {
			Races = new List<Race> ();

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
				race.KnownLanguages.Add(languages.GetCommaStringOptional ("known"));
				race.AvailableLanguages.Add(languages.GetCommaStringOptional ("available"));
				Races.Add (race);
			}
		}


	}
}

