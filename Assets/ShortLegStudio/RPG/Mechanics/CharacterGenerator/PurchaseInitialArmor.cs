using System;
using System.Linq;
using ShortLegStudio;
using ShortLegStudio.RPG.Equipment.Gateways;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class PurchaseInitialArmor {
		private IArmorGateway _armors;

		public PurchaseInitialArmor (IArmorGateway armorRepo) {
			_armors = armorRepo;
		}

		public void PurchaseArmorAndShield(Inventory inventory) {
			var armor = _armors.FindByArmorTypes(
				ArmorType.LightArmor,
				ArmorType.MediumArmor,
				ArmorType.HeavyArmor).ToList ().ChooseOne ();
			inventory.EquipItem (armor);

			var shield = _armors.FindByArmorType (ArmorType.Shield).ToList ().ChooseOne ();
			inventory.EquipItem (shield);
		}
	}
}

