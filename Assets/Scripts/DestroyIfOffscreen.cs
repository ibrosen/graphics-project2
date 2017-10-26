using UnityEngine;
using System.Collections;

public class DestroyIfOffscreen : MonoBehaviour {
    // Triggered as soon as the object is outside of the camera frustrum
	void OnBecameInvisible () {
        Destroy(this.gameObject);
	}

    private void Update()
    {
        var player = GameObject.Find("Player");
		RestrictToBoundaries();

    }

	void RestrictToBoundaries() {

		// Get boundary of generated Terrain
		Vector3 pos;
		float islandScale = GameObject.Find("Island").GetComponent<IslandGenerate>().radius;
		float boundary = islandScale * 2;

		// Destroy if outside of boundaries
		pos = this.transform.position;
		if (pos.magnitude >=boundary) {
			Destroy(this.gameObject);
		}

	}
}
