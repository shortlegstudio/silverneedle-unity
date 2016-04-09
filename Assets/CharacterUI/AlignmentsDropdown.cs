using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShortLegStudio.RPG.Characters;

public class AlignmentsDropdown : MonoBehaviour {
	public Dropdown list;

	// Use this for initialization
	void Start () {
		list = GetComponent<Dropdown> ();
		var vals = System.Enum.GetNames (typeof(CharacterAlignment));
		foreach (var s in vals) {
			list.options.Add (new Dropdown.OptionData(s));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
