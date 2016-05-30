using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	public class Race {
		public string Name { get; set; }
		public IList<AbilityScoreAdjustment> AbilityModifiers { get; private set;  }
		public IList<string> Traits { get; private set; }
		public IList<string> KnownLanguages { get; private set; }
		public IList<string> AvailableLanguages { get; private set; }
		public CharacterSize SizeSetting { get; set; }
		public Cup HeightRange { get; set; }
		public Cup WeightRange { get; set; }
		public int BaseMovementSpeed { get; set; }

		public Race() {
			AbilityModifiers = new List<AbilityScoreAdjustment> ();
			Traits = new List<string> ();
			AvailableLanguages = new List<string> ();
			KnownLanguages = new List<string> ();
		}
	}
}