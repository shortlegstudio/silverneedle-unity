using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;

namespace ShortLegStudio.Dice {
	public static class DiceStrings  {
		public static DiceSides ParseSides(string die) {
			return (DiceSides) Enum.Parse (typeof(DiceSides), die);
		}

		public static Cup ParseDice(string diceString) {
			var cup = new Cup ();
			//Split on d
			var regEx = new Regex("^(?<dieCount>\\d+)?d(?<dieSides>\\d+)(?<modifier>\\+\\d+)?");
			var match = regEx.Match(diceString);
			var dieCount = DefaultOrNumber(match.Groups ["dieCount"].Value, 1);
			var dieSides = DefaultOrNumber(match.Groups ["dieSides"].Value, 1);
			var modifier = DefaultOrNumber(match.Groups ["modifier"].Value, 0);

			var dice = Die.GetDice ((DiceSides)dieSides, dieCount);
			cup.AddDice (dice);
			cup.Modifier = modifier;

			return cup;
		}

		private static int DefaultOrNumber(string val, int def) {
			if (string.IsNullOrEmpty (val))
				return def;
			else
				return int.Parse (val);
		}
	}
}
