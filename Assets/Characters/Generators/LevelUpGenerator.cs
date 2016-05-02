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
		}
	}
}
