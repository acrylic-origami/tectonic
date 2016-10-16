using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class Point2DCollection<T> {
    public Dictionary<float, Dictionary<float, T>> M {
        get;
        protected set;
    }
    public int count {
        get;
        protected set;
    }
    public override String ToString() {
        StringBuilder ret = new StringBuilder();
        foreach(KeyValuePair<float, Dictionary<float, T>> x_y_T in this.M) {
            foreach(KeyValuePair<float, T> y_T in x_y_T.Value) {
                ret.Append(String.Format("({0}, {1}) ", x_y_T.Key, y_T.Key));
            }
        }
        return ret.ToString();
    }
    public Point2DCollection(List<Vector2> L, T v) {
        foreach(Vector2 vec in L) {
            if(!this.M.ContainsKey(vec.x))
                M[vec.x] = new Dictionary<float, T>();
            M[vec.x][vec.y] = v;
        }
        count = L.Count;
    }
    public Point2DCollection() {
        this.M = new Dictionary<float, Dictionary<float, T>>();
    }
    public Point2DCollection(List<KeyValuePair<Vector2, T>> L) {
        foreach(KeyValuePair<Vector2, T> v in L) {
            Vector2 vec = v.Key;
            if(!this.M.ContainsKey(vec.x))
                M[vec.x] = new Dictionary<float, T>();
            M[vec.x][vec.y] = v.Value;
        }
        count = L.Count;
    }
    public bool has(Vector2 v) {
        return this.M.ContainsKey(v.x) && this.M[v.x].ContainsKey(v.y);
    }
    public T get(Vector2 v) {
        if(this.has(v))
            return this.M[v.x][v.y];
        else
            return default(T);
    }
    public Vector2 rip_point_by_index(int idx) {
        foreach(KeyValuePair<float, Dictionary<float, T>> x_y_T in this.M) {
            if((idx -= x_y_T.Value.Count) < 0) {
                Dictionary<float, T>.KeyCollection k_collection = x_y_T.Value.Keys;
                int j = x_y_T.Value.Count + idx;
                float k = 0.0f;
                foreach(float temp_k in k_collection) {
                    k = temp_k;
                    if(j-- < 0)
                        break;
                }
                Vector2 ret = new Vector2(x_y_T.Key, k);
                this.M[x_y_T.Key].Remove(k);
                return ret;
            }
        }
        throw new IndexOutOfRangeException();
    }
    public Point2DCollection<T> filter_to_new_by_point(Func<Vector2, bool> F) {
        Point2DCollection<T> ret = new Point2DCollection<T>();
        foreach(KeyValuePair<float, Dictionary<float, T>> x_y_T in this.M) {
            foreach(KeyValuePair<float, T> y_T in x_y_T.Value) {
                Vector2 maybe_vec = new Vector2(x_y_T.Key, y_T.Key);
                if(F(maybe_vec))
                    ret.put(maybe_vec, y_T.Value);
            }
        }
        return ret;
    }
    public void pointed_iterate(Action<Vector2, T> F) {
        foreach(KeyValuePair<float, Dictionary<float, T>> x_y_T in this.M) {
            foreach(KeyValuePair<float, T> y_T in x_y_T.Value) {
                F(new Vector2(x_y_T.Key, y_T.Key), y_T.Value);
            }
        }
    }
    public Point2DCollection<T> put(Vector2 vec, T v) {
        if(!this.M.ContainsKey(vec.x))
            M[vec.x] = new Dictionary<float, T>();
        M[vec.x][vec.y] = v;
        this.count++;
        return this;
    }
    // public void follow_until(Func<Vector2, Vector2?, T> F, Vector2 k) {
    //     if(this.M.ContainsKey(v.x) && this.M[v.x].ContainsKey(v.y)) {
    //         Vector2 maybe_vec2 = F(this.M[v.x][v.y]);
    //         if(maybe_T != null) {
    //             this.follow_until(F, maybe_vec2);
    //         }
    //     }
    // }
    public Point2DCollection<T> intersect_to_new(Point2DCollection<T> incoming) {
        Point2DCollection<T> ret = new Point2DCollection<T>();
        foreach(KeyValuePair<float, Dictionary<float, T>> x_y_T in this.M) {
            foreach(KeyValuePair<float, T> y_T in x_y_T.Value) {
                Vector2 pt = new Vector2(x_y_T.Key, y_T.Key);
                if(incoming.has(pt))
                    ret.put(pt, incoming.get(pt));
            }
        }
        return ret;
    }
    public List<Vector2> rezip() {
        List<Vector2> ret = new List<Vector2>();
        foreach(KeyValuePair<float, Dictionary<float, T>> x_y_T in this.M) {
            foreach(KeyValuePair<float, T> y_T in x_y_T.Value) {
                ret.Add(new Vector2(x_y_T.Key, y_T.Key));
            }
        }
        return ret;
    }
}