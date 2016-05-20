using UnityEngine;
using System;
using System.Collections;
using ShortLegStudio.RPG.Characters;

public class SkillListGUI : MonoBehaviour {
	public GameObject SkillUIPrefab;
	CharacterBuilder character;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<CharacterBuilder> ();
		character.CharacterChanged += this.CharacterChangedEvent;
	}

	void CharacterChangedEvent(object source, EventArgs e) {
		BuildSkillList ();
	}

	void ClearSkillList() {
		DestroyChildrenCascade (this.transform);
	}

	void BuildSkillList() {
		ClearSkillList ();
		float currentY = 0;
		float paddingY = 1;
		var skills = character.CurrentCharacter.SkillRanks.GetRankedSkills();
		var trans = GetComponent<RectTransform> ();
		foreach (var skill in skills) {
			var skillScore = (GameObject)Instantiate (SkillUIPrefab);
			skillScore.transform.SetParent (trans, false);

			var transform = skillScore.GetComponent<RectTransform> ();
			transform.Translate (new Vector3 (0, currentY, 0));
			currentY -= transform.rect.height + paddingY;
			var skillUI = skillScore.GetComponent<SkillScoreUI> ();
			skillUI.SetSkill (skill.Skill);
			skillUI.UpdateUI (skill);
		}

		trans.sizeDelta = new Vector2 (trans.sizeDelta.x, -currentY + 20);

	}

	public void DestroyChildrenCascade(Transform root) {
		int childCount = root.childCount;

		for (int i=0; i<childCount; i++) {
			
			var child = root.GetChild (i);
			Debug.Log ("deleting child: " + child);
			GameObject.Destroy(child.gameObject);

		}
	}
}
