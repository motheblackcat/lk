using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	PlayerAttack playerAttack;
	public GameObject weapon;

	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
		playerAttack = GetComponent<PlayerAttack>();
		weapon = GameObject.FindGameObjectsWithTag("Weapon")[0];
	}

	void Update() {
		PlayerAnimate();
		if (weapon) { SwordPosition(); }
	}

	// TODO: Refactor the animation system with proper use of the animator
	void PlayerAnimate() {
		if (playerControl.canMove && playerControl.isGrounded) {
			animator.SetBool("run", Input.GetAxis("Horizontal") != 0 ? true : false); 
			if (weapon) { weapon.GetComponent<Animator>().Play(Input.GetAxis("Horizontal") != 0 ? "Sword_Run" : "Sword_Idle"); }
		}

		animator.SetBool("air", playerControl.isGrounded ? false : true);
		if (weapon && !playerControl.isGrounded) { weapon.GetComponent<Animator>().Play("Sword_Jump"); }
		
		animator.SetBool("attack", playerAttack.isAttacking ? true : false);

		if (playerAttack.isAttacking) {
			weapon.GetComponent<Animator>().PlayInFixedTime("Sword_Attack", 0, playerAttack.attackDuration);
		}

		if (playerHealth) { animator.SetBool("hurt", playerHealth.tookDamage && !playerHealth.isDead ? true : false); }

		if (playerHealth && playerHealth.isDead) {
			animator.SetTrigger("die");
			if (weapon) { weapon.GetComponent<SpriteRenderer>().enabled = false; }
		}
	}

	void SwordPosition() {
		bool flipX = GetComponent<SpriteRenderer>().flipX;
		float y = weapon.transform.localPosition.y;
		weapon.GetComponent<SpriteRenderer>().flipX = flipX;
		weapon.transform.localPosition = flipX ? new Vector2(-0.74f, y) : new Vector2(0.74f, y);
		weapon.GetComponent<BoxCollider2D>().offset = flipX ? new Vector2(-0.25f, 0.45f): new Vector2(0.25f, 0.45f);
	}
}
