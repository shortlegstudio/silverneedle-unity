using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using YamlDotNet.Core.Tokens;

namespace ShortLegStudio {
	public static class EnumHelpers {
		public static IEnumerable<T> GetValues<T>() {
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		public static T ChooseOne<T>() {
			return GetValues<T> ().ToList ().ChooseOne ();
		}

		public static bool TryParse<T> (string value, bool ignorecase, out T Parsed) {
			Parsed = default(T);
			try {
				var result = Enum.Parse(typeof(T), value, ignorecase);
				Parsed = (T)result;
				return true;
			}
			catch {
				return false;
			}
		}
	}
}
