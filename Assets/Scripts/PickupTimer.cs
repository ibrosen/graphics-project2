using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTimer : MonoBehaviour {

	public float existTime;
	private float timer = 0;

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		// Destroy object after existing for specified seconds
		if (timer >= existTime) {
			Destroy(this.gameObject);
		}
	}
}
