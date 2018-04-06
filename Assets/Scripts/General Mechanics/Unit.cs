using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public float health = 0f;
	public float armor = 0f;
	public float attack = 0f;
	public float attackSpeed = 0f;
	public float attackRange = 1f;

	[HideInInspector]
	public bool invulnerability = false;
	[HideInInspector]
	public bool attackCheck = true;
	[HideInInspector]
	public bool stunned = false;
	[HideInInspector]
	public bool alive = true;

	public float moveSpeed = 2f;
	[HideInInspector]
	public float input = 0f;
	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public float direction = 1f;

	public abstract void GetDamage ();

	public abstract void SetDamage (float damage, float impulseDirection);

	public abstract void SetStun ();

	public abstract void Die ();
}
