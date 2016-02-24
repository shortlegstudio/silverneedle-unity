using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JustOneInstance : MonoBehaviour {
	public string instanceID;
	private static Dictionary<string, GameObject> instance = new Dictionary<string, GameObject >();

	void Awake() {
		if (!instance.ContainsKey(instanceID) || instance[instanceID] == null) {
			instance [instanceID] = this.gameObject;
			GameObject.DontDestroyOnLoad(this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}

	public static GameObject GetInstance(string name) {
		if (instance.ContainsKey (name)) {
			return instance [name];
		}

		return null;
	}
}
