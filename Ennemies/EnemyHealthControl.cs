using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {
	public int enemyHealth = 2;
	public bool hasTakenDamage = false;
	public bool isDead = false;
	GameObject player;
	public int pushX = 5;
	public int pushY = 5;
	SpriteRenderer sprite;
	public float timer  = 0.5f;

	void Start() {
		player = GameObject.Find("Player");
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update () {
		if (enemyHealth <= 0) {
			StartCoroutine(Death());
		}
	}

	void LateUpdate() {
		float maxVelocity = 10;		
     	GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxVelocity);
		
		if (hasTakenDamage) {
			hasTakenDamage = false;
		}
	}

	IEnumerator Death() {
		isDead = true;
		// Multiple collider enemies (temp to be refactored)
		Collider2D[] colliders = GetComponents<Collider2D>();
		foreach(Collider2D collider in colliders) {
			collider.enabled = false;
		}
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSeconds(timer);		
		Destroy(gameObject);
	}

	void TakeDamage() {
		if (!hasTakenDamage) {
			enemyHealth -= 1;
			hasTakenDamage = true;
		}
	}
	
	IEnumerator PushBack(GameObject player) {
		if (!isDead) {
			GetComponent<EnemyMoveControl>().canMove = false;
			if (player.transform.position.x > transform.position.x) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushX, pushY), ForceMode2D.Impulse);
			} else {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(pushX, pushY), ForceMode2D.Impulse);			
			}
			yield return new WaitForSeconds(timer);
			GetComponent<EnemyMoveControl>().canMove = true;
		}
	}

	IEnumerator Flick() {
		sprite.enabled = false;
		yield return new WaitForSeconds(0.1f);
		sprite.enabled = true;
	}

	// Two colliders are used one for trigger the other for physics, this should be refactored it create a bug with the vision field (enemy receive damage from afar)
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Weapon") {
			StartCoroutine(PushBack(player));
			StartCoroutine(Flick());
			TakeDamage();
		}
	}
}
