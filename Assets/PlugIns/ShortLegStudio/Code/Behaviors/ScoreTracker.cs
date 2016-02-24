	using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTracker : MonoBehaviour {
	private static int score = 0;
	private const string SCORE_FORMAT = "{0}";
	private Text label;

	void Start() {
		label = GetComponent<Text> ();
		UpdateDisplay ();
	}

	public static void Reset() {
		score = 0;
	}

	public void ScorePoints(int points) {
		score += points;
		UpdateDisplay ();
	}

	private void UpdateDisplay() {
		label.text = string.Format (SCORE_FORMAT, GetScoreString());
	}

	public static ScoreTracker GetInstance() {
		return FindObjectOfType<ScoreTracker> ();
	}

	public static string GetScoreString() {
		return score.ToString ("D7");
	}

	public static int GetScore() {
		return score;
	}
}
