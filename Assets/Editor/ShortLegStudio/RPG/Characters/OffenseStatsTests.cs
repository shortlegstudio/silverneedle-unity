using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.Dice;

namespace RPG.Characters {

	[TestFixture]
	public class OffenseStatsTests {
		OffenseStats smallStats;
		Inventory inventory;

		[SetUp]
		public void SetUp() {
			var abilities = new AbilityScores ();
			abilities.SetScore (AbilityScoreTypes.Strength, 16);
			abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
			var size = new SizeStats (CharacterSize.Small, 1,1);
			inventory = new Inventory();
			smallStats = new OffenseStats (abilities, size, inventory);
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

		[Test]
		public void ContainsAListOfAllWeaponsAvailableAndTheirStats() {
			var longsword = Longsword();
			inventory.AddItem(longsword);
			Assert.AreEqual(1, smallStats.Attacks().Count);
			Assert.AreEqual("Longsword", smallStats.Attacks().First().Name);
			Assert.AreEqual(longsword, smallStats.Attacks().First().Weapon);
		}

		[Test]
		public void MeleeWeaponAttacksCalculateDamageBonuses() {
			inventory.AddItem(Longsword());
			var atk = smallStats.Attacks().First();
			Assert.IsNotNull(atk);
			var diceRoll = atk.Damage;
			Assert.AreEqual(3, diceRoll.Modifier);
			Assert.AreEqual(DiceSides.d8, diceRoll.Dice.First().Sides);
			Assert.AreEqual(smallStats.MeleeAttackBonus(), atk.AttackBonus);
		}

		[Test]
		public void RangeAttackBonusHaveAttackBonusButNotDamage() {
			inventory.AddItem(ShortBow());
			var atk = smallStats.Attacks().First();
			Assert.IsNotNull(atk);
			var diceRoll = atk.Damage;
			Assert.AreEqual(0, diceRoll.Modifier);
			Assert.AreEqual(DiceSides.d6, diceRoll.Dice.First().Sides);
			Assert.AreEqual(smallStats.RangeAttackBonus(), atk.AttackBonus);
		}



		private Weapon Longsword() {
			return new Weapon("Longsword", 0, "1d8", DamageTypes.Slashing, 19, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Martial);
		}

		private Weapon ShortBow() {
			return new Weapon("Short bow", 0, "1d6", DamageTypes.Piercing, 19, 2, 0, WeaponType.Ranged, WeaponGroup.Bows, WeaponTrainingLevel.Martial);
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
