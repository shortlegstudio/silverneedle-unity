using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorClassUI : MonoBehaviour {
	private const string ARMORCLASS_FORMAT = "AC: {0}\nTouch: {1}\nFlat-Footed: {2}";
	private Text textDisplay;
	private CharacterBuilder character;

	// Use this for initialization
	void Start () {
		textDisplay = GetComponent<Text> ();
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterModified;
	}

	void CharacterModified (object sender, System.EventArgs e) {
		var defense = character.CurrentCharacter.Defense;
		textDisplay.text = string.Format (
			ARMORCLASS_FORMAT,
			defense.ArmorClass(),
			defense.TouchArmorClass(),
			defense.FlatFootedArmorClass()
		);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
