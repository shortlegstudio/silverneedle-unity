using System.Collections.Generic;
using ShortLegStudio.Dice;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public static class HitPointGenerator  {
		public static int RollHitPoints(CharacterSheet character) {
			//First Level is Max hit die + constitution bonus
			int hp = (int)character.Class.HitDice + character.Abilities.GetModifier(AbilityScoreTypes.Constitution);
			return hp;
		}

		public static int RollLevelUp(CharacterSheet character) {
			var cup = new Cup ();
			cup.AddDie (new Die (character.Class.HitDice));
			cup.Modifier = character.Abilities.GetModifier (AbilityScoreTypes.Constitution);
			return cup.Roll ();
		}
	}
}