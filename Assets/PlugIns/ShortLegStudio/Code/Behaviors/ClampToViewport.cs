using UnityEngine;
using System.Collections;

public class ClampToViewport : MonoBehaviour {
	float minX;
	float maxX;
	float minY;
	float maxY;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftBottomMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightTopMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));

		//Calculate Offsets
		SpriteRenderer render = GetComponent<SpriteRenderer> ();
		float offsetX = render.sprite.pivot.x / render.sprite.pixelsPerUnit;
		float offsetY = render.sprite.pivot.y / render.sprite.pixelsPerUnit;

		//set Clamps
		minX = leftBottomMost.x + offsetX;
		maxX = rightTopMost.x - offsetX;

		minY = leftBottomMost.y + offsetY;
		maxY = rightTopMost.y - offsetY;
	}
	
	// Update is called once per frame
	void Update () {
		//Clamp
		Vector3 clamped = new Vector3(
			Mathf.Clamp (transform.position.x, minX, maxX),
			Mathf.Clamp (transform.position.y, minY, maxY),
			transform.position.z
		);
		transform.position = clamped;
	}
}
