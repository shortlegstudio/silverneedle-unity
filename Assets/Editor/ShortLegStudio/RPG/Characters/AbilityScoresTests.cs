using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;


namespace RPG.Characters {
[TestFixture]
	public class AbilityScoresTests {
		private AbilityScores Subject;

		[SetUp]
		public void Configure() {
			Subject = new AbilityScores ();
		}

		[Test]
		public void AbilityScoresContainerHasAllTheStatsSetToZero() {
			Assert.AreEqual(0, Subject.GetScore(AbilityScoreTypes.Strength));
			Assert.AreEqual(0, Subject.GetScore(AbilityScoreTypes.Wisdom));
			Assert.AreEqual(0, Subject.GetScore("Intelligence"));
		}

		[Test]
		public void AbilityScoresCanSetAbilities() {
			Subject.SetScore (AbilityScoreTypes.Charisma, 15);
			Assert.AreEqual (15, Subject.GetScore ("Charisma"));
		}

		[Test]
		public void YouMayGetTheAbilityModifier() {
			Subject.SetScore (AbilityScoreTypes.Charisma, 12);

			Assert.AreEqual (1, Subject.GetModifier (AbilityScoreTypes.Charisma));
		}

		[Test]
		public void WhenAbilityIsModifiedAnEventIsRaised() {
			bool modified = false;

			Subject.Modified += (object sender, AbilityModifiedEventArgs e) =>  {
				Assert.AreEqual(AbilityScoreTypes.Intelligence, e.Ability.Name);
				modified = true;
			};
		
			Subject.SetScore (AbilityScoreTypes.Intelligence, 19);

			Assert.IsTrue (modified);
		}

		[Test]
		public void YouCanCopyOneSetOfAbilityScoresToAnother() {
			var abilityScores = new AbilityScores ();
			var copyFrom = new AbilityScores ();
			foreach (var e in copyFrom.GetAbilities()) {
				e.SetValue (15);
			}

			abilityScores.Copy (copyFrom);

			foreach (var e in abilityScores.GetAbilities()) {
				Assert.AreEqual (e.TotalValue, copyFrom.GetScore (e.Name));
			}
		}
	}
}