using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : MonoBehaviour {

	public CanvasGroup intro;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Return)) {
			intro.alpha = 0f;
		}
		
	}
}
