using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSound : MonoBehaviour {
    AudioSource audioSource;

	PlayerControl playerControl;
	PlayerHealth playerHealth;
	PlayerAttack playerAttack;

    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    public bool soundPlayed = false;

	void Start () {
        audioSource = GetComponent<AudioSource>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
		playerAttack = GetComponent<PlayerAttack>();
	}

    // Audio clip format and volume normalization required when removing placeholders
    void Update() {
        if (!GameObject.Find("DialogBox").GetComponent<Image>().enabled) {
            if (Input.GetButtonDown("Jump") && playerControl.isGrounded && playerControl.canMove) {
                audioSource.PlayOneShot(jumpSound, 0.2f);
            }

            if (playerAttack.isAttacking) {
                audioSource.PlayOneShot(attackSound);
            }


            if (playerHealth.isDead && !soundPlayed) {
                audioSource.PlayOneShot(deathSound);
                soundPlayed = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && playerHealth.tookDamage && !playerHealth.isDead) {
            audioSource.PlayOneShot(hurtSound);
        }
    }
}
