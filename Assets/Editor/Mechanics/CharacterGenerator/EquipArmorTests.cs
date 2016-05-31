using System;
using System.Linq;
using NUnit.Framework;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using ShortLegStudio.RPG.Equipment.Gateways;
using System.Runtime.InteropServices;
using ShortLegStudio.RPG.Equipment;
using System.Collections.Generic;
using ShortLegStudio.RPG.Characters;

[TestFixture]
public class EquipArmorTests
{
	[Test]
	public void EquipWithRandomArmor () {
		var equip = new EquipArmor(new TestArmorGateway());
		var inventory = new Inventory ();
		equip.Equip (inventory);

		Assert.AreEqual (1, inventory.All.Count ());
	}


	private class TestArmorGateway : IArmorGateway {
		List<Armor> armors;

		public TestArmorGateway() {
			armors = new List<Armor>();
			armors.Add(new Armor());
			armors.Add(new Armor());
			armors.Add(new Armor());
		}

		public IEnumerable<Armor> All() {
			return armors;
		}

		public ShortLegStudio.RPG.Equipment.Armor GetByName (string name)
		{
			return armors [0];
		}

		public System.Collections.Generic.IEnumerable<ShortLegStudio.RPG.Equipment.Armor> FindByArmorType (ShortLegStudio.RPG.Equipment.ArmorType type)
		{
			return armors;
		}

	}
}

