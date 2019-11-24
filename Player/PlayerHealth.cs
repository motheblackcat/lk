using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	Image healthBar;
	SpriteRenderer sprite;
	SpriteRenderer wSprite;
	GameObject weapon;
	public float pushX = 10f;
	public float pushY = 10f;
	public float playerHealth = 100f;
	public bool isDead = false;
	public bool tookDamage = false;
	public bool isInv = false;
	public float restartLevelTimer = 2f;
	public float flickTimer = 0.2f;
	public float invicibilityTimer = 1f;
	public int currentSceneIndex;
	float invTimerTemp;

	private void Awake() {
		// TODO: Simple logic to keep the player health between scenes, will be expanded for weapons & items
		if (!GameObject.Find("PlayerState")) {
			GameObject playerState = Instantiate(Resources.Load("States/PlayerState")as GameObject);
			playerState.name = playerState.name.Replace("(Clone)", "");
			GameObject.Find("PlayerState").GetComponent<PlayerStateSave>().playerHealth = playerHealth;
			DontDestroyOnLoad(playerState);
		} else {
			playerHealth = GameObject.Find("PlayerState").GetComponent<PlayerStateSave>().playerHealth;
		}
	}

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
		weapon = GameObject.FindGameObjectsWithTag("Weapon")[0];
		healthBar = GameObject.Find("Content") ? GameObject.Find("Content").GetComponent<Image>() : null;
		invTimerTemp = invicibilityTimer;
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	void Update() {
		if (weapon) { wSprite = weapon.GetComponent<SpriteRenderer>(); }
		if (healthBar) { healthBar.fillAmount = playerHealth / 100; }
		InvincibilityTimerStart();
		Death();
	}

	void InvincibilityTimerStart() {
		if (tookDamage) {
			isInv = true;
			tookDamage = false;
		}
		if (isInv) {
			invicibilityTimer -= Time.deltaTime;
			if (invicibilityTimer <= 0) {
				sprite.enabled = true;
				if (weapon) { wSprite.enabled = true; }
				isInv = false;
				invicibilityTimer = invTimerTemp;
				CancelInvoke();
			}
		}
	}

	//TODO: The sprite flicking should just be an animation
	void SpriteFlick() {
		sprite.enabled = !sprite.enabled;
		if (weapon) { wSprite.enabled = !wSprite.enabled; }
	}

	//TODO: Pushback strength sould be taken from the enemy
	//TOFIX: Pushback is in the incorrect direction?
	void PushBack(GameObject enemy) {
		bool enemyPos = enemy.transform.position.x > transform.position.x;
		Vector2 pushDirection = enemyPos ? new Vector2(-pushX, pushY) : new Vector2(pushX, pushY);
		GetComponent<Rigidbody2D>().AddForce(pushDirection, ForceMode2D.Impulse);
	}

	void Death() {
		if (playerHealth <= 0) {
			isDead = true;
			GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
			restartLevelTimer -= Time.deltaTime;
			if (restartLevelTimer <= 0) {
				// TODO: Death reload current scene from start or a checkpoint, need to make logic for special cases
				GameObject.Find("SceneTransition").GetComponent<SceneLoader>().LoadScene(currentSceneIndex, false);
				// TODO: This should be handled in the SceneLoader or it's own script
				GameObject.Find("PlayerState").GetComponent<PlayerStateSave>().playerHealth = 100;
			}
		}
	}

	void TakeDamage(GameObject enemy) {
		playerHealth -= enemy.GetComponent<EnemyHealthControl>().damage;
		Death();
		if (!isDead) {
			tookDamage = true;
			InvokeRepeating("SpriteFlick", 0, flickTimer);
			PushBack(enemy);
		};
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			if (!tookDamage && !isInv && !isDead) {
				TakeDamage(col.gameObject);
			}
		}
	}
}