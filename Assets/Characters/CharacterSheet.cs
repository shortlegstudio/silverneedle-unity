using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSheet {
		public string Name { get; set; }
		public CharacterAlignment Alignment { get; set; }
		public float Height { get; set; }
		public int Weight { get; set; }
		public IDictionary<AbilityScoreTypes, AbilityScore> AbilityScores { get; set; }
		public Race Race { get; set; }
		public Class Class { get; set; }
		public IDictionary<string, CharacterSkill> Skills { get; set; }

		public CharacterSheet() {
			AbilityScores = new Dictionary<AbilityScoreTypes, AbilityScore> ();
			Skills = new Dictionary<string, CharacterSkill> ();
		}

		public void SetAbilityScores(IList<AbilityScore> scores) {
			foreach (var ab in scores) {
				SetAbility (ab);
			}
		}

		public void SetAbility(AbilityScore score) {
			if (AbilityScores.ContainsKey (score.Name)) {
				AbilityScores [score.Name] = score;
			} else {
				AbilityScores.Add (score.Name, score);
			}
		}

		public AbilityScore GetAbilityScore(AbilityScoreTypes name) {
			return AbilityScores [name];
		}

		public int GetAbilityModifier(AbilityScoreTypes ability) {
			return AbilityScores [ability].BaseModifier;
		}

		public void SetSkills(IList<Skill> skills) {
			foreach (var s in skills) {
				Skills.Add (
					s.Name,
					new CharacterSkill (s, this)
				);
			}
		}

		public int GetSkillValue(string name) {
			return Skills [name].Score;
		}
	}

}