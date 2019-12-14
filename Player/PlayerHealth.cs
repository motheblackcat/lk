using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	Image healthBar;
	public float pushX = 10f;
	public float pushY = 10f;
	public float playerHealth = 100f;
	public bool isDead = false;
	public bool tookDamage = false;
	public bool isInv = false;
	public float restartLevelTimer = 2f;
	public float invicibilityTimer = 0.5f;
	public int currentSceneIndex = 0;
	float invTimerTemp = 0;

	void Start() {
		healthBar = GameObject.Find("Content") ? GameObject.Find("Content").GetComponent<Image>() : null;
		invTimerTemp = invicibilityTimer;
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		playerHealth = PlayerState.Instance.playerHealth;
	}

	void Update() {
		if (healthBar)healthBar.fillAmount = playerHealth / 100;
		if (isInv)StartInvTimer();
		Death();
	}

	void StartInvTimer() {
		tookDamage = false;
		invicibilityTimer -= Time.deltaTime;
		if (invicibilityTimer <= 0) {
			invicibilityTimer = invTimerTemp;
			isInv = false;
		}
	}

	// TODO: Refactor the preventing of pushback on death and its strength sould be taken from the enemy
	void TakeDamage(GameObject enemy) {
		playerHealth -= enemy.GetComponent<EnemyHealthControl>().damage;
		Death();
		if (!isDead) {
			bool enemyPos = enemy.transform.position.x > transform.position.x;
			Vector2 pushDirection = new Vector2(enemyPos ? -pushX : pushX, pushY);
			GetComponent<Rigidbody2D>().AddForce(pushDirection, ForceMode2D.Impulse);
			PlayerState.Instance.Save();
			isInv = true;
			tookDamage = true;
		}
	}

	void Death() {
		if (playerHealth <= 0) {
			isDead = true;
			PlayerState.Instance.playerHealth = 100;
			GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
			restartLevelTimer -= Time.deltaTime;
			if (restartLevelTimer <= 0) {
				// TODO: Cleaner refs
				GameObject.Find("SceneTransition").GetComponent<SceneLoader>().sceneIndex = currentSceneIndex;
				GameObject.Find("SceneTransition").GetComponent<SceneLoader>().StartLoadScene();
			}
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy" && !isInv) {
			TakeDamage(col.gameObject);
		}
	}
}