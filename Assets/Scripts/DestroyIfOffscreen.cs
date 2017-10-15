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
		float islandScale = GameObject.Find("Island").GetComponent<Transform>().localScale.x;
		float boundary = islandScale * 5;

		// Destroy if outside of boundaries
		pos = this.transform.position;
		if (Mathf.Abs(pos.x) > boundary || Mathf.Abs(pos.z) > boundary) {
			Destroy(this.gameObject);
		}

	}
}
