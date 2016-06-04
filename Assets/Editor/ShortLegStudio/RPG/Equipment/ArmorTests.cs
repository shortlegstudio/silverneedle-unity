using System;
using NUnit.Framework;
using ShortLegStudio.RPG.Equipment;


namespace RPG.Equipment {
	[TestFixture]
	public class ArmorTests {
		[Test]
		public void DefaultArmorTypeIsNone() {
			var armor = new Armor ();
			Assert.AreEqual (ArmorType.None, armor.ArmorType);
		}
	}

}
