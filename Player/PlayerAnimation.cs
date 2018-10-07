using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	public GameObject sword;

	// TODO: Sword control is specific only to the current sword

	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
	}

	void Update() {
		sword = GameObject.Find("Sword");
		PlayerAnimate();
		if (sword) { SwordPosition(); }
	}

	void PlayerAnimate() {
		if (playerControl.canMove && playerControl.isGrounded) {
			animator.SetBool("run", Input.GetAxis("Horizontal") != 0 ? true : false); 
			if (sword) { sword.GetComponent<Animator>().Play(Input.GetAxis("Horizontal") != 0 ? "Sword_Run" : "Sword_Idle"); }
		}

		if (playerHealth && playerHealth.isDead) {
			sword.GetComponent<SpriteRenderer>().enabled = false;
			animator.SetTrigger("die");
		}
		
		if (sword && !playerControl.isGrounded) { sword.GetComponent<Animator>().Play("Sword_Jump"); }
		animator.SetBool("air", playerControl.isGrounded ? false : true);
		if (playerHealth) { animator.SetBool("hurt", playerHealth.tookDamage && !playerHealth.isDead ? true : false); }
	}

	void SwordPosition() {
		bool flipX = GetComponent<SpriteRenderer>().flipX;
		float y = sword.transform.localPosition.y;
		sword.GetComponent<SpriteRenderer>().flipX = flipX;
		sword.transform.localPosition = flipX ? new Vector2(-0.74f, y) : new Vector2(0.74f, y);
		sword.GetComponent<BoxCollider2D>().offset = flipX ? new Vector2(-0.25f, 0.45f): new Vector2(0.25f, 0.45f);
	}
}
