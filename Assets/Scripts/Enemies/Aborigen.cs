using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Aborigen : Unit {

	//Атакуемые слои
	public LayerMask attackCollision;
	GameObject target;

	void Start() {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		InvokeRepeating ("RunOrIdle", 0f, 4f);
	}

	/*void Update() {
		
		animator.speed = newPosition;

		if (AlertState && !AttackCheck) {
			PlayerDetection ();                                                            //Определения положения игрока
			targetVelocityX = Vector2.right.x * moveSpeed * moveSpeedDirection;
			moveSpeedDirection = Mathf.Sign (player.transform.position.x - transform.position.x);
		}

		//Анимации
		animator.SetFloat ("speed", moveSpeed);
		animator.SetBool ("alive", alive);

		//Оглушение
		if (aborigenParams.stun) {
			aborigenParams.stun = false;
			StopAllCoroutines ();
			StartCoroutine ("Stunning");
		}
			
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		step = 1f * Time.time;
		velocity.x = Mathf.MoveTowards (velocity.x, targetVelocityX, step);	velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

	}


	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.CompareTag ("Quit")) {
			Destroy (gameObject);
		}

		//Патрулирование
		if (other.CompareTag ("ReversTrigger")) {
			moveSpeedDirection = - moveSpeedDirection;
			targetVelocityX = Vector2.right.x * moveSpeed * moveSpeedDirection;
		}
	}

	//Определение: Игрок в зоне видимости или нет
	void OnTriggerStay2D(Collider2D other) {

		//Если игрок в том же триггере, что и враг, то перейти в состояние тревоги (преследования)
		if (other.CompareTag ("AlertTrigger") && other == player.GetComponent<AlertDetection> ().AlertTrigger && !AlertState && alive) {
			AlertState = true;
			CancelInvoke ();
			StopAllCoroutines ();
			moveSpeed = 4f;
		}

		//Если в триггере (зоне видимости) врага нет игрока перейти в состояние покоя 
		if (other.CompareTag("AlertTrigger") && other != player.GetComponent<AlertDetection> ().AlertTrigger && AlertState && alive) {
			AlertState = false;
			InvokeRepeating ("RunOrIdle", 0, 2f);
			moveSpeedDirection = 1f;
		}
	}

	void Attacking () {
		animator.SetTrigger ("attack");
		audioController.PlayAudio (2);
	}

	public void Attack() {
		
		RaycastHit2D hit = detectionScript.Detection ();

		if (hit) {
			hit.transform.GetComponent<IAttackable<float>>().SetDamage(10f);
		}
	}

	IEnumerator ResetAttackCheck () {
		yield return new WaitForSeconds (AttackCD);
		AttackCheck = false;
	}

	//Поиск игрока
	void PlayerDetection () {

		RaycastHit2D hit = detectionScript.Detection ();
		if (hit || (!hit && Mathf.Abs(transform.position.x -player.transform.position.x) < 0.1f)) {
			moveSpeed = 0;
			AttackCheck = true;
			animator.SetTrigger ("attack");
		} else
			moveSpeed = 4f;
	}

	//Покой
	void RunOrIdle() {
		int chance = Random.Range (-1, 1);
		if (chance < -0.5f) {
			moveSpeed = 2f;
			targetVelocityX = Vector2.right.x * moveSpeed * moveSpeedDirection;
		} else {
			moveSpeed = 0;
			targetVelocityX = Vector2.right.x * moveSpeed * moveSpeedDirection;
		}
	}

	//Интерфейсы
	public void SetDamage(float damageTaken) {
		if (aborigenParams.Health > damageTaken) {
			aborigenParams.Health -= damageTaken;
		} else {
			GameManager.Experience += 5;
			alive = false;
			StopAllCoroutines();
			CancelInvoke ();
			moveSpeed = 0f;
			animator.SetTrigger ("Die");
			transform.tag = "Corpse";
			audioController.PlayAudio(3);
			controller.collisionMask.value = 512;
			controller.enabled = !controller.enabled;
			gameObject.layer = 11;
		}
	}

	public IEnumerator Stun(float secondsOfStun) {
		animator.SetBool ("sleep", true);
		moveSpeed = 0f;
		aborigenParams.stunning = true;
		AttackCheck = true;
		yield return new WaitForSeconds(secondsOfStun);
		animator.SetBool ("sleep", false);
		aborigenParams.stunning = false;
		AttackCheck = false;
	}*/

	public override void GetDamage (){}

	public override void SetDamage (float damage, float impulseDirection){}

	public override void SetStun (){}

	public override void Die (){}
}
