using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	// public int pushX = 10;
	// public int pushY = 10;
	Image healthBar;
	SpriteRenderer sprite;
	SpriteRenderer wSprite;
	public float playerHealth = 100f;
	public bool isDead = false;
	public bool tookDamage = false;
	public float restartLevelTimer = 3f;
	public float flickTimer = 0.2f;
	float invTimerTemp;
	public float invicibilityTimer = 1f;

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
		wSprite = GameObject.FindGameObjectsWithTag("Weapon")[0] ? GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<SpriteRenderer>() : null;
		healthBar = GameObject.Find("Content").GetComponent<Image>();
		invTimerTemp = invicibilityTimer;
	}

	void Update() {
		healthBar.fillAmount = playerHealth / 100;
		InvincibilityTimerStart();
		if (playerHealth <= 0) { Death(); }
	}

	// IEnumerator PushBack(GameObject enemy) {
	// 	if (!isDead) {
	// 		GetComponent<PlayerControl>().canMove = false;
	// 		isHurt = true;
	// 		if (enemy.transform.position.x > transform.position.x) {
	// 			GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushX, pushY), ForceMode2D.Impulse);
	// 		} else {
	// 			GetComponent<Rigidbody2D>().AddForce(new Vector2(pushX, pushY), ForceMode2D.Impulse);
	// 		}
	// 		yield return new WaitForSeconds(timer);
	// 		isHurt = false;
	// 		GetComponent<PlayerControl>().canMove = true;
	// 	}
	// }

	void InvincibilityTimerStart() {
		if (tookDamage) {
			invicibilityTimer -= Time.deltaTime;
			if (invicibilityTimer <= 0) {
				sprite.enabled = true;
				wSprite.enabled = true;
				tookDamage = false;
				invicibilityTimer = invTimerTemp;
				CancelInvoke();
			}
		}
	}

	void SpriteFlick() {
		sprite.enabled = !sprite.enabled;
		if (wSprite) {
			wSprite.enabled = !wSprite.enabled;
		}
	}

	void Death() {
		isDead = true;
		GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
		GetComponent<PlayerControl>().canMove = false;
		restartLevelTimer -= Time.deltaTime;
		if (restartLevelTimer <= 0) {
			SceneManager.LoadScene("Scene_1_RoadtoForest");
		}
	}

	void TakeDamage(GameObject enemy) {
		playerHealth -= 25;
		tookDamage = true;
		Destroy(GameObject.Find("Slime"));
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") { 
			if (!tookDamage) {
				TakeDamage(col.gameObject);
				InvokeRepeating("SpriteFlick", 0, flickTimer);
			}
		}
	}
}
