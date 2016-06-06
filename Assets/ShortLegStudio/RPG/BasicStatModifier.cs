using System;

namespace ShortLegStudio.RPG {
	public class BasicStatModifier {
		public float Modifier { get; set; }
		public string Reason { get; set; }
		public string Type { get; set; }
		public string StatName { get; set; }

		public BasicStatModifier() { }

		public BasicStatModifier(float mod, string reas) {
			Modifier = mod;
			Reason = reas;
		}

		public BasicStatModifier(string stat, float mod, string type, string reason) {
			Modifier = mod;
			StatName = stat;
			Type = type;
			Reason = reason;
		}
	}
}

