using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;
using System.Linq;
using System.Collections.Generic;


[TestFixture]
public class LevelUpGeneratorTests {
	CharacterSheet character;

	[SetUp]
	public void SetUp() {
		character = new CharacterSheet (new List<Skill>());

		//Go with flat Twelves to make calculations easy
		character.Abilities.SetScore(AbilityScoreTypes.Strength, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Dexterity, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Constitution, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Intelligence, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Wisdom, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Charisma, 12);

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

	[Test]
	public void EveryFourLevelsYouGetAnExtraAbilityScore() {
		LevelUpGenerator.BringCharacterToLevel (character, 4);

		//At least one ability should be greater than 12 now
		Assert.IsTrue (
			character.Abilities.GetAbilities().Any (x => x.TotalValue > 12)
		);
	}
}
