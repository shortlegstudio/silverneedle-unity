using System;
using ShortLegStudio.RPG;

namespace ShortLegStudio.RPG.Characters {
	public class ConditionalSkillModifier : ConditionalStatModifier {
		public string SkillName { get; private set; }
		public ConditionalSkillModifier(int modifier, string reason, string name, string condition) : base(modifier, reason, condition) {
			SkillName = name;
		}
	}
}

