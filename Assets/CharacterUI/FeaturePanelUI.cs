using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class FeaturePanelUI : MonoBehaviour {
	CharacterBuilder character;
	public GameObject FeatureUI;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += CharacterChanged;
	}

	void CharacterChanged (object sender, EventArgs e) {
		//Clear everything out
		ClearTraitList();
		BuildTraitList ();

	}

	void ClearTraitList() {
		var elements = GetComponentsInChildren<FeatureUI> ();
		foreach (var e in elements) {
			Destroy (e.gameObject);
		}
	}

	void BuildTraitList() {
		foreach(var trait in character.CurrentCharacter.Traits) {
			var newTrait = Instantiate (FeatureUI);
			newTrait.transform.SetParent (transform, false);
			var tagScript = newTrait.GetComponent<FeatureUI> ();
			tagScript.SetFeature (trait.Name, trait.Description);
		}

		foreach (var feat in character.CurrentCharacter.Feats) {
			var featUI = Instantiate (FeatureUI);
			featUI.transform.SetParent (transform, false);
			var tagScript = featUI.GetComponent<FeatureUI> ();
			tagScript.SetFeature (feat.Name, feat.Description);
		}
	}

}
