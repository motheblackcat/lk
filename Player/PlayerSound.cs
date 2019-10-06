﻿using System.Collections;
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
    public bool deathSoundPlayed = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update() {
        if (GameObject.Find("DialogBox") ? !GameObject.Find("DialogBox").GetComponent<Image>().enabled : true) {
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

            if (playerHealth.tookDamage && !playerHealth.isDead) {
                audioSource.PlayOneShot(hurtSound);
            }
        }
    }
}