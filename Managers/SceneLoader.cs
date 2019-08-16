using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer = 1.5f;
	public string transitionType = "box";
	bool startScene;
	float startTimer;

	void Start() {
		animator = GameObject.Find("Transition").GetComponent<Animator>();
		animator.SetFloat("transitionSpeed", 1 / transitionTimer);
		animator.SetTrigger("start" + transitionType);
		playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
		startTimer = transitionTimer;
		startScene = true;
	}

	void Update() {
		if (startScene) {
			startTimer -= Time.deltaTime;
			if (startTimer >= 0) {
				playerControl.canMove = false;
			} else {
				playerControl.canMove = true;
				startTimer = transitionTimer;
				startScene = false;
			}
		}

		if (loadScene) {
			LoadScene(sceneIndex);
		}
	}

	void LoadScene(int sceneId) {
		IntroSceneManager introSceneManager = Camera.main.GetComponent<IntroSceneManager>();
		if (introSceneManager) { introSceneManager.introMove = false; }
		playerControl.canMove = false;
		transitionTimer -= Time.deltaTime;
		GameObject.Find("Transition").GetComponent<Animator>().SetTrigger("end" + transitionType);
		if (transitionTimer <= 0) {
			SceneManager.LoadScene(sceneId);
			loadScene = false;
		}
	}
}