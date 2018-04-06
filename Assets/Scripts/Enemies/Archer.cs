using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit {

	//Атакуемые слои
	public LayerMask attackCollision;
	GameObject target;

	void Start() {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		InvokeRepeating ("RunOrIdle", 0, 2f);
	}

	/*void Update() {

		animator.speed = newPosotion;

		targetVelocityX = Vector2.left.x * moveSpeed * moveSpeedDirection;

		//Оглушение
		if (archerParams.stun) {
			archerParams.stunning = true;
			archerParams.stun = false;
			StopAllCoroutines ();
			StartCoroutine ("Stunning");
		}

		//Анимации
		animator.SetFloat ("speed", Mathf.Abs(moveSpeed));
		animator.SetBool("alive", alive);
			
		if (AlertState && !AttackCheck && alive) {
			AttackCheck = true;
			moveSpeedDirection = Mathf.Sign (transform.position.x - player.transform.position.x);
			moveSpeed = Mathf.Lerp (0f, 0.1f, Time.deltaTime);
			StartCoroutine ("Attacking");
		}

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		targetVelocityX = Vector2.left.x * moveSpeed * moveSpeedDirection;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

	}


	void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("Quit") && gameObject != null) {
			Destroy (gameObject);
		}

		//Патрулирование
		if (other.CompareTag ("ReversTrigger")) {
			moveSpeedDirection = -moveSpeedDirection;
			targetVelocityX = Vector2.left.x * moveSpeed * moveSpeedDirection;
		}
	}

	//Определение: Игрок в зоне видимости или нет
	void OnTriggerStay2D(Collider2D other) {
		if (alive) {
			//Если игрок в том же триггере, что и враг, то перейти в состояние тревоги (преследования)
			if (other.CompareTag ("AlertTrigger") && other == player.GetComponent<AlertDetection> ().AlertTrigger && !AlertState) {
				moveSpeed = 0f;
				CancelInvoke ();
				StopAllCoroutines ();
				AlertState = true;
			}

			//Если в триггере (зоне видимости) врага нет игрока перейти в состояние покоя 
			if (other.CompareTag ("AlertTrigger") && other != player.GetComponent<AlertDetection> ().AlertTrigger && AlertState) {
				AlertState = false;
				InvokeRepeating ("RunOrIdle", 0, 2f);
			}
		}
	}
		
	//Нанесение урона
	IEnumerator Attacking () {
		animator.SetTrigger ("attack");
		audioController.PlayAudio (1);
		yield return new WaitForSeconds (attackCD);
		AttackCheck = false;
	}

	public void CreateArrow() {
		arrowInstance = Instantiate (arrow, new Vector3 (tran.position.x, tran.position.y + 0.9f, tran.position.z), Quaternion.identity);
		Arrow arrowScript = arrowInstance.GetComponent<Arrow> ();
		arrowScript.SetCaster (this, (flipScript.isFacingRight) ? -1 : 1);
	}
		
	//Оглушение
	IEnumerator Stunning() {
		animator.SetBool ("sleep", true);
		moveSpeed = 0f;
		AttackCheck = true;
		yield return new WaitForSeconds(3f);
		AttackCheck = false;
		animator.SetBool ("sleep", false);
		archerParams.stunning = false;
	}
		
	//Пройти пару шагов или остановиться
	void RunOrIdle() {
		int chance = Random.Range (-1, 1);
		if (chance < -0.9f) {
			StartCoroutine ("Run");
		}
		else if (chance >= -0.9f) {
			StartCoroutine ("Idle");
		}
	}

	IEnumerator Idle() {
		moveSpeed = 0;
		yield return new WaitForSeconds (Random.Range(0.5f,2f));
	}

	IEnumerator Run() {
		moveSpeed = 2f;
		yield return new WaitForSeconds (Random.Range(0.5f,2f));
	}

	//Интерфейсы
	public void SetDamage(float damageTaken) {
		if (archerParams.Health > damageTaken) {
			archerParams.Health -= damageTaken;
		} else {
			GameManager.Experience += 5;
			alive = false;
			audioController.PlayAudio (2);
			moveSpeed = 0f;
			animator.SetTrigger ("Die");
			transform.tag = "Corpse";
			controller.enabled = !controller.enabled;
			gameObject.layer = 11;
		}
	}*/

	public override void GetDamage (){}

	public override void SetDamage (float damage, float impulseDirection){}

	public override void SetStun (){}

	public override void Die (){}
}
