using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionTypes { Box, Fade }
public class SceneTransition : MonoBehaviour {
	Animator animator;
	GameObject player;
	public TransitionTypes transitionType;
	public bool isLoading = false;
	public int sceneIndex = 0;

	void Start() {
		player = GameObject.Find("Player");
		animator = GetComponent<Animator>();
		animator.Play(transitionType + "Transition");
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
		Debug.Log(SceneManager.GetActiveScene().buildIndex);
		Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene is '" + scene.name + "'.");
		SetPlayerStartPosition();
	}

	void Update() {
		isLoading = animator.GetCurrentAnimatorStateInfo(0).IsName(transitionType + "Transition");
	}

	public void StartLoadScene(bool reload) {
		PlayerState.Instance.lastSceneIndex = sceneIndex;
		if (!reload) SetNextSceneIndex();
		StartCoroutine(LoadScene(reload));
	}

	public IEnumerator LoadScene(bool reload) {
		animator.SetFloat("direction", -1f);
		animator.Play(transitionType + "Transition", 0, 1);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneIndex);
	}

	// TODO: Handle advanced logic (multiple entry/exit points && player position)
	void SetPlayerStartPosition() {
		GameObject backPoint = GameObject.Find("BackPointStart");
		if (PlayerState.Instance.lastSceneIndex > sceneIndex && backPoint) {
			player.transform.position = backPoint.transform.position;
			player.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	void SetNextSceneIndex() {
		bool goBack = player.transform.position.x < GameObject.Find("Environment").GetComponent<Collider2D>().bounds.min.x;
		if (goBack) --sceneIndex;
		else if (sceneIndex + 1 < SceneManager.sceneCountInBuildSettings) ++sceneIndex;
	}
}