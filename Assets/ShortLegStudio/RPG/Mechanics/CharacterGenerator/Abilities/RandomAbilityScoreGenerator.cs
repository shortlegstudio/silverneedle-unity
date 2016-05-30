using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities {
	public class RandomAbilityScoreGenerator : IAbilityScoreGenerator  {
		public RandomAbilityScoreGenerator() { }

		public AbilityScores Get() {
			var abilities = new AbilityScores ();
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.GetAbility (e).Roll4d6 ();
			}
			return abilities;
		}


	}
}