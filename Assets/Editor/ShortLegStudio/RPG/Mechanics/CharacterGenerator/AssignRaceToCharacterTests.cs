﻿using System;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Dice;
using System.Linq;
using ShortLegStudio.Enchilada;
using System.Collections.Generic;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;

namespace RPG.Mechanics.CharacterGenerator {
	[TestFixture]
	public class AssignRaceToCharacterTests {
		List<Skill> _testSkills;

		[SetUp]
		public void SetUp() {
			_testSkills = new List<Skill> ();
			_testSkills.Add (new Skill("Climb", AbilityScoreTypes.Strength, false));
			_testSkills.Add (new Skill ("Disable Device", AbilityScoreTypes.Dexterity, true));
			_testSkills.Add (new Skill ("Spellcraft", AbilityScoreTypes.Intelligence, true));
		}


		[Test]
		public void SettingRaceLoadsTraits() {
			var sheet = new CharacterSheet (_testSkills);
			var gateway = new TestTraitGateway();

			//Set up the trait
			var trait = new Trait ();
			trait.Name = "Elfy";
			gateway.Traits.Add(trait);

			//Set up the race
			var elf = new Race ();
			elf.Traits.Add("Elfy");
			elf.SizeSetting = CharacterSize.Medium;
			elf.HeightRange = DiceStrings.ParseDice ("10d6");
			elf.WeightRange = DiceStrings.ParseDice ("20d8");

			//sheet.SetRace (elf);
			var assign = new AssignRaceToCharacter(new TestRacesGateway(), gateway);
			assign.SetRace(sheet, elf);
			Assert.AreEqual(elf, sheet.Race);
			Assert.IsTrue(sheet.Traits.Any(x => x == trait));
		}

		[Test]
		public void SettingRaceCalculatesSize() {
			var gateway = new TestTraitGateway();

			var sheet = new CharacterSheet (_testSkills);

			var smallGuy = new Race ();
			smallGuy.SizeSetting = CharacterSize.Small;
			smallGuy.HeightRange = DiceStrings.ParseDice ("2d4+10");
			smallGuy.WeightRange = DiceStrings.ParseDice ("2d4+100");


			var assign = new AssignRaceToCharacter(new TestRacesGateway(), gateway);
			assign.SetRace(sheet, smallGuy);
			Assert.AreEqual (CharacterSize.Small, sheet.Size.Size);
			Assert.GreaterOrEqual (sheet.Size.Height, 12);
			Assert.GreaterOrEqual (sheet.Size.Weight, 102);
		}


		[Test]
		public void SettingRaceAssignsMovement() {
			var sheet = new CharacterSheet (_testSkills);
			var fastGuy = new Race ();
			fastGuy.SizeSetting = CharacterSize.Small;
			fastGuy.HeightRange = DiceStrings.ParseDice ("2d4+10");
			fastGuy.WeightRange = DiceStrings.ParseDice ("2d4+100");
			fastGuy.BaseMovementSpeed = 45;

			var assign = new AssignRaceToCharacter(new TestRacesGateway(), new TestTraitGateway());
			assign.SetRace(sheet, fastGuy);
			Assert.AreEqual (45, sheet.BaseMovementSpeed);
		}
	}

	class TestRacesGateway : EntityGateway<Race> {
		public List<Race> Races = new List<Race>();

		public IEnumerable<Race> All() {
			return Races; 
		}
	}

	class TestTraitGateway : EntityGateway<Trait> {
		public List<Trait> Traits = new List<Trait>();

		public IEnumerable<Trait> All() {
			return Traits;
		}
	}
}

