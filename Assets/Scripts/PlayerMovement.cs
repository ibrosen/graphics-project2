using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CanvasGroup intro;

	private float maxSpeed = 10;
	private float boundaryPadding = 3;
	public ProjectileController projectilePrefab;
	private float sinceLastShot = 0;

	// Update is called once per frame
	void Update () {

		if (intro.alpha == 0) {
			float rollSpeed = maxSpeed * 15;

			// Set moveSpeed
			float moveSpeed;
			StatusBar status = this.GetComponent<StatusBar>();
			// Energy status out of max energy
			float energyRatio = status.getEnergyRatio();

			if (energyRatio <= 0.3f) {
				moveSpeed = maxSpeed * 0.3f;
			} else {
				moveSpeed = maxSpeed * energyRatio;
			}

			// TRANSLATION
			// Move player forwards
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				this.transform.localPosition += this.transform.forward * Time.deltaTime * moveSpeed;
				RestrictToBoundaries();

				// Move player backwards
			} else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				this.transform.localPosition -= this.transform.forward * Time.deltaTime * moveSpeed;
				RestrictToBoundaries();

				// Move player left
			} else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				this.transform.localPosition -= this.transform.right * Time.deltaTime * moveSpeed;
				RestrictToBoundaries();

			} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				this.transform.localPosition += this.transform.right * Time.deltaTime * moveSpeed;
				RestrictToBoundaries();
			}

			// ROTATION
			// Rotate player right
			if (Input.GetAxis("Mouse X") > 0) {
				this.transform.RotateAround(transform.position, transform.up, 1 * Time.deltaTime * rollSpeed);

				// Rotate player left
			} else if (Input.GetAxis("Mouse X") < 0) {
				this.transform.RotateAround(transform.position, transform.up, -1 * Time.deltaTime * rollSpeed);

			}

//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//			if (Input.GetAxis("Mouse X") != 0) {
//				Vector3 playerRotation = ray.GetPoint(10.0f);
//
//				this.transform.rotation = Quaternion.LookRotation(new Vector3(playerRotation.x, 0.0f, playerRotation.z) * Time.deltaTime);
//			}

			// SHOOTING
			// Shoot with mouse click
			if (Input.GetMouseButtonDown(0)) {
				StatusBar statusBar = this.gameObject.GetComponent<StatusBar>();

				if (statusBar.HasBullets()) {
					statusBar.shootBullet();

					// Create the projectile
					ProjectileController p = Instantiate<ProjectileController>(projectilePrefab);
					p.transform.position = this.transform.position + this.transform.forward.normalized * 1.4f + Vector3.up * 0.5f;
					p.velocity = this.transform.forward * 20.0f;

				}
			}
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
