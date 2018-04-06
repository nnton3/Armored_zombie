using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect_En_Dis : MonoBehaviour {

	Animator animator;

	void Start () {
		
		animator = GetComponent<Animator> ();

	}

	public void Enable () {
		animator.SetTrigger ("enable");
	}

	public void Disable () {
		animator.SetTrigger ("disable");
	}
}
