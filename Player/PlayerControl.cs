using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float runSpeed = 40;
	public float jumpSpeed = 600;
	public bool canMove = true;
	public bool isGrounded;
	Rigidbody2D rb;
	

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.drag = 8;
	}
	
	// Update is called once per frame
	void Update() {
		PlayerMove();
		Flip();
	}

	void PlayerMove() {
		// Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
		// Vector2 pos = transform.position;
		// pos += move * runSpeed * Time.deltaTime;

		if (Input.GetAxis("Horizontal") > 0) {
			rb.AddForce(Vector2.right * runSpeed);
		}
		
		else if (Input.GetAxis("Horizontal") < 0) {
			rb.AddForce(-Vector2.right * runSpeed);
		}

		if (Input.GetButtonDown("Jump") && isGrounded) {
			rb.AddForce(Vector2.up * jumpSpeed);
			// rb.velocity = new Vector2(0, jumpSpeed);
			
		}

		if (Input.GetButtonDown("Attack")) {
            Debug.Log("Player attacked.");
        }
	}

	void Flip() {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();

		if (Input.GetAxis("Horizontal") > 0) {
			sprite.flipX = false;
			col.offset = new Vector2(0.06f, -0.04f);
		}

		else if (Input.GetAxis("Horizontal") < 0) {
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
