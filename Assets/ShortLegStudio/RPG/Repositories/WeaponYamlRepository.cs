using System;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.Enchilada;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Repositories
{
	public class WeaponYamlRepository : EntityGateway<Weapon>
	{
		private IList<Weapon> weapons;

		public WeaponYamlRepository (YamlNodeWrapper yamlData) { 
			weapons = LoadFromYaml (yamlData);
		}

		private IList<Weapon> LoadFromYaml(YamlNodeWrapper yaml) {
			var weapons = new List<Weapon> ();

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
				weapons.Add (w);
			}

			return weapons;
		}

		public IEnumerable<Weapon> All() {
			return weapons;
		}
	}
}

