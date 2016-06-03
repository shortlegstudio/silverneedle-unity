using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using System.Linq;
using System.Collections.Generic;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.Enchilada;


[TestFixture]
public class EquipMeleeAndRangedWeaponTests {
	[Test]
	public void CharactersGetARangedAndMeleeWeapon() {
		//Bad test, but good enough for now
		for (int i = 0; i < 1000; i++) {
			var inventory = new Inventory ();
			var repo = new EquipMeleeAndRangedWeapon (new WeaponTestRepo ());
			repo.AssignWeapons (inventory);
			Assert.AreEqual (inventory.Weapons.Count (), 2);
			Assert.IsTrue (inventory.Weapons.Any (x => x.Type == WeaponType.Ranged));
			Assert.IsTrue (inventory.Weapons.Any (x => x.Type != WeaponType.Ranged));
		}
	}

	private class WeaponTestRepo : EntityGateway<Weapon> {
		public IEnumerable<Weapon> All() {
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
			return weapons;
		}
	}
}
