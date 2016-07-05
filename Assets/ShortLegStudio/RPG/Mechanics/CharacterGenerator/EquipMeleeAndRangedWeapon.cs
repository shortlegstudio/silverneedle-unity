using System.Collections.Generic;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.RPG.Gateways;
using System.Linq;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment.Gateways;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class EquipMeleeAndRangedWeapon {
		private IWeaponGateway WeaponGateway;

		public EquipMeleeAndRangedWeapon(IWeaponGateway weapons) {
			WeaponGateway = weapons;
		}

		
		public void AssignWeapons(Inventory inv, IEnumerable<WeaponProficiency> proficiencies) {
			var melee = WeaponGateway.FindByProficient(proficiencies).Where(x => x.Type != WeaponType.Ranged).ToList();
			var ranged = WeaponGateway.FindByProficient(proficiencies).Where (x => x.Type == WeaponType.Ranged).ToList();

			inv.AddItem (melee.ChooseOne ());
			inv.AddItem (ranged.ChooseOne ());
		}
	}
}