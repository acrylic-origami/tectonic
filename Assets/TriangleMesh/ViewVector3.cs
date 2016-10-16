using UnityEngine;
using System.Collections;

public class ViewVector3 : ViewVector2 {
	public float z {
		get { return this.vec.z; }
		set { this.vec.z = value; }
	}
	public ViewVector3() {}
	public ViewVector3(Vector3 vec) {
		this.vec = vec;
	}
	public ViewVector3(float x, float y, float z) {
		this.vec = new Vector3(x, y, z);
	}
	public Vector3 toVec3() {
		return this.vec;
	}
}
