using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeatureUI : MonoBehaviour {
	public string Name { get; set; }
	public string Description { get; set; }
	public Text nameText;

	public void SetFeature(string name, string desc) {
		Name = name;
		Description = desc;
		nameText = GetComponentInChildren<Text> ();
		nameText.text = Name;
	}

	public void OnMouseOver() {
		Tooltip.ShowTip (Name, "Trait", Description);
	}
}
