using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class Feat {
		//Static Values
		const string TRAIT_DATA_FILE = "Data/feats.yml";
		static IList<Feat> _Feat = new List<Feat>();

		public string Name { get; set; }
		public string Description { get; set; }
		public IList<SkillAdjustment> SkillModifiers { get; protected set; }
		public Prerequisites Prerequisites { get; protected set; }
		public bool IsCombatFeat { get; set; }

		public Feat() {
			SkillModifiers = new List<SkillAdjustment> ();
			Prerequisites = new Prerequisites ();
		}

		public bool Qualified(CharacterSheet character) {
			return Prerequisites.Qualified(character);
		}

		public static IList<Feat> LoadFromYaml(YamlNodeWrapper yaml) {
			var feats = new List<Feat> ();

			foreach (var featNode in yaml.Children()) {
				var feat = new Feat ();
				feat.Name = featNode.GetString ("name"); 
				Debug.LogFormat ("Loading Feat: {0}" , feat.Name);
				feat.Description = featNode.GetString ("description");

				//Get Any skill Modifiers if they exist
				var skills = featNode.GetNodeOptional("skillmodifiers");
				if (skills != null) {
					foreach (var skillAdj in skills.Children()) {
						var skillName = skillAdj.GetString("skill");
						var amount = int.Parse(skillAdj.GetString ("amount"));
						feat.SkillModifiers.Add(new SkillAdjustment(
							string.Format("{0} (feat)", feat.Name),
							amount,
							skillName
						));
					}
				}

				//Get Any Prerequisites
				var prereq = featNode.GetNodeOptional("prerequisites");
				if(prereq != null)
					feat.Prerequisites = new Prerequisites (prereq);


				feat.IsCombatFeat = featNode.GetStringOptional ("combat") == "yes";
				feats.Add (feat);
			}

			return feats;
		}

		public static Feat GetFeat(string name) {
			return GetFeats().First (x => x.Name == name); 
		}

		public static IList<Feat> GetFeats() {
			
			if (_Feat == null || _Feat.Count == 0) {
				var yaml = FileHelper.OpenYaml (TRAIT_DATA_FILE);
				_Feat = LoadFromYaml (yaml);
				Debug.Log ("Loaded Traits: " + _Feat.Count);
			}
			return _Feat;
		}

		public static IEnumerable<Feat> GetQualifyingFeats(CharacterSheet character) {
			return GetFeats ().Where (x => x.Qualified (character));
		}

		public static void SetFeats(IList<Feat> feats) {
			_Feat = feats;
		}
	}


}