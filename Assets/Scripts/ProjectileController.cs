using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour
{

    public Vector3 velocity;

    public int damageAmount = 50;
    public string tagToDamage;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(velocity * Time.deltaTime);
    }

    // Handle collisions
	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == tagToDamage)
        {
            // Damage object with relevant tag
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(damageAmount);

            // Destroy self
            Destroy(this.gameObject);
        }
    }
}
