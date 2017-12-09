using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
	AudioSource audiosource;
	public AudioClip air;
	public AudioClip attack;
	public AudioClip hurt;
	public AudioClip die;
	public bool wasPlayed = false;

	void Start () {
		audiosource = GetComponent<AudioSource>();
	}
	
	void Update () {
		StartCoroutine(Soundify());
	}

	IEnumerator Soundify() {
		// Refactor this method
		if(Input.GetButtonDown("Jump")) {
			audiosource.PlayOneShot(air);
		}

		if(Input.GetButtonDown("Attack")) {
			audiosource.PlayOneShot(attack);
		}

		// TOFIX: Sound play in loop
		 if(GetComponent<PlayerHealth>().isHurt && !wasPlayed) {
			wasPlayed = true;
			audiosource.PlayOneShot(hurt);
			yield return new WaitForSeconds(hurt.length);
			wasPlayed = false;
		}

		if(GetComponent<PlayerHealth>().isDead && !wasPlayed) {
			wasPlayed = true;
			audiosource.PlayOneShot(die);
		}
	}
}
