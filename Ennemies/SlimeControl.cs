using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeControl : MonoBehaviour {
	public int enemyHealth = 2;

	void Update () {
		if (enemyHealth <= 0) {
			Death();
		}
	}

	void Death() {
		Destroy(gameObject);		
	}

	void TakeDamage() {
		enemyHealth -= 1;		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Weapon") {
			TakeDamage(); // TOFIX: Applies damage twice;
		}
	}
}
