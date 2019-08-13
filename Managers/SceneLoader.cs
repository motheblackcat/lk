using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public int sceneIndex;
	bool loadScene = false;
	public float transitionTimer = 1.0f;

	void Start() {
		GameObject.Find("Sprite Mask").GetComponent<Animator>().SetTrigger("start");
	}

	void Update() {
		if (loadScene) {
			LoadScene(sceneIndex);
		}
	}

	public void LoadScene(int sceneId) {
		GameObject.Find("Player").GetComponent<PlayerControl>().canMove = false;
		loadScene = false;
		transitionTimer -= Time.deltaTime;
		GameObject.Find("Sprite Mask").GetComponent<Animator>().SetTrigger("end");
		if (transitionTimer <= 0) {
			SceneManager.LoadScene(sceneId);
		}
	}
	public void QuitGame() {
		Application.Quit();
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			loadScene = true;
		}
	}
}