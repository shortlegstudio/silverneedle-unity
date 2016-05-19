using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseAttackBonusUI : MonoBehaviour {
	private CharacterBuilder character;
	private const string BAB_FORMAT = "Base Attack Bonus: {0}\n\tMelee: {1}\n\tRange: {2}";
	private Text textbox;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterChanged;
		textbox = GetComponent<Text> ();
	}

	void CharacterChanged (object sender, System.EventArgs e) {
		textbox.text = string.Format (BAB_FORMAT, 
			character.CurrentCharacter.Offense.BaseAttackBonus.TotalValue,
			character.CurrentCharacter.Offense.MeleeAttackBonus(),
			character.CurrentCharacter.Offense.RangeAttackBonus()
		);
	}

}
