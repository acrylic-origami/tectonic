using UnityEngine;
using System.Collections;
using System;

public class PauseController : MonoBehaviour {

	// Use this for initialization
	private bool state = false;
	void Start () {
        Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Return)) {
			Time.timeScale = Convert.ToSingle(state = !state);
		}
	}
}
