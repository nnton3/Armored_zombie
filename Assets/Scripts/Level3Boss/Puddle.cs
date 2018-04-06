using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {

	Animator animator;
	public bool AttackCheck = false;
	public float damage = 10f;

	void Start () {
		animator = GetComponent<Animator> ();
		StartCoroutine ("PuddleTimer");
	}
	
	void Update () {
		
	}

	void Activate() {
		animator.SetTrigger ("puddleActivate");
	}

	public void GetDamage() {
		AttackCheck = true;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.CompareTag ("Player") && AttackCheck) {
			AttackCheck = false;
			other.GetComponent<Unit> ().SetDamage(damage, 0f);
		}
	}

	IEnumerator PuddleTimer() {
		yield return new WaitForSeconds (1f);
		Activate ();
	}

	public void Destroy () {
		Destroy (gameObject);
	}
}
