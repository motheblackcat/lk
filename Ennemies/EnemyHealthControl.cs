using System.Collections;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	SpriteRenderer sprite;
	public float enemyHealth = 2;
	public bool isDead = false;
	public bool isStunned = false;
	public float stunTimer = 0.5f;
	public float pushX = 10f;
	public float pushY = 0f;
	public float destroyTimer = 1.0f;
	public int damage = 25;

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		Death();
	}

	public IEnumerator TakeDamage(int damage) {
		isStunned = true;
		enemyHealth -= damage;
		GetComponent<EnemyAudioControl>().PlayHitSound();
		bool pos = GameObject.Find("Player").transform.position.x > transform.position.x;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(pos ? -pushX : pushX, pushY), ForceMode2D.Impulse);
		yield return new WaitForSeconds(stunTimer);
		isStunned = false;
	}

	//	TODO: destroyTimer should be death animation length
	void Death() {
		if (enemyHealth <= 0) {
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