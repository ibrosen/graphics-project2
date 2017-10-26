using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    
    public GameObject destroyBloodPrefab;
    

    // This should be hooked up to the health manager on this object
    public void DestroyMe()
    {
        // Create blood effect
        GameObject blood = Instantiate(this.destroyBloodPrefab);
        blood.transform.position = this.transform.position;

        // Destroy self
        Destroy(this.gameObject);
    }
    
}
