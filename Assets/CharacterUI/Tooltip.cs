using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {
	public static Tooltip tipper;
	public bool ShowingTip = true;
	public Text DescriptionText;
	public Text TopicText;
	public Text SummaryText;

	// Use this for initialization
	void Start () {
		tipper = this;
	}
	
	// Update is called once per frame
	void Update () {
		//If Visible Follow the mouse
		if (ShowingTip) {
			
		}
	}

	public static void ShowTip(string topic, string summary, string text) {
		tipper.DescriptionText.text = text;
		tipper.TopicText.text = topic;
		tipper.SummaryText.text = summary;
	}

	public static void HideTip() {
		
	}

}