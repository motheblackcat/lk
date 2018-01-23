using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveControl : MonoBehaviour {
    public bool canMove = true;
    public float moveSpeed = 3;
    GameObject player;
    SpriteRenderer sprite;

    void Start() {
	    player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
	}

    void Update() {
        if (canMove && !player.GetComponent<PlayerHealth>().isDead) {
            if (player.transform.position.x > transform.position.x) {
                sprite.flipX = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
                GetComponent<BoxCollider2D>().offset = new Vector2(3.4f, GetComponent<BoxCollider2D>().offset.y);
			} else {
                sprite.flipX = false;                
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
                GetComponent<BoxCollider2D>().offset = new Vector2(-3.4f, GetComponent<BoxCollider2D>().offset.y);
			}
        }
    }
    
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            canMove = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            canMove = false;
        }
    }
}