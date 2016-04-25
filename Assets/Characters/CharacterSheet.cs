using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSheet {
		// Basic Stats
		public string Name { get; set; }
		public CharacterAlignment Alignment { get; set; }
		public int Height { get; set; }
		public int Weight { get; set; }

		//Race and Class
		public Race Race { get; protected set; }
		public Class Class { get; set; }

		//Abilities
		public int Level { get; private set; }
		public IDictionary<AbilityScoreTypes, AbilityScore> AbilityScores { get; set; }
		private IDictionary<string, CharacterSkill> Skills { get; set; }
		public IList<Trait> Traits { get; private set; }
		public IList<Feat> Feats { get; private set; }

		//Combat Related
		public int MaxHitPoints { get; set; }
		public int CurrentHitPoints { get; set; } 

		public event EventHandler<CharacterSheetEventArgs> Modified;

		public CharacterSheet() {
			AbilityScores = new Dictionary<AbilityScoreTypes, AbilityScore> ();
			Skills = new Dictionary<string, CharacterSkill> ();
			Traits = new List<Trait> ();
			Feats = new List<Feat> ();
			Level = 1;
		}

		/// <summary>
		/// Sets the ability scores.
		/// </summary>
		/// <param name="scores">Scores.</param>
		public void SetAbilityScores(IList<AbilityScore> scores) {
			foreach (var ab in scores) {
				SetAbility (ab, false);
			}
			NotifyModified ();
		}

		/// <summary>
		/// Sets the ability.
		/// </summary>
		/// <param name="score">Score.</param>
		public void SetAbility(AbilityScore score, bool notify = true) {
			if (AbilityScores.ContainsKey (score.Name)) {
				AbilityScores [score.Name] = score;
			} else {
				AbilityScores.Add (score.Name, score);
			}

			if (notify)
				NotifyModified ();
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
			return AbilityScores [ability].TotalModifier;
		}

		public void SetRace(Race race) {
			Race = race;

			//Add Ability Modifiers
			foreach (var adj in race.AbilityModifiers) {
				if (adj.RacialChose) {
					var rand = AbilityScores.Values.ToArray().ChooseOne();
					rand.AddAdjustment (adj);
				} else {
					var abl = GetAbility (adj.ability);
					abl.AddAdjustment (adj);
				}
			}

			//Add Traits
			foreach (var trait in race.Traits) {
				AddTrait (trait, false);
			}
			NotifyModified ();
		}

		public void AddTrait(string trait, bool notify = true) {
			var traitInfo = Trait.GetTrait (trait);
			AddTrait (traitInfo, notify);

		}

		public void AddTrait(Trait trait, bool notify = true) {
			Traits.Add (trait);

			if (notify) {
				NotifyModified ();
			}
		}

		public void AddFeat(Feat feat, bool notify = true) {
			Feats.Add (feat);
			if (notify) {
				NotifyModified ();
			}
		}

		public void SetSkills(IList<Skill> skills) {
			foreach (var s in skills) {
				Skills.Add (
					s.Name,
					new CharacterSkill (s, this)
				);
			}
			NotifyModified ();
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

		public IList<SkillAdjustment> FindSkillAdjustments(string name) {

			//Traits
			var adjustments = Traits.SelectMany (x =>
				x.SkillModifiers.Where (y => y.SkillName == name)
			).Concat(
				Feats.SelectMany (x =>
					x.SkillModifiers.Where (y => y.SkillName == name)
				)
			);


			return adjustments.ToList();
		}

		public bool IsClassSkill(string name) {
			if (Class == null)
				return false;

			return Class.IsClassSkill (name);
		}

		public int GetSkillPointsPerLevel() {
			return Class.SkillPoints + GetAbilityModifier (AbilityScoreTypes.Intelligence);
		}

		public void SetHitPoints(int hp) {
			MaxHitPoints = hp;
			CurrentHitPoints = hp;
		}

		private void NotifyModified() {
			if (Modified != null) { 
				var args = new CharacterSheetEventArgs ();
				args.Sheet = this;
				Modified (this, args);
			}
		}
	}

	public class CharacterSheetEventArgs : EventArgs {
		public CharacterSheet Sheet { get; set; }
	}
}