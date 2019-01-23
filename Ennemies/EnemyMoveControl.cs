using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveControl : MonoBehaviour {
    public bool canMove = false;
    public bool canSee = false;
    public float moveSpeed = 3;
    GameObject player;
    SpriteRenderer sprite;
    BoxCollider2D box;
    float boxOffsetX;
    bool isGrounded;

    void Start() {
	    player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponentsInChildren<BoxCollider2D>()[0];
        boxOffsetX = box.offset.x;
	}

    void Update() {
        canMove = !player.GetComponent<PlayerHealth>().isDead && !GetComponent<EnemyHealthControl>().tookDamage;
        Move();
    }

    void Move() {
        if (!GetComponent<EnemyHealthControl>().isDead) {
            sprite.flipX =  player.transform.position.x > transform.position.x ? true : false;
        }
        box.offset = sprite.flipX ? new Vector2(-boxOffsetX, box.offset.y) : new Vector2(boxOffsetX, box.offset.y);
        if (canSee && canMove) {
            GetComponent<Rigidbody2D>().velocity = player.transform.position.x > transform.position.x ? new Vector2(moveSpeed, 0) : new Vector2(-moveSpeed, 0);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && isGrounded) {
            canSee = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") { canSee = false; }
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