using UnityEngine;
using System.Collections;

namespace ShortLegStudio {
	public static class VectorHelpers {
		/// <summary>
		/// Takes a vector3 and rounds all coordinates to nearest integer
		/// </summary>
		/// <returns>The to grid.</returns>
		/// <param name="pos">Position.</param>
		public static Vector3 SnapToGrid(Vector3 pos) {
			return new Vector3 (
				Mathf.RoundToInt(pos.x),
				Mathf.RoundToInt(pos.y),
				Mathf.RoundToInt(pos.z)
			);
		}

		public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max) {
			return new Vector3 (
				Mathf.Clamp (value.x, min.x, max.x),
				Mathf.Clamp (value.y, min.y, max.y),
				Mathf.Clamp (value.z, min.z, max.z)
			);
		}
	}
}