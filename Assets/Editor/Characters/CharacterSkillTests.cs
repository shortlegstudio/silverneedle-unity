using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using YamlDotNet.RepresentationModel;
using System.Text;

[TestFixture]
public class CharacterSkillTests {
	CharacterSkill flySkill;

	[SetUp]
	public void SetUp() {
		//Set up a climb skill
		var fly = new Skill (
			"Fly",
			AbilityScoreTypes.Dexterity,
			false
		);

		var character = new CharacterSheet ();
		character.Abilities.SetScore (AbilityScoreTypes.Dexterity, 10);

		flySkill = new CharacterSkill (fly, character);
	}
		
	[Test]
	public void UntrainedSkillsAreBasedOffOfAttributeScore() {
		//Set up a skill
		var skill = new Skill (
			"Climb",
			AbilityScoreTypes.Strength,
			false
		);

		//Set up a character
		var character = new CharacterSheet ();
		character.Abilities.SetScore (AbilityScoreTypes.Strength, 15);

		var charSkill = new CharacterSkill (skill, character);
		Assert.AreEqual (character.Abilities.GetModifier(AbilityScoreTypes.Strength), charSkill.Score);
		Assert.IsTrue (charSkill.AbleToUse);
	}

	[Test]
	public void TrainedSkillsStartAtMinValueAndUnableToUse() {
		var skill = new Skill (
			            "Disable Device",
			            AbilityScoreTypes.Dexterity,
			            true
		            );
		var character = new CharacterSheet ();
		character.Abilities.SetScore (AbilityScoreTypes.Dexterity, 18);
		var charSkill = new CharacterSkill (skill, character);
		Assert.AreEqual (int.MinValue, charSkill.Score);
		Assert.IsFalse (charSkill.AbleToUse);
	}

	[Test]
	public void AddingPointsToSkillsIncreasesTheirScore() {
		var skill = new Skill (
			            "Swim",
			            AbilityScoreTypes.Strength,
			            false
		);
		var character = new CharacterSheet ();
		character.Abilities.SetScore (AbilityScoreTypes.Strength, 15);
		var charSkill = new CharacterSkill (skill, character);
		var baseValue = charSkill.Score;
		charSkill.AddRank ();
		Assert.AreEqual (1, charSkill.Ranks);
		Assert.AreEqual (baseValue + 1, charSkill.Score);
	}

	[Test]
	public void AddingARankAllowsToUseTrainingSkill() {
		var skill = new Skill (
			            "Spellcraft",
			            AbilityScoreTypes.Intelligence,
			            true
		            );
		var character = new CharacterSheet ();
		character.Abilities.SetScore (AbilityScoreTypes.Intelligence, 15);
		var charSkill = new CharacterSkill (skill, character);
		Assert.IsFalse (charSkill.AbleToUse);
		charSkill.AddRank ();
		Assert.IsTrue (charSkill.AbleToUse);
		Assert.AreEqual (3, charSkill.Score);
	}

	[Test]
	public void ClassSkillsGetOneTimeBonus() {
		var skill = new Skill (
			            "Climb",
			            AbilityScoreTypes.Strength,
			            false
		            );
		var character = new CharacterSheet ();
		character.Abilities.SetScore (AbilityScoreTypes.Strength, 10);
		var charSkill = new CharacterSkill (skill, character);
		charSkill.ClassSkill = true;
		charSkill.AddRank ();
		Assert.AreEqual (4, charSkill.Score);
		charSkill.AddRank ();
		Assert.AreEqual (5, charSkill.Score);
	}

	[Test]
	public void SkillsCanHaveAdjustmentsFromTraitsOrFeats() {
		var adjust = new SkillAdjustment (
			             "Acrobatic Feat",
			             2,
			             "Fly"
		             );
		flySkill.AddAdjustment (adjust);

		Assert.AreEqual (2, flySkill.Score);
	}

}
