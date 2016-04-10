using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class HitPointsUI : MonoBehaviour {
	CharacterBuilder character;
	Text hpText;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += this.CharacterUpdate;
		hpText = GetComponent<Text> ();
	}

	void CharacterUpdate(object sender, EventArgs args) {
		hpText.text = character.CurrentCharacter.CurrentHitPoints.ToString ();
	}
}
