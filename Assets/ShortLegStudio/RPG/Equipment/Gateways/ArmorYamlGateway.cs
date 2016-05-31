using System;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Equipment;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.RPG.Equipment.Gateways {
	public class ArmorYamlGateway : IArmorGateway {
		const string ARMOR_YAML_FILE = "Data/Armors.yml";
		private IList<Armor> _armors;

		public ArmorYamlGateway() {
			LoadFromYaml (FileHelper.OpenYaml (ARMOR_YAML_FILE));
		}

		public ArmorYamlGateway (YamlNodeWrapper yaml) {
			LoadFromYaml (yaml);
		}

		public System.Collections.Generic.IEnumerable<Armor> All () {
			return _armors;
		}

		public Armor GetByName(string name) {
			return _armors.FirstOrDefault (x => x.Name == name);
		}

		public IEnumerable<Armor> FindByArmorType(ArmorType type) {
			return _armors.Where(x => x.ArmorType == type);
		}

		public IEnumerable<Armor> FindByArmorTypes(params ArmorType[] types) {
			return _armors.Where (
				x => types.Contains (x.ArmorType)
			);
		}

		private void LoadFromYaml(YamlNodeWrapper yaml) {
			_armors = new List<Armor> ();

			foreach (var node in yaml.Children()) {
				ShortLog.DebugFormat ("Loading Armor: {0}", node.GetString ("name"));
				var armor = new Armor (
					node.GetString("name"),
					node.GetInteger("armor_class"),
					node.GetFloat("weight"),
					node.GetInteger("maximum_dexterity_bonus"),
					node.GetInteger("armor_check_penalty"),
					node.GetInteger("arcane_spell_failure_chance"),
					node.GetEnum<ArmorType>("armor_type")
				);

				_armors.Add (armor);
			}
		}
	}
}

