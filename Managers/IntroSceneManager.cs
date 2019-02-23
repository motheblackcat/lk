using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour {
	Image fadeImage;
	GameObject player;
	public float speed = 1.0f;
	public float globalTimer;
	public float startTimer = 5.0f;
	
	void Start () {
		Cursor.visible = false;
		fadeImage = GameObject.Find("Fade Image").GetComponent<Image>();
		fadeImage.enabled = true;
		player = GameObject.Find("Player");
	}
	
	void FixedUpdate () {
		globalTimer = Time.time;		
		FadeOut();
		FreePlayer();
	}

	void FadeOut() {
		Color clearColor = Color.clear;
		fadeImage.color = Color.Lerp(fadeImage.color, clearColor, Time.time * Time.deltaTime * speed);
	}

	void FreePlayer() {
		if (globalTimer > startTimer) {
			player.GetComponent<PlayerControl>().canMove = true;
			player.GetComponent<Animator>().enabled = true;
			player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}
