using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer = 1.5f;
	float transitionTimerTemp;
	// TODO: all transition types should be imported as const from a class (could maybe include start / end)
	public string transitionType = "box";
	public bool isStarting = true;

	void Start() {
		player = GameObject.Find("Player");
		animator = GetComponent<Animator>();
		animator.SetFloat("transitionSpeed", 1 / transitionTimer);
		transitionTimerTemp = transitionTimer;
		loadScene = true;
	}

	void Update() {
		// Make the transition gameobject follow the camera
		transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);

		if (loadScene) {
			LoadScene();
		}
	}

	void LoadScene() {
		PlayerNavigation();
		string transitionName = (isStarting ? "start" : "end") + transitionType;
		animator.SetTrigger(transitionName);
		transitionTimer -= Time.deltaTime;
		if (transitionTimer <= 0) {
			if (!isStarting) {
				SceneManager.LoadScene(sceneIndex);
			}
			transitionTimer = transitionTimerTemp;
			isStarting = false;
			loadScene = false;
		}
	}

	// TODO: Make a scene navigation script to handle advanced logic (multiple entry/exit points, player position, etc)
	void PlayerNavigation() {
		if (player.GetComponent<PlayerHealth>() && !player.GetComponent<PlayerHealth>().isDead) {
			int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
			int nextSceneIndex = player.transform.position.x < GameObject.Find("Environment").GetComponent<Collider2D>().bounds.min.x ?
				currentSceneIndex - 1 : currentSceneIndex + 1;
			if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
				sceneIndex = currentSceneIndex;
			} else {
				sceneIndex = nextSceneIndex;
			}
		}
	}
}