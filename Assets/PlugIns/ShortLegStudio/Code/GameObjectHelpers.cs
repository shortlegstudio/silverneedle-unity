using UnityEngine;
using System.Collections;

namespace ShortLegStudio {
	public static class GameObjectHelpers {
		public static GameObject FindOrCreate(string name) {
			GameObject obj = GameObject.Find (name);
			if (!obj) {
				obj = new GameObject (name);
			}

			return obj;
		}

		public static void Organize(string folderName, GameObject obj) {
			var parent = FindOrCreate (folderName);
			obj.transform.SetParent (parent.transform);
		}
	}
}