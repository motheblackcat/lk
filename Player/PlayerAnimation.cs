using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	PlayerAttack playerAttack;
	GameObject weapon;

	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
		playerAttack = GetComponent<PlayerAttack>();
		weapon = GameObject.FindGameObjectsWithTag("Weapon").Length > 0 ? GameObject.FindGameObjectsWithTag("Weapon")[0] : null;
	}

	void Update() {
		PlayerAnimate();
		if (weapon) { SwordPosition(); }
	}

	void PlayerAnimate() {
		if (playerControl.canMove && playerControl.isGrounded) { animator.SetBool("run", Input.GetAxis("Horizontal") != 0 ? true : false); }
		animator.SetBool("air", playerControl.isGrounded ? false : true);
		if (playerAttack) { animator.SetBool("attack", playerAttack.isAttacking ? true : false); }
		if (playerHealth) { animator.SetBool("hurt", playerHealth.tookDamage && !playerHealth.isDead ? true : false); }
		if (playerHealth && playerHealth.isDead) { animator.SetTrigger("die"); }
	}

	void SwordPosition() {
		bool flipX = GetComponent<SpriteRenderer>().flipX;
		weapon.GetComponent<SpriteRenderer>().flipX = flipX;
		float y = weapon.transform.localPosition.y;
		weapon.transform.localPosition = flipX ? new Vector2(-0.74f, y) : new Vector2(0.74f, y);
	}
}
