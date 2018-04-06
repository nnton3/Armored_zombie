using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour {

	public GameObject PointerText;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")) {
			PointerText.SetActive(true);
		}
	}
		
	void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag("Player")) {
			PointerText.SetActive(false);
		}
	}
}
