using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters  {
	public class Trait : ISkillModifier {
		//Static Values
		const string TRAIT_DATA_FILE = "Data/traits.yml";
		static IList<Trait> _Traits = new List<Trait>();

		public string Name { get; set; }
		public string Description { get; set; }
		public IList<SkillAdjustment> SkillModifiers { get; protected set; }

		public Trait() {
			SkillModifiers = new List<SkillAdjustment> ();
		}

		public static IList<Trait> LoadFromYaml(YamlNodeWrapper yaml) {
			var traits = new List<Trait> ();

			foreach (var traitNode in yaml.Children()) {
				var trait = new Trait ();
				trait.Name = traitNode.GetString ("name"); 
				ShortLog.Debug ("Loading Trait: " + trait.Name);
				trait.Description = traitNode.GetString ("description");

				//Get Any skill Modifiers if they exist
				var skills = traitNode.GetNodeOptional("skillmodifiers");
				if (skills != null) {
					foreach (var skillAdj in skills.Children()) {
						var skillName = skillAdj.GetString("skill");
						var amount = int.Parse(skillAdj.GetString ("amount"));
						trait.SkillModifiers.Add(new SkillAdjustment(
							string.Format("{0} (trait)", trait.Name),
							amount,
							skillName
						));
					}
				}
				traits.Add (trait);
			}

			return traits;
		}

		public static Trait GetTrait(string name) {
			return GetTraits().First (x => x.Name == name); 
		}

		public static IList<Trait> GetTraits() {
			
			if (_Traits == null || _Traits.Count == 0) {
				var yaml = FileHelper.OpenYaml (TRAIT_DATA_FILE);
				_Traits = LoadFromYaml (yaml);
				ShortLog.Debug ("Loaded Traits: " + _Traits.Count);
			}
			return _Traits;
		}

		public static void SetTraits(IList<Trait> traits) {
			_Traits = traits;
		}
	}


}