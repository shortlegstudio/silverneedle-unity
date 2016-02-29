using UnityEngine;
using System.Collections;

namespace ShortLegStudio {
	public static class StringExtensions {
		public static string Capitalize(this string source) {
			if (string.IsNullOrEmpty (source)) {
				return source;
			}

			return char.ToUpper (source [0]) + source.Substring (1);
		}
	}
}