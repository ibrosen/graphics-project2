using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Text countType;
		int count;

		if (other.gameObject.CompareTag ("pickup")) {
			other.gameObject.SetActive (false);

			if (other.gameObject.name.Contains("Coconut")) {

			} else if (other.gameObject.name.Contains("Rock")) {

			} else {
				return;
			}
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
}
