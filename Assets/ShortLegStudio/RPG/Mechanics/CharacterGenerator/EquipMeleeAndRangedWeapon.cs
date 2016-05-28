using System.Collections.Generic;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.RPG.Repositories;
using System.Linq;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class EquipMeleeAndRangedWeapon {
		private EntityGateway<Weapon> WeaponGateway;

		public EquipMeleeAndRangedWeapon(EntityGateway<Weapon> weapons) {
			WeaponGateway = weapons;
		}

		
		public void AssignWeapons(Inventory inv) {
			var melee = WeaponGateway.All().Where(x => x.Type != WeaponType.Ranged).ToList();
			var ranged = WeaponGateway.All().Where (x => x.Type == WeaponType.Ranged).ToList();

			inv.AddItem (melee.ChooseOne ());
			inv.AddItem (ranged.ChooseOne ());
		}
	}
}