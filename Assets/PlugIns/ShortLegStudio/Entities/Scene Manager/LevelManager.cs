using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public void ExitGame() {
		Application.Quit ();
	}

	public void StartGame() {
		LoadNextLevel ();
	}

	public void LoadLevel(string levelName) {
		SceneManager.LoadScene (levelName);
	}


	public void LoadNextLevel() {
		SceneManager.LoadScene (ActiveSceneIndex + 1);
	}

	public void LoadNextLevel(float delay) {
		Invoke ("LoadNextLevel", delay);
	}

	public int ActiveSceneIndex {
		get {
			return SceneManager.GetActiveScene ().buildIndex;
		}
	}

	public static LevelManager GetInstance() {
		return FindObjectOfType<LevelManager> ();
	}

}
