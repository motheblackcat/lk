using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	PlayerAttack playerAttack;

	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
		playerAttack = GetComponent<PlayerAttack>();
	}
	
	void Update () {
		// Refactor this section?
		if (playerAttack.isAttacking) {
			animator.SetBool("attack", true);
		}
		else {
			animator.SetBool("attack", false);
		}

		if (playerControl.canMove) {
			if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) {
				animator.SetBool("run", true);
			} else {
				animator.SetBool("run", false);
			}
		}

		if (playerControl.isGrounded) {
			animator.SetBool("air", false);
		} else {
			animator.SetBool("air", true);
		}
		
		if (playerHealth.isHurt) {
			animator.SetBool("hurt", true);
		}
		else {
			animator.SetBool("hurt", false);
		}

		// TOFIX: If isHurt is not used the ghost appears before the death animation
		if (playerHealth.isDead && !playerHealth.isHurt) {
			GetComponent<Animator>().SetTrigger("die");
		}
	}
}
