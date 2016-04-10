using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {
	public static Tooltip tipper;
	public bool ShowingTip = true;
	private Text toolTipText;

	// Use this for initialization
	void Start () {
		tipper = this;
		toolTipText = GetComponentInChildren<Text> ();
		this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//If Visible Follow the mouse
		if (ShowingTip) {
			
		}
	}

	public static void ShowTip(string text) {
		tipper.toolTipText.text = text;
		tipper.gameObject.SetActive (true);
	}

	public static void HideTip() {
		tipper.gameObject.SetActive (false);
	}

}