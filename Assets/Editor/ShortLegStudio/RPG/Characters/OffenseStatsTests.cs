using System;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG;
using ShortLegStudio.RPG.Characters;

namespace RPG.Characters {

	[TestFixture]
	public class OffenseStatsTests {
		OffenseStats smallStats;

		[SetUp]
		public void SetUp() {
			var abilities = new AbilityScores ();
			abilities.SetScore (AbilityScoreTypes.Strength, 16);
			abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
			var size = new SizeStats (CharacterSize.Small, 1,1);
			smallStats = new OffenseStats (abilities, size);
		}

		[Test]
		public void BaseAttackBonusIsAStat() {
			Assert.IsInstanceOf<BasicStat> (smallStats.BaseAttackBonus);
		}

		[Test]
		public void BaseMeleeBonusIsBABAndStrengthAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (7, smallStats.MeleeAttackBonus());
		}

		[Test]
		public void BaseRangeBonusIsBABAndDexterityAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (7, smallStats.RangeAttackBonus());
		}

		[Test]
		public void CMBIsBABAndStrengthAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (5, smallStats.CombatManueverBonus ());
		}

		[Test]
		public void CMDIsBABStrengthAndDexterityAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (18, smallStats.CombatManueverDefense ());
		}

	}
}
