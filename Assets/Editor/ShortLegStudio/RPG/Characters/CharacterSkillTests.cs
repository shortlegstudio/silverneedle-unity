using System.Linq;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

namespace RPG.Characters {

	[TestFixture]
	public class CharacterSkillTests {
		[Test]
		public void UntrainedSkillsAreBasedOffOfAttributeScore() {
			//Set up a skill
			var skill = new Skill (
				"Climb",
				AbilityScoreTypes.Strength,
				false
			);

			var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Strength, 14), false);
			Assert.AreEqual (2, charSkill.Score());
			Assert.IsTrue (charSkill.AbleToUse);
		}

		[Test]
		public void TrainedSkillsStartAtMinValueAndUnableToUse() {
			var skill = new Skill (
				            "Disable Device",
				            AbilityScoreTypes.Dexterity,
				            true
			            );
			var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Dexterity, 18), false);
			Assert.AreEqual (int.MinValue, charSkill.Score());
			Assert.IsFalse (charSkill.AbleToUse);
		}

		[Test]
		public void AddingPointsToSkillsIncreasesTheirScore() {
			var skill = new Skill (
				            "Swim",
				            AbilityScoreTypes.Strength,
				            false
			);
			var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Strength, 15), false);
			var baseValue = charSkill.Score();
			charSkill.AddRank ();
			Assert.AreEqual (1, charSkill.Ranks);
			Assert.AreEqual (baseValue + 1, charSkill.Score());
		}

		[Test]
		public void AddingARankAllowsToUseTrainingSkill() {
			var skill = new Skill (
				            "Spellcraft",
				            AbilityScoreTypes.Intelligence,
				            true
			            );
			var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Intelligence, 15), false);
			Assert.IsFalse (charSkill.AbleToUse);
			charSkill.AddRank ();
			Assert.IsTrue (charSkill.AbleToUse);
			Assert.AreEqual (3, charSkill.Score());
		}

		[Test]
		public void ClassSkillsGetOneTimeBonus() {
			var skill = new Skill (
				            "Climb",
				            AbilityScoreTypes.Strength,
				            false
			            );
			var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Strength, 10), true);
			charSkill.ClassSkill = true;
			charSkill.AddRank ();
			Assert.AreEqual (4, charSkill.Score());
			charSkill.AddRank ();
			Assert.AreEqual (5, charSkill.Score());
		}

		[Test]
		public void SkillsCanHaveAdjustmentsFromTraitsOrFeats() {
			var adjust = new SkillModifier (
							2,
							"Acrobatic Feat",
				             "Fly"
			             );
			var flySkill = new Skill ("Fly", AbilityScoreTypes.Dexterity, false);
			var charSkill = new CharacterSkill (flySkill, new AbilityScore (AbilityScoreTypes.Dexterity, 10), false);
			charSkill.AddModifier (adjust);

			Assert.AreEqual (2, charSkill.Score());
		}

		[Test]
		public void SkillsRecalculateWhenAbilityIsUpdated() {
			var skill = new Skill ("Chew", AbilityScoreTypes.Strength, false);
			var ability = new AbilityScore (AbilityScoreTypes.Strength, 10);
			var charSkill = new CharacterSkill (skill, ability, false);

			var oldVal = charSkill.Score();
			ability.SetValue (16);
			Assert.Greater (charSkill.Score(), oldVal);

		}

		[Test]
		public void ModificationToAnAdjustmentAreReflectedInTotalScore() {
			var skill = new Skill ("Chew", AbilityScoreTypes.Strength, false);
			var ability = new AbilityScore (AbilityScoreTypes.Strength, 10);
			var charSkill = new CharacterSkill (skill, ability, false);
			var adj = new SkillModifier (0, "Teeth", "Chew");
			charSkill.AddModifier (adj);
			Assert.AreEqual (0, charSkill.Score());
			adj.Modifier = 5;
			Assert.AreEqual (5, charSkill.Score());
		}
	}
}
