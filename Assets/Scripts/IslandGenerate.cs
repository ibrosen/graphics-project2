using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandGenerate : MonoBehaviour {

    public int numPoints = 128;
    public float randomness = 0.1f;
    public int radius = 100;
	Vector2[] uvs;
    Vector3[] vertices ;
	Color[] colours;
    int[] tris;

	private const int COLOUR_MAX = 255;

    // Use this for initialization
    void Start() {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {

		vertices = new Vector3[numPoints + 2];
		uvs = new Vector2[numPoints + 2];
		tris = new int[(numPoints) * 3];
		colours = new Color[numPoints +2];


		for (int i = 0; i < 3 * numPoints - 1; i++)
		{
			tris[i] = 0;
		}

		numPoints -= numPoints % 2;

		// Get mesh filter and mesh collider
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		MeshCollider collide = new MeshCollider();
		collide = GetComponent<MeshCollider>();

		vertices[0]=  new Vector3(0.0f, 0.0f, 0.0f);

		for (int i = 1; i < numPoints+1; i ++)
		{
			float randomScale = Random.Range(1 - randomness, 1 + randomness) * radius;
			vertices[i] = new Vector3(Mathf.Cos(2 * i * Mathf.PI / (numPoints+1)), 0.0f, Mathf.Sin(2 *i* Mathf.PI / (numPoints + 1))) * randomScale ;

		}
		vertices[numPoints + 1] = vertices[1];

		mesh.vertices = vertices;
		Bounds bounds = mesh.bounds;
		int j = 0;
		while (j < uvs.Length)
		{
			uvs[j] = new Vector2(vertices[j].x / bounds.size.x, vertices[j].z / bounds.size.x);
			colours[j] = new Color(242, 230, 181)/COLOUR_MAX;
			j++;
		}




		for (int i = 0; i < numPoints; i++)
		{

			tris[(3 * i)] = i + 2;
			tris[(3 * i) + 1] = i + 1;
			tris[(3 * i) + 2] = 0;

		}

		mesh.uv = uvs;

		mesh.colors = colours;
		mesh.triangles = tris;

		collide.sharedMesh = mesh;

		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
    }
}
