using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour {

	public Text FoodCount;

	private int count;

	void Start () {
		count = 0;
		IncrementCount(FoodCount);
	}

	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("pickup")) {
			other.gameObject.SetActive (false);
			count += 1;
			IncrementCount(FoodCount);
		}
	}

	void IncrementCount (Text countType) {
		string name = "";
		if (countType.Equals(FoodCount)) {
			name = "Food Count: ";
		}
		countType.text = name + count.ToString ();
	}
}
