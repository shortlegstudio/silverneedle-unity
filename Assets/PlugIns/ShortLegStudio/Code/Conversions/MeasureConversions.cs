using UnityEngine;
using System.Collections;


namespace ShortLegStudio.Conversions {
		
	public static class MeasureConversion  {
		public static string InchesToFeetString(int val) {
			var mod = val % 12;
			var ft = val / 12;
			return string.Format ("{0}' {1}\"", ft, mod);
		}

		public static string InchesToFeetString(float val) {
			var mod = val % 12;
			var ft = val / 12;
			return string.Format ("{0}' {1}\"", ft, mod);
		}
	}
}