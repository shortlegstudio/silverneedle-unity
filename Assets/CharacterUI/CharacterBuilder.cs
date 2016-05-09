﻿
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;
using System.Linq;

public class CharacterBuilder : MonoBehaviour {
	public Text Name;
	public Dropdown Races;
	public Dropdown Classes;
	public AlignmentsDropdown AlignmentsUI;
	public GameObject SkillPanel;

	public CharacterSheet CurrentCharacter { get; private set; }
	private IList<Race> _races;
	private IList<Class> _classes;

	public event EventHandler CharacterChanged;

	// Use this for initialization
	void Start () {
		AlignmentsUI = FindObjectOfType<AlignmentsDropdown> ();
		_races = Race.GetRaces();
		_classes = Class.GetClasses ();
		BuildRaceDropdown ();
		BuildClassDropdown ();
	}

	public void Generate() {
		CurrentCharacter = CharacterGenerator.CreateLevel0 ();
		CharacterGenerator.SelectClass (CurrentCharacter);
		CurrentCharacter.AddFeat (Feat.GetQualifyingFeats (CurrentCharacter).ToList ().ChooseOne ());

		LevelUpGenerator.BringCharacterToLevel(CurrentCharacter, UnityEngine.Random.Range (1, 21));

		//Generate Appearance
		CurrentCharacter.Height = AppearanceGenerator.RollHeight(CurrentCharacter);
		CurrentCharacter.Weight = AppearanceGenerator.RollWeight (CurrentCharacter);


		//Assign Skill Points
		SkillPointGenerator.AssignSkillPointsRandomly(CurrentCharacter);
		UpdateInterface ();
	}

	private void UpdateInterface() {
		Name.text = CurrentCharacter.Name;
		Races.SelectOption (CurrentCharacter.Race.Name);
		Classes.SelectOption (CurrentCharacter.Class.Name);
		AlignmentsUI.list.SelectOption (CurrentCharacter.Alignment.ToString());
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

	protected void OnCharacterChanged(EventArgs e) {
		EventHandler handler = CharacterChanged;
		if (handler != null) {
			handler (this, e);
		}
	}
}
