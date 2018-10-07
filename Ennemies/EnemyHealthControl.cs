using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	public float enemyHealth = 2;
	public bool isDead;

	// TODO: Add feedback when the enemy take damage
	void Update () {
		if (enemyHealth <= 0) { Destroy(gameObject); }
	}
}
