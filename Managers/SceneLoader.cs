using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public int sceneIndex;

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
