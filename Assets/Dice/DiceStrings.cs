using UnityEngine;
using System;
using System.Linq;
using System.Collections;

namespace ShortLegStudio.Dice {
	public static class DiceStrings  {
		public static DiceSides ParseSides(string die) {
			return (DiceSides) Enum.Parse (typeof(DiceSides), die);
		}
	}
}
