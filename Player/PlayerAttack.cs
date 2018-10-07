using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

PlayerHealth playerHealth;
public GameObject weapon;
public bool isAttacking = false;
public float attackDuration = 0.2f;
float tempAtkStored;

	void Start() {
		playerHealth = GetComponent<PlayerHealth>();
		weapon = GameObject.FindGameObjectsWithTag("Weapon")[0];
		tempAtkStored = attackDuration;
	}
		
	void Update () {
		if (weapon) { Attack(); }
		if (isAttacking) { StartAttackTimer(); }
	}

	void StartAttackTimer() {
		attackDuration -= Time.deltaTime;
		if (attackDuration <= 0) {
			isAttacking = false;
			weapon.GetComponent<BoxCollider2D>().enabled = false;
			attackDuration = tempAtkStored;
		}
	}

	void Attack() {
		if (Input.GetButtonDown("Attack") && !playerHealth.tookDamage && !playerHealth.isDead) {
			isAttacking = true;
			weapon.GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
