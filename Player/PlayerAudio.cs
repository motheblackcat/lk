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

	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Soundify();
	}

	void Soundify() {
		// Refactor this method
		if(Input.GetButtonDown("Jump")) {
			audiosource.PlayOneShot(air);
		}

		if(Input.GetButtonDown("Attack")) {
			audiosource.PlayOneShot(attack);
		}

		// TOFIX: Sound play in loop
		if(GetComponent<PlayerHealth>().isHurt) {
			audiosource.PlayOneShot(hurt);
		}

		if(GetComponent<PlayerHealth>().isDead) {
			audiosource.PlayOneShot(die);
		}
	}
}
