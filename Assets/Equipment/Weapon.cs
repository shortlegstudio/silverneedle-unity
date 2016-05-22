using System;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.RPG.Characters;
using System.IO;

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
			CriticalThreat = critThreat;
			CriticalModifier = critMod;
			Range = range;
			Type = type;
			Group = group;
			Level = level;
		}

		public static IList<Weapon> LoadFromYaml(YamlNodeWrapper yaml) {
			var weapons = new List<Weapon> ();

			foreach (var node in yaml.Children()) {
				var w = new Weapon (
					        node.GetString ("name"),
					        node.GetFloat ("weight"),
					        node.GetString ("damage"),
							node.GetEnum<DamageTypes>("damage_type"),
							node.GetInteger ("critical_threat"),
					        node.GetInteger ("critical_modifier"),
							node.GetIntegerOptional ("range"),
							node.GetEnum<WeaponType>("type"),
							node.GetEnum<WeaponGroup>("group"),
							node.GetEnum<WeaponTrainingLevel>("training_level")
				        );
				weapons.Add (w);
			}

			return weapons;
		}

		public static IList<Weapon> GetWeapons() {
			if (_Weapons == null || _Weapons.Count == 0) {
				_Weapons = LoadFromYaml (FileHelper.OpenYaml (WEAPON_YAML_FILE));
			}

			return _Weapons;
		}

		public static string ConvertDamageBySize(string mediumDamageAmount, CharacterSize size) {
			int index = MEDIUM_DAMAGE_TABLE.IndexOf (mediumDamageAmount);
			switch (size) {
			case CharacterSize.Tiny:
				return TINY_DAMAGE_TABLE [index];
			case CharacterSize.Small:
				return SMALL_DAMAGE_TABLE [index];
			case CharacterSize.Medium:
				return mediumDamageAmount;
			case CharacterSize.Large:
				return LARGE_DAMAGE_TABLE [index];
			}
			//Get Index for medium damage
			throw new NotImplementedException(string.Format("Character Size: {0} has not been implemented in damage tables.", size));
		}

		public static List<string> TINY_DAMAGE_TABLE = new List<string>
			{ "0", 		"1", 	"1d2", 	"1d3", 	"1d4", 	"1d6", 	"1d8", 	"1d4", 	"1d8", 	"1d10", "2d6" };
		public static List<string> SMALL_DAMAGE_TABLE = new List<string>
			{ "1", 		"1d2", 	"1d3", 	"1d4", 	"1d6", 	"1d8", 	"1d10", "1d6", 	"1d10", "2d6", 	"2d8" };	
		public static List<string> MEDIUM_DAMAGE_TABLE = new List<string>
			{ "1d2", 	"1d3", 	"1d4", 	"1d6", 	"1d8", 	"1d10", "1d12", "2d4", 	"2d6", 	"2d8", 	"2d10" };
		public static List<string> LARGE_DAMAGE_TABLE = new List<string> 
			{ "1d3", 	"1d4", 	"1d6", 	"1d8", 	"2d6", 	"2d8", 	"3d6", 	"2d6", 	"3d6", 	"3d8", 	"4d8" };
	}
}

