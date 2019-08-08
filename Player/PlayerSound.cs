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
    public AudioClip damageSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public bool deathSoundPlayed = false;
    public bool damageSoundPlayed = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update() {
        if (!GameObject.Find("DialogBox").GetComponent<Image>().enabled) {
            if (Input.GetButtonDown("Jump") && playerControl.isGrounded && playerControl.canMove) {
                audioSource.PlayOneShot(jumpSound);
            }

            if (playerAttack.isAttacking) {
                audioSource.PlayOneShot(attackSound);
            }

            if (playerHealth.isDead && !deathSoundPlayed) {
                audioSource.PlayOneShot(deathSound);
                deathSoundPlayed = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            if (playerHealth.tookDamage && !playerHealth.isDead) {
                audioSource.PlayOneShot(hurtSound);
            }
        }
    }
}