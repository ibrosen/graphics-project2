using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CanvasGroup intro;

	private float maxSpeed = 10;
	private float boundaryPadding = 7;
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

			Vector3 originalPosition;

			// TRANSLATION
			// Move player forwards
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				originalPosition = this.transform.localPosition;
				this.transform.localPosition += this.transform.forward * Time.deltaTime * moveSpeed;
				RestrictToBoundaries(originalPosition);

				// Move player backwards
			} else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				originalPosition = this.transform.localPosition;
				this.transform.localPosition -= this.transform.forward * Time.deltaTime * moveSpeed;
				RestrictToBoundaries(originalPosition);

				// Move player left
			} else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				originalPosition = this.transform.localPosition;
				this.transform.localPosition -= this.transform.right * Time.deltaTime * moveSpeed;
				RestrictToBoundaries(originalPosition);

			} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				originalPosition = this.transform.localPosition;
				this.transform.localPosition += this.transform.right * Time.deltaTime * moveSpeed;
				RestrictToBoundaries(originalPosition);
			}

			// ROTATION
			// Rotate player right
			if (Input.GetAxis("Mouse X") > 0) {
				this.transform.RotateAround(transform.position, transform.up, 1 * Time.deltaTime * rollSpeed);

				// Rotate player left
			} else if (Input.GetAxis("Mouse X") < 0) {
				this.transform.RotateAround(transform.position, transform.up, -1 * Time.deltaTime * rollSpeed);

			}

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

	void RestrictToBoundaries(Vector3 originalPostion) {

		// Get boundary of generated Terrain
		float radiusBoundary = GameObject.Find("Island").GetComponent<IslandGenerate>().radius - boundaryPadding;

		// Restrict movement to boundaries
		float playerMagnitute = this.transform.position.magnitude;
		if (playerMagnitute >= radiusBoundary) {
			this.transform.position = originalPostion;
		}

	}
}
