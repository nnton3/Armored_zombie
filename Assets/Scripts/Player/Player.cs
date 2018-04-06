using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {

	public LayerMask attackCollision;
	public GameObject arrow;

	bool inBlock = false;

	Flip flip;

	public float rollCD = 3f;
	bool rollCheck = true;

	//Сила толчка во время получения урона
	public float impulsePower = 3;

	void Start () {
		flip = GetComponent<Flip> ();
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {

		if (attackCheck) {
			//Управление
			if (Input.GetKeyDown (KeyCode.F)) {       //Атака мечом
				GetDamage ();
			}

			if (Input.GetKeyDown (KeyCode.R)) {       //Перекат
				Roll ();
			}

			if (Input.GetKeyDown (KeyCode.B)) {       //Блок
				Block ();
			}

			if (Input.GetKeyDown (KeyCode.P)) {       //Атака из лука
				PullBow ();
			}
		}

		if (stunned || !alive) {
			float step = 0.01f * Time.time;
			moveSpeed = Mathf.MoveTowards (impulsePower, 0f, step);
		} else if (!invulnerability && !stunned || inBlock) {
			input = Input.GetAxisRaw ("Horizontal");
		} else {
				float step = 0.01f * Time.time;
				moveSpeed = Mathf.MoveTowards (7f, 0f, step);
		}
			
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);

		anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
	}

	//Атака
	public override void GetDamage () {

		if (inBlock) {
			StopBlock ();
		}

		if (attackCheck && !stunned) {
			anim.SetTrigger ("attack");
			attackCheck = false;
			//Меняем скорость атаки в зависимости от заданного параметра
			anim.speed = 1 / attackSpeed;
		}
	}

	//Сбросить чек атаки
	public void ResetAttackCheck () {
		attackCheck = true;
		anim.speed = 1;
	}

	//Построить луч атаки
	public void CreateAttackVector() {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);

		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Unit> ().SetDamage (attack, direction);
		}
	}

	//Получить урон
	public override void SetDamage (float damage, float impulseDirection) {
		if (!invulnerability) {
			if (health > damage) {
				anim.SetTrigger ("attackable");
				SetStun ();
				input = impulseDirection;
				health -= damage;
			} else
				Die ();
		} else if (inBlock) {
			if (impulseDirection == direction) {
				health -= damage;
				anim.SetTrigger ("attackable");
				return;
			}
			SetStun ();
			input = impulseDirection;
			anim.SetTrigger ("blocked");
		}
	}

	//Получить стан
	public override void SetStun () {
		flip.enabled = false;
		stunned = true;
	}

	//Сбросить чек стана
	public void ResetStunCheck () {
		flip.enabled = true;
		input = 0f;
		moveSpeed = 5f;
		stunned = false;
	}

	//Умереть
	public override void Die () {
		alive = false;
	}

	//Использовать блок
	void Block() {
		if (!invulnerability && !inBlock) {
			invulnerability = true;
			inBlock = true;
			anim.SetTrigger ("block");
		} else
			StopBlock ();
	}

	//Завершить блок
	public void StopBlock () {
		invulnerability = false;
		inBlock = false;
		anim.SetTrigger ("block");
	}

	//Использовать перекат
	void Roll() {

		if (inBlock) {
			StopBlock ();
		}

		if (!stunned && rollCheck) {
			rollCheck = false;
			invulnerability = true;
			Physics2D.IgnoreLayerCollision (9, 8, true);
			attackCheck = false;
			anim.SetTrigger ("roll");
			input = Mathf.Sign (direction);
			StartCoroutine ("RollCD");
		}
	}

	//Завершить перекат
	public void StopRoll() {
		moveSpeed = 5f;
		Physics2D.IgnoreLayerCollision (9, 8, false);
		invulnerability = false;
	}

	//Стрельба из лука
	void PullBow () {

		if (inBlock) {
			StopBlock ();
		}

		attackCheck = false;
		anim.SetTrigger ("pullBow");
	}

	//Выпустить стрелу
	public void CreateArrow() {
		GameObject arrowInstance = Instantiate (arrow, new Vector3 (transform.position.x, transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
		Аrrow arrowScript = arrowInstance.GetComponent<Аrrow> ();
		arrowScript.SetDirection (direction);
	}

	IEnumerator RollCD () {
		yield return new WaitForSeconds (rollCD);
		rollCheck = true;
	}
}
