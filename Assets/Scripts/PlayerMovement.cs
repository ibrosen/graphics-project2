using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	private float moveSpeed = 10;
	private float boundaryPadding = 3;
	public ProjectileController projectilePrefab;
	private float sinceLastShot = 0;

	// Update is called once per frame
	void Update () {
		float rollSpeed = moveSpeed * 10;

		// TRANSLATION
		// Move camera forwards
		if (Input.GetKey(KeyCode.UpArrow)) {
			this.transform.localPosition += this.transform.forward * Time.deltaTime * moveSpeed;
			RestrictToBoundaries();

		// Move camera backwards
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			this.transform.localPosition -= this.transform.forward * Time.deltaTime * moveSpeed;
			RestrictToBoundaries();

		}

		// ROTATION
		// Move camera yaw left
		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.RotateAround(transform.position, transform.up, -1 * Time.deltaTime * rollSpeed);

		// Move camera yaw right
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * rollSpeed);
		}

		// SHOOTING
		if (Input.GetKey(KeyCode.Space))
		{
			sinceLastShot += Time.deltaTime;

			PickupObject pickup = this.gameObject.GetComponent<PickupObject>();
			if(sinceLastShot >= 0.25 && pickup.HasRocks())
			{
				pickup.DecrementRocks();
				sinceLastShot = 0;
				Vector3 fireToWorldPos = this.transform.localRotation.eulerAngles;

				// Finally we create a new projectile instance, and set its velocity to
				// be the difference between the projected mouse position and the player
				// position so that it effectively travels towards the mouse.
				ProjectileController p = Instantiate<ProjectileController>(projectilePrefab);
				p.transform.position = this.transform.position + this.transform.forward.normalized * 1.4f + Vector3.up * 0.5f;
				p.velocity = (this.transform.forward).normalized * 10.0f;
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

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy")
		{
			// Damage object with relevant tag
			HealthManager healthManager = this.gameObject.GetComponent<HealthManager>();
			healthManager.ApplyDamage(1);
			Debug.Log(healthManager.GetHealth());

			// Destroy self
			Destroy(col.gameObject);
		}
	}

}
