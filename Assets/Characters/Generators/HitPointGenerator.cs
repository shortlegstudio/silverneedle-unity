using UnityEngine;
using System;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class HitPointGenerator  {
		public static int RollHitPoints(CharacterSheet character) {
			//First Level is Max hit die + constitution bonus
			int hp = (int)character.Class.HitDice + character.GetAbilityModifier(AbilityScoreTypes.Constitution);
			return hp;
		}

		public static int RollLevelUp(CharacterSheet character) {
			var cup = new Cup ();
			cup.AddDie (new Die (character.Class.HitDice));
			cup.Modifier = character.GetAbilityModifier (AbilityScoreTypes.Constitution);
			return cup.Roll ();
		}
	}
}