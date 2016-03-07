using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ShortLegStudio {
	public static class EnumHelpers {
		public static IEnumerable<T> GetValues<T>() {
			return Enum.GetValues(typeof(T)).Cast<T>();
		}
	}
}
