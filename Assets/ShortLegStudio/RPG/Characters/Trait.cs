using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters  {
	public class Trait : IModifiesSkills {
		
		public string Name { get; set; }
		public string Description { get; set; }
		public IList<SkillModifier> SkillModifiers { get; protected set; }

		public Trait() {
			SkillModifiers = new List<SkillModifier> ();
		}


	}


}