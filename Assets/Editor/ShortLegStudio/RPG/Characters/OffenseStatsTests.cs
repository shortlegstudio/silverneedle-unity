using System;
using System.Collections.Generic;
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

		[Test]
		public void ModifiersCanBeAppliedToCombatManeuverDefense() {
			var mods = new MockMod();
			var oldCMD = smallStats.CombatManueverDefense();
			smallStats.ProcessModifier(mods);
			Assert.AreEqual(oldCMD + 1, smallStats.CombatManueverDefense());
		}

		[Test]
		public void ModifiersCanBeAppliedToCombatManeuverBonus() {
			var mods = new MockMod();
			var oldCMB = smallStats.CombatManueverBonus();
			smallStats.ProcessModifier(mods);
			Assert.AreEqual(oldCMB + 1, smallStats.CombatManueverBonus());
		}

		class MockMod : IModifiesStats {
			public IList<BasicStatModifier> Modifiers { get; set;  }

			public MockMod() {
				Modifiers = new List<BasicStatModifier>();
				Modifiers.Add(new BasicStatModifier("CMD", 1, "racial", "Trait"));
				Modifiers.Add(new BasicStatModifier("CMB", 1, "racial", "Trait"));
			}
		}
	}
}
