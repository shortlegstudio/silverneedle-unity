using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using ShortLegStudio.RPG.Characters;

public class AbilityScoreGUI : MonoBehaviour {
	public AbilityScoreTypes Ability;
	public Text Name;
	public Text Score;
	public Text Modifier;
	private CharacterBuilder character;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterChanged;
		Name.text = Ability.ToString ();
	}
	
	void CharacterChanged(object sender, EventArgs args) {
		var c = character.CurrentCharacter;

		Score.text = c.GetAbilityScore (Ability).ToString ();
		Modifier.text = c.GetAbilityModifier (Ability).ToString ();
		Modifier.color = Color.white;
		if (c.GetAbilityModifier (Ability) > 0)
			Modifier.color = Color.green;
		if (c.GetAbilityModifier (Ability) < 0)
			Modifier.color = Color.red;
	}
}
