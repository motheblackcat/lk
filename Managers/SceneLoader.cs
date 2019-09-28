using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	public bool loadScene;
	public int sceneIndex;
	public float transitionTimer = 1.5f;
	public string transitionType = "box";
	bool startScene;
	float startTimer;

	void Start() {
		animator = GameObject.Find("Transition") ? GameObject.Find("Transition").GetComponent<Animator>() : null;
		if (animator) {
			animator.SetFloat("transitionSpeed", 1 / transitionTimer);
			animator.SetTrigger("start" + transitionType);
		}
		playerControl = GameObject.Find("Player") ? GameObject.Find("Player").GetComponent<PlayerControl>() : null;
		startTimer = transitionTimer;
		startScene = true;
	}

	void Update() {
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

	void LoadScene(int sceneId) {
		playerControl.canMove = false;
		transitionTimer -= Time.deltaTime;
		GameObject.Find("Transition").GetComponent<Animator>().SetTrigger("end" + transitionType);
		if (transitionTimer <= 0) {
			SceneManager.LoadScene(sceneId);
			loadScene = false;
		}
	}
}