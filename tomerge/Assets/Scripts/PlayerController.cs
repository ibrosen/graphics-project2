using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1; // Default speed sensitivity
    public float rotateSpeed = 10; //rotation speet
    public ProjectileController projectilePrefab;
    private float sinceLastShot = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.localPosition += this.transform.forward * Time.deltaTime * moveSpeed;
            

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.localPosition -= this.transform.forward * Time.deltaTime * moveSpeed;
          
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotateSpeed);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.RotateAround(transform.position, transform.up, -1 * Time.deltaTime * rotateSpeed);
            
        }
        if (Input.GetKey(KeyCode.Space))
        {
            sinceLastShot += Time.deltaTime;

            if(sinceLastShot >= 0.1)
            {
                sinceLastShot = 0;
                Vector3 fireToWorldPos = this.transform.localRotation.eulerAngles;

                // Finally we create a new projectile instance, and set its velocity to
                // be the difference between the projected mouse position and the player
                // position so that it effectively travels towards the mouse.
                ProjectileController p = Instantiate<ProjectileController>(projectilePrefab);
                p.transform.position = this.transform.position;
                p.velocity = (this.transform.forward).normalized * 10.0f;
            }

            


            //Destroy(p, 7);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("!11111111111111111111111");
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Enemy")
        {
            // Damage object with relevant tag
            HealthManager healthManager = this.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(1);
            Debug.Log(healthManager.GetHealth());

            // Destroy self
            Destroy(col.gameObject);
        }
    }


}
