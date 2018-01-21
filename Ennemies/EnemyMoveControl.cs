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
        if (canMove) {
            if (player.transform.position.x > transform.position.x) {
                sprite.flipX = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
			} else {
                sprite.flipX = false;                
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
			}
        }
    }
}