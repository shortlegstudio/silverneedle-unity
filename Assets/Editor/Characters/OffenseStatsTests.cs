using System;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

[TestFixture]
public class OffenseStatsTests {
	OffenseStats smallStats;

	[SetUp]
	public void SetUp() {
		var abilities = new AbilityScores ();
		abilities.SetScore (AbilityScoreTypes.Strength, 16);
		abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
		smallStats = new OffenseStats (abilities);
	}

	[Test]
	public void BaseAttackBonusIsAStat() {
		Assert.IsInstanceOf<BasicStat> (smallStats.BaseAttackBonus);
	}

	[Test]
	public void BaseMeleeBonusIsBABAndStrength() {
		smallStats.BaseAttackBonus.SetValue (3);
		Assert.AreEqual (6, smallStats.MeleeAttackBonus());
	}

	[Test]
	public void BaseRangeBonusIsBABAndDexterity() {
		smallStats.BaseAttackBonus.SetValue (3);
		Assert.AreEqual (6, smallStats.RangeAttackBonus());
	}

	[Test]
	public void CMBIsBABAndStrength() {
		smallStats.BaseAttackBonus.SetValue (3);
		Assert.AreEqual (6, smallStats.CombatManueverBonus ());
	}

	[Test]
	public void CMDIsBABStrengthAndDexterity() {
		smallStats.BaseAttackBonus.SetValue (3);
		Assert.AreEqual (19, smallStats.CombatManueverDefense ());
	}
}
