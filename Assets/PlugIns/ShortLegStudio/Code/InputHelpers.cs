using UnityEngine;
using System.Collections;

namespace ShortLegStudio {
	public static class InputHelpers {
		public static Vector3 GetMouseWorldCoordinates2D() {
			var mouse = Input.mousePosition;
			mouse.z = mouse.z - Camera.main.transform.position.z;
			return Camera.main.ScreenToWorldPoint (mouse);
		}
	}
}