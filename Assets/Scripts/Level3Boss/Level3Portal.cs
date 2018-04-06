using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Portal : MonoBehaviour {

	public Vector3 exitPosition;

	void Start () {

	}

	void Update() {

	}

	/*void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player") && Level3Manager.GoPortal) {
			other.transform.position = exitPosition;
			Level3Manager.GoPortal = false;
		}
	}*/
}
