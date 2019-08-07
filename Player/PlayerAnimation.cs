using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	PlayerAttack playerAttack;
	GameObject weapon;

	void Start() {
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

	// Attacks right after landing are not animated but still deals damages
	void PlayerAnimate() {
		animator.SetBool("run", GetComponent<Rigidbody2D>().velocity.x != 0);
		animator.SetBool("air", !playerControl.isGrounded);
		if (playerAttack && !GameObject.Find("DialogBox").GetComponent<Image>().enabled) { animator.SetBool("attack", playerAttack.isAttacking); }
		if (playerHealth) { animator.SetBool("hurt", playerHealth.tookDamage && !playerHealth.isDead); }
		if (playerHealth && playerHealth.isDead) { animator.SetTrigger("die"); }
	}

	void SwordPosition() {
		bool flipX = GetComponent<SpriteRenderer>().flipX;
		weapon.GetComponent<SpriteRenderer>().flipX = flipX;
		float y = weapon.transform.localPosition.y;
		weapon.transform.localPosition = flipX ? new Vector2(-0.74f, y) : new Vector2(0.74f, y);
	}
}