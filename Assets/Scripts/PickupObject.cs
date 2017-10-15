using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour {

	public Text FoodCount;
	public Text RockCount;
	public Text Health;

	private int foodCount = 0;
	private int rockCount = 50;

	void Start () {
		SetCount(FoodCount, foodCount);
		SetCount(RockCount, rockCount);
		SetCount(Health, 100);
	}

	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		Text countType;
		int count;

		if (other.gameObject.CompareTag ("pickup")) {
			other.gameObject.SetActive (false);

			if (other.gameObject.name.Contains("Coconut")) {
				countType = FoodCount;
				foodCount += 1;
				count = foodCount;

			} else if (other.gameObject.name.Contains("Rock")) {
				countType = RockCount;
				rockCount += 10;
				count = rockCount;

			} else {
				return;
			}

			SetCount(countType, count);
		}

		if (other.gameObject.tag.Equals("Enemy")) {
			HealthManager healthManager = this.gameObject.GetComponent<HealthManager>();
			healthManager.ApplyDamage(1);
			countType = Health;
			count = healthManager.GetHealth();

			SetCount(countType, count);
		}
	}

	void SetCount (Text countType, int count) {
		string name = "";

		if (countType.tag.Equals("FoodCount")) {
			name = "Food Count: ";

		} else if (countType.tag.Equals("RockCount")) {
			name = "Rock Count: ";

		} else if (countType.tag.Equals("Health")) {
			name = "Health: ";

		} else {
			return;
		}

		countType.text = name + count.ToString ();
	}

	public void DecrementRocks()
	{
		if (HasRocks()) {
			rockCount--;
			SetCount(RockCount, rockCount);
		}
	}

	public bool HasRocks()
	{
		if (rockCount > 0) return true;
		return false;
	}
}
