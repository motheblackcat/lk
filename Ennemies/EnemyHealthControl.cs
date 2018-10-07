using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	public float enemyHealth = 2;
	public bool isDead;
	public bool tookDamage;
	public float stunTimer = 0.5f;
	float stunTimerReset;
	public float destroyTimer = 1.0f;

	void Start() {
		stunTimerReset = stunTimer;
	}

	// TODO: Add feedback when the enemy take damage
	void Update () {
		Stun();
		Death();
	}
	void Stun() {
		if (tookDamage) {
			stunTimer -= Time.deltaTime;
			GetComponent<EnemyMoveControl>().canMove = false;
			if (stunTimer <= 0) {
				tookDamage = false;
				GetComponent<EnemyMoveControl>().canMove = true;
				stunTimer = stunTimerReset;
			}
 		}
	}

	public void TakeDamage(int damage) {
		enemyHealth -= damage;
		tookDamage = true;
	}

	void Death() {
		if (enemyHealth <= 0) {
			isDead = true;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			GetComponent<Collider2D>().enabled = false;
			// Failed attempt to destroy the object after the death animation according to its length, the timer is always reset
			// if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slime_Death")) { destroyTimer = animator.GetCurrentAnimatorStateInfo(0).length; }
			destroyTimer -= Time.deltaTime;
			if (destroyTimer <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
