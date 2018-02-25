using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

public bool isAttacking = false;
public float attackDuration = 0.2f;
	
	void Update () {
		if (!GetComponent<PlayerHealth>().isDead) {
			if(Input.GetButtonDown("Attack") && !isAttacking && GetComponent<PlayerControl>().canMove) {
				StartCoroutine(Attack());
			}
		}
	}

	IEnumerator Attack() {
		// FIX THE COLLIDER ACTIVATION
		GameObject.Find("Sword").GetComponent<Collider2D>().enabled = true;
		isAttacking = true;		
		yield return new WaitForSeconds(attackDuration);
		GameObject.Find("Sword").GetComponent<Collider2D>().enabled = false;
		isAttacking = false;
	}
}
