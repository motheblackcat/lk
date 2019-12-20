using UnityEngine;

public class EnemyAudioControl : MonoBehaviour {
	public AudioClip hit;
	public AudioClip death;
	AudioSource audioSource;
	EnemyHealthControl enemyHealth;
	bool wasPlayed = false;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		enemyHealth = GetComponent<EnemyHealthControl>();
	}

	void Update() {
		if (enemyHealth.isDead && !wasPlayed) {
			audioSource.PlayOneShot(death);
			wasPlayed = true;
		}
	}

	public void PlayHitSound() {
		audioSource.PlayOneShot(hit);
	}
}