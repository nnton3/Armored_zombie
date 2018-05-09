using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieBrian : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;
	//Область агра
	DangerArea start;

	//Ссылка на игрока
	GameObject target;
	public float bornDelay = 0f;
	bool idle = true;

	//Сила толчка во время получения урона
	public float impulsePower = 3;

	//Местоположения относительно игрока
	float targetRange = 0f;
	float targetDirection =0f;

	GameObject hpBar;

	void Awake() {
		start = GetComponentInParent<DangerArea> ();
		start.AddEnemie (this);
	}

	void Start () {
		hpBar = transform.Find ("HPBar").gameObject;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		
		if (!idle) {
			if (alive && !stunned) {

				//Определение местоположения игрока
				targetRange = Mathf.Abs (transform.position.x - target.transform.position.x);
				targetDirection = Mathf.Sign (transform.position.x - target.transform.position.x);
				flipParam = input;

				if (targetRange < (attackRange - 0.5f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f))) {
					input = 0f;
					GetDamage ();
				} else if (attackCheck) {
					input = -targetDirection;
				} 
				anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
			} else if (!alive || stunned) {
				Impulse ();
			}
		}

		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	//Нанести урон
	public override void GetDamage () {

		if (stunned) {
			ResetStunCheck ();
		}

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
		
		if (!invulnerability) {
			//Перейти на блокировку
			anim.SetTrigger ("block");
			invulnerability = true;
		}
		yield return new WaitForSeconds (attackSpeed);

		//Перейти к Атаке/Преследованию
		//Если в блоке - снять блок
		if (invulnerability) {
			invulnerability = false;
			anim.SetTrigger ("block");
		}

		attackCheck = true;
	}

	//Построить луч атаки
	public void CreateAttackVector() {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);

		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Unit> ().SetDamage (attack, direction, attackModify);
		}
	}

	public override void SetDamage (float damage, float impulseDirection, bool[] attackModify){

		bool backToTheEnemy = impulseDirection == direction;

		if (invulnerability) {
			if (backToTheEnemy) {
				SetStun (impulseDirection);
				anim.SetTrigger ("attackable");
				ReduceHP (damage);
			} else {
				SetStun (impulseDirection);
				anim.SetTrigger ("attackableInBlock");
			}
		} else 
			ReduceHP (damage);
	}

	public override void SetStun (float direction){
		stunned = true;
		input = direction;
	}

	//Сбросить чек стана
	public void ResetStunCheck () {
		input = 0f;
		moveSpeed = 2f;
		stunned = false;
	}

	public override void Die (){
		Destroy (hpBar);
		anim.SetTrigger ("die");
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
		hpBar.SetActive (true);
		gameObject.layer = 9;
		idle = false;
	}

	//Остановить преследование
	public void Idle () {}

	void Impulse () {
		moveSpeed = Mathf.Sqrt(Time.deltaTime) * impulsePower;
	}

	//Уменьшить ХП + проверка на "смерть"
	void ReduceHP (float damage) {
		if (health <= damage) {
			Die ();
		}
		health -= damage;
	}
}
