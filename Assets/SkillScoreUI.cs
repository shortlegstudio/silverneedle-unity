using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShortLegStudio.RPG.Characters;

public class SkillScoreUI : MonoBehaviour {
	public Text skillName;
	public Text score;
	public Text classSkill;
	public Skill Skill;

	public void SetSkill(Skill skill) {
		Skill = skill;
		skillName.text = skill.Name;
	}

	public void UpdateUI(CharacterSkill skill) {
		classSkill.text = skill.ClassSkill ? "X" : "";

		if (skill.AbleToUse) {
			skillName.color = Color.white;
			score.text = skill.Score.ToString ();
		} else {
			skillName.color = Color.gray;
			score.text = "-";
		}
	}
}
