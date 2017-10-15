using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    
    public GameObject destroyExplosionPrefab;
    

    // This should be hooked up to the health manager on this object
    public void DestroyMe()
    {
        // Create explosion effect
        //GameObject explosion = Instantiate(this.destroyExplosionPrefab);
        //explosion.transform.position = this.transform.position;

        // Destroy self
        Destroy(this.gameObject);
    }
    
    // Update is called once per frame
    void Update ()
    {
        HealthManager healthManager = this.gameObject.GetComponent<HealthManager>();
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        // Make enemy material darker based on its health
        renderer.material.color = Color.red * ((float)healthManager.GetHealth() / 100.0f);

        
	}

    
}
