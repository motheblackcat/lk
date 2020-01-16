using UnityEngine;

public class IntroSceneManager : MonoBehaviour {
	PlayerState playerState;
	GameObject player;
	public float startTimer = 5.0f;

	void Start() {
		playerState = PlayerState.Instance;
		player = GameObject.Find("Player");

		if (playerState.introDone) {
			FreePlayer();
		}
	}

	void Update() {
		startTimer -= Time.deltaTime;
		if (startTimer <= 0) {
			FreePlayer();
		}
	}

	void FreePlayer() {
		playerState.introDone = true;
		player.GetComponent<Animator>().enabled = true;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}