using UnityEngine;
using System.Collections;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class LevelUpGenerator  {
		public static void BringCharacterToLevel(CharacterSheet character, int targetLevel) {
			for (int i = character.Level; i <= targetLevel; i++) {
				LevelUp(character);
			}
		}

		public static void LevelUp(CharacterSheet character) {
			character.SetLevel (character.Level + 1);
			var incrementHitpoints = HitPointGenerator.RollLevelUp (character);
			character.MaxHitPoints += incrementHitpoints;
			character.CurrentHitPoints += incrementHitpoints;
			character.UpdateSaveStats ();


			//Special Level ups
			if (character.Level % 4 == 0) {
				AssignAbilityPoints (character);
			}
		}

		private static void AssignAbilityPoints(CharacterSheet character) {
			var ability = EnumHelpers.ChooseOne<AbilityScoreTypes>();
			var adjust = new AbilityScoreAdjustment();
			adjust.Reason = "Level Up";
			adjust.Modifier = 1;
			adjust.ability = ability;

			character.Abilities.GetAbility(ability).AddAdjustment(adjust);
		}
	}
}
