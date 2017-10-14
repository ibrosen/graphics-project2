using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public float moveSpeed = 100;


	// Update is called once per frame
	void Update () {

		// KEY MOVEMENTS (forwards, backwards, right, left, roll)

		// Move camera forwards
		if (Input.GetKey(KeyCode.UpArrow)) {
			this.transform.localPosition += this.transform.forward * Time.deltaTime * moveSpeed;

		// Move camera backwards
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			this.transform.localPosition -= this.transform.forward * Time.deltaTime * moveSpeed;

		// Move camera right
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.localPosition += this.transform.right * Time.deltaTime * moveSpeed;

		// Move camera left
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.localPosition -= this.transform.right * Time.deltaTime * moveSpeed;
		}

	}

}
