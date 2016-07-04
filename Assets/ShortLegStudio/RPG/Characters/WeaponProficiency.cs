using System;
using ShortLegStudio.RPG.Equipment;

namespace ShortLegStudio.RPG.Characters {
	public class WeaponProficiency {
		public string Name { get; private set; }
		private bool IsLevel;
		private WeaponTrainingLevel TrainingLevel;

		public WeaponProficiency(string proficiency) {
			Name = proficiency;
			IsLevel = EnumHelpers.TryParse<WeaponTrainingLevel>(proficiency, true, out TrainingLevel);
		}

		public bool IsProficient(Weapon wpn) {
			if (IsLevel) {
				return wpn.Level == TrainingLevel;
			}
			return string.Compare(wpn.Name, Name, true) == 0;
		}
	}
}

