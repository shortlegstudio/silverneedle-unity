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
		var abilities = character.CurrentCharacter.Abilities;

		Score.text = abilities.GetScore (Ability).ToString ();
		Modifier.text = abilities.GetModifier (Ability).ToString ();
		Modifier.color = Color.white;
		if (abilities.GetModifier (Ability) > 0)
			Modifier.color = Color.green;
		if (abilities.GetModifier (Ability) < 0)
			Modifier.color = Color.red;
	}

	public void MouseEnter() {
		if (character.CurrentCharacter == null)
			return;
		
		var ability = character.CurrentCharacter.Abilities.GetAbility (Ability);
		Tooltip.ShowTip(
			Ability.ToString(),
			Score.text + " (" + Modifier.text + ")",
			ability.ToString()
		);

	}
}
