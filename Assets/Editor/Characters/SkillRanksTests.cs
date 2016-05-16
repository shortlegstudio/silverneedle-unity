using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class SkillRanksTests {
	
	[SetUp]
	public void SetupCharacter() {

	}

	[Test]
	public void SkillRanksLoadsAllTheSkills() {
		
		var skills = new List<Skill> ();
		skills.Add (new Skill ("Climb", AbilityScoreTypes.Strength, false));
		skills.Add (new Skill ("Disable Device", AbilityScoreTypes.Dexterity, true));

		var abilityScores = new AbilityScores ();
		abilityScores.SetScore (AbilityScoreTypes.Strength, 14);
		var ranks = new SkillRanks (skills, abilityScores);

		Assert.AreEqual (2, ranks.GetScore ("Climb"));
		Assert.AreEqual (int.MinValue, ranks.GetScore ("Disable Device"));
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
	public void SettingRaceLoadsTraits() {
		var sheet = new CharacterSheet (new List<Skill>());

		//Set up the trait
		var trait = new Trait ();
		trait.Name = "Elfy";
		Trait.SetTraits (new List<Trait> () { trait });

		//Set up the race
		var elf = new Race ();
		elf.Traits.Add ("Elfy");

		sheet.SetRace (elf);
		Assert.IsTrue(sheet.Traits.Any(x => x == trait));
		Trait.SetTraits (null);
	}

	[Test]
	public void AddTraitTriggersModifiedEvent() {
		bool called = false;

		CharacterSheet sheet = new CharacterSheet (new List<Skill>());
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

	[Test]
	public void AccessAllSkillAdjustments() {
		var sheet = new CharacterSheet (new List<Skill>());
		var trait = new Trait ();
		trait.SkillModifiers.Add(
			new SkillAdjustment(
				"Trait Adj",
				3,
				"Heal"
			)
		);

		trait.SkillModifiers.Add(
			new SkillAdjustment(
				"Trait Adj",
				3,
				"Heal"
			)
		);

		trait.SkillModifiers.Add(
			new SkillAdjustment(
				"Trait Adj",
				3,
				"Fly"
			)
		);
		sheet.AddTrait (trait);

		var adjustments = sheet.FindSkillAdjustments ("Heal");
		Assert.AreEqual (2, adjustments.Count);

	}
}
