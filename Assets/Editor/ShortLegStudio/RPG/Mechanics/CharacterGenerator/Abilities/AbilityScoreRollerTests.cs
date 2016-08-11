using NUnit.Framework;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities;
using ShortLegStudio.RPG.Characters;


namespace RPG.Mechanics.CharacterGenerator.Abilities {
	[TestFixture]
	public class AbilityScoreRollerTests {
		[Test]
		public void CharactersCanRollSomeStats() {
			var roller = new StandardAbilityScoreGenerator ();
			var abilities = new AbilityScores ();
			roller.AssignAbilities (abilities);

			//Values should be between 3 and 18 for all abilities
			foreach (var a in abilities.Abilities) {
				Assert.GreaterOrEqual (a.TotalValue, 3);
				Assert.LessOrEqual (a.TotalValue, 18);
			}
		}

		[Test]
		public void CreateAverageScores() {
			var roller = new AverageAbilityScoreGenerator ();
			var abilities = new AbilityScores ();
			roller.AssignAbilities (abilities);
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Strength));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Dexterity));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Constitution));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Intelligence));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Wisdom));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Charisma));

		}
	}
}