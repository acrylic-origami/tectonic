using UnityEngine;
using System.Collections;

public class ViewVector2 {
	public float x {
		get { return this.vec.x; }
		set { this.vec.x = value; }
	}
	public float y {
		get { return this.vec.y; }
		set { this.vec.y = value; }
	}
	protected Vector3 vec = new Vector3();
	public ViewVector2() {}
	public ViewVector2(Vector2 vec) {
		// NOTE THIS IS A COPY CONSTRUCTOR!!
		this.vec = new Vector3(vec.x, vec.y, 0.0f);
	}
	public ViewVector2(Vector3 vec) {
		this.vec = vec;
	}
	public ViewVector2(float x, float y) {
		this.vec = new Vector3(x, y, 0.0f);
	}
	public Vector2 toVec2() {
		// Actually not copy surprisingly
		return (Vector2)this.vec;
	}
}
