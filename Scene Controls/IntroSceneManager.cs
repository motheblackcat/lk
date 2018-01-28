using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour {
	Image fadeImage;
	public float speed = 1.0f;
	
	void Start () {
		fadeImage = GameObject.Find("Fade Image").GetComponent<Image>();
		fadeImage.enabled = true;
	}
	
	void FixedUpdate () {
		FadeOut();
	}

	void FadeOut() {
		Color clearColor = Color.clear;
		fadeImage.color = Color.Lerp(fadeImage.color, clearColor, Time.time * Time.deltaTime * speed);
	}
}
