using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveControl : MonoBehaviour {
    public bool canMove = false;
    public float moveSpeed = 3;
    GameObject player;
    SpriteRenderer sprite;
    BoxCollider2D box;
    float boxOffsetX;

    void Start() {
	    player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponentsInChildren<BoxCollider2D>()[0];
        boxOffsetX = box.offset.x;
	}

    void Update() {
        if (player && !player.GetComponent<PlayerHealth>().isDead && !GetComponent<EnemyHealthControl>().isDead) { Move(); }
    }

    void Move() {
        sprite.flipX = player.transform.position.x > transform.position.x ? true : false;
        box.offset = sprite.flipX ? new Vector2(-boxOffsetX, box.offset.y) : new Vector2(boxOffsetX, box.offset.y);
        if (canMove) { GetComponent<Rigidbody2D>().velocity = player.transform.position.x > transform.position.x ? new Vector2(moveSpeed, 0) : new Vector2(-moveSpeed, 0); }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") { canMove = true; }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") { canMove = false; }
    }
}