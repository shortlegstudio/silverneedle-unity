using System;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.RPG.Equipment;
using System.Linq;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class EquipCharacter {
		
		public static void AssignWeapons(CharacterSheet character) {
			AssignWeapons (character, Weapon.GetWeapons ());
		}

		public static void AssignWeapons(CharacterSheet character, IList<Weapon> weaponList) {
			//Assign a melee weapon
			var melee = weaponList.Where(x => x.Type != WeaponType.Ranged).ToList();
			var ranged = weaponList.Where (x => x.Type == WeaponType.Ranged).ToList();

			character.Inventory.AddItem (melee.ChooseOne ());
			character.Inventory.AddItem (ranged.ChooseOne ());
		}
	}
}