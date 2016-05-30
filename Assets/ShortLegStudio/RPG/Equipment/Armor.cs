using System;
using ShortLegStudio.RPG.Equipment;

namespace ShortLegStudio.RPG.Equipment {
	public class Armor : IEquipment {
		public string Name { get; private set; }
		public float Weight { get; private set; }
		public int ArmorClass { get; private set; }
		public int MaximumDexterityBonus { get; private set; }
		public int ArmorCheckPenalty { get; private set; }
		public int ArcaneSpellFailureChance { get; private set; }
		public ArmorType ArmorType { get; private set; }
			
		public Armor () {
		}

		public Armor(
			string name,
			int ac,
			float weight,
			int maxDexBonus,
			int armorCheckPenalty,
			int arcaneSpell,
			ArmorType armorType
		) {
			Name = name;
			ArmorClass = ac;
			Weight = weight;
			MaximumDexterityBonus = maxDexBonus;
			ArmorCheckPenalty = armorCheckPenalty;
			ArcaneSpellFailureChance = arcaneSpell;
			ArmorType = armorType;
		}
	}
}

