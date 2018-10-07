using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	public float enemyHealth = 2;
	public bool isDead;

	void Update () {
		if (enemyHealth <= 0) { Destroy(gameObject); }
	}

	void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Weapon") {
			Debug.Log("ToUCHED");
			enemyHealth -= 1;
		}		
	}
}
