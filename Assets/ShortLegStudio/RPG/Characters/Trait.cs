using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;
using System.Configuration;

namespace ShortLegStudio.RPG.Characters  {
	public class Trait : IModifiesStats {
		
		public string Name { get; set; }
		public string Description { get; set; }
		public IList<BasicStatModifier> Modifiers { get; protected set; }
		public IList<string> Tags { get; protected set; }

		public Trait() {
			Modifiers = new List<BasicStatModifier> ();
			Tags = new List<string>();
		}


	}


}