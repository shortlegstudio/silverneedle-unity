using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public static class AbilityScoreGenerator  {
		public static void RandomStandardHeroScores(AbilityScores abilities) {
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.GetAbility (e).Roll4d6 ();
			}
		}
	}
}