using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public Vector3 exitPosition;

	void Start () {
		
	}
	
	void Update() {

	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			other.transform.position = exitPosition;
		}
	}
}
