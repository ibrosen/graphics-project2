using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptLight : MonoBehaviour {
	public List<Light> lights;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// Get renderer component (in order to pass params to shader)
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

		// Pass updated light positions to shader
		Color totalColor = Color.black;
		float totalIntensity = 0.0f;
		Vector3 pos = GameObject.Find("Sun").transform.position;
		pos.y = Mathf.Abs(pos.y);
		foreach (Light l in lights)
		{
			totalColor += l.color * l.intensity;
			totalIntensity += l.intensity;
		}

		renderer.material.SetColor("_PointLightColor", totalColor/totalIntensity);
		renderer.material.SetVector("_PointLightPosition", pos);

	}
}
