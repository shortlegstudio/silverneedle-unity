using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SavingThrowsUI : MonoBehaviour {
	private CharacterBuilder character;
	private const string SVG_FORMAT = "Saving Throws\n\tFort:\t\t{0}\n\tReflex:\t{1}\n\tWill: \t\t{2}";
	private Text textbox;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterChanged;
		textbox = GetComponent<Text> ();
	}

	void CharacterChanged (object sender, System.EventArgs e) {
		textbox.text = string.Format (SVG_FORMAT,
			character.CurrentCharacter.Defense.FortitudeSave(),
			character.CurrentCharacter.Defense.ReflexSave(),
			character.CurrentCharacter.Defense.WillSave()
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
