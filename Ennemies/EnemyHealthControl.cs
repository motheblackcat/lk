using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	public float enemyHealth = 2;
	public bool isDead = false;
	public bool isStunned = false;
	public float stunTimer = 0.5f;
	float stunTimerReset;
	public float pushX = 10f;
	public float pushY = 0f;
	public float destroyTimer = 1.0f;
	public float flickTimer = 0.1f;
	SpriteRenderer sprite;
	public int damage = 25;

	void Start() {
		stunTimerReset = stunTimer;
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		Stun();
		Death();
	}
	void Stun() {
		if (isStunned) {
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0) {
				isStunned = false;
				CancelInvoke();
				sprite.enabled = true;
				stunTimer = stunTimerReset;
			}
		}

	}

	void SpriteFlick() {
		sprite.enabled = !sprite.enabled;
	}

	void PushBack() {
		bool pos = GameObject.Find("Player").transform.position.x > transform.position.x;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(pos ? -pushX : pushX, pushY), ForceMode2D.Impulse);
	}

	public void TakeDamage(int damage) {
		enemyHealth -= damage;
		isStunned = true;
		GetComponent<EnemyAudioControl>().PlayHitSound();
		PushBack();
		InvokeRepeating("SpriteFlick", 0, flickTimer);
	}

	void Death() {
		if (enemyHealth <= 0) {
			CancelInvoke();
			sprite.enabled = true;
			isDead = true;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			GetComponent<Collider2D>().enabled = false;
			destroyTimer -= Time.deltaTime;
			if (destroyTimer <= 0) {
				Destroy(gameObject);
			}
		}
	}
}