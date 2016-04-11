using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	public class AbilityScore {
		public AbilityScoreTypes Name { get; set; }
		public IList<AbilityScoreAdjustment> Adjustments { get; protected set; }
		public int BaseValue { get; set; }
		public int BaseModifier {
			get {
				return CalculateModifier (BaseValue);
			}
		}
		public int TotalValue { 
			get { 
				return BaseValue + SumAdjustments; 
			}
		}
		public int TotalModifier {
			get {
				return CalculateModifier (TotalValue);
			}
		}
		public int SumAdjustments { get; private set; }

		public AbilityScore() { Adjustments = new List<AbilityScoreAdjustment> (); }
		public AbilityScore(AbilityScoreTypes type, int val) {
			Name = type;
			BaseValue = val;
			Adjustments = new List<AbilityScoreAdjustment> ();
		}

		public void AddAdjustment(AbilityScoreAdjustment adjustment) {
			Adjustments.Add (adjustment);
			SumAdjustments = Adjustments.Sum (x => x.value);
		}

		public override string ToString ()
		{
			return string.Format ("[AbilityScore: Name={0}, Adjustments={1}, BaseValue={2}, BaseModifier={3}, TotalValue={4}, TotalModifier={5}, SumAdjustments={6}]", Name, Adjustments, BaseValue, BaseModifier, TotalValue, TotalModifier, SumAdjustments);
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

		public static AbilityScoreTypes GetType(string name) {
			return (AbilityScoreTypes)System.Enum.Parse (typeof(AbilityScoreTypes), name, true);
		}

		public static int CalculateModifier(int val) {
			return val / 2 - 5;
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

	public struct AbilityScoreAdjustment {
		public bool RacialChose;
		public AbilityScoreTypes ability;
		public int value;
		public string reason;
	}
}