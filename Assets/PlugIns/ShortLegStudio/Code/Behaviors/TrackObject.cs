using UnityEngine;
using System.Collections;
using ShortLegStudio;

public class TrackObject : MonoBehaviour {
	public GameObject target;
	public Vector3 minBoundary;
	public Vector3 maxBoundary;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!target)
			return;
		Vector3 pos = target.transform.position + offset;
		pos = VectorHelpers.Clamp (pos, minBoundary, maxBoundary);
		transform.position = pos;
	}
}
