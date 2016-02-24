using UnityEngine;
using System.Collections;


/// <summary>
/// Destroy an object after some amount of time
/// </summary>
public class LifeLimit : MonoBehaviour {
	public float killTime;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, killTime);
	}
}
