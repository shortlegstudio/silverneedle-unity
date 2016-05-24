using System;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Characters {
	public class Initiative : BasicStat
	{
		private AbilityScores _abilities;
		public Initiative (AbilityScores abilities) : base() {
			_abilities = abilities;
			_abilities.Modified += AbilitiesModified;
			UpdateInitiative ();
		}

		void AbilitiesModified (object sender, AbilityModifiedEventArgs e) {
			if (e.Ability.Name == AbilityScoreTypes.Dexterity)
				UpdateInitiative ();
		}

		private void UpdateInitiative() {
			SetValue( _abilities.GetModifier (AbilityScoreTypes.Dexterity));
		}

	}
}

