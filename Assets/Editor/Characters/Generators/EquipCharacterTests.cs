using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;
using System.Linq;
using System.Collections.Generic;
using ShortLegStudio.RPG.Equipment;


[TestFixture]
public class EquipCharacterTests {
	CharacterSheet character;

	[SetUp]
	public void SetUp() {
		character = new CharacterSheet (new List<Skill>());

		//Go with flat Twelves to make calculations easy
		character.Abilities.SetScore(AbilityScoreTypes.Strength, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Dexterity, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Constitution, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Intelligence, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Wisdom, 12);
		character.Abilities.SetScore(AbilityScoreTypes.Charisma, 12);

		var cls = new Class ();
		character.SetClass (cls);
	}

	[Test]
	public void CharactersGetARangedAndMeleeWeapon() {
		var weapons = new List<Weapon> ();
		var wpn1 = new Weapon ("Mace", 0f, "1d6", 
			DamageTypes.Bludgeoning, 20, 2, 0, 
			WeaponType.OneHanded, WeaponGroup.Hammers, 
			WeaponTrainingLevel.Simple);
		var wpn2 = new Weapon ("Bow", 0, "1d6", 
			DamageTypes.Piercing, 20, 2, 0, 
			WeaponType.Ranged, WeaponGroup.Bows, 
			WeaponTrainingLevel.Martial);
		weapons.Add (wpn1);
		weapons.Add (wpn2);

		//Bad test, but good enough for now
		for (int i = 0; i < 1000; i++) {
			var character = new CharacterSheet (new List<Skill>());
			EquipCharacter.AssignWeapons (character, weapons);
			Assert.AreEqual (character.Inventory.Weapons.Count (), 2);
			Assert.IsTrue (character.Inventory.Weapons.Any (x => x.Type == WeaponType.Ranged));
			Assert.IsTrue (character.Inventory.Weapons.Any (x => x.Type != WeaponType.Ranged));
		}
			
	}
}
