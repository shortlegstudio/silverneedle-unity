using UnityEngine;
using System.Collections;

public class LoadNextDelay : MonoBehaviour {
	public float loadDelay;

	void Start () {
		Invoke ("LoadLevel", loadDelay);
	}

	void LoadLevel() {
		LevelManager.GetInstance ().LoadNextLevel ();
	}
}
