using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	// var isHurt = GetComponent(PlayerHealth).playerHurt;
	// var isDead = GetComponent(PlayerHealth).isDead;
	// var isAttacking = GetComponent(PlayerControl).isAttacking;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		bool playerCanMove = playerControl.canMove;
		bool isGrounded = playerControl.isGrounded;

		if (playerCanMove) {
			if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) {
				animator.SetBool("run", true);
			}
			else {
				animator.SetBool("run", false);
			}
		}

		if (isGrounded) {
			animator.SetBool("air", false);
		}
		else {
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
