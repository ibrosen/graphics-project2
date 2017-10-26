using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
public class SwarmManager : MonoBehaviour {

	public CanvasGroup intro;

    // External parameters/variables
    public GameObject enemyTemplate;
    private float currElapsed = 0;
    private int distFromPlayer = 20;
	private int secondsBetweenCreate = 2;
	private float boundaryPadding = 7;

	private int days = 0;
	public Text dayCounter;

	private bool isDay = true;
	private GameObject sun;

	// Update is called once per frame
	void Update () {
		
		sun = GameObject.Find("Sun");
		if (sun.transform.position.y > 0 && !isDay) {
			isDay = true;
			days++;

			if (days == 1) {
				dayCounter.text = days.ToString() + " Day";
			} else {
				dayCounter.text = days.ToString() + " Days";
			}

		} else if (sun.transform.position.y <= -1) {
			isDay = false;
		}

		if (!isDay && intro.alpha == 0) {
			currElapsed += Time.deltaTime;
			if(currElapsed >= secondsBetweenCreate)
			{
				currElapsed = 0;
				CreateEnemy();
			}

		}
        
	}

    // Method to automatically generate swarm of enemies based on the set public attributes
    public void CreateEnemy()
    {

		// Get boundary of generated Terrain
		float radiusBoundary = GameObject.Find("Island").GetComponent<IslandGenerate>().radius - boundaryPadding;

		// Generate random magnitue
		float randMag = Random.Range(0, radiusBoundary);

		// Generate random angle
		float randAngle = Random.Range(0, Mathf.PI * 2);

		// Generate random x and z coordinates from random magnitude and angle
		Vector3 randPos = new Vector3(Mathf.Cos(randAngle) * randMag, -0.5f, Mathf.Sin(randAngle) * randMag);

        GameObject enemy = GameObject.Instantiate<GameObject>(enemyTemplate);
        enemy.transform.parent = this.transform;
		enemy.transform.localPosition = randPos;
        MoveToPlayer m = enemy.GetComponent<MoveToPlayer>();
        m.speed = Random.Range(1, 4);
    }
    
}
