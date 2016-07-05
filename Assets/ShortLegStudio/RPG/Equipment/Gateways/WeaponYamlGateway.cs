using System;
using System.Linq;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.Enchilada;
using System.Collections.Generic;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Equipment.Gateways {
	public class WeaponYamlGateway : IWeaponGateway {
		const string WEAPON_YAML_FILE = "Data/weapons.yml";

		private IList<Weapon> Weapons;

		public WeaponYamlGateway()  { 
			LoadFromYaml (FileHelper.OpenYaml (WEAPON_YAML_FILE));
		}

		public WeaponYamlGateway (YamlNodeWrapper yamlData) { 
			LoadFromYaml (yamlData);
		}

		private void LoadFromYaml(YamlNodeWrapper yaml) {
			Weapons = new List<Weapon> ();

			foreach (var node in yaml.Children()) {
				ShortLog.DebugFormat ("Loading Weapon: {0}", node.GetString ("name"));
				var w = new Weapon (
					node.GetString ("name"),
					node.GetFloat ("weight"),
					node.GetString ("damage"),
					node.GetEnum<DamageTypes>("damage_type"),
					node.GetIntegerOptional ("critical_threat"),
					node.GetIntegerOptional ("critical_modifier"),
					node.GetIntegerOptional ("range"),
					node.GetEnum<WeaponType>("type"),
					node.GetEnum<WeaponGroup>("group"),
					node.GetEnum<WeaponTrainingLevel>("training_level")
				);
				Weapons.Add (w);
			}
		}

		public IEnumerable<Weapon> All() {
			return Weapons;
		}

		public IEnumerable<Weapon> FindByProficient(IEnumerable<WeaponProficiency> proficiencies) {
			return Weapons.Where(x => proficiencies.IsProficient(x));
		}
	}
}

