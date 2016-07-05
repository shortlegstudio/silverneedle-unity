using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.Dice;
using NUnit.Framework.Constraints;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG;
using ShortLegStudio.RPG.Equipment;

namespace RPG.Characters {

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
		public void AssigningClassUpdatesWeaponProficiencies() {
			var sheet = new CharacterSheet(new List<Skill>());
			var fighter = new Class();
			fighter.WeaponProficiencies.Add("martial");
			fighter.WeaponProficiencies.Add("simple");
			sheet.SetClass(fighter);

			var wpn = new Weapon();
			wpn.Level = WeaponTrainingLevel.Martial;
			Assert.IsTrue(sheet.Offense.IsProficient(wpn));
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

			sheet.AddTrait(trait, true);

			//Make sure the event was called
			Assert.IsTrue (called);
		}

		[Test]
		public void AddingATraitToWillSaveBoostsDefense() {
			CharacterSheet sheet = new CharacterSheet(_testSkills);
			var trait = new Trait();
			trait.Modifiers.Add(
				new BasicStatModifier("Will", 10, "Trait", "Cause")
			);
			var oldScore = sheet.Defense.WillSave();
			sheet.AddTrait(trait);
			Assert.AreEqual(oldScore + 10, sheet.Defense.WillSave());
		}
	}


}
