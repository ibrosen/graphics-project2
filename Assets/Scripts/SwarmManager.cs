using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SwarmManager : MonoBehaviour {

    // External parameters/variables
    public GameObject enemyTemplate;
    private float currElapsed = 0;
    private int distFromPlayer = 20;
	private int secondsBetweenCreate = 2;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        currElapsed += Time.deltaTime;

		if(currElapsed >= secondsBetweenCreate)
        {
            currElapsed = 0;
            CreateEnemy();
        }

        
	}

    // Method to automatically generate swarm of enemies based on the set public attributes
    public void CreateEnemy()
    {
        var v = GameObject.Find("Player");


        GameObject enemy = GameObject.Instantiate<GameObject>(enemyTemplate);
        enemy.transform.parent = this.transform;
        float rand = Random.Range(0, 2 * Mathf.PI);
        enemy.transform.localPosition = new Vector3(Mathf.Sin(rand), 0, Mathf.Cos(rand));
        enemy.transform.localPosition *= distFromPlayer;
        enemy.transform.localPosition += v.transform.position;
        MoveToPlayer m = enemy.GetComponent<MoveToPlayer>();
        m.speed = Random.Range(1, 4);
        //Debug.Log(enemy.transform.localPosition);

		RestrictToBoundaries(enemy);
    }

	// TODO: CHANGE THIS SO THAT THEY AREN'T CREATED IN THE FIRST PLACE
	void RestrictToBoundaries(GameObject enemy) {

		// Get boundary of generated Terrain
		Vector3 pos;
		float islandScale = GameObject.Find("Island").GetComponent<Transform>().localScale.x;
		float boundary = islandScale * 5;

		// Destroy if outside of boundaries
		pos = enemy.transform.position;
		if (Mathf.Abs(pos.x) > boundary || Mathf.Abs(pos.z) > boundary) {
			Destroy(enemy.gameObject);
		}
	}

    
}
