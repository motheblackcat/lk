using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	public bool startInv = false;
	public float restartLevelTimer = 2f;
	public float flickTimer = 0.2f;
	float invTimerTemp;
	public float invicibilityTimer = 1f;

	void Start() {
		weapon = GameObject.FindGameObjectsWithTag("Weapon")[0];
		sprite = GetComponent<SpriteRenderer>();
		healthBar = GameObject.Find("Content") ? GameObject.Find("Content").GetComponent<Image>() : null;
		invTimerTemp = invicibilityTimer;
	}

	void Update() {
		if (weapon) { wSprite = weapon.GetComponent<SpriteRenderer>(); }
		if (healthBar) { healthBar.fillAmount = playerHealth / 100; }
		InvincibilityTimerStart();
		Death();
	}

	void InvincibilityTimerStart() {
		if (tookDamage) {
			startInv = true;
			tookDamage = false;
		}
		if (startInv) {
			invicibilityTimer -= Time.deltaTime;
			if (invicibilityTimer <= 0) {
				sprite.enabled = true;
				if (!isDead) {
					GetComponent<PlayerControl>().canMove = true;
					if (weapon) { wSprite.enabled = true; }
				}
				startInv = false;
				invicibilityTimer = invTimerTemp;
				CancelInvoke();
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
		// Push back strength sould be taken / set by the enemy?
		GetComponent<Rigidbody2D>().AddForce(pos ? new Vector2(-pushX, pushY) : new Vector2(pushX, pushY), ForceMode2D.Impulse);
	}

	void Death() {
		if (playerHealth <= 0) {
			isDead = true;
			GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
			GetComponent<PlayerControl>().canMove = false;
			restartLevelTimer -= Time.deltaTime;
			if (restartLevelTimer <= 0) {
				GameObject.Find("Transition").GetComponent<SceneLoader>().loadScene = true;
			}
		}
	}

	void TakeDamage(GameObject enemy) {
		playerHealth -= enemy.GetComponent<EnemyHealthControl>().damage;
		Death();
		if (!isDead) {
			tookDamage = true;
			InvokeRepeating("SpriteFlick", 0, flickTimer);
			PushBack(enemy);
		};
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			if (!tookDamage && !startInv && !isDead) {
				TakeDamage(col.gameObject);
			}
		}
	}
}