using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniBossOgr : Unit {

	//Атакуемые слои
	public LayerMask attackCollision;
	GameObject target;

	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	/*void Update () {

		animator.speed = newPosition;

		animator.SetBool ("alert", AlertState);
		animator.SetFloat ("speed", moveSpeed);

		//Алгоритм атаки
		if (AlertState && Mathf.Abs(transform.position.x - player.transform.position.x) < 5 && !AttackCheck && alive) {
			RaycastHit2D hit = detectionScript.Detection ();

			if (hit) {
				moveSpeed = 0f;
				animator.SetTrigger ("attack");
				//audioController.PlayOneShot (audioFakeHit);
			}

			if (!hit) {
				moveSpeed = 6f;
			}
		}

		if (AlertState && Mathf.Abs (transform.position.x - player.transform.position.x) > 5 && !AttackCheck && alive) {
			moveSpeed = 6f;
		}
			

		//Звук бега
		if (Mathf.Abs(velocity.x) > 0 && audioRunCheck && controller.collisions.below) {
			audioRunCheck = false;
			StartCoroutine ("AudioRun");
		}

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		targetVelocityX = Vector2.left.x * moveSpeed * moveSpeedDirection;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	IEnumerator AudioRun () {
		//audioController.PlayOneShot (audioRun);
		yield return new WaitForSeconds (.3f);
		audioRunCheck = true;
	}

	//Определение: Игрок в зоне видимости или нет
	void OnTriggerStay2D(Collider2D other) {

		//Если игрок в том же триггере, что и враг, то перейти в состояние тревоги (преследования)
		if (other.CompareTag("AlertTrigger") && other == player.GetComponent<AlertDetection> ().AlertTrigger && !AttackCheck && alive) {
			AlertState = true;                                                                     //Игрок в зоне видимости
			moveSpeedDirection = Mathf.Sign (transform.position.x - player.transform.position.x);
		}

		//Если в триггере (зоне видимости) врага нет игрока перейти в состояние покоя 
		if (other.CompareTag("AlertTrigger") && other != player.GetComponent<AlertDetection> ().AlertTrigger && alive) {
			AlertState = false;
			moveSpeed = 0f;
		}
	}

	public void Attacking() {
		
		RaycastHit2D hit = detectionScript.Detection ();

		if (hit) {
			//Звук удара "Попал"
			//audioController.PlayOneShot (audioTrueHit);
			hit.transform.GetComponent<UnitParams> ().Health -= ogreParams.Attack;
		}
	}

	public void DisableTriggers() {
		startDieTrigger.SetActive (false);
	}

	//Интерфейс
	public void SetDamage (float damage) {
		if (ogreParams.Health > damage) {
			ogreParams.Health -= damage;
		} else {
			moveSpeed = 0f;
			animator.SetTrigger ("die");
			transform.tag = "Corpse";
			controller.enabled = !controller.enabled;
			transform.gameObject.layer = 11;
			alive = false;
			startDieTrigger.SetActive (true);
		}
	}*/
	public override void GetDamage (){}

	public override void SetDamage (float damage, float impulseDirection){}

	public override void SetStun (){}

	public override void Die (){}
}
