using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	private float moveSpeed = 10;
	private float boundaryPadding = 3;

	// Update is called once per frame
	void Update () {
		float rollSpeed = moveSpeed * 10;

		// KEY MOVEMENTS (forwards, backwards, right, left, roll)

		// Move camera forwards
		if (Input.GetKey(KeyCode.UpArrow)) {
			this.transform.localPosition += this.transform.forward * Time.deltaTime * moveSpeed;
			RestrictToBoundaries();

		// Move camera backwards
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			this.transform.localPosition -= this.transform.forward * Time.deltaTime * moveSpeed;
			RestrictToBoundaries();

		}

		// Move camera yaw left
		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.RotateAround(transform.position, transform.up, -1 * Time.deltaTime * rollSpeed);

		// Move camera yaw right
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * rollSpeed);
		}

	}

	void RestrictToBoundaries() {

		// Get boundary of generated Terrain
		Vector3 pos;
		float islandScale = GameObject.Find("Island").GetComponent<Transform>().localScale.x;
		float boundary = islandScale * 5 - boundaryPadding;

		// Restrict movement to boundaries
		pos = this.transform.position;
		pos.x = Mathf.Clamp(pos.x, -boundary, boundary);
		pos.y = Mathf.Clamp(pos.y, -boundary, boundary);
		pos.z = Mathf.Clamp(pos.z, -boundary, boundary);
		this.transform.position = pos;

	}

}
