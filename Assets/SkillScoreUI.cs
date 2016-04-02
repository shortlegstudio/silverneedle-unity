using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShortLegStudio.RPG.Characters;

public class SkillScoreUI : MonoBehaviour {
	public Text skillName;
	public Text score;
	public Text classSkill;

	public void UpdateUI(Skill skill) {
		skillName.text = skill.Name;
		score.text = "--";
		classSkill.text = "...";
	}
}
