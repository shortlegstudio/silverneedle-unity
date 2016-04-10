using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SkillRanksUI : MonoBehaviour {
	CharacterBuilder character;
	Text SkillPointsText;
	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += this.CharacterChangedEvent;
		SkillPointsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void RefreshUI () {
		SkillPointsText.text = character.CurrentCharacter.GetSkillPointsPerLevel ().ToString ();
	}

	void CharacterChangedEvent(object source, EventArgs e) {
		RefreshUI ();
	}
}
