using UnityEngine;
using System.Collections.Generic;


namespace ShortLegStudio.RPG.Characters.ClassFeatures {
	public class BonusFeat : ClassFeature  {
		public string TagGroup { get; set; }



		public IEnumerable<Feat> GetFeats(CharacterSheet character) {
			return Feat.GetQualifyingFeats (character, TagGroup);
		}
	}
}