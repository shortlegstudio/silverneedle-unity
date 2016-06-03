using System;

namespace ShortLegStudio.RPG
{
	public class ConditionalStatModifier {
		public string Condition { get; private set; }
		public int Modifier { get; private set; }
		public string Reason { get; private set; }

		public ConditionalStatModifier(int modifier, string reason, string condition) {
			Modifier = modifier;
			Reason = reason;
			Condition = condition;			
		}
	}
}

