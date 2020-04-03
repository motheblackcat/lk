using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionTypes { Box, Fade }
public class SceneTransition : MonoBehaviour {
	public Transform backSpawnPoint;
	Animator animator;
	GameObject player;
	Bounds levelBounds;
	public TransitionTypes transitionType;
	public bool isLoading = false;
	public int sceneIndex = 0;
	public float spawnOffset = 5f;

	void Start() {
		player = GameObject.Find("Player");
		animator = GetComponent<Animator>();
		animator.Play(transitionType + "Transition");
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
		levelBounds = GameObject.Find("Environment").GetComponent<Collider2D>().bounds;
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

	/** TODO: Will be evolved with more advanced logic (multiple entry / exit points) */
	void SetPlayerStartPosition() {
		if (PlayerState.Instance.lastSceneIndex > sceneIndex) {
			Vector2 spawnPosition = backSpawnPoint ?
				new Vector2(backSpawnPoint.position.x, backSpawnPoint.position.y) : new Vector2(levelBounds.max.x - spawnOffset, player.transform.position.y);
			player.transform.position = spawnPosition;
			player.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	void SetNextSceneIndex() {
		bool goBack = player.transform.position.x < levelBounds.min.x;
		if (goBack) --sceneIndex;
		else if (sceneIndex + 1 < SceneManager.sceneCountInBuildSettings) ++sceneIndex;
	}
}