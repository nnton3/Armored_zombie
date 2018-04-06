using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour {

	Button activateBtn;
	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		activateBtn = GetComponentInChildren <Button> ();
		activateBtn.gameObject.SetActive(false);
	}
	
	void Update () {
		
	}

	public void Activate() {
		activateBtn.gameObject.SetActive(true);
	}

	public void GoNext() {
		rb.bodyType = RigidbodyType2D.Dynamic;
		activateBtn.gameObject.SetActive(false);
	}
}
