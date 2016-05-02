using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelExperienceUI : MonoBehaviour {
	const string LVL_FORMAT = "Level: {0}\t\tXP: {1}";
	CharacterBuilder character;
	Text textbox;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterChanged;;
		textbox = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CharacterChanged (object sender, System.EventArgs e)
	{
		textbox.text = string.Format (LVL_FORMAT,
			character.CurrentCharacter.Level,
			character.CurrentCharacter.XP
		);
	}
}
