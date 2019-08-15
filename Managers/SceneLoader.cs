using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	GameObject spriteMask;
	Animator animator;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer;
	public string transitionType = "box";

	void Start() {
		spriteMask = GameObject.Find("Transition");
		animator = spriteMask.GetComponent<Animator>();
		animator.SetFloat("transitionSpeed", 1 / transitionTimer);
		animator.SetTrigger("start" + transitionType);
	}

	void Update() {
		if (loadScene) {
			LoadScene(sceneIndex);
		}
	}

	void LoadScene(int sceneId) {
		IntroSceneManager introSceneManager = Camera.main.GetComponent<IntroSceneManager>();
		if (introSceneManager) { introSceneManager.introMove = false; }
		GameObject.Find("Player").GetComponent<PlayerControl>().canMove = false;
		transitionTimer -= Time.deltaTime;
		GameObject.Find("Transition").GetComponent<Animator>().SetTrigger("end" + transitionType);
		if (transitionTimer <= 0) {
			SceneManager.LoadScene(sceneId);
			loadScene = false;
		}
	}
}