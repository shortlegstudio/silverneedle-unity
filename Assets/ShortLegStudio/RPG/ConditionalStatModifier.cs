using System;

namespace ShortLegStudio.RPG {
	public class ConditionalStatModifier : BasicStatModifier {
		public string Condition { get; set; }

		public ConditionalStatModifier(string condition, string stat, float mod, string type, string reason) : base(stat, mod, type, reason) {
			Condition = condition;
		}
	}
}

