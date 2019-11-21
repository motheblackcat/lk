using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer = 1.5f;
	// TODO: all transition types should be imported as const from a class (could maybe include start / end)
	public string transitionType = "box";
	bool startScene;
	float startTimer;

	void Start() {
		// Makes the SceneTransition GameObject a child of the Main Camera and set its transform so the black screen stays centered
		transform.parent = Camera.main.transform;
		transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);

		// Start the transition animation
		animator = GetComponent<Animator>();
		animator.SetFloat("transitionSpeed", 1 / transitionTimer);
		animator.SetTrigger("start" + transitionType);

		startTimer = transitionTimer;
		startScene = true;

		player = GameObject.Find("Player") ? GameObject.Find("Player") : null;
		playerControl = player ? player.GetComponent<PlayerControl>() : null;
		playerHealth = player ? player.GetComponent<PlayerHealth>() : null;
	}

	void Update() {
		// TODO: Check that line, logic might be simpler
		bool introDone = Camera.main.GetComponent<IntroSceneManager>() ? Camera.main.GetComponent<IntroSceneManager>().introDone : true;

		if (startScene) {
			startTimer -= Time.deltaTime;
			if (startTimer >= 0 && playerControl) {
				playerControl.canMove = false;
			} else {
				if (introDone && playerControl) {
					playerControl.canMove = true;
				}
				startTimer = transitionTimer;
				startScene = false;
			}
		}
		if (loadScene) {
			LoadScene(sceneIndex);
		}
	}

	public void LoadScene(int sceneId) {
		// Setting PlayerState life to the current player life before loading a new scene
		GameObject.Find("PlayerState").GetComponent<PlayerStateSave>().playerHealth = playerHealth.playerHealth;
		playerControl.canMove = false;
		transitionTimer -= Time.deltaTime;
		GetComponent<Animator>().SetTrigger("end" + transitionType);
		if (transitionTimer <= 0) {
			SceneManager.LoadScene(sceneId);
			loadScene = false;
		}
	}
}