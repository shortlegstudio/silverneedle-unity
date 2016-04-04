﻿using UnityEngine;
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
		private IDictionary<string, CharacterSkill> Skills { get; set; }

		public CharacterSheet() {
			AbilityScores = new Dictionary<AbilityScoreTypes, AbilityScore> ();
			Skills = new Dictionary<string, CharacterSkill> ();
		}


		/// <summary>
		/// Sets the ability scores.
		/// </summary>
		/// <param name="scores">Scores.</param>
		public void SetAbilityScores(IList<AbilityScore> scores) {
			foreach (var ab in scores) {
				SetAbility (ab);
			}
		}

		/// <summary>
		/// Sets the ability.
		/// </summary>
		/// <param name="score">Score.</param>
		public void SetAbility(AbilityScore score) {
			if (AbilityScores.ContainsKey (score.Name)) {
				AbilityScores [score.Name] = score;
			} else {
				AbilityScores.Add (score.Name, score);
			}
		}

		/// <summary>
		/// Sets the ability.
		/// </summary>
		/// <param name="ability">Ability.</param>
		/// <param name="score">Score.</param>
		public void SetAbility(AbilityScoreTypes ability, int score) {
			SetAbility (new AbilityScore (ability, score));
		}

		/// <summary>
		/// Gets the ability score.
		/// </summary>
		/// <returns>The ability score.</returns>
		/// <param name="name">Name.</param>
		public AbilityScore GetAbility(AbilityScoreTypes name) {
			return AbilityScores [name];
		}

		public int GetAbilityScore(AbilityScoreTypes ability) {
			return AbilityScores [ability].TotalValue;
		}

		/// <summary>
		/// Gets the ability modifier.
		/// </summary>
		/// <returns>The ability modifier.</returns>
		/// <param name="ability">Ability.</param>
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

		public CharacterSkill GetSkill(Skill skill) {
			return Skills [skill.Name];
		}

		public int GetSkillValue(string name) {
			return Skills [name].Score;
		}

		public IList<CharacterSkill> GetSkillList() {
			return Skills.Values.ToList ();
		}
	}

}