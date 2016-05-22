using System;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

[TestFixture]
public class DefenseStatsTests {
	DefenseStats smallStats;

	[SetUp]
	public void SetUp() {
		var abilities = new AbilityScores ();
		abilities.SetScore (AbilityScoreTypes.Strength, 16);
		abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
		var size = new SizeStats (CharacterSize.Small);
		smallStats = new DefenseStats (abilities, size);
	}

	[Test]
	public void ACIsBasedOnDexterityAndSize() {
		Assert.AreEqual (14, smallStats.ArmorClass());
	}

	[Test]
	public void TouchACIsBasedOnDexterityAndSize() {
		Assert.AreEqual (14, smallStats.TouchArmorClass ());
	}

	[Test]
	public void FlatFootedACIsBaseACAndSize() {
		Assert.AreEqual (11, smallStats.FlatFootedArmorClass ());
	}

}
