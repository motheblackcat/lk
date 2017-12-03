using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;

	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
	}
	
	void Update () {
		bool canMove = playerControl.canMove;
		bool isGrounded = playerControl.isGrounded;
		bool isDead = playerHealth.isDead;

		if (canMove) {
			if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) {
				animator.SetBool("run", true);
			} else {
				animator.SetBool("run", false);
			}
		}

		if (isGrounded) {
			animator.SetBool("air", false);
		} else {
			animator.SetBool("air", true);
		}
		
		// if (isHurt) {
		// 	animator.SetBool("hurt", true);
		// }
		// else {
		// 	animator.SetBool("hurt", false);
		// }

		if (isDead) {
			GetComponent<Animator>().SetTrigger("die");
		}
	}
}
