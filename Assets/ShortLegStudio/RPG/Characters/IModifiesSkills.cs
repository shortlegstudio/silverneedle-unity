using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters {
	public interface IModifiesSkills {
		IList<SkillModifier> SkillModifiers { get; }
		IList<ConditionalSkillModifier> ConditionalModifiers { get; }
	}
}

