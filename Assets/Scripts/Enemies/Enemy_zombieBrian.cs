using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieBrian : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;
	//Область агра
	DangerArea start;
	Flip flip;

	//Ссылка на игрока
	GameObject target;
	public float bornDelay = 0f;
	bool idle = true;

	//Сила толчка во время получения урона
	public float impulsePower = 3;

	void Awake() {
		start = GetComponentInParent<DangerArea> ();
		start.AddEnemie (this);
	}

	void Start () {
		flip = GetComponent<Flip> ();
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {

		if (!idle && alive && !stunned) {
			if (Mathf.Abs (target.transform.position.x - transform.position.x) < (attackRange - 0.5f) && ((target.transform.position.x > transform.position.x && direction > 0f) || (target.transform.position.x < transform.position.x && direction < 0f))) {
				input = 0f;
				GetDamage ();
			} else if (attackCheck) {
				input = (target.transform.position.x > transform.position.x) ? 1 : -1;
			} 
		} else if (!alive || stunned) {
			float step = 0.01f * Time.time;
			moveSpeed = Mathf.MoveTowards (impulsePower, 0f, step);
		}

		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
		anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
	}

	//Нанести урон
	public override void GetDamage () {
		if (attackCheck) {

			if (invulnerability) {
				anim.SetTrigger ("block");
			}
			attackCheck = false;
			float attackAnim = Random.Range (0f, 1f);
			if (attackAnim <= 0.5f) {
				anim.SetTrigger ("attack1");
				return;
			}

			if (attackAnim > 0.5f) {
				anim.SetTrigger ("attack2");
				return;
			}
		}
	}

	//Сбросить чек атаки
	public IEnumerator ResetAttackCheck () {

		//Перейти на блокировку
		anim.SetTrigger ("block");
		invulnerability = true;
		yield return new WaitForSeconds (attackSpeed);

		//Перейти к Атаке/Преследованию
		invulnerability = false;
		anim.SetTrigger ("block");
		attackCheck = true;
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

	public override void SetDamage (float damage, float impulseDirection){

		if (health <= damage && !invulnerability) {
			flip.enabled = false;
			Die ();
			return;
		}

		if (invulnerability) {
			SetStun ();
			input = impulseDirection;
			if (impulseDirection != direction) {
				anim.SetTrigger ("attackableInBlock");
				return;
			} else {
				health -= damage;
				anim.SetTrigger ("attackable");
			}
		} else 
			health -= damage;
	}

	public override void SetStun (){
		flip.enabled = false;
		stunned = true;
	}

	//Сбросить чек стана
	public void ResetStunCheck () {
		input = 0f;
		flip.enabled = true;
		moveSpeed = 2f;
		stunned = false;
	}

	public override void Die (){
		anim.SetTrigger ("die");
		start.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}

	//Начать преследование
	public void Chase (GameObject player) {
		target = player;
		StartCoroutine ("TimeToBorn");
	}

	//Задержка перед воскрешением
	IEnumerator TimeToBorn() {
		yield return new WaitForSeconds (bornDelay);
		anim.SetTrigger ("born");
	}

	public void StartChase() {
		gameObject.layer = 9;
		idle = false;
	}

	//Остановить преследование
	public void Idle () {
		
	}
}
