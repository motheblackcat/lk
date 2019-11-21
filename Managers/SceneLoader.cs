using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	GameObject playerUI;
	PlayerControl playerControl;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer = 1.5f;
	// TODO: all transition types should be imported as const from a class (could maybe include start / end)
	public string transitionType = "box";
	bool startScene;
	float startTimer;
	bool introDone;

	void Start() {
		// Make the scenetransition gameobject a child of the main camera and set its transform
		transform.parent = Camera.main.transform;
		transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);

		animator = GetComponent<Animator>();
		animator.SetFloat("transitionSpeed", 1 / transitionTimer);
		animator.SetTrigger("start" + transitionType);

		startTimer = transitionTimer;
		startScene = true;

		player = GameObject.Find("Player") ? GameObject.Find("Player") : null;
		playerControl = player ? player.GetComponent<PlayerControl>() : null;
		playerUI = player ? GameObject.Find("Player UI") : null;

		// TODO: Check that line, logic might be simpler
		introDone = Camera.main.GetComponent<IntroSceneManager>() ? Camera.main.GetComponent<IntroSceneManager>().introDone : true;

		// if (player && introDone) {
		// Shouldn't be necessary to set this every Start()
		// DontDestroyOnLoad(player);
		// Set the main camera to follow the player in general cases
		// GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
		// }

		// if (playerUI)DontDestroyOnLoad(playerUI);
	}

	void Update() {
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
		playerControl.canMove = false;
		transitionTimer -= Time.deltaTime;
		GetComponent<Animator>().SetTrigger("end" + transitionType);
		if (transitionTimer <= 0) {
			SceneManager.LoadScene(sceneId);
			loadScene = false;
		}
	}
}