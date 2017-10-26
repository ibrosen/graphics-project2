using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StatusBar : MonoBehaviour {

	// Health
	public Image healthBar;
	public Text healthRatio;

	private float healthStatus = 100;
	private float maxHealth = 100;
	private string healthName = "Health: ";

	// energy
	public Image energyBar;
	public Text energyRatio;

	private float energyStatus = 100;
	private float maxEnergy = 100;
	private string energyName = "Energy: ";

	// Bullets
	public Image bulletBar;
	public Text bulletRatio;

	private float bulletsStatus = 0;
	private float maxBullets = 100;
	private string bulletsName = "Bullets: ";

	public CanvasGroup intro;
	public GameObject destroyBloodPrefab;

	private void Start() {
		UpdateBar("health", healthStatus);
		UpdateBar("energy", energyStatus);
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

		} else if (type.Equals("energy")) {
			ratio = status / maxEnergy;
			currentBar = energyBar;
			ratioText = energyRatio;
			name = energyName;

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

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals("Enemy")) {

			// Create blood effect
			GameObject blood = Instantiate(this.destroyBloodPrefab);
			blood.transform.position = this.transform.position;


			DecrementBar(5, "health");
			Destroy(other.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag.Equals("pickup")) {
			Destroy(other.gameObject);

			if (other.gameObject.name.Contains("Coconut")) {
				IncrementBar(7, "energy");
			} else if (other.gameObject.name.Contains("Rock")) {
				IncrementBar(10, "bullets");
			}
		}
	}

	private void DecrementBar(float decrement, string type) {
		if (type.Equals("health")) {
			healthStatus -= decrement;
			if (healthStatus <= 0) {
				healthStatus = 0;
				RestartGame();
				//Debug.Log("Dead!");
			}
			UpdateBar(type, healthStatus);
		}

		if (type.Equals("energy")) {
			energyStatus -= decrement;
			if (energyStatus < 0) {
				energyStatus = 0;
			}
			UpdateBar(type, energyStatus);
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

		if (type.Equals("energy")) {
			energyStatus += increment;
			if (energyStatus > maxEnergy) {
				energyStatus = maxEnergy;
			}
			UpdateBar(type, energyStatus);
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

	public float getEnergyRatio() {
		return energyStatus/maxEnergy;
	}

	private float timePassed = 0.0f;

	// Decrease energy over time
	void Update() {
		timePassed += Time.deltaTime;

		// Decrease energy every 2 seconds
		if (intro.alpha == 0 && timePassed >= 2) {
			timePassed = 0;
			DecrementBar(1, "energy");
		}

	}

	//function to reset the game, sourced from here
	//http://answers.unity3d.com/questions/1261937/creating-a-restart-button.html
	private void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
	}
}