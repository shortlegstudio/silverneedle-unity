using System.Collections.Generic;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.RPG.Repositories;
using System.Linq;
using ShortLegStudio.Enchilada;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class EquipCharacter {
		
		public static void AssignWeapons(CharacterSheet character) {
			AssignWeapons (character, new WeaponYamlRepository());
		}

		public static void AssignWeapons(CharacterSheet character, EntityGateway<Weapon> weapons) {
			//Assign a melee weapon

			var melee = weapons.All().Where(x => x.Type != WeaponType.Ranged).ToList();
			var ranged = weapons.All().Where (x => x.Type == WeaponType.Ranged).ToList();

			character.Inventory.AddItem (melee.ChooseOne ());
			character.Inventory.AddItem (ranged.ChooseOne ());
		}
	}
}