using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	GameObject enemy;
	public int pushX = 10;
	public int pushY = 10;
	Image healthBar;
	public bool isDead = false;
	public bool isHurt = false;
	SpriteRenderer sprite;
	// Invicibility timer
	public float timer  = 0.5f;

	void Start() {
		healthBar = GameObject.Find("Content").GetComponent<Image>();
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		if(healthBar.fillAmount <= 0) {
			StartCoroutine(Death());
		}
	}

	// Refactor so it can be used by both player and enemies
	IEnumerator PushBack(GameObject enemy) {
		if (!isDead) {
			GetComponent<PlayerControl>().canMove = false;
			isHurt = true;
			if (enemy.transform.position.x > transform.position.x) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushX, pushY), ForceMode2D.Impulse);
			} else {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(pushX, pushY), ForceMode2D.Impulse);			
			}
			yield return new WaitForSeconds(timer);
			isHurt = false;		
			GetComponent<PlayerControl>().canMove = true;
		}
	}

	IEnumerator Flick() {
		sprite.enabled = false;
		yield return new WaitForSeconds(0.1f);
		sprite.enabled = true;
	}
	
	IEnumerator Death() {
		isDead = true;
		GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("Scene_1_RoadtoForest");
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<EnemyHealthControl>().isDead) {
			StartCoroutine(PushBack(other.gameObject));
			StartCoroutine(Flick());
			// Convert to more useable values
			// Replace by enemy damage other.gameObject.GetComponent<EnemyDamage>.enemyDamage
			healthBar.fillAmount -= 0.25f;
		}
	}
}
