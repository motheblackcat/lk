using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	PlayerState playerState;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer = 1.5f;
	float transitionTimerTemp;
	// TODO: all transition types should be imported as const from a class (could maybe include start / end)
	public string transitionType = "box";
	public bool isStarting = true;

	void Start() {
		player = GameObject.FindWithTag("Player");
		playerState = GameObject.Find("PlayerState") ? GameObject.Find("PlayerState").GetComponent<PlayerState>() : null;
		animator = GetComponent<Animator>();
		animator.SetFloat("transitionSpeed", 1 / transitionTimer);
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

	// TOFIX: Scene load before end of transition
	public void LoadScene(int sceneIndex, bool starting) {
		string transitionName = (starting ? "start" : "end") + transitionType;
		animator.SetTrigger(transitionName);

		// TODO: Simple horizontal scene navigation, add logic for vertical placed warps and player position (move to its own method)
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = player.transform.position.x < GameObject.Find("Environment").GetComponent<Collider2D>().bounds.min.x ?
			currentSceneIndex - 1 : currentSceneIndex + 1;
		if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
			sceneIndex = currentSceneIndex;
		} else {
			sceneIndex = nextSceneIndex;
		}

		transitionTimer -= Time.deltaTime;
		if (transitionTimer <= 0) {
			if (!starting) {
				playerState.Save();
				SceneManager.LoadScene(sceneIndex);
			}
			transitionTimer = transitionTimerTemp;
			isStarting = false;
			loadScene = false;
		}
	}
}