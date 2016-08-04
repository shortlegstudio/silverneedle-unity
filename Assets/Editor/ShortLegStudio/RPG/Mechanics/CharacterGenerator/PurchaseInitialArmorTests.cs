using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using ShortLegStudio.RPG.Equipment.Gateways;
using System.Runtime.InteropServices;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.RPG.Characters;


namespace RPG.Mechanics.CharacterGenerator {

	[TestFixture]
	public class PurchaseInitialArmorTests
	{
		[Test]
		public void EquipWithArmorAndShield () {
			var equip = new PurchaseInitialArmor(new TestArmorGateway());
			var inventory = new Inventory ();
			equip.PurchaseArmorAndShield (inventory);


			Assert.IsTrue (inventory.OfType<Armor> ().Any (x => x.ArmorType == ArmorType.Shield));
			Assert.IsTrue (inventory.OfType<Armor> ().Any (x => x.ArmorType != ArmorType.Shield));
		}

		private class TestArmorGateway : IArmorGateway {
			List<Armor> armors;

			public TestArmorGateway() {
				armors = new List<Armor>();
				var shield = new Armor();
				shield.ArmorType = ArmorType.Shield;

				var armor = new Armor();
				armor.ArmorType = ArmorType.Heavy;
				armors.Add(armor);
				armors.Add(shield);
			}

			public IEnumerable<Armor> All() {
				return armors;
			}

			public ShortLegStudio.RPG.Equipment.Armor GetByName (string name)
			{
				return armors [0];
			}

			public IEnumerable<Armor> FindByArmorType (ArmorType type)
			{
				return armors.Where( x => x.ArmorType == type);
			}

			public IEnumerable<Armor> FindByArmorTypes (params ArmorType[] types)
			{
				return armors.Where (x => types.Contains (x.ArmorType));
			}

		}
	}
}

