using UnityEngine;

public class IntroSceneManager : MonoBehaviour {
	GameObject player;
	public float startTimer = 5.0f;

	void Start() {
		player = GameObject.Find("Player");
		if (PlayerState.Instance.lastSceneIndex != 0) PlayerState.Instance.introDone = true;
	}

	void Update() {
		if (PlayerState.Instance.introDone) FreePlayer();
		else {
			startTimer -= Time.deltaTime;
			if (startTimer <= 0) {
				PlayerState.Instance.introDone = true;
				FreePlayer();
			} else {
				player.GetComponent<SpriteRenderer>().flipX = false;
				GameObject.Find("Arth").GetComponent<Animator>().SetBool("watch", false);
			}
		}
	}

	void FreePlayer() {
		player.GetComponent<Animator>().enabled = true;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}