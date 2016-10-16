using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BallPitGenerator : MonoBehaviour {
	public Transform ball;
	// Use this for initialization
	protected List<Vector2> hex_directions;
	protected readonly float HEX_EXTENT = 2 * (Constants.EXTENT.x - Constants.EXTENT.y/Mathf.Sqrt(3));
	protected List<Point2DCollection<Boolean>> points_per_level = new List<Point2DCollection<Boolean>>();
	void Awake() {
		// hex_directions.Add(new Vector2(1, 0));
		// hex_directions.Add(new Vector2(0, 1));
		// hex_directions.Add(new Vector2(-1, 1));
		// hex_directions.Add(new Vector2(-1, 0));
		// hex_directions.Add(new Vector2(0, -1));
		// hex_directions.Add(new Vector2(1, -1));
	}
	
	void Start () {
		Point2DCollection<Boolean> candidates = new Point2DCollection<Boolean>();
		int readjusted_x = Mathf.RoundToInt(Constants.EXTENT.x / Constants.BALL_DIAMETER), readjusted_y = Mathf.RoundToInt(Constants.EXTENT.y / Constants.BALL_DIAMETER);
		for(int i=0; i<readjusted_x; i++) {
			for(int j=0; j<readjusted_y; j++) {
             candidates.put(new Vector2(i, j), true);
			}
		}
		points_per_level.Add(candidates);
		SemiSigmoid weighter = new SemiSigmoid();
		int level = 0;
		Vector2 up = new Vector2(0, 1), across = new Vector2(1, 1), right = new Vector2(1, 0);
		float y_sep = Mathf.Pow(2.0f, -0.5f);
		while(candidates.count > 0) {
			float thresh = weighter.eval(UnityEngine.Random.value);
			// stability conditions
			candidates = candidates.filter_to_new_by_point((Vector2 v) => (System.Convert.ToInt32(candidates.has(v + up)) + System.Convert.ToInt32(candidates.has(v + across)) + System.Convert.ToInt32(candidates.has(v + right)) >= 2) && UnityEngine.Random.value < thresh && v.x < readjusted_x && v.y < readjusted_y);
			Debug.Log(candidates);
			Debug.Log(candidates.count);
			points_per_level.Add(candidates);
		}
		for(int i=0; i<points_per_level.Count; i++) {
			points_per_level[i].pointed_iterate((Vector2 vec, Boolean v) => 
				Instantiate(this.ball, (new Vector3(vec.x + 0.5f * i, vec.y + 0.5f * i, Constants.BALL_Z_OFFSET - i * y_sep)) * Constants.BALL_DIAMETER, Quaternion.identity));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
