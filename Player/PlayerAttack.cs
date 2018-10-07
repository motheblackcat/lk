using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public float attackDuration;
	public bool isAttacking;
	private float timeBtwAttack;
	public float startTimeBtwAttack;
	public Transform atkPos;
	public float atkRange;
	public LayerMask whatIsEnemies;
	public int damage;

	void Update () {
		if (timeBtwAttack <= 0) {
			if (Input.GetButtonDown("Attack")) {
				isAttacking = true;
				Collider2D[] ennemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemies);
				for (int i = 0; i < ennemiesToDamage.Length; i++) {
					ennemiesToDamage[i].GetComponent<EneStartTimeBtwAttackmyHealthControl>().enemyHealth -= damage;
				}
				timeBtwAttack = startTimeBtwAttack;
			}
		} else {
			isAttacking = false;
			timeBtwAttack -= Time.deltaTime;
		}
	}

	// Move it to atkpos object
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(atkPos.position, atkRange);
	}
}
