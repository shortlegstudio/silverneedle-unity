using System;

namespace ShortLegStudio.RPG
{
	public class ConditionalStatModifier {
		public string Condition { get; set; }
		public int Modifier { get; set; }
		public string Reason { get; set; }

		public ConditionalStatModifier(int modifier, string reason, string condition) {
			Modifier = modifier;
			Reason = reason;
			Condition = condition;			
		}
	}
}

