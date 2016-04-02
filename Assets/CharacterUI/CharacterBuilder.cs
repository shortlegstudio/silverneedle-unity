using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;

public class CharacterBuilder : MonoBehaviour {
	public Text Name;
	public Text Strength;
	public Text Dexterity;
	public Text Constitution;
	public Text Intelligence;
	public Text Wisdom;
	public Text Charisma;
	public Dropdown Races;
	public Dropdown Classes;
	public GameObject SkillPanel;
	public GameObject SkillUIPrefab;

	private CharacterSheet _sheet;
	private IList<Race> _races;
	private IList<Class> _classes;
	private IList<Skill> _skills;

	// Use this for initialization
	void Start () {
		_races = Race.GetRaces();
		_classes = Class.GetClasses ();
		_skills = Skill.GetSkills ();
		BuildRaceDropdown ();
		BuildClassDropdown ();
		BuildSkillList ();
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate() {
		_sheet = new CharacterSheet ();
		_sheet.Name = NameGenerator.CreateFullName ();
		_sheet.SetAbilityScores (AbilityScoreGenerator.RandomStandardHeroScores ());
		_sheet.Race = Race.GetRaces ().ChooseOne ();
		_sheet.Class = Class.GetClasses ().ChooseOne ();
		UpdateInterface ();
	}

	private void UpdateInterface() {
		Name.text = _sheet.Name;
		Strength.text = _sheet.AbilityScores [AbilityScoreTypes.Strength].TotalValue.ToString();
		Dexterity.text = _sheet.AbilityScores [AbilityScoreTypes.Dexterity].TotalValue.ToString();
		Constitution.text = _sheet.AbilityScores [AbilityScoreTypes.Constitution].TotalValue.ToString();
		Intelligence.text = _sheet.AbilityScores [AbilityScoreTypes.Intelligence].TotalValue.ToString();
		Wisdom.text = _sheet.AbilityScores [AbilityScoreTypes.Wisdom].TotalValue.ToString();
		Charisma.text = _sheet.AbilityScores [AbilityScoreTypes.Charisma].TotalValue.ToString();
		Races.SelectOption (_sheet.Race.Name);
		Classes.SelectOption (_sheet.Class.Name);
	}

	void BuildRaceDropdown() {
		foreach (var race in _races) {
			Races.options.Add(new Dropdown.OptionData(race.Name));
		}
	}

	void BuildClassDropdown() {
		foreach (var cls in _classes) {
			Classes.options.Add(new Dropdown.OptionData(cls.Name));
		}
	}

	void BuildSkillList() {
		float currentY = 0;
		float paddingY = 1;
		foreach (var skill in _skills) {
			var skillScore = (GameObject)Instantiate (SkillUIPrefab);
			skillScore.transform.SetParent (SkillPanel.GetComponent<RectTransform>(), false);

			var transform = skillScore.GetComponent<RectTransform> ();
			transform.Translate (new Vector3 (0, currentY, 0));
			currentY -= transform.rect.height + paddingY;
			var skillUI = skillScore.GetComponent<SkillScoreUI> ();
			skillUI.UpdateUI (skill);
		}
	}
}
