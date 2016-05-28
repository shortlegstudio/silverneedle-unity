using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.Dice;

[TestFixture]
public class CharacterSheetTests {
	CharacterSheet character;
	List<Skill> _testSkills;

	[SetUp]
	public void SetUp() {
		_testSkills = new List<Skill> ();
		_testSkills.Add (new Skill("Climb", AbilityScoreTypes.Strength, false));
		_testSkills.Add (new Skill ("Disable Device", AbilityScoreTypes.Dexterity, true));
		_testSkills.Add (new Skill ("Spellcraft", AbilityScoreTypes.Intelligence, true));
	}

	[Test]
	public void CalculatesSkillPointsBasedOnClassAndIntelligence() {
		var sheet = new CharacterSheet (new List<Skill>());
		var fighter = new Class ();
		fighter.SkillPoints = 2;
		sheet.Abilities.SetScore (AbilityScoreTypes.Intelligence, 14);
		sheet.Class = fighter;
		Assert.AreEqual (4, sheet.GetSkillPointsPerLevel());
	}

    [Test]
    public void CharactersHaveVitalStats() {
		var sheet = new CharacterSheet (_testSkills);
		sheet.Name = "Foobar";
		sheet.Alignment = CharacterAlignment.LawfulGood;
		Assert.AreEqual ("Foobar", sheet.Name);
		Assert.AreEqual (CharacterAlignment.LawfulGood, sheet.Alignment);
		Assert.AreEqual (1, sheet.Level);
    }

	[Test]
	public void CharactersCanRollSomeStats() {
		var sheet = new CharacterSheet (_testSkills);
		AbilityScoreGenerator.RandomStandardHeroScores (sheet.Abilities);
		var abilities = sheet.Abilities;
		Assert.IsNotNull (abilities.GetAbility (AbilityScoreTypes.Strength));
		Assert.IsNotNull (abilities.GetAbility (AbilityScoreTypes.Charisma));
		Assert.IsNotNull (abilities.GetAbility (AbilityScoreTypes.Intelligence));

	}

	[Test]
	public void SettingRaceLoadsTraits() {
		var sheet = new CharacterSheet (_testSkills);

		//Set up the trait
		var trait = new Trait ();
		trait.Name = "Elfy";
		Trait.SetTraits (new List<Trait> () { trait });

		//Set up the race
		var elf = new Race ();
		elf.Traits.Add ("Elfy");
		elf.SizeSetting = CharacterSize.Medium;
		elf.HeightRange = DiceStrings.ParseDice ("10d6");
		elf.WeightRange = DiceStrings.ParseDice ("20d8");

		sheet.SetRace (elf);
		Assert.IsTrue(sheet.Traits.Any(x => x == trait));
		Trait.SetTraits (null);
	}

	[Test]
	public void SettingRaceCalculatesSize() {
		var sheet = new CharacterSheet (_testSkills);
		var smallGuy = new Race ();
		smallGuy.SizeSetting = CharacterSize.Small;
		smallGuy.HeightRange = DiceStrings.ParseDice ("2d4+10");
		smallGuy.WeightRange = DiceStrings.ParseDice ("2d4+100");

		sheet.SetRace (smallGuy);
		Assert.AreEqual (CharacterSize.Small, sheet.Size.Size);
		Assert.GreaterOrEqual (sheet.Size.Height, 12);
		Assert.GreaterOrEqual (sheet.Size.Weight, 102);
	}

	[Test]
	public void AddTraitTriggersModifiedEvent() {
		bool called = false;

		CharacterSheet sheet = new CharacterSheet (_testSkills);
		sheet.Modified += (object sender, CharacterSheetEventArgs e) => {
			called = true;
		};

		//Set up the trait
		var trait = new Trait ();
		trait.Name = "Elfy";
		Trait.SetTraits (new List<Trait> () { trait });

		sheet.AddTrait ("Elfy");

		//Make sure the event was called
		Assert.IsTrue (called);
	}

}
