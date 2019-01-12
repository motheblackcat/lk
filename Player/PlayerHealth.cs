using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	Image healthBar;
	SpriteRenderer sprite;
	SpriteRenderer wSprite;
	GameObject weapon;
	public float pushX = 10f;
	public float pushY = 10f;
	public float playerHealth = 100f;
	public bool isDead = false;
	public bool tookDamage = false;
	public float restartLevelTimer = 3f;
	public float flickTimer = 0.2f;
	float invTimerTemp;
	public float invicibilityTimer = 1f;
	PlayerSound playerSound;

	void Start() {
		playerSound = GetComponent<PlayerSound>();
		weapon = GameObject.FindGameObjectsWithTag("Weapon")[0];
		sprite = GetComponent<SpriteRenderer>();
		healthBar = GameObject.Find("Content") ?  GameObject.Find("Content").GetComponent<Image>() : null;
		invTimerTemp = invicibilityTimer;
	}

	void Update() {
		if (weapon) { wSprite = weapon.GetComponent<SpriteRenderer>(); }
		if (healthBar) { healthBar.fillAmount = playerHealth / 100; }
		InvincibilityTimerStart();
		resetLevelTimerStart();
	}

	void InvincibilityTimerStart() {
		if (tookDamage) {
			invicibilityTimer -= Time.deltaTime;
			if (invicibilityTimer <= 0) {
				sprite.enabled = true;
				if (!isDead) {
					GetComponent<PlayerControl>().canMove = true;
					if (weapon) { wSprite.enabled = true; }
				}
				tookDamage = false;
				invicibilityTimer = invTimerTemp;
				CancelInvoke();
			}
		}
	}

	void resetLevelTimerStart() {
		if (isDead) {
			restartLevelTimer -= Time.deltaTime;
			if (restartLevelTimer <= 0) {
				SceneManager.LoadScene("Scene_1_RoadtoForest");
			}
		}
	}

	void SpriteFlick() {
		sprite.enabled = !sprite.enabled;
		if (weapon) { wSprite.enabled = !wSprite.enabled; }
	}

	void PushBack(GameObject enemy) {
		GetComponent<PlayerControl>().canMove = false;
		bool pos = enemy.transform.position.x > transform.position.x;
		GetComponent<Rigidbody2D>().AddForce(pos ? new Vector2(-pushX, pushY) : new Vector2(pushX, pushY), ForceMode2D.Impulse);
	}

	void Death() {
		if (playerHealth <= 0) { 
			isDead = true;
			GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
			GetComponent<PlayerControl>().canMove = false;
			// Try to find a more local way to play the sound in regards to the separation of responsabilities
			playerSound.soundPlayed = false;
		}
	}

	void TakeDamage(GameObject enemy) {
		// Will need to move enemy damage to an enemy damage script for enemies with more advanced attack tactics
		playerHealth -= enemy.GetComponent<EnemyHealthControl>().damage;
		tookDamage = true;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") { 
			if (!tookDamage) {
				TakeDamage(col.gameObject);
				Death();
				if (!isDead) { 
					InvokeRepeating("SpriteFlick", 0, flickTimer);
					PushBack(col.gameObject);
				};
			}
		}
	}
}
