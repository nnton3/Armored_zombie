using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour {

	public LineRenderer LaserPosition;
	Vector3 position1;
	Vector3 position2;
	float step;
	public float speed = 1f;
	public float targetPosition = 0f;

	void Start () {
		position1 = new Vector3 (0f, 0f, 0f);
		position2 = new Vector3 (0f, -2f, 0f);
		LaserPosition.SetPosition (0, position1);
		LaserPosition.SetPosition (1, position2);

	}
	
	void Update () {
		step = speed * Time.time;
		print ("targetPosition = " + targetPosition);
		targetPosition = Mathf.MoveTowards (-3f, 3f, step);
		position2.x = targetPosition;
		LaserPosition.SetPosition (1, position2);
	}
}
