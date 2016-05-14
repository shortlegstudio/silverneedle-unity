using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters {
	public interface IAbilityScores {
		AbilityScore GetAbility (AbilityScoreTypes ability);
		AbilityScore GetAbility (string ability);
		int GetScore (AbilityScoreTypes ability);
		int GetScore (string ability);
		int GetModifier (AbilityScoreTypes ability);
		int GetModifier (string ability);
		void SetScore (AbilityScoreTypes ability, int val);
		void SetScore (string ability, int val);
		IEnumerable<AbilityScore> GetAbilities();
	}

	public class AbilityScores : IAbilityScores {
		private Dictionary<AbilityScoreTypes, AbilityScore> _abilities;

		public AbilityScores () {
			FillAbilities ();
		}

		public AbilityScore GetAbility(AbilityScoreTypes ability) {
			return _abilities [ability];
		}

		public AbilityScore GetAbility(string ability) {
			return GetAbility (AbilityScore.GetType (ability));
		}

		public int GetScore(AbilityScoreTypes ability) {
			return _abilities [ability].TotalValue;
		}

		public int GetScore(string ability) {
			return GetScore (AbilityScore.GetType (ability));
		}

		public void SetScore(AbilityScoreTypes ability, int val) {
			_abilities [ability].SetValue (val);
		}

		public void SetScore(string ability, int val) {
			SetScore (AbilityScore.GetType (ability), val);
		}

		public int GetModifier(AbilityScoreTypes ability) {
			return _abilities [ability].TotalModifier;
		}

		public int GetModifier(string ability) {
			return GetModifier (AbilityScore.GetType (ability));
		}
		 
		public IEnumerable<AbilityScore> GetAbilities() {
			return _abilities.Values;
		}
		private void FillAbilities() {
			_abilities = new Dictionary<AbilityScoreTypes, AbilityScore> ();
			foreach (var v in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				_abilities.Add (v, new AbilityScore (v, 0));
			}
		}
	}
}

