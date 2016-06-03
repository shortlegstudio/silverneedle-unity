using System;

namespace ShortLegStudio.RPG {
	public class BasicStatModifier {
		public float Modifier { get; set; }
		public string Reason { get; set; }

		public BasicStatModifier() { }

		public BasicStatModifier(float mod, string reas) {
			Modifier = mod;
			Reason = reas;
		}
	}
}

