using UnityEngine;
using System.Collections;
using ShortLegStudio;
using ShortLegStudio.RPG;
using ShortLegStudio.RPG.Generators;

public class TestSomeStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 100; i++) {
			Debug.Log (NameGenerator.CreateFullName ());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
