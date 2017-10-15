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
        if (Vector3.Distance(transform.position, player.transform.position) > 50)
        {
            Destroy(this.gameObject);
        }
    }
}
