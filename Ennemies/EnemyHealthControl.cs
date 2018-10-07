using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	public float enemyHealth = 2;
	public bool isDead;
	Animator animator;
	public float destroyTimer = 1.0f;

	void Start() {
		animator = GetComponent<Animator>();
	}

	// TODO: Add feedback when the enemy take damage
	void Update () {
		Death();
	}

	void Death() {
		if (enemyHealth <= 0) {
			isDead = true;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			GetComponent<Collider2D>().enabled = false;
			// Failed attempt to destroy the object after the death animation according to its length
			// if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slime_Death")) { destroyTimer = animator.GetCurrentAnimatorStateInfo(0).length; }
			destroyTimer -= Time.deltaTime;
			if (destroyTimer <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
