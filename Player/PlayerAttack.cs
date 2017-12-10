using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

public bool isAttacking = false;
public float attackDuration = 0.1f;
	
	void Update () {
		if(Input.GetButtonDown("Attack")) {
			if (GetComponent<PlayerControl>().canMove) {
				StartCoroutine(Attack());
			}
		}
	}

	IEnumerator Attack() {
		// Fix the attack system (player can spam, the animation is not fully played everytime but the damages are dealt)
		isAttacking = true;
		// Can be switched with the sword's attack duration
		yield return new WaitForSeconds(attackDuration);
		isAttacking = false;
	}
}
