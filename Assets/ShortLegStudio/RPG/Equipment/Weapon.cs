using System;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Repositories;

namespace ShortLegStudio.RPG.Equipment {
	public class Weapon : IEquipment {
		const string WEAPON_YAML_FILE = "Data/weapons.yml";
		static IList<Weapon> _Weapons;

		public string Name { get; private set; }
		public float Weight { get; private set; }
		public string Damage { get; private set; }
		public DamageTypes DamageType { get; private set; }
		public int CriticalThreat { get; private set; }
		public int CriticalModifier { get; private set; }
		public int Range { get; private set; }
		public WeaponType Type { get; private set; }
		public WeaponGroup Group { get; private set; }
		public WeaponTrainingLevel Level { get; private set; }

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


		/// <summary>
		/// Returns either "Melee" or "Ranged"
		/// </summary>
		/// <returns>The basic type.</returns>
		public string GetBasicType() {
			return Type == WeaponType.Ranged ? "Ranged" : "Melee";
		}

		public static IList<Weapon> GetWeapons() {
			var repo = new WeaponYamlRepository (FileHelper.OpenYaml (WEAPON_YAML_FILE));
			return repo.All ().ToList ();
		}

		public static string ConvertDamageBySize(string mediumDamageAmount, CharacterSize size) {
			int index = DamageTables.MEDIUM_DAMAGE_TABLE.IndexOf (mediumDamageAmount);
			switch (size) {
			case CharacterSize.Tiny:
				return DamageTables.TINY_DAMAGE_TABLE [index];
			case CharacterSize.Small:
				return DamageTables.SMALL_DAMAGE_TABLE [index];
			case CharacterSize.Medium:
				return mediumDamageAmount;
			case CharacterSize.Large:
				return DamageTables.LARGE_DAMAGE_TABLE [index];
			}
			//Get Index for medium damage
			throw new NotImplementedException(string.Format("Character Size: {0} has not been implemented in damage tables.", size));
		}

	}

	public interface WeaponRepository : EntityGateway<Weapon> {

	}
}

