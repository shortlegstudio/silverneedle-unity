using System;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

[TestFixture]
public class DefenseStatsTests {
	DefenseStats stats;

	[SetUp]
	public void SetUp() {
		var abilities = new AbilityScores ();
		abilities.SetScore (AbilityScoreTypes.Strength, 16);
		abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
		stats = new DefenseStats (abilities);
	}

	[Test]
	public void ACIsBasedOnDexterity() {
		Assert.AreEqual (13, stats.ArmorClass());
	}

	[Test]
	public void TouchACIsBasedOnDexterity() {
		Assert.AreEqual (13, stats.TouchArmorClass ());
	}

	[Test]
	public void FlatFootedACIsBaseAC() {
		Assert.AreEqual (10, stats.FlatFootedArmorClass ());
	}

}
