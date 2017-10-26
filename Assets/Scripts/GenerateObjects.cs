using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour {

	// External parameters/variables
	public GameObject itemTemplate;
	public int itemQuantity;
	public float itemSpacing;
	public float y;
	public bool pickup;
	public int secondsBetweenCreate;
	public bool gradualSpawning;
	public CanvasGroup intro;

	// Internal parameters/variables
	private float islandBoundary;
	private ArrayList xValues;
	private ArrayList zValues;
	private float currElapsed = 0;
	private float boundaryPadding = 7;

	private bool isDay;
	private GameObject sun;

	// Use this for initialization
	void Start () {
		if (!gradualSpawning) {
			GenerateSwarm();
		}

	}

	// Update
	void Update () {

		sun = GameObject.Find("Sun");

		isDay = sun.transform.position.y >= 0;
		if (isDay && intro.alpha == 0 && gradualSpawning) {
			
			currElapsed += Time.deltaTime;

			if (currElapsed >= secondsBetweenCreate) {
				currElapsed = 0;
				GenerateObject();
			}
		}

		// Spin the objects if they are pickup types
		if (pickup) {
			int childCount = this.transform.childCount;
			GameObject child;

			for (int i = 0; i < childCount; i++) {
				child = this.transform.GetChild(i).gameObject;
				child.transform.Rotate(new Vector3(0, 200, 0) * Time.deltaTime);
			}
		}

	}

	private void GenerateObject() {

		// Get boundary of generated Terrain
		float radiusBoundary = GameObject.Find("Island").GetComponent<IslandGenerate>().radius - boundaryPadding;

		// Generate random magnitue
		float randMag = Random.Range(0, radiusBoundary);

		// Generate random angle
		float randAngle = Random.Range(0, Mathf.PI * 2);

		// Generate random x and z coordinates from random magnitude and angle

		Vector3 randPos = new Vector3(Mathf.Cos(randAngle) * randMag, y, Mathf.Sin(randAngle) * randMag);

		// Instantiate the individual item
		GameObject item = GameObject.Instantiate<GameObject>(itemTemplate);

		// Item is made as a child of its swarm
		item.transform.parent = this.transform;

		// Set the position of the item
		item.transform.localPosition = randPos;

	}

	// Method to automatically generate swarm of enemies based on the set public attributes
	private void GenerateSwarm()
	{
		xValues = new ArrayList();
		zValues = new ArrayList();

		// Get boundary of generated Terrain
		float radiusBoundary = GameObject.Find("Island").GetComponent<IslandGenerate>().radius - boundaryPadding;

		// Create swarm of enemies in a grid formation
		for (int i = 0; i < itemQuantity; i++) {
			
			// Generate random magnitue
			float randMag = Random.Range(0, radiusBoundary -3);

			// Generate random angle
			float randAngle = Random.Range(0, Mathf.PI * 2);

			// Generate random x and z coordinates from random magnitude and angle
			
			Vector3 randPos = new Vector3(Mathf.Cos(randAngle) * randMag + 3, y, Mathf.Sin(randAngle) * randMag + 3);

			// Store coordinate in ArrayList
			xValues.Add(randPos.x);
			zValues.Add(randPos.z);

			// Instantiate the individual item
			GameObject item = GameObject.Instantiate<GameObject>(itemTemplate);

			// Item is made as a child of its swarm
			item.transform.parent = this.transform;

			// Set the position of the item
			item.transform.localPosition = randPos;
		}
	}
}