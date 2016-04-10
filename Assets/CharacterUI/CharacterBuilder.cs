
using UnityEngine;
using UnityEngine.UI;
using System;
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
	public AlignmentsDropdown AlignmentsUI;
	public GameObject SkillPanel;
	public GameObject SkillUIPrefab;

	public CharacterSheet CurrentCharacter { get; private set; }
	private IList<Race> _races;
	private IList<Class> _classes;
	private IList<Skill> _skills;

	public event EventHandler CharacterChanged;

	// Use this for initialization
	void Start () {
		AlignmentsUI = FindObjectOfType<AlignmentsDropdown> ();
		_races = Race.GetRaces();
		_classes = Class.GetClasses ();
		_skills = Skill.GetSkills ();
		BuildRaceDropdown ();
		BuildClassDropdown ();
		BuildSkillList ();
		Generate ();
	}

	public void Generate() {
		CurrentCharacter = new CharacterSheet ();
		CurrentCharacter.Name = NameGenerator.CreateFullName ();
		CurrentCharacter.SetAbilityScores (AbilityScoreGenerator.RandomStandardHeroScores ());
		CurrentCharacter.Race = Race.GetRaces ().ChooseOne ();
		CurrentCharacter.Class = Class.GetClasses ().ChooseOne ();
		CurrentCharacter.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();
		CurrentCharacter.SetSkills (_skills);
		CurrentCharacter.SetHitPoints (HitPointGenerator.RollHitPoints (CurrentCharacter));
		UpdateInterface ();
	}

	private void UpdateInterface() {
		Name.text = CurrentCharacter.Name;
		Strength.text = CurrentCharacter.AbilityScores [AbilityScoreTypes.Strength].TotalValue.ToString();
		Dexterity.text = CurrentCharacter.AbilityScores [AbilityScoreTypes.Dexterity].TotalValue.ToString();
		Constitution.text = CurrentCharacter.AbilityScores [AbilityScoreTypes.Constitution].TotalValue.ToString();
		Intelligence.text = CurrentCharacter.AbilityScores [AbilityScoreTypes.Intelligence].TotalValue.ToString();
		Wisdom.text = CurrentCharacter.AbilityScores [AbilityScoreTypes.Wisdom].TotalValue.ToString();
		Charisma.text = CurrentCharacter.AbilityScores [AbilityScoreTypes.Charisma].TotalValue.ToString();
		Races.SelectOption (CurrentCharacter.Race.Name);
		Classes.SelectOption (CurrentCharacter.Class.Name);
		AlignmentsUI.list.SelectOption (CurrentCharacter.Alignment.ToString());
		UpdateSkillList ();
		OnCharacterChanged (new EventArgs ());
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
			skillUI.SetSkill (skill);
		}
	}

	void UpdateSkillList() {
		var skillView = SkillPanel.GetComponentsInChildren<SkillScoreUI> ();

		foreach (var s in skillView) {
			var skill = CurrentCharacter.GetSkill (s.Skill);
			s.UpdateUI (skill);
		}
	}

	protected void OnCharacterChanged(EventArgs e) {
		EventHandler handler = CharacterChanged;
		if (handler != null) {
			handler (this, e);
		}
	}
}
