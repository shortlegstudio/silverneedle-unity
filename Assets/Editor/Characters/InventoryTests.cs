using System.Linq;
using System.IO;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment;

[TestFixture]
public class InventoryTests {
	[Test]
	public void InventoryTracksWeapons() {
		var inv = new Inventory ();
		var wpn1 = new Weapon ();
		var gear = new PieceOfJunk ();
		inv.AddItem (wpn1);
		inv.AddItem (gear);

		Assert.AreEqual (wpn1, inv.Weapons.First ());
	}

	[Test]
	public void InventoryWillReturnCurrentArmor() {
		var inv = new Inventory ();
		var armor = new Armor ();
		inv.AddItem (armor);

		Assert.AreEqual (armor, inv.Armor.First());

	}


	class PieceOfJunk : IEquipment {
		public string Name { get { return "Junk"; } }
		public float Weight { get { return 0.5f; } }
	}
}
