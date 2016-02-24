using UnityEngine;
using System.Collections;

namespace ShortLegStudio.Shredders {
	public class ShredOnExit3D : MonoBehaviour {

		void OnTriggerExit(Collider other) {
			Debug.Log (name + " Destroying " + other.name);
			Destroy (other.gameObject);
		}
	}
}
