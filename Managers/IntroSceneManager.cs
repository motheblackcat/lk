using UnityEngine;

public class IntroSceneManager : MonoBehaviour {
	// TODO: This script could be included in the SceneLoader script
	GameObject player;
	public bool introDone = false;
	public float startTimer = 5.0f;

	void Start() {
		player = GameObject.FindWithTag("Player");
	}

	void Update() {
		startTimer -= Time.deltaTime;
		if (startTimer <= 0) {
			FreePlayer();
		}
	}

	void FreePlayer() {
		introDone = true;
		player.GetComponent<Animator>().enabled = true;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}