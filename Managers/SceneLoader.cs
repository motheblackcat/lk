using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionTypes { Box, Fade }
public class SceneLoader : MonoBehaviour {
	Animator animator;
	GameObject player;
	public TransitionTypes transitionType;
	public bool isLoading = false;
	public int sceneIndex = 0;

	void Start() {
		player = GameObject.Find("Player");
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
		animator = GetComponent<Animator>();
		animator.Play(transitionType + "Transition");
	}

	void Update() {
		isLoading = animator.GetCurrentAnimatorStateInfo(0).IsName(transitionType + "Transition");
	}

	public void StartLoadScene(bool reload) {
		if (!reload)SetNextSceneIndex();
		StartCoroutine(LoadScene(reload));
	}

	public IEnumerator LoadScene(bool reload) {
		animator.SetFloat("direction", -1f);
		animator.Play(transitionType + "Transition", 0, 1);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneIndex);
	}

	// TODO: Make a scene navigation script to handle advanced logic (multiple entry/exit points, player position, etc)
	void SetNextSceneIndex() {
		bool goBack = player.transform.position.x < GameObject.Find("Environment").GetComponent<Collider2D>().bounds.min.x;
		sceneIndex = goBack ? --sceneIndex : ++sceneIndex;
		sceneIndex = sceneIndex >= SceneManager.sceneCountInBuildSettings ? --sceneIndex : sceneIndex;
	}
}