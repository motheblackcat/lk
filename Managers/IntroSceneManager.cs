using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour {
	Image fadeImage;
	GameObject player;
    public bool introDone = false;
	public float fadeSpeed = 1.0f;
	public float startTimer = 5.0f;
	
	void Start () {
		Cursor.visible = false;
		fadeImage = GameObject.Find("Fade Image").GetComponent<Image>();
		fadeImage.enabled = true;
		player = GameObject.Find("Player");
	}
	
	void Update () {
		FadeOut();
		CheckTimer();
	}

	void FadeOut() {
		Color clearColor = Color.clear;
		fadeImage.color = Color.Lerp(fadeImage.color, clearColor, Time.time * Time.deltaTime * fadeSpeed);
	}

	void CheckTimer() {
		if (Time.time > GameObject.Find("MainCamera").GetComponent<IntroSceneManager>().startTimer) {
            introDone = true;
			FreePlayer();
        }
	}

	void FreePlayer() {
		if (Time.time > startTimer) {
			player.GetComponent<PlayerControl>().canMove = true;
			player.GetComponent<Animator>().enabled = true;
			player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}
