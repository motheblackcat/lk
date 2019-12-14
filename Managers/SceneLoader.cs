using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionTypes { Box, Fade }
public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	public TransitionTypes transitionType;
	public int sceneIndex = 0;
	public bool loadScene = false;

	void Start() {
		player = GameObject.Find("Player");
		animator = GetComponent<Animator>();
		animator.Play(transitionType + "Transition");
	}

	public void StartLoadScene() {
		StartCoroutine(LoadScene());
	}

	public IEnumerator LoadScene() {
		SetNextSceneIndex();
		animator.SetFloat("direction", -1f);
		animator.Play(transitionType + "Transition", 0, 1);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneIndex);
	}

	// TODO: Make a scene navigation script to handle advanced logic (multiple entry/exit points, player position, etc)
	void SetNextSceneIndex() {
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