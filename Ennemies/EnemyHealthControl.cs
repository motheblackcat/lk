using System.Collections;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	SpriteRenderer sprite;
	Animator animator;
	Rigidbody2D rb;
	Collider2D col;
	EnemyAudioControl enemyAudio;
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
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		enemyAudio = GetComponent<EnemyAudioControl>();
	}

	void Update() {
		Death();
	}

	public IEnumerator TakeDamage(int damage) {
		isStunned = true;
		enemyHealth -= damage;
		enemyAudio.PlayHitSound();
		bool pos = GameObject.Find("Player").transform.position.x > transform.position.x;
		rb.AddForce(new Vector2(pos ? -pushX : pushX, pushY), ForceMode2D.Impulse);
		yield return new WaitForSeconds(stunTimer);
		isStunned = false;
	}

	void Death() {
		if (enemyHealth <= 0) {
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !isDead) destroyTimer = animator.GetCurrentAnimatorStateInfo(0).length;
			isDead = true;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			col.enabled = false;
			destroyTimer -= Time.deltaTime;
			if (destroyTimer <= 0) {
				Destroy(gameObject);
			}
		}
	}
}