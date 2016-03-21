using UnityEngine;
using System;
using System.Collections.Generic;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSheet {
		public string Name { get; set; }
		public CharacterAlignment Alignment { get; set; }
		public float Height { get; set; }
		public int Weight { get; set; }
		public IDictionary<AbilityScoreTypes, AbilityScore> AbilityScores { get; set; }
		public Race Race { get; set; }
		public Class Class { get; set; }

		public void SetAbilityScores(IList<AbilityScore> scores) {
			AbilityScores = new Dictionary<AbilityScoreTypes, AbilityScore> ();
			foreach (var ab in scores) {
				AbilityScores.Add (ab.Name, ab);
			}
		}
	}
}