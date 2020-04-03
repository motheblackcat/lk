using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	Slider healthBarSlider;
	Animator animator;
	AudioSource audioSource;
	PlayerSound playerSound;
	public float pushX = 10f;
	public float pushY = 10f;
	public float playerMaxHealth;
	public float playerHealth;
	public bool isDead = false;
	public bool isInv = false;
	public float restartLevelTimer = 2f;
	public float invicibilityTimer = 0.5f;
	float invTimerTemp = 0;

	void Start() {
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		playerSound = GetComponent<PlayerSound>();
		healthBarSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
		invTimerTemp = invicibilityTimer;
		playerHealth = PlayerState.Instance.playerCurrentHealth;
		playerMaxHealth = PlayerState.Instance.playerMaxHealth;
		healthBarSlider.value = playerHealth;
		healthBarSlider.maxValue = playerMaxHealth;
	}

	void Update() {
		if (isInv) StartInvTimer();
		animator.SetBool("hurt", isInv);
		if (isDead) {
			restartLevelTimer -= Time.deltaTime;
			if (restartLevelTimer <= 0) GameObject.Find("SceneTransition").GetComponent<SceneTransition>().StartLoadScene(true);
		}
	}

	void StartInvTimer() {
		invicibilityTimer -= Time.deltaTime;
		if (invicibilityTimer <= 0) {
			invicibilityTimer = invTimerTemp;
			isInv = false;
		}
	}

	void TakeDamage(GameObject enemy) {
		playerHealth -= enemy.GetComponent<EnemyControl>().damages;
		healthBarSlider.value = playerHealth;
		if (playerHealth <= 0) {
			isDead = true;
			animator.SetTrigger("die");
			audioSource.PlayOneShot(playerSound.deathSound);
			GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
			PlayerState.Instance.Reset();
		} else {
			audioSource.PlayOneShot(playerSound.hurtSound);
			PushBack(enemy);
			PlayerState.Instance.SaveState();
			isInv = true;
		}
	}

	/** TODO: Pushback strength taken from the enemy (currently taken from the inspector) */
	void PushBack(GameObject enemy) {
		bool enemyPos = enemy.transform.position.x > transform.position.x;
		Vector2 pushDirection = new Vector2(enemyPos ? -pushX : pushX, pushY);
		GetComponent<Rigidbody2D>().AddForce(pushDirection, ForceMode2D.Impulse);
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy" && !isInv && !isDead) TakeDamage(col.gameObject);
	}
}