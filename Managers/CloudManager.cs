using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudManager : MonoBehaviour {
	GameObject[] clouds;
	float[] speeds = new float[3];

	void Start() {
		clouds = GameObject.FindGameObjectsWithTag("Cloud");
		for (int i = 0; i < clouds.Length; i++) {
			speeds[i] = Random.value;
		}
	}

	void Update() {
		for (int i = 0; i < clouds.Length; i++) {
			clouds[i].transform.Translate(Vector2.left * (speeds[i] / 3) * Time.deltaTime);
		}
	}
}