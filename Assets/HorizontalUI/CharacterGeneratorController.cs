using UnityEngine;
using System.Collections;
using ShortLegStudio.RPG.Characters;
using System;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using ShortLegStudio.RPG.Repositories;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities;

public class CharacterGeneratorController : MonoBehaviour {
	public CharacterSheet Character;
	public event EventHandler Generated;
	private CharacterGenerator generator;


	//TODO: IoC Container will be essential as this gets more complicated
	void Start() {
		generator = new CharacterGenerator (
			new RandomAbilityScoreGenerator(),
			new LanguageSelector(new LanguageYamlRepository()),
			new RaceYamlRepository(),
			new NameGenerator()
		);
	}

	public void GenerateCharacter() {
		Character = generator.GenerateRandomCharacter ();
		OnGenerate ();
	}

	private void OnGenerate() {
		if (Generated != null)
			Generated (this, new EventArgs ());
	}
}
