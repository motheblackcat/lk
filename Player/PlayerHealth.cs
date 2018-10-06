﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	Image healthBar;
	SpriteRenderer sprite;
	SpriteRenderer wSprite;
	public float pushX = 10f;
	public float pushY = 10f;
	public float playerHealth = 100f;
	public bool isDead = false;
	public bool tookDamage = false;
	public float restartLevelTimer = 3f;
	public float flickTimer = 0.2f;
	float invTimerTemp;
	public float invicibilityTimer = 1f;

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
		healthBar = GameObject.Find("Content").GetComponent<Image>();
		invTimerTemp = invicibilityTimer;
	}

	void Update() {
		wSprite = GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<SpriteRenderer>();
		healthBar.fillAmount = playerHealth / 100;
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
					wSprite.enabled = true;
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
		if (wSprite != null) {
			wSprite.enabled = !wSprite.enabled;
		}
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
		}
	}

	void TakeDamage(GameObject enemy) {
		// Use enemy damage value instead
		playerHealth -= 25;
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
