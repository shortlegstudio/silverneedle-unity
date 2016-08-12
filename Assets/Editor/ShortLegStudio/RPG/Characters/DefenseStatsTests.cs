using System;
using System.Collections.Generic;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment;

namespace RPG.Characters {

	[TestFixture]
	public class DefenseStatsTests {
		DefenseStats smallStats;

		[SetUp]
		public void SetUp() {
			var abilities = new AbilityScores ();
			abilities.SetScore (AbilityScoreTypes.Strength, 16);
			abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
			abilities.SetScore (AbilityScoreTypes.Constitution, 9);
			abilities.SetScore (AbilityScoreTypes.Wisdom, 12);
			var size = new SizeStats (CharacterSize.Small);
			smallStats = new DefenseStats (abilities, size, new Inventory());
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

		[Test]
		public void ReflexSavingThrowIsBasedOnDexterity() {
			Assert.AreEqual (3, smallStats.ReflexSave());
		}

		[Test]
		public void FortitudeSavingThrowIsBasedOnConstitution() {
			Assert.AreEqual (-1, smallStats.FortitudeSave ());
		}

		[Test]
		public void WillSavingThrowIsBasedOnWisdom() {
			Assert.AreEqual (1, smallStats.WillSave ());
		}

		[Test]
		public void MarkingASaveGoodGivesItAPlus2Bonus() {
			Assert.AreEqual (3, smallStats.ReflexSave ());
			smallStats.SetReflexGoodSave ();
			Assert.AreEqual (5, smallStats.ReflexSave ());

			smallStats.SetFortitudeGoodSave ();
			Assert.AreEqual (1, smallStats.FortitudeSave ());

			smallStats.SetWillGoodSave ();
			Assert.AreEqual (3, smallStats.WillSave ());
		}

		[Test]
		public void SettingGoodSaveRepeatedlyDoesntBoostSave() {
			smallStats.SetReflexGoodSave ();
			smallStats.SetReflexGoodSave ();
			smallStats.SetReflexGoodSave ();

			Assert.AreEqual (5, smallStats.ReflexSave ());
		}

		[Test]
		public void LevelingUpAClassMarksGoodSaves() {
			var fighter = new Class ();
			fighter.WillSaveRate = Class.PoorSaveRate;
			fighter.FortitudeSaveRate = Class.GoodSaveRate;
			fighter.ReflexSaveRate = Class.PoorSaveRate;

			smallStats.LevelUpDefenseStats (fighter);

			Assert.AreEqual (1, smallStats.FortitudeSave ());
			Assert.AreEqual (3, smallStats.ReflexSave ());
			Assert.AreEqual (1, smallStats.WillSave ());
		}

		[Test]
		public void LevelingUpMultipleTimesIncreasesTheSaveStats() {
			var fighter = new Class ();
			fighter.WillSaveRate = Class.PoorSaveRate;
			fighter.FortitudeSaveRate = Class.GoodSaveRate;
			fighter.ReflexSaveRate = Class.PoorSaveRate;

			smallStats.LevelUpDefenseStats (fighter);
			smallStats.LevelUpDefenseStats (fighter);
			smallStats.LevelUpDefenseStats (fighter);

			Assert.AreEqual (3, smallStats.FortitudeSave ());
			Assert.AreEqual (4, smallStats.ReflexSave ());
			Assert.AreEqual (2, smallStats.WillSave ());
		}


		[Test]
		public void EquippedArmorIncreasesYourDefenseAndYourFlatFootedDefenseButNotTouchDefense() {
			//TODO: Not sure if inventory is correct, or if inventory items should have modifiers?
			var inventory = new Inventory ();
			var def = new DefenseStats (
				          new AbilityScores (),
				          new SizeStats (),
							inventory
			          );
			var startAC = def.ArmorClass();
			var startFlat = def.FlatFootedArmorClass ();
			var startTouch = def.TouchArmorClass ();

			var armor = new Armor ();
			armor.ArmorClass = 10;

			inventory.AddGear (armor);
			inventory.EquipItem (armor);
			Assert.AreEqual (10, def.EquippedArmorBonus ());
			Assert.AreEqual (startAC + 10, def.ArmorClass());
			Assert.AreEqual (startFlat + 10, def.FlatFootedArmorClass ());
			Assert.AreEqual (startTouch, def.TouchArmorClass ());
		}

		[Test]
		public void UnEquippedArmorMakesNoDifference() {
			var inv = new Inventory ();
			var def = new DefenseStats (
				          new AbilityScores (),
				          new SizeStats (),
				          inv
			          );

			var armor = new Armor ();
			armor.ArmorClass = 12;
			inv.AddGear (armor);

			Assert.AreEqual (0, def.EquippedArmorBonus ());

		}

		[Test]
		public void ModifiersCanBeAppliedToArmorClass() {
			var def = new DefenseStats(
				          new AbilityScores(),
				          new SizeStats(),
				          new Inventory()
			          );
			var ac = def.ArmorClass();
			def.ProcessModifier(new MockMod());
			Assert.AreEqual(ac + 1, def.ArmorClass());
		}

		[Test]
		public void ModifiersCanBeAppliedToSavingsThrows() {
			var def = new DefenseStats(
				          new AbilityScores(),
				          new SizeStats(),
				          new Inventory()
			          );
			var will = def.WillSave();
			var fort = def.FortitudeSave();
			var reflex = def.ReflexSave();
			def.ProcessModifier(new MockMod());
			Assert.AreEqual(will + 1, def.WillSave());
			Assert.AreEqual(fort + 1, def.FortitudeSave());
			Assert.AreEqual(reflex + 1, def.ReflexSave());
		}

		[Test]
		public void CanAddArmorProficiency() {
			var def = new DefenseStats(
				          new AbilityScores(),
				          new SizeStats(),
				          new Inventory()
			          );
			def.AddArmorProficiency("Light");
			Assert.IsTrue(def.IsProficient(Leather()));
		}

        [Test]
        public void CanAddArmorProficiencies() {
            var def = new DefenseStats(
                          new AbilityScores(),
                          new SizeStats(),
                          new Inventory());
            def.AddArmorProficiencies(new string[] { "Light", "Heavy" });
            Assert.IsTrue(def.IsProficient(Leather()));
        }

		class MockMod : IModifiesStats {
			public IList<BasicStatModifier> Modifiers { get; set;  }

			public MockMod() {
				Modifiers = new List<BasicStatModifier>();
				Modifiers.Add(new BasicStatModifier("Armor Class", 1, "Cause", "Dodge"));
				Modifiers.Add(new BasicStatModifier("Will", 1, "Halfing Luck", "Trait"));
				Modifiers.Add(new BasicStatModifier("Reflex", 1, "Halfing Luck", "Trait"));
				Modifiers.Add(new BasicStatModifier("Fortitude", 1, "Halfing Luck", "Trait"));

			}
		}

		private Armor Leather() {
			return new Armor(
				"Leather",
				2,
				10,
				6,
				0,
				10,
				ArmorType.Light
			);
		}

	}
}
