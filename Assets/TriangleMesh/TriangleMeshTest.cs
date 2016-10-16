using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Delaunay;
using Delaunay.Geo;

public class TriangleMeshTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<Vector2> vertices = new List<Vector2>();
        vertices.Add(new Vector2(0.0f, 0.0f));
        vertices.Add(new Vector2(1.0f, 0.0f));
        vertices.Add(new Vector2(1.0f, -2.0f));
        vertices.Add(new Vector2(1.0f, 1.0f));
        List<uint> colors = new List<uint>();
        colors.Add(0);
        colors.Add(0);
        colors.Add(0);
        colors.Add(0);
        Delaunay.Voronoi V = new Delaunay.Voronoi(vertices, colors, new Rect(-1, -1, 2, 2));
        List<LineSegment> lines = V.DelaunayTriangulation();
        for(int i=0; i<lines.Count; i++)
            Debug.Log(string.Format("{0}, {1}", lines[i].p0, lines[i].p1));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
