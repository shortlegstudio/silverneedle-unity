using UnityEngine;
using System;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class AbilityScoreGenerator  {
		public static void RandomStandardHeroScores(AbilityScores abilities) {
			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				abilities.GetAbility (e).Roll4d6 ();
			}
		}
	}
}