using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {
	public float fadeInTime;

	// Fade in and then remove object
	void Start () {
		Image alphaChannel = GetComponent<Image> ();
		alphaChannel.CrossFadeAlpha (0, fadeInTime, true);
		Destroy (this.gameObject, fadeInTime);
	}
}
