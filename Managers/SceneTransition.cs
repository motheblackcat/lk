using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionTypes { Box, Fade }

public class SceneTransition : MonoBehaviour {
	[SerializeField] TransitionTypes transitionType;
	[SerializeField] Transform spawnPoint;
	[SerializeField] int currentSceneIndex = 0;
	public int previousSceneIndex = 0;
	public int nextSceneIndex = 0;
	public bool isLoading = false;

	Animator animator;
	GameObject player;

	void Start() {
		player = GameObject.Find("Player");

		animator = GetComponent<Animator>();
		animator.Play(transitionType + "Transition");

		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		if (currentSceneIndex > 1) PlayerState.Instance.introDone = true;

		SetPlayerStartPosition();
	}

	void Update() {
		isLoading = animator.GetCurrentAnimatorStateInfo(0).IsName(transitionType + "Transition");
	}

	/* TODO: Find a way to remove coroutine or use animation duration for delay */
	public IEnumerator LoadScene(int nextSceneIndex) {
		if (!isLoading) {
			animator.SetFloat("direction", -1f);
			animator.Play(transitionType + "Transition", 0, 1);
			PlayerState.Instance.Save();
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene(nextSceneIndex != -1 ? nextSceneIndex : currentSceneIndex);
		}
	}

	/* TODO: Refactor with entry / exit points & player facing*/
	void SetPlayerStartPosition() {
		if (PlayerState.Instance.introDone && spawnPoint) {
			player.transform.position = spawnPoint.position;
			player.transform.eulerAngles = new Vector3(0, 180, 0);
		};
	}
}