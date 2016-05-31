using System;
using System.Linq;
using ShortLegStudio;
using ShortLegStudio.RPG.Equipment.Gateways;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator
{
	public class EquipArmor
	{
		private IArmorGateway _armors;

		public EquipArmor (IArmorGateway armorRepo) {
			_armors = armorRepo;
		}

		public void Equip(Inventory inventory) {
			inventory.AddItem (_armors.All().ToList().ChooseOne());
		}
	}
}

