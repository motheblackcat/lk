using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionTypes { box, fade }
public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	public TransitionTypes transitionType = TransitionTypes.box;
	public bool loadScene = false;
	public bool sceneLoaded = false;
	public int sceneIndex = 0;
	public float transitionDuration = 1f;
	float transitionTimerTemp = 0;

	void Start() {
		player = GameObject.Find("Player");
		animator = GetComponent<Animator>();
		transitionTimerTemp = transitionDuration;
		loadScene = true;
		sceneLoaded = false;
	}

	void Update() {
		if (loadScene) {
			LoadScene();
		}
	}

	// TODO: Negative speed used with two clips instead of negative multiplier
	void LoadScene() {
		animator.Play(sceneLoaded ? "BoxTransitionR" : "BoxTransition");
		transitionDuration -= Time.deltaTime;
		if (transitionDuration <= 0) {
			loadScene = false;
			transitionDuration = transitionTimerTemp;
			if (sceneLoaded)SceneManager.LoadScene(sceneIndex);
			sceneLoaded = true;
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