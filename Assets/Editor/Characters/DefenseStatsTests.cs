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
		abilities.SetScore (AbilityScoreTypes.Constitution, 9);
		abilities.SetScore (AbilityScoreTypes.Wisdom, 12);
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
		fighter.WillSaveRate = Class.POOR_SAVE_RATE;
		fighter.FortitudeSaveRate = Class.GOOD_SAVE_RATE;
		fighter.ReflexSaveRate = Class.POOR_SAVE_RATE;

		smallStats.LevelUpDefenseStats (fighter);

		Assert.AreEqual (1, smallStats.FortitudeSave ());
		Assert.AreEqual (3, smallStats.ReflexSave ());
		Assert.AreEqual (1, smallStats.WillSave ());
	}

	[Test]
	public void LevelingUpMultipleTimesIncreasesTheSaveStats() {
		var fighter = new Class ();
		fighter.WillSaveRate = Class.POOR_SAVE_RATE;
		fighter.FortitudeSaveRate = Class.GOOD_SAVE_RATE;
		fighter.ReflexSaveRate = Class.POOR_SAVE_RATE;

		smallStats.LevelUpDefenseStats (fighter);
		smallStats.LevelUpDefenseStats (fighter);
		smallStats.LevelUpDefenseStats (fighter);

		Assert.AreEqual (3, smallStats.FortitudeSave ());
		Assert.AreEqual (4, smallStats.ReflexSave ());
		Assert.AreEqual (2, smallStats.WillSave ());
	}
}
