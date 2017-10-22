using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatusBar : MonoBehaviour {

	// Health
	public Image healthBar;
	public Text healthRatio;

	private float healthStatus = 100;
	private float maxHealth = 100;
	private string healthName = "Health: ";

	// Food
	public Image foodBar;
	public Text foodRatio;

	private float foodStatus = 0;
	private float maxFood = 100;
	private string foodName = "Food: ";

	// Bullets
	public Image bulletBar;
	public Text bulletRatio;

	private float bulletsStatus = 0;
	private float maxBullets = 100;
	private string bulletsName = "Bullets: ";


	private void Start() {
		UpdateBar("health", healthStatus);
		UpdateBar("food", foodStatus);
		UpdateBar("bullets", bulletsStatus);
	}

	private void UpdateBar(string type, float status) {
		float ratio;
		Image currentBar;
		Text ratioText;
		string name;

		if (type.Equals("health")) {
			ratio = status / maxHealth;
			currentBar = healthBar;
			ratioText = healthRatio;
			name = healthName;

		} else if (type.Equals("food")) {
			ratio = status / maxFood;
			currentBar = foodBar;
			ratioText = foodRatio;
			name = foodName;

		} else if (type.Equals("bullets")) {
			ratio = status / maxBullets;
			currentBar = bulletBar;
			ratioText = bulletRatio;
			name = bulletsName;
		} else {
			return;
		}

		currentBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
		ratioText.text = name + (ratio * 100).ToString("0");
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals("Enemy")) {
			DecrementBar(5, "health");
			Destroy(other.gameObject);
		}

		if (other.gameObject.tag.Equals("pickup")) {
			other.gameObject.SetActive (false);

			if (other.gameObject.name.Contains("Coconut")) {
				IncrementBar(2, "food");
			} else if (other.gameObject.name.Contains("Rock")) {
				IncrementBar(3, "bullets");
			}
		}

	}

	private void DecrementBar(float decrement, string type) {
		if (type.Equals("health")) {
			healthStatus -= decrement;
			if (healthStatus < 0) {
				healthStatus = 0;
				Debug.Log("Dead!");
			}
			UpdateBar(type, healthStatus);
		}

		if (type.Equals("food")) {
			foodStatus -= decrement;
			if (foodStatus < 0) {
				foodStatus = 0;
			}
			UpdateBar(type, foodStatus);
		}

		if (type.Equals("bullets")) {
			bulletsStatus -= decrement;
			if (bulletsStatus < 0) {
				bulletsStatus = 0;
			}
			UpdateBar(type, bulletsStatus);
		}

	}

	private void IncrementBar (float increment, string type) {
		if (type.Equals("health")) {
			healthStatus += increment;
			if (healthStatus > maxHealth) {
				healthStatus = maxHealth;
			}
			UpdateBar(type, healthStatus);
		}

		if (type.Equals("food")) {
			foodStatus += increment;
			if (foodStatus > maxFood) {
				foodStatus = maxFood;
			}
			UpdateBar(type, foodStatus);
		}

		if (type.Equals("bullets")) {
			bulletsStatus += increment;
			if (bulletsStatus > maxBullets) {
				bulletsStatus = maxBullets;
			}
			UpdateBar(type, bulletsStatus);
		}

	}

	public void shootBullet() {
		DecrementBar(1, "bullets");
	}

	public bool HasBullets() {
		if (bulletsStatus > 0) return true;
		return false;
	}
}