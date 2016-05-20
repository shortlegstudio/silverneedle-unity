using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseAttackBonusUI : MonoBehaviour {
	private CharacterBuilder character;
	private const string BAB_FORMAT = "Base Attack Bonus: {0}\n\tMelee: {1}\n\tRange: {2}\nCMB: {3}\nCMD: {4}";
	private Text textbox;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterChanged;
		textbox = GetComponent<Text> ();
	}

	void CharacterChanged (object sender, System.EventArgs e) {
		var offense = character.CurrentCharacter.Offense;
		textbox.text = string.Format (BAB_FORMAT, 
			offense.BaseAttackBonus.TotalValue,
			offense.MeleeAttackBonus(),
			offense.RangeAttackBonus(),
			offense.CombatManueverBonus(),
			offense.CombatManueverDefense()
		);
	}

}
