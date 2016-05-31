﻿using ShortLegStudio;
using ShortLegStudio.Dice;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities {
	public class RandomAbilityScoreGenerator : IAbilityScoreGenerator  {
		public RandomAbilityScoreGenerator() { }

		public void AssignAbilities(AbilityScores abilities) {
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.SetScore (e, Roll4d6());
			}
		}

		private int Roll4d6() {
			var diceCup = new Cup (Die.GetDice (DiceSides.d6, 4));
		 	diceCup.Roll ();
			return diceCup.SumTop (3);
		}
	}
}