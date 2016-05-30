using System;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities {
	public class AverageAbilityScoreGenerator : IAbilityScoreGenerator {
		private const int averageScore = 10;

		public AverageAbilityScoreGenerator () {
		}

		public ShortLegStudio.RPG.Characters.AbilityScores Get () {
			var abilities = new AbilityScores ();
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.SetScore (e, averageScore);
			}
			return abilities;
		}
	}
}

