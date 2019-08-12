using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public int sceneIndex;

	public void LoadScene(int sceneId) {
		SceneManager.LoadScene(sceneId);
	}
	public void QuitGame() {
		Application.Quit();
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			LoadScene(sceneIndex);
		}
	}
}