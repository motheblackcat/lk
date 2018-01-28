using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveControl : MonoBehaviour {
    public bool canMove = true;
    public float moveSpeed = 3;
    GameObject player;
    SpriteRenderer sprite;
    public float visionDistance = 8.0f;
    

    void Start() {
	    player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
	}

    void Update() {
        if (canMove && !player.GetComponent<PlayerHealth>().isDead) {
            if (player.transform.position.x > transform.position.x) {
                sprite.flipX = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
			} else {
                sprite.flipX = false;                
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
			}
        }

        followPlayer(); 
    }

    void followPlayer() {
        float xPos = player.transform.position.x - transform.position.x;
        float yPos = player.transform.position.y - transform.position.y;

        if (xPos < visionDistance && xPos > -visionDistance && yPos < 2.5f && yPos > -1 && !GetComponent<EnemyHealthControl>().hasTakenDamage) {
            if (!GetComponent<EnemyHealthControl>().wasPushed) {
                canMove = true;
            }
        } else {
            canMove = false;
        }
    }
}