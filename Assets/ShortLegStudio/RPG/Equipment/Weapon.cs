using System;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Gateways;
using UnityEngineInternal;
using System.Runtime.Remoting.Messaging;

namespace ShortLegStudio.RPG.Equipment {
	public class Weapon : IEquipment {
		public string Name { get; set; }
		public float Weight { get; set; }
		public string Damage { get; set; }
		public DamageTypes DamageType { get; set; }
		public int CriticalThreat { get; set; }
		public int CriticalModifier { get; set; }
		public int Range { get;  set; }
		public WeaponType Type { get; set; }
		public WeaponGroup Group { get; set; }
		public WeaponTrainingLevel Level { get; set; }

		public Weapon () { }
		public Weapon(
			string name,
			float weight,
			string damage,
			DamageTypes damageType,
			int critThreat,
			int critMod,
			int range,
			WeaponType type,
			WeaponGroup group,
			WeaponTrainingLevel level
		) {
			Name = name;
			Weight = weight;
			Damage = damage;
			DamageType = damageType;
			CriticalThreat = critThreat == 0 ? 20 : critThreat;
			CriticalModifier = critMod == 0 ? 2 : critMod; 
			Range = range;
			Type = type;
			Group = group;
			Level = level;
		}

		public bool IsRanged {
			get { return Type == WeaponType.Ranged || Range > 0; }
		}

		public bool IsMelee {
			get { return Type != WeaponType.Ranged; }
		}

		/// <summary>
		/// Returns either "Melee" or "Ranged"
		/// </summary>
		/// <returns>The basic type.</returns>
		public string GetBasicType() {
			return Type == WeaponType.Ranged ? "Ranged" : "Melee";
		}
	}
}

