using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyAborigenFast : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;
	GameObject target;
	Level3Boss bossScript;
	int numberOfPlatform = 1;

	void Awake() {
		bossScript = GameObject.Find ("Level3Boss").GetComponent<Level3Boss> ();
		bossScript.AddEnemy (this, numberOfPlatform);
	}

	void Start() {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update() {

		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
		anim.SetFloat ("speed", Mathf.Abs (input * moveSpeed));

	}

	//Нанесение урона
	public override void GetDamage () {
		if (attackCheck) {
			attackCheck = false;
			anim.SetTrigger ("attack");
		}
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
		anim.SetTrigger ("die");
		Die ();
	}

	//Получить стан
	public override void SetStun () {

	}

	//Умереть
	public override void Die () {
		alive = false;
		gameObject.layer = 2;
	}

	//Начать преследование
	public void Chase (GameObject player) {
		target = player;
	}

	//Остановить преследование
	public void Idle () {

	}
}
