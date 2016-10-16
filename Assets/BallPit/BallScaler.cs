using UnityEngine;
using System.Collections;

public class BallScaler : MonoBehaviour {

	// Use this for initialization
	void Awake() {
        GetComponent<Transform>().localScale = (new Vector3(1, 1, 1)) * Constants.BALL_DIAMETER;
	}
	void Start () {
        // default DIAMETER is 1, let's scale to the desired world dimensions
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
