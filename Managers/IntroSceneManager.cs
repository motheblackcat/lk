using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour {
	GameObject player;
	public bool introDone = false;
	public bool introMove = true;
	public float startTimer = 5.0f;

	void Start() {
		player = GameObject.Find("Player");
	}

	void Update() {
		CheckTimer();
	}

	void CheckTimer() {
		startTimer -= Time.deltaTime;
		if (startTimer <= 0) {
			FreePlayer();
		}
	}

	void FreePlayer() {
		introDone = true;
		if (introMove) {
			player.GetComponent<PlayerControl>().canMove = true;
		}
		player.GetComponent<Animator>().enabled = true;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		return;
	}
}