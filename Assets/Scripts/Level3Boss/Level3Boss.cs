using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Boss : Unit, IReaction<GameObject> {

	//Воскрешение
	List<IReaction<GameObject>> enemiesOnPlatform1 = new List<IReaction<GameObject>>();
	List<IReaction<GameObject>> enemiesOnPlatform2 = new List<IReaction<GameObject>>();
	List<IReaction<GameObject>> enemiesOnPlatform3 = new List<IReaction<GameObject>>();
	List<IReaction<GameObject>> enemiesOnPlatform4 = new List<IReaction<GameObject>>();

	public DangerArea[] triggers;

	alert resurrection1;
	alert resurrection2;
	alert resurrection3;
	alert resurrection4;

	idle reset1;
	idle reset2;
	idle reset3;
	idle reset4;
	                                   
	public int BattlePhase = 1;

	//Лужи
	public GameObject puddle;
	GameObject puddleInstance;
	GameObject player;

	void Awake() {
		foreach (DangerArea trigger in triggers) {
			trigger.AddEnemie (this);
		}
	}

	void Start () {
		
		attackCheck = false;

		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		foreach (IReaction<GameObject> enemy in enemiesOnPlatform1) {
			resurrection1 += enemy.Chase;
			reset1 += enemy.Idle;
		}

		foreach (IReaction<GameObject> enemy in enemiesOnPlatform2) {
			resurrection2 += enemy.Chase;
			reset2 += enemy.Idle;
		}
	
		foreach (IReaction<GameObject> enemy in enemiesOnPlatform3) {
			resurrection3 += enemy.Chase;
			reset3 += enemy.Idle;
		}

		foreach (IReaction<GameObject> enemy in enemiesOnPlatform4) {
			resurrection4 += enemy.Chase;
			reset4 += enemy.Idle;
		}
	}
	
	void Update () {
		if (attackCheck && alive) {
			attackCheck = false;
			StartCreatingAnArea ();
		}
	}

	void StartCreatingAnArea () {
		Debug.Log ("teleport");
		anim.SetTrigger ("creatingAnArea");
	}

	public void CreatingAnArea() {
		Instantiate (puddle, new Vector3 (player.transform.position.x, player.transform.position.y - 0.08f, player.transform.position.z), Quaternion.identity);
	}

	public void Teleport() {
		Vector3 position;
		BattlePhase++;
		position = transform.position;
		position.y += 17f;
		transform.position = position;
	}
		
	void StartResurrection () {
		anim.SetTrigger ("resurrection");
	}

	public void Resurrection() {

		if (BattlePhase == 1) {
			resurrection1 (player);
			attackCheck = true;
		}

		else if (BattlePhase == 2) {
			resurrection2 (player);
			attackCheck = true;
		}

		else if (BattlePhase == 3) {	
			resurrection3 (player);
			attackCheck = true;
		} 

		else if (BattlePhase == 4) {
			resurrection4 (player);
			attackCheck = true;
		}
	}
		
	public void Chase(GameObject target) {
		player = target;
		StartResurrection ();
	}

	public void Idle() {
		
	}

	public override void GetDamage () {
		if (attackCheck) {
			attackCheck = false;
			anim.SetTrigger ("attack");
		}
	}

	public override void SetDamage (float damage, float impulseDirection) {
		
		if ((BattlePhase == 1 && health <= 450f) || (BattlePhase == 2 && health <= 300f) || (BattlePhase == 3 && health <= 150f)) {
			attackCheck = false;
			reset1 ();
			BattlePhase++;
			anim.SetTrigger ("teleport");
			return;
		}

		if (health <= 0f) {
			return;
		}

		health -= damage;
		Debug.Log ("-1");
	}

	public override void SetStun (){
		
	}

	public override void Die (){
		
	}

	public void AddEnemy(IReaction<GameObject> newEnemy, int i) {
		switch (i) {
		case 1:
			enemiesOnPlatform1.Add (newEnemy);
			break;
		case 2:
			enemiesOnPlatform2.Add (newEnemy);
			break;
		case 3:
			enemiesOnPlatform3.Add (newEnemy);
			break;
		case 4:
			enemiesOnPlatform4.Add (newEnemy);
			break;
		}
	}

	public void ResetAttackCheck() {
		attackCheck = true;
	}
}
