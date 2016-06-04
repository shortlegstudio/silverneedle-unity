using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters.Skills {
	public interface ISkillModifier {
		IList<SkillAdjustment> SkillModifiers { get; }
	}
}

