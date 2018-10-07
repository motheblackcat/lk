using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float runSpeed = 40;
	public float jumpSpeed = 600;
	public bool canMove = true;
	public bool isGrounded;
	Rigidbody2D rb;
	SpriteRenderer sprite;
	GameObject ghost;
	
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		ghost = GameObject.Find("Ghost");
	}

	void Update() {
		if (canMove) {
			PlayerMove();
			Flip();
		}
	}

	void PlayerMove() {
		if (Input.GetAxis("Horizontal") != 0) { rb.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rb.velocity.y) ; }
		if (Input.GetButtonDown("Jump") && isGrounded) { rb.velocity = Vector2.up * jumpSpeed; }
	}

	void Flip() {
		if (Input.GetAxis("Horizontal") > 0) { sprite.flipX = false; }
		if (Input.GetAxis("Horizontal") < 0) { sprite.flipX = true; }
		GetComponent<CapsuleCollider2D>().offset = Input.GetAxis("Horizontal") > 0 ? new Vector2(0.06f, -0.04f) : new Vector2(-0.06f, -0.04f);
		if (ghost) { ghost.GetComponent<SpriteRenderer>().flipX = sprite.flipX; }
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}
}
