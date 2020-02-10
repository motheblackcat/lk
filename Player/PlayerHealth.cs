using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	Slider healthBarSlider;
	public float pushX = 10f;
	public float pushY = 10f;
	public float playerMaxHealth = 100f;
	public float playerHealth = 100f;
	public bool isDead = false;
	public bool tookDamage = false;
	public bool isInv = false;
	public float restartLevelTimer = 2f;
	public float invicibilityTimer = 0.5f;
	float invTimerTemp = 0;

	void Start() {
		healthBarSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
		invTimerTemp = invicibilityTimer;
		// TODO: Should be moved to a init / loading game state script
		playerHealth = PlayerState.Instance ? PlayerState.Instance.playerHealth : 100;
		healthBarSlider.value = playerHealth;
	}

	void Update() {
		if (isInv) StartInvTimer();
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

	void TakeDamage(GameObject enemy) {
		playerHealth -= enemy.GetComponent<EnemyHealthControl>().damage;
		healthBarSlider.value = playerHealth;
		Death();
		if (!isDead) {
			PushBack(enemy);
			PlayerState.Instance.SaveState();
			isInv = true;
			tookDamage = true;
		}
	}

	// TODO: Pushback strength taken from the enemy?
	void PushBack(GameObject enemy) {
		bool enemyPos = enemy.transform.position.x > transform.position.x;
		Vector2 pushDirection = new Vector2(enemyPos ? -pushX : pushX, pushY);
		GetComponent<Rigidbody2D>().AddForce(pushDirection, ForceMode2D.Impulse);
	}

	void Death() {
		if (playerHealth <= 0) {
			isDead = true;
			PlayerState.Instance.playerHealth = 100;
			GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
			restartLevelTimer -= Time.deltaTime;
			if (restartLevelTimer <= 0) GameObject.Find("SceneTransition").GetComponent<SceneTransition>().StartLoadScene(true);
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy" && !isInv) TakeDamage(col.gameObject);
	}
}