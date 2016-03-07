using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	public class AbilityScore {
		public AbilityScoreTypes Name { get; set; }
		public int BaseValue { get; set; }
		public int BaseModifier {
			get {
				return BaseValue / 2 - 5;
			}
		}
		public int TotalValue { 
			get { return BaseValue; }
		}

		public AbilityScore() { }
		public AbilityScore(AbilityScoreTypes type, int val) {
			Name = type;
			BaseValue = val;
		}


		/// <summary>
		/// Generates an ability score by rolling 4d6 and taking the top 3
		/// </summary>
		/// <returns>The from4d6.</returns>
		/// <param name="types">Types.</param>
		public static AbilityScore CreateFrom4d6(AbilityScoreTypes types) {
			var diceCup = new Cup (Die.GetDice (DiceSides.d6, 4));
			diceCup.Roll ();
			return new AbilityScore (types, diceCup.SumTop (3));
		}

	}

	public enum AbilityScoreTypes {
		Strength,
		Dexterity,
		Constitution,
		Intelligence,
		Wisdom,
		Charisma
	}
}