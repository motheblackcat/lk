using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	PlayerHealth playerHealth;
	GameObject playerState;
	PlayerStateSave playerStateSave;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer = 1.5f;
	float transitionTimerTemp;
	// TODO: all transition types should be imported as const from a class (could maybe include start / end)
	public string transitionType = "box";
	public bool isStarting = true;

	void Start() {
		player = GameObject.FindWithTag("Player");
		playerHealth = player ? player.GetComponent<PlayerHealth>() : null;
		playerState = GameObject.FindWithTag("PlayerState");
		playerStateSave = playerState ? playerState.GetComponent<PlayerStateSave>() : null;

		animator = GetComponent<Animator>();
		animator.SetFloat("transitionSpeed", 1 / transitionTimer);

		// Temp value to reset transitionTimer
		transitionTimerTemp = transitionTimer;

		loadScene = true;
	}

	void Update() {
		// Make the transition gameobject follow the camera
		transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);

		if (loadScene) {
			LoadScene(sceneIndex, isStarting);
		}
	}

	public void LoadScene(int sceneIndex, bool starting) {
		string transitionName = (starting ? "start" : "end") + transitionType;
		GetComponent<Animator>().SetTrigger(transitionName);

		transitionTimer -= Time.deltaTime;

		if (transitionTimer <= 0) {
			if (!starting) {
				// TODO: Check these conditions (should be it's own script?)
				if (playerState && player) {
					playerStateSave.playerHealth = playerHealth.playerHealth;
				}
				SceneManager.LoadScene(sceneIndex);
			}
			transitionTimer = transitionTimerTemp;
			isStarting = false;
			loadScene = false;
		}
	}
}