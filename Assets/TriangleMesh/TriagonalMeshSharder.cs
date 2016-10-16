using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Delaunay;
using Delaunay.Geo;

public class TriagonalMeshSharder : MonoBehaviour {
    // protected List<int[]> triangles = new List<int[]>();
    protected List<int> triangles = new List<int>();
    protected int triangle_idx = 0;
    protected bool _is_clockwise(Vector2 a, Vector2 b, Vector2 c) {
        return (b.x - a.x)*(b.y + a.y) + (c.x - b.x)*(c.y + b.y) + (a.x - c.x)*(a.y + c.y) > 0;
    }
	void Start () {
		Mesh M = new Mesh();
        SemiSigmoid weighter = new SemiSigmoid();
        int terrain_points = (int)(weighter.eval(Random.value) * Constants.MAX_POINTS);
        List<Vector3> vertices = new List<Vector3>();
        Point2DCollection<int> vec2_vertices = new Point2DCollection<int>();
        List<uint> colors = new List<uint>();

         // vertices.Add(new Vector3(0.6626031f, 0.05119444f, 0.0f) * 10.0f);
         // vertices.Add(new Vector3(0.6618521f, 0.2226332f, 0.0f) * 10.0f);
         // vertices.Add(new Vector3(0.3871413f, 0.5442802f, 0.0f) * 10.0f);
         // vertices.Add(new Vector3(0.6093425f, 0.3753652f, 0.0f) * 10.0f);
         // vertices.Add(new Vector3(0.7540823f, 0.5187992f, 0.0f) * 10.0f);
        
         // for(int i=0; i<vertices.Count; i++) {
         //     vec2_vertices.put(vertices[i], i);
         //     colors.Add(0);
         // }
        
		for(int i=0; i<terrain_points; i++) {
          float x = Random.value, y = Random.value;
          Vector3 vec = new Vector3(x * Constants.EXTENT.x, y * Constants.EXTENT.y, Mathf.PerlinNoise(x * Constants.PERLIN_SCALE, y * Constants.PERLIN_SCALE) * Constants.MAX_ELEVATION - Constants.MAX_ELEVATION/2);
          Debug.Log(System.String.Format("{0}, {1}", vec.x, vec.y));
          vertices.Add(vec);
          vec2_vertices.put(vec, i);
          colors.Add(0);
		}
        M.vertices = vertices.ToArray();
        Debug.Log(vec2_vertices.M.Count);
        List<LineSegment> triangle_line_segments = new Delaunay.Voronoi(vec2_vertices.rezip(), colors, new Rect(0, 0, Constants.EXTENT.x, Constants.EXTENT.y)).DelaunayTriangulation();
        Point2DCollection<Point2DCollection<bool>> adjs = new Point2DCollection<Point2DCollection<bool>>(); // second parameters of Point2DCollection not really all that useful anymore, because we're storing the index to the original array now anyways
        for (int i = 0; i < triangle_line_segments.Count; i++) {
            // Debug.Log(System.String.Format("{0}, {1}", triangle_line_segments[i].p0, triangle_line_segments[i].p1));
            Vector2 src, dst;
            src = (Vector2)triangle_line_segments[i].p0;
            dst = (Vector2)triangle_line_segments[i].p1;
            if(adjs.get(src) == null) {
                Point2DCollection<bool> new_adj = new Point2DCollection<bool>();
                new_adj.put(dst, true);
                adjs.put(src, new_adj);
            }
            else {
                adjs.get(src).put(dst, true);
            }
            
            if(adjs.get(dst) == null) {
                Point2DCollection<bool> new_adj = new Point2DCollection<bool>();
                new_adj.put(src, true);
                adjs.put(dst, new_adj);
            }
            else {
                adjs.get(dst).put(src, true);
            }
            Debug.Log(System.String.Format("({0}, {1}) <-> ({2}, {3})", dst.x, dst.y, src.x, src.y));
            
            Point2DCollection<bool> maybe_intersect = adjs.get(src).intersect_to_new(adjs.get(dst));
            if(maybe_intersect.count > 0) {
                Debug.Log(System.String.Format("({0}, {1}), ({2}, {3}), ({4}, {5})", src.x, src.y, dst.x, dst.y, maybe_intersect.rezip()[0].x, maybe_intersect.rezip()[0].y));
                int A = vec2_vertices.get(src), B = vec2_vertices.get(dst), C = vec2_vertices.get(maybe_intersect.rezip()[0]);
                if(this._is_clockwise(vertices[A], vertices[B], vertices[C])) {
                    int[] triangle = {A, B, C};
                    this.triangles.AddRange(triangle);
                }
                else {
                    int[] triangle = {A, C, B};
                    this.triangles.AddRange(triangle);
                }
            }
        }
        
        M.triangles = this.triangles.ToArray();
        M.RecalculateNormals();
        // M.Optimize();
        
        GetComponent<MeshFilter>().mesh = M;
        MeshCollider collider = GetComponent<MeshCollider>();
        collider.convex = false;
        collider.enabled = true;
        collider.sharedMesh = M;
        StringBuilder str_vertices = new StringBuilder();
        for(int i=0; i<this.triangles.Count; i++) {
            str_vertices.Append(System.String.Format(this.triangles[i].ToString()));
        }
        Debug.Log(str_vertices.ToString());
    }
    
	// Update is called once per frame
	void Update () {
    //     Vector3[] triangle_coords = GetComponent<MeshFilter>().mesh.vertices;
    //     Mesh M = GetComponent<MeshFilter>().mesh;
	   // if(Input.GetKeyUp(KeyCode.Return)) {
    //         int[] triangle = this.triangles[this.triangle_idx % this.triangles.Count];
    //         Vector2 A = triangle_coords[triangle[0]], B = triangle_coords[triangle[1]], C = triangle_coords[triangle[2]];
    //         Debug.Log(System.String.Format("({6}, {7}, {8}) -> ({0}, {1}), ({2}, {3}), ({4}, {5})", A.x, A.y, B.x, B.y, C.x, C.y, triangle[0], triangle[1], triangle[2]));
    //         Debug.Log(Vector3.Cross((new Vector3(A.x-B.x, B.y-B.y, 0.0f)), (new Vector3(A.x-C.x, A.y-C.y, 0.0f))).magnitude / 2);
    //         M.triangles = this.triangles[this.triangle_idx++ % this.triangles.Count];
    //         M.RecalculateNormals();
    //    }
	}
}