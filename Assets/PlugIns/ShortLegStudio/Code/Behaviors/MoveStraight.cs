using UnityEngine;
using System.Collections;

/// <summary>
/// Simple script that moves straight line along some direction
/// </summary>
public class MoveStraight : MonoBehaviour {
	public Vector3 moveDirection;
	public float moveSpeed;

	void Start() {
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}
}
