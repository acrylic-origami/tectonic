using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Triangle_simple : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int[] triangle = {2, 3, 4};
		List<Vector3> vertices = new List<Vector3>();
		// vertices.Add(new Vector3(0.7540823f, 0.5187992f, 0.0f) * 10.0f);
		// vertices.Add(new Vector3(0.6618521f, 0.2226332f, 0.0f) * 10.0f);
		// vertices.Add(new Vector3(0.3871413f, 0.5442802f, 0.0f) * 10.0f);
		// vertices.Add(new Vector3(0.0f, 0.0f, 0.0f));
		// vertices.Add(new Vector3(0.0f, 10.0f, 0.0f));
		// vertices.Add(new Vector3(10.0f, 10.0f, 0.0f));
		
		vertices.Add(new Vector3(0.6626031f, 0.05119444f, 0.0f) * 10.0f);
		vertices.Add(new Vector3(0.6618521f, 0.2226332f, 0.0f) * 10.0f);
		vertices.Add(new Vector3(0.3871413f, 0.5442802f, 0.0f) * 10.0f);
		vertices.Add(new Vector3(0.6093425f, 0.3753652f, 0.0f) * 10.0f);
		vertices.Add(new Vector3(0.7540823f, 0.5187992f, 0.0f) * 10.0f);
		List<Vector2> uvs = new List<Vector2>();
		for(int i=0; i<vertices.Count; i++) {
			uvs.Add(new Vector2(vertices[i].x / 10.0f, vertices[i].y / 10.0f));
		}
		Mesh M = new Mesh();
		M.vertices = vertices.ToArray();
		M.triangles = triangle;
		M.uv = uvs.ToArray();
		M.RecalculateNormals();
		GetComponent<MeshFilter>().mesh = M;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
