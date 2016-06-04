using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters.Skills;

namespace ShortLegStudio.RPG.Characters {
	public class Feat : ISkillModifier {
		//Static Values
		const string TRAIT_DATA_FILE = "Data/feats.yml";
		static IList<Feat> _Feats = new List<Feat>();

		public string Name { get; set; }
		public string Description { get; set; }
		public IList<SkillAdjustment> SkillModifiers { get; protected set; }
		public Prerequisites Prerequisites { get; protected set; }
		public bool IsCombatFeat { get { return Tags.Contains ("combat"); } }
		public bool IsCriticalFeat { get { return Tags.Contains ("critical"); } }
		public bool IsItemCreation { get { return Tags.Contains ("itemcreation"); } }
		public IList<string> Tags { get; set; }
		public PathfinderRuleset Ruleset { get; set; }

		public Feat() {
			SkillModifiers = new List<SkillAdjustment> ();
			Prerequisites = new Prerequisites ();
			Tags = new List<string> ();
			Ruleset = PathfinderRuleset.Core;
		}

		public bool Qualified(CharacterSheet character) {
			return Prerequisites.Qualified(character);
		}

		public static IList<Feat> LoadFromYaml(YamlNodeWrapper yaml) {
			var feats = new List<Feat> ();

			foreach (var featNode in yaml.Children()) {
				var feat = new Feat ();
				feat.Name = featNode.GetString ("name"); 
				ShortLog.DebugFormat ("Loading Feat: {0}" , feat.Name);
				feat.Description = featNode.GetString ("description");

				//Get Any skill Modifiers if they exist
				var skills = featNode.GetNodeOptional("skillmodifiers");
				if (skills != null) {
					foreach (var skillAdj in skills.Children()) {
						var val = Regex.Split(skillAdj.Value, "\\s*=\\s*");
						var skillName = val[0];
						var amount = int.Parse(val[1]);
						feat.SkillModifiers.Add(new SkillAdjustment(
							amount,
							string.Format("{0} (feat)", feat.Name),
							skillName
						));
					}
				}

				//Get Any Prerequisites
				var prereq = featNode.GetNodeOptional("prerequisites");
				if(prereq != null)
					feat.Prerequisites = new Prerequisites (prereq);

				feat.Tags = featNode.GetCommaStringOptional ("tags").ToList ();
				feats.Add (feat);
			}

			return feats;
		}

		public static Feat GetFeat(string name) {
			return GetFeats().First (x => x.Name == name); 
		}

		public static IList<Feat> GetFeats() {
			
			if (_Feats == null || _Feats.Count == 0) {
				var yaml = FileHelper.OpenYaml (TRAIT_DATA_FILE);
				_Feats = LoadFromYaml (yaml);
				ShortLog.Debug ("Loaded Traits: " + _Feats.Count);
			}
			return _Feats;
		}

		public static IEnumerable<Feat> GetQualifyingFeats(CharacterSheet character) {
			return GetFeats ().Where (x => x.Qualified (character) && !character.Feats.Contains(x));
		}

		public static IEnumerable<Feat> GetQualifyingFeats(CharacterSheet character, string tag) {
			return GetQualifyingFeats (character).Where(x => 
				x.Tags.Contains(tag) || string.IsNullOrEmpty(tag));
		}

		public static void SetFeats(IList<Feat> feats) {
			_Feats = feats;
		}
	}


}