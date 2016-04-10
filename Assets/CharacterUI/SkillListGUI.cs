using UnityEngine;
using System.Collections;
using ShortLegStudio.RPG.Characters;

public class SkillListGUI : MonoBehaviour {
	public GameObject SkillUIPrefab;

	// Use this for initialization
	void Start () {
		BuildSkillList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BuildSkillList() {
		float currentY = 0;
		float paddingY = 1;
		var skills = Skill.GetSkills ();
		var trans = GetComponent<RectTransform> ();
		foreach (var skill in skills) {
			var skillScore = (GameObject)Instantiate (SkillUIPrefab);
			skillScore.transform.SetParent (trans, false);

			var transform = skillScore.GetComponent<RectTransform> ();
			transform.Translate (new Vector3 (0, currentY, 0));
			currentY -= transform.rect.height + paddingY;
			var skillUI = skillScore.GetComponent<SkillScoreUI> ();
			skillUI.SetSkill (skill);
		}

		trans.sizeDelta = new Vector2 (trans.sizeDelta.x, -currentY + 10);

	}
}
