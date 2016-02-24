using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public AudioClip[] LevelMusic;
	private AudioSource _audioSource;

	void OnLevelWasLoaded(int level) {
		if (level >= 0 && level < LevelMusic.Length) {
			var clip = LevelMusic [level];
			if (clip && _audioSource.clip != clip) {
				_audioSource.loop = true;
				_audioSource.clip = clip;
				_audioSource.Play ();
			}
		}
	}

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
		SetVolume (PlayerPrefsManager.GetMasterVolume ());
	}
	
	public void SetVolume(float volume) {
		_audioSource.volume = volume;
	}
}
