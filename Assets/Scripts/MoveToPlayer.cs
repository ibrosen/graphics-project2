using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour {

    private float currElapsed = 0;
    public float speed = 1;
    private GameObject player;

    void Start()
    {
        // Get player reference if none attached already
        if (this.player == null)
        {
            this.player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update () {
        currElapsed += Time.deltaTime;

		if (currElapsed >= 1 / 60) {

			var DistVector = (player.transform.position - this.transform.position).normalized / 60;
			DistVector = new Vector3(DistVector.x, 0.0f, DistVector.z);
			this.transform.position += DistVector * speed;
			this.transform.forward = DistVector;

			currElapsed = 0;

		}
    }
}
