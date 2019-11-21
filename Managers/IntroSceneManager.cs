using UnityEngine;

public class IntroSceneManager : MonoBehaviour {
	GameObject player;
	SceneLoader sceneLoader;
	public bool introDone = false;
	public float startTimer = 5.0f;

	void Start() {
		player = GameObject.Find("Player");
		// TODO: Get a safer reference
		sceneLoader = GameObject.Find("SceneTransition").GetComponent<SceneLoader>();
	}

	void Update() {
		startTimer -= Time.deltaTime;
		if (startTimer <= 0 && !sceneLoader.loadScene) {
			FreePlayer();
		}
	}

	void FreePlayer() {
		introDone = true;
		player.GetComponent<PlayerControl>().canMove = true;
		player.GetComponent<Animator>().enabled = true;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}