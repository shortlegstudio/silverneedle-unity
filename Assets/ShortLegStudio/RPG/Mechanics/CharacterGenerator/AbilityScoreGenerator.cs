using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class AbilityScoreGenerator  {
		private int averageScore = 10;

		public AbilityScoreGenerator() { }
		public void RandomStandardHeroScores(AbilityScores abilities) {
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.GetAbility (e).Roll4d6 ();
			}
		}

		public void CreateAverageCharacter(AbilityScores abilities) {
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.SetScore (e, averageScore);
			}
		}
	}
}