using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
	AudioSource audiosource;
	public AudioClip air;
	public AudioClip attack;
	public AudioClip hurt;
	public AudioClip hit;
	public AudioClip die;
	public bool wasPlayed = false;
	PlayerHealth playerHealth;

	void Start () {
		audiosource = GetComponent<AudioSource>();
		playerHealth = GetComponent<PlayerHealth>();
	}
	
	void Update () {
		StartCoroutine(Soundify());
	}

	IEnumerator Soundify() {
		if(!playerHealth.isDead) {
			if(Input.GetButtonDown("Jump") && GetComponent<PlayerControl>().isGrounded) {
				audiosource.PlayOneShot(air);
			}

			if(Input.GetButtonDown("Attack")) {
				audiosource.PlayOneShot(attack);
			}
		}

		// TODO: Refactor this
		if(playerHealth.isHurt && !wasPlayed) {
			wasPlayed = true;
			audiosource.PlayOneShot(hurt);
			yield return new WaitForSeconds(1);
			wasPlayed = false;
		}
		
		// TODO: Refactor this
		if(GetComponent<Animator>().GetBool("die") && !wasPlayed) {
			wasPlayed = true;
			audiosource.PlayOneShot(die);
		}
	}

	// TODO: Fix this for the hit sound, it is not working
	IEnumerator OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "enemy" && !wasPlayed) {
		wasPlayed = true;
		audiosource.PlayOneShot(hit);
		yield return new WaitForSeconds(1);
		wasPlayed = false;
		}
	}
}
