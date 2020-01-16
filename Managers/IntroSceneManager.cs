using UnityEngine;

public class IntroSceneManager : MonoBehaviour {
	GameObject player;
	public float startTimer = 5.0f;

	void Start() {
		player = GameObject.Find("Player");
	}

	void Update() {
		startTimer -= Time.deltaTime;
		if (startTimer <= 0) {
			FreePlayer();
		} else {
			GameObject.Find("Bartender").GetComponent<Animator>().SetBool("watch", false);
		}
	}

	void FreePlayer() {
		player.GetComponent<Animator>().enabled = true;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}