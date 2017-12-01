using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;

	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
	}
	
	void Update () {
		bool playerCanMove = playerControl.canMove;
		bool isGrounded = playerControl.isGrounded;

		if (playerCanMove) {
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

		// if (isDead) {
		// 	animator.SetBool("hurt", false);
		// 	GetComponent(Animator).SetTrigger("die");
		// }
	}
}
