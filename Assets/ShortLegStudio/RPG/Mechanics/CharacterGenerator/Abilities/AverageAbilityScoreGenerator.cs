using System;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities {
	public class AverageAbilityScoreGenerator : IAbilityScoreGenerator {
		private const int averageScore = 10;

		public AverageAbilityScoreGenerator () {
		}

		public void AssignAbilities(ShortLegStudio.RPG.Characters.AbilityScores abilities) {
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.SetScore (e, averageScore);
			}
		}
	}
}

