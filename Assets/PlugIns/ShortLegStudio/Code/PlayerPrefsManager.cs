using UnityEngine;
using System.Collections;

public class PlayerPrefsManager {
	const string MASTER_VOLUME_KEY = "MasterVolume";
	const string DIFFICULTY_KEY = "Difficulty";

	public static float GetMasterVolume() {
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY); 
	}

	public static void SetMasterVolume(float volume) {
		PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, 
		                      Mathf.Clamp (volume, 0, 1)); 
	}

	public static float GetDifficulty() {
		return PlayerPrefs.GetInt (DIFFICULTY_KEY);
	}

	public static void SetDifficulty(int difficulty) {
		PlayerPrefs.SetInt (DIFFICULTY_KEY, Mathf.Clamp(difficulty,1, 3));
	}
}
 