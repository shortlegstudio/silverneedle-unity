using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class Prerequisites : List<Prerequisite> {
		string[] PREREQ_KEYS = { "ability", "race", "feat" };
		public Prerequisites() {
		}

		public Prerequisites(YamlNodeWrapper yaml) {
			ParseYaml (yaml);
		}

		private void ParseYaml(YamlNodeWrapper yaml) {
			foreach (var prereq in yaml.Children()) {
				Prerequisite newreq = null;

				foreach (var key in PREREQ_KEYS) {
					var val = prereq.GetStringOptional(key);
					if (val != null) {
						switch(key) {
						case "ability":
							newreq = new AbilityPrerequisite (val);
							break;
						case "race":
							newreq = new RacePrerequisite (val);
							break;
						case "feat":
							newreq = new FeatPrerequisite (val);
							break;
						}
						break;
					}
				}
					
				if (newreq != null)
					Add(newreq);
			}
		}
	}

	public abstract class Prerequisite {
		
	}

	public class AbilityPrerequisite : Prerequisite {
		public AbilityScoreTypes Ability { get; set; }
		public int Minimum { get; set; }

		public AbilityPrerequisite(string req) {
			var vals = req.Split (' ');
			Ability = (AbilityScoreTypes)System.Enum.Parse(typeof(AbilityScoreTypes), vals[0]);
			Minimum = int.Parse (vals [1]);
		}
	}

	public class RacePrerequisite : Prerequisite {
		public string Race { get; set; }

		public RacePrerequisite(string value) {
			Race = value;
		}
	}

	public class FeatPrerequisite : Prerequisite {
		public string Feat { get; set; }

		public FeatPrerequisite(string value) {
			Feat = value;
		}
	}
}