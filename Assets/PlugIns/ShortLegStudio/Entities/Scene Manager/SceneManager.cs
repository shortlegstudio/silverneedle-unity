using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneManager : MonoBehaviour {
	
	public void ExitGame() {
		Application.Quit ();
	}

	public void StartGame() {
		ScoreTracker.Reset ();
		LoadNextLevel ();
	}

	public void LoadLevel(string levelName) {
		Debug.Log ("Load Level");
		UnityEngine.SceneManagement.SceneManager.LoadScene (levelName);
	}


	public void LoadNextLevel() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (Application.loadedLevel + 1);
	}

	public void LoadNextLevel(float delay) {
		Invoke ("LoadNextLevel", delay);
	}

	public static SceneManager GetInstance() {
		return FindObjectOfType<SceneManager> ();
	}

}
