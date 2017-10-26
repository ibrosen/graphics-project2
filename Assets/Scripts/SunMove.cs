using UnityEngine;
using System.Collections;
using System;
public class SunMove : MonoBehaviour
{

	public float radius = 1000;		// the radius of the circle that the sun rotates around (sun follows the circumference)
    public float speed = 10;		// speed at which the sun moves
    public float startTime = 0;
    public float intensity = 1.5f;
    private float increaseRate = 1.1f;
    private bool isDay;
    private Light light;
    private float initialSpeed;
    private Color startColour;
	public CanvasGroup intro;
	private float elapsedTime;

    // Update is called once per frame
    void Start()
    {
        var trigOffset = (startTime / 24) * 2 * Mathf.PI;
        this.transform.localScale = new Vector3(100, 100, 100);
        this.transform.position = new Vector3(Mathf.Cos((trigOffset)), Mathf.Sin((trigOffset)), 0.0f);
        initialSpeed = speed;
        isDay = this.transform.position.y > 0;
        light = this.GetComponent<Light>();
        startColour = this.light.color;
    }

    private float time;
    void Update()
    {
		if (intro.alpha == 0) {
			elapsedTime += Time.deltaTime;

			time = elapsedTime / 5;

			var trigOffset = (startTime / 24) * 2 * Mathf.PI;
			light = this.GetComponent<Light>();


			//turn from sun to moon
			if (isDay != this.transform.position.y > 0 && isDay) {
				isDay = this.transform.position.y > 0;
				this.speed /= increaseRate;
			}

			// Moves the sun in a circlular motion so it rises and sets
			if (isDay) {
				this.transform.position = new Vector3(Mathf.Cos((time * initialSpeed + trigOffset)), Mathf.Sin((time * initialSpeed + trigOffset)), 0.0f) * radius;
			} else {
				this.transform.position = new Vector3(Mathf.Cos((time * speed + trigOffset)), Mathf.Sin((time * speed + trigOffset)), 0.0f) * radius;
			}
	        

			//orange gradient at dusk/dawn
			if (true) {
				if (this.transform.position.y >= 0) {
					if (this.transform.position.normalized.y <= 0.5) {
						light.intensity = 2 * intensity * this.transform.position.normalized.y;
						light.color = startColour * 2 * (this.transform.position.normalized.y);
					} else {
						light.intensity = intensity;
						light.color = startColour;
					}
	                
				} else {
					light.color = Color.black;
				}
			}
		}
    }
}
