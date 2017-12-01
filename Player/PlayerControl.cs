using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float runSpeed = 40;
	public float jumpSpeed = 600;
	public bool canMove;
	public bool isGrounded;
	Rigidbody2D rb;
	

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
		PlayerMove();
		Flip();
	}

	void PlayerMove() {
		if (Input.GetAxis("Horizontal") > 0) {
			rb.AddForce(Vector2.right * runSpeed * 10);
		}
		
		if (Input.GetAxis("Horizontal") < 0) {
			rb.AddForce(-Vector2.right * runSpeed * 10);
		}

		if (Input.GetButtonDown("Jump") && isGrounded) {
			rb.AddForce(Vector2.up * jumpSpeed * 100);
		}

		if (Input.GetButtonDown("Attack")) {
            Debug.Log("Player attacked.");
        }

		// TODO: Limit the velocity during a jump
		// Debug.Log(rb.velocity.x);
		if (rb.velocity.x > 7) {
			rb.velocity = new Vector2(7, rb.velocity.y);
		}
	}

	void Flip() {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();

		if (Input.GetAxis("Horizontal") > 0) {
			sprite.flipX = false;
			col.offset = new Vector2(0.06f, -0.04f);
		}

		if (Input.GetAxis("Horizontal") < 0) {
			sprite.flipX = true;
			col.offset = new Vector2(-0.06f, -0.04f);
		}

		if (sprite.flipX) {
        	GameObject.Find("Ghost").GetComponent<SpriteRenderer>().flipX = true;
    }
    	else {
       	 	GameObject.Find("Ghost").GetComponent<SpriteRenderer>().flipX = false;
    	}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}
}
