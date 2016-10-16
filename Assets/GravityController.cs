using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0, 0, -Physics.gravity.y);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space)) {
			Physics.gravity = new Vector3(0, 0, -Physics.gravity.z);
		}
	}
}
