using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour {

	// External parameters/variables
	public GameObject itemTemplate;
	public int itemQuantity;
	public float itemSpacing;
	public float y;
	public bool spin;
	public int secondsBetweenCreate;
	public bool gradualSpawning;
	public CanvasGroup intro;

	// Internal parameters/variables
	private float islandBoundary;
	private ArrayList xValues;
	private ArrayList zValues;
	private float currElapsed = 0;

	// Use this for initialization
	void Start () {
		if (!gradualSpawning) {
			GenerateSwarm();
		}
	}

	// Update
	void Update () {

		if (intro.alpha == 0 && gradualSpawning) {
				currElapsed += Time.deltaTime;

				if (currElapsed >= secondsBetweenCreate) {
					currElapsed = 0;
					GenerateObject();
				}
		}

		// Spin the objects if required
		if (spin) {
			int childCount = this.transform.childCount;
			GameObject child;

			for (int i = 0; i < childCount; i++) {
				child = this.transform.GetChild(i).gameObject;
				child.transform.Rotate(new Vector3(0, 200, 0) * Time.deltaTime);
			}
		}
	}

	private void GenerateObject() {

		// Calculate island generation boundaries so that
		// the item isn't instantiated on the edge of the island
		float islandScale = GameObject.Find("Island").GetComponent<Transform>().localScale.x;
		islandBoundary = islandScale * 5 - itemSpacing;

		// Generate random x and z coordinates between -islandBoundary and islandBoundary
		float x = Random.Range(-islandBoundary, islandBoundary);
		float z = Random.Range(-islandBoundary, islandBoundary);

		// Instantiate the individual item
		GameObject item = GameObject.Instantiate<GameObject>(itemTemplate);

		// Item is made as a child of its swarm
		item.transform.parent = this.transform;

		// Set the position of the item
		item.transform.localPosition = new Vector3(x, y, z);

	}

	// Method to automatically generate swarm of enemies based on the set public attributes
	private void GenerateSwarm()
	{
		xValues = new ArrayList();
		zValues = new ArrayList();

		// Calculate island generation boundaries so that
		// the item isn't instantiated on the edge of the island
		float islandScale = GameObject.Find("Island").GetComponent<Transform>().localScale.x;
		islandBoundary = islandScale * 5 - itemSpacing;

		// Create swarm of enemies in a grid formation
		for (int i = 0; i < itemQuantity; i++) {

			// Generate random x and z coordinates between -islandBoundary and islandBoundary
			float x = Random.Range(-islandBoundary, islandBoundary);
			float z = Random.Range(-islandBoundary, islandBoundary);

			// Calculate distance between other generated items if it's not the first
			// item generated, and re-generate random coordinates if items are too close
//			if (i != 0) {
//				float[] coordinate = distanceCheck(x, z);
//				x = coordinate[0];
//				z = coordinate[1];
//			}

			// Store coordinate in ArrayList
			xValues.Add(x);
			zValues.Add(z);

			// Instantiate the individual item
			GameObject item = GameObject.Instantiate<GameObject>(itemTemplate);

			// Item is made as a child of its swarm
			item.transform.parent = this.transform;

			// Set the position of the item
			item.transform.localPosition = new Vector3(x, y, z);
		}
	}

	private float calculateDistance(float x1, float z1, float x2, float z2) {
		return Mathf.Sqrt((x2 - x1) * (x2 - x1) + (z2 - z1) * (z2 - z1));
	}

	private float[] distanceCheck(float x0, float z0) {
		float xVal, zVal, distance;
		float[] coordinate = new float[2];

		for (int j = 0; j < xValues.Count; j++) {
			xVal = (float) xValues[j];
			zVal = (float) zValues[j];

			distance = calculateDistance(x0, z0, xVal, zVal);

			if (distance < itemSpacing) {
				x0 = Random.Range(-islandBoundary, islandBoundary);
				z0 = Random.Range(-islandBoundary, islandBoundary);
				float[] recursiveCoordinate = distanceCheck(x0, z0);
				x0 = recursiveCoordinate[0];
				z0 = recursiveCoordinate[1];
			}
		}

		coordinate[0] = x0;
		coordinate[0] = z0;

		return coordinate;

	}
}