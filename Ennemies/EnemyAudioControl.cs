using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioControl : MonoBehaviour {
	AudioSource audioS;
	EnemyHealthControl enemyHealth;
	bool wasPlayed = false;
	public AudioClip hit;
	public AudioClip death;

	void Start() {
		audioS = GetComponent<AudioSource>();
		enemyHealth = GetComponent<EnemyHealthControl>();
	}

	void Update() {
		if (enemyHealth.isDead && !wasPlayed) {
			audioS.PlayOneShot(death);
			wasPlayed = true;
		}
	}

	public void PlayHitSound() {
		audioS.PlayOneShot(hit);
	}
}