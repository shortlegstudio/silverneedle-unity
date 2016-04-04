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
	[SetUp]
	public void SetUp() {
	}
		
	[Test]
	public void UntrainedSkillsAreBasedOffOfAttributeScore() {
		//Set up a skill
		var skill = new Skill (
			"Climb",
			AbilityScoreTypes.Strength,
			false
		);

		//Set up an ability
		var ability = new AbilityScore (AbilityScoreTypes.Strength, 15);

		//Set up a character
		var character = new CharacterSheet ();
		character.SetAbility (ability);

		var charSkill = new CharacterSkill (skill, character);
		Assert.AreEqual (ability.BaseModifier, charSkill.Score);
		Assert.IsTrue (charSkill.AbleToUse);
	}

	[Test]
	public void TrainedSkillsStartAtMinValueAndUnableToUse() {
		var skill = new Skill (
			            "Disable Device",
			            AbilityScoreTypes.Dexterity,
			            true
		            );
		var ability = new AbilityScore (AbilityScoreTypes.Dexterity, 18);

		var character = new CharacterSheet ();
		character.SetAbility (ability);
		var charSkill = new CharacterSkill (skill, character);
		Assert.AreEqual (int.MinValue, charSkill.Score);
		Assert.IsFalse (charSkill.AbleToUse);
	}

}
