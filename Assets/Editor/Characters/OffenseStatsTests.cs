using System;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

[TestFixture]
public class OffenseStatsTests {
	OffenseStats stats;

	[SetUp]
	public void SetUp() {
		var abilities = new AbilityScores ();
		abilities.SetScore (AbilityScoreTypes.Strength, 16);
		abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
		stats = new OffenseStats (abilities);
	}

	[Test]
	public void BaseAttackBonusIsAStat() {
		Assert.IsInstanceOf<BasicStat> (stats.BaseAttackBonus);
	}

	[Test]
	public void BaseMeleeBonusIsBABAndStrength() {
		stats.BaseAttackBonus.SetValue (3);
		Assert.AreEqual (6, stats.MeleeAttackBonus());
	}

	[Test]
	public void BaseRangeBonusIsBABAndDexterity() {
		stats.BaseAttackBonus.SetValue (3);
		Assert.AreEqual (6, stats.RangeAttackBonus());
	}

}
