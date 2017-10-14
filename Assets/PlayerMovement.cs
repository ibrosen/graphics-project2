using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	private float moveSpeed = 10;

	// Update is called once per frame
	void Update () {
		float rollSpeed = moveSpeed * 10;

		// KEY MOVEMENTS (forwards, backwards, right, left, roll)

		// Move camera forwards
		if (Input.GetKey(KeyCode.UpArrow)) {
			this.transform.localPosition += this.transform.forward * Time.deltaTime * moveSpeed;

		// Move camera backwards
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			this.transform.localPosition -= this.transform.forward * Time.deltaTime * moveSpeed;

		// Move camera right
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * rollSpeed);
//			this.transform.localPosition += this.transform.right * Time.deltaTime * moveSpeed;

		// Move camera left
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.RotateAround(transform.position, transform.up, -1 * Time.deltaTime * rollSpeed);
//			this.transform.localPosition -= this.transform.right * Time.deltaTime * moveSpeed;
		}

	}

}
