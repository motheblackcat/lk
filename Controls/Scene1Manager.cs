using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour {
	GameObject[] clouds;
	float[] speeds = new float[3];

	void Start() {
		Cursor.visible = false;
		GameObject.Find("Sky").transform.parent = GameObject.Find("MainCamera").transform;
		clouds = GameObject.FindGameObjectsWithTag("Cloud");
		for (int i = 0; i < clouds.Length; i++) {
			speeds[i] = Random.value;
		}
	}

	void FixedUpdate () {
		for (int i = 0; i < clouds.Length; i++) {
			clouds[i].transform.Translate(Vector2.left * (speeds[i] / 3) * Time.deltaTime);
			Debug.Log(clouds[i] + " " + speeds[i] / 3);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		GameObject.Find("MainCamera").GetComponent<CameraFollowPlayer>().enabled = false;
	}

	void OnTriggerExit2D(Collider2D other) {
		GameObject.Find("MainCamera").GetComponent<CameraFollowPlayer>().enabled = true;
	}
}
