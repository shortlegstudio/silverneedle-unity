using System;
using ShortLegStudio.RPG.Equipment;

namespace ShortLegStudio.RPG.Equipment {
	public class Armor : IEquipment {
		public string Name { get; private set; }
		public float Weight { get; private set; }
		public int ArmorClass { get; private set; }

			
		public Armor () {
		}

		public Armor(string name) {
			Name = name;
		}
	}
}

