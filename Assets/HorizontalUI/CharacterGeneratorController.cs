using UnityEngine;
using System.Collections;
using ShortLegStudio.RPG.Characters;
using System;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;

public class CharacterGeneratorController : MonoBehaviour {
	public CharacterSheet Character;
	public event EventHandler Generated;

	public void GenerateCharacter() {
		Character = CharacterGenerator.GenerateRandomCharacter ();
		OnGenerate ();
	}

	private void OnGenerate() {
		if (Generated != null)
			Generated (this, new EventArgs ());
	}


}
