using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreatureSizeUI : MonoBehaviour {
	public Text size;
	public Text height;
	public Text weight;
	CharacterBuilder character;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterChanged;
	}
	
	void CharacterChanged (object sender, System.EventArgs e) {
				
	}
}
