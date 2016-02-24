using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public Vector3 rotateSpeed = Vector3.zero;

	// Update is called once per frame
	void Update () {
		this.transform.Rotate (rotateSpeed * Time.deltaTime);
	}
}
