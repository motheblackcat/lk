using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Manager : MonoBehaviour {
	GameObject[] clouds;
	float[] speeds = new float[3];
	public float cameraPos;
	public float paraSpeed = 2.0f;

	void Start() {
		Cursor.visible = false;
		GameObject.Find("Sky").transform.parent = GameObject.Find("MainCamera").transform;
		clouds = GameObject.FindGameObjectsWithTag("Cloud");
		for (int i = 0; i < clouds.Length; i++) {
			speeds[i] = Random.value;
		}
	}

	void FixedUpdate () {
		MoveClouds();
		SimpleParallax();
	}

	void MoveClouds() {
		for (int i = 0; i < clouds.Length; i++) {
			clouds[i].transform.Translate(Vector2.left * (speeds[i] / 3) * Time.deltaTime);
		}
	}

	void SimpleParallax() {
		GameObject thirdPlan = GameObject.Find("Plains 3rd Plan");
		cameraPos = GameObject.Find("MainCamera").transform.position.x;
		thirdPlan.transform.position = new Vector2((cameraPos - 10) / paraSpeed, 0);
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			SceneManager.LoadScene("Scene_2_Forest");
		}	
	}
}
