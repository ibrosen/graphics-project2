using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SwarmManager : MonoBehaviour {

    // External parameters/variables
    public GameObject enemyTemplate;
    private float currElapsed = 0;
    private int distFromPlayer = 20;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        currElapsed += Time.deltaTime;

        if(currElapsed >= 1)
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
        float rand = Random.Range(0, 2*Mathf.PI);
        enemy.transform.localPosition = new Vector3(Mathf.Sin(rand), 0, Mathf.Cos(rand));
        enemy.transform.localPosition *= distFromPlayer;
        enemy.transform.localPosition += v.transform.position;
        MoveToPlayer m = enemy.GetComponent<MoveToPlayer>();
        m.speed = Random.Range(1, 3);
        //Debug.Log(enemy.transform.localPosition);
    }



    
}
