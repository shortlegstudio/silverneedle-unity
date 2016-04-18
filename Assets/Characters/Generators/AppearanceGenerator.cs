using UnityEngine;
using System;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class AppearanceGenerator  {
		public static int RollHeight(CharacterSheet character) {
			return character.Race.HeightRange.Roll ();
		}

		public static int RollWeight(CharacterSheet character) {
			return character.Race.WeightRange.Roll ();
		}
	}
}