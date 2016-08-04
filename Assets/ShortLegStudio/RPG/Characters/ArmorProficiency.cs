using System;
using ShortLegStudio.RPG.Equipment;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.RPG.Characters {
	public class ArmorProficiency {
		public string Name { get; private set; }
		private bool IsLevel;
		private ArmorType armorType;

		public ArmorProficiency(string proficiency) {
			Name = proficiency;
			IsLevel = EnumHelpers.TryParse<ArmorType>(proficiency, true, out armorType);
		}

		public bool IsProficient(Armor armor) {
			if (IsLevel) {
				return armor.ArmorType == armorType;
			}
			return string.Compare(armor.Name, Name, true) == 0;
		}
	}

	public static class ArmorProficiencyEnumerableExtensions {
		public static bool IsProficient(this IEnumerable<ArmorProficiency> proficiencies, Armor armor) {
			return proficiencies.Any(x => x.IsProficient(armor));
		}
	}
}

