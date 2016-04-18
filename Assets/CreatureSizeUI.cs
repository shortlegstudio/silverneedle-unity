using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShortLegStudio.Conversions;

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
		size.text = character.CurrentCharacter.Race.Size.ToString();
		height.text = MeasureConversion.InchesToFeetString(character.CurrentCharacter.Height);
		weight.text = string.Format ("{0} lbs", character.CurrentCharacter.Weight);
	}
}
