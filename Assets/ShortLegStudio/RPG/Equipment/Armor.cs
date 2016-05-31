using System;
using ShortLegStudio.RPG.Equipment;

namespace ShortLegStudio.RPG.Equipment {
	public class Armor : IEquipment {
		public string Name { get; set; }
		public float Weight { get; set; }
		public int ArmorClass { get; set; }
		public int MaximumDexterityBonus { get; set; }
		public int ArmorCheckPenalty { get; set; }
		public int ArcaneSpellFailureChance { get; set; }
		public ArmorType ArmorType { get; set; }
			
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

