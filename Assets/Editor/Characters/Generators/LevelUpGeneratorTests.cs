using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;


[TestFixture]
public class LevelUpGeneratorTests {
	CharacterSheet character;

	[SetUp]
	public void SetUp() {
		character = new CharacterSheet ();

		//Go with flat Twelves to make calculations easy
		character.SetAbility(AbilityScoreTypes.Strength, 12);
		character.SetAbility(AbilityScoreTypes.Dexterity, 12);
		character.SetAbility(AbilityScoreTypes.Constitution, 12);
		character.SetAbility(AbilityScoreTypes.Intelligence, 12);
		character.SetAbility(AbilityScoreTypes.Wisdom, 12);
		character.SetAbility(AbilityScoreTypes.Charisma, 12);

		var cls = new Class ();
		character.SetClass (cls);
	}



    [Test]
    public void LevelingUpIncrementsTheLevelNumber() {
		LevelUpGenerator.LevelUp (character);
		Assert.AreEqual (2, character.Level);
    }

	[Test]
	public void HitpointsIncreaseWhenYouLevelUp() {
		var hp = character.MaxHitPoints;
		LevelUpGenerator.LevelUp (character);
		Assert.Greater (character.MaxHitPoints, hp);
	}
}
