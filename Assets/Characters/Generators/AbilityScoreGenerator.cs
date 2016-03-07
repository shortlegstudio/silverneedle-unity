using UnityEngine;
using System;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class AbilityScoreGenerator  {
		public static IList<AbilityScore> RandomStandardHeroScores() {
			var list = new List<AbilityScore> ();

			foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>()) {
				list.Add (AbilityScore.CreateFrom4d6 (e));	
			}

			return list;
		}
	}
}