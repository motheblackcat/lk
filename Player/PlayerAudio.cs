// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerAudio : MonoBehaviour {
// 	AudioSource audiosource;
// 	public AudioClip air;
// 	public AudioClip attack;
// 	public AudioClip hurt;
// 	public AudioClip die;
// 	public bool wasPlayed = false;
// 	PlayerHealth playerHealth;


// 	void Start () {
// 		audiosource = GetComponent<AudioSource>();
// 		playerHealth = GetComponent<PlayerHealth>();
// 	}
	
// 	void Update () {
// 		StartCoroutine(Soundify());
// 	}

// 	void Soundify() {
// 		// SOUNDS SHOULD BE PLAYED ACCORDING TO THE PLAYER ANIMATIONS INSTEAD OF BUTTON PRESS?
// 		// Recheck this script	
// 		if(!playerHealth.isDead && GetComponent<PlayerControl>().canMove) {
// 			if(Input.GetButtonDown("Jump") && GetComponent<PlayerControl>().isGrounded) {
// 				audiosource.PlayOneShot(air);
// 			}

// 			if(Input.GetButtonDown("Attack") && !GetComponent<PlayerAttack>().isAttacking) {
// 				audiosource.PlayOneShot(attack);
// 			}
// 		}

// 		// if(playerHealth.isHurt && !wasPlayed) {
// 		// 	wasPlayed = true;
// 		// 	audiosource.PlayOneShot(hurt);
// 		// 	yield return new WaitForSeconds(1);
// 		// 	wasPlayed = false;
// 		// }
		
// 		if(GetComponent<Animator>().GetBool("die") && !wasPlayed) {
// 			wasPlayed = true;
// 			audiosource.PlayOneShot(die);
// 		}
// 	}
// }
