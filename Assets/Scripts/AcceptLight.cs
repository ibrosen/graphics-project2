using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptLight : MonoBehaviour {

	public SunMove sun;

	void Update() {
		// Get renderer component (in order to pass params to shader)
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

		renderer.material.SetColor("_PointLightColor", Color.white);
		renderer.material.SetVector("_PointLightPosition", sun.transform.position);
	}
}
