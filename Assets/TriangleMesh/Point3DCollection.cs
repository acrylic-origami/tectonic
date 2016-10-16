using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Point3DCollection<T> {
    protected Dictionary<float, Point2DCollection<T>> M;
    public Point3DCollection(List<Vector3> L, T v) {
        foreach(Vector3 vec in L) {
            if(!this.M.ContainsKey(vec.z))
                this.M[vec.z] = new Point2DCollection<T>();
            this.M[vec.z].put((Vector2)vec, v);
        }
    }
    public Point3DCollection(List<KeyValuePair<Vector3, T>> L) {
        foreach(KeyValuePair<Vector3, T> v in L) {
            Vector3 vec = v.Key;
            if(!this.M.ContainsKey(vec.z))
                M[vec.z] = new Point2DCollection<T>();
            M[vec.z].put((Vector2)vec, v.Value);
        }
    }
    public Point3DCollection() {
        this.M = new Dictionary<float, Point2DCollection<T>>();
    }
    public T get(Vector3 v) {
        if(this.M.ContainsKey(v.z))
            return this.M[v.z].get((Vector2) v);
        else
            return default(T);
    }
    public Point3DCollection<T> put(Vector3 vec, T v) {
        if(!this.M.ContainsKey(vec.z))
            this.M[vec.z] = new Point2DCollection<T>();
        this.M[vec.z].put((Vector2) vec, v);
        return this;
    }
}
