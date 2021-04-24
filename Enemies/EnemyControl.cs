using UnityEngine;

public class EnemyControl : MonoBehaviour {
	[SerializeField] GameObject corpse;
	[SerializeField] AudioClip hit;
	[SerializeField] float health = 2;
	[SerializeField] float pushX = 10;
	[SerializeField] float pushY = 10;
	[SerializeField] float invCd = 0.5f;

	public float damages = 25;

	Animator animator;
	AudioSource audioSource;
	Rigidbody2D rb;

	public float invTimer = 0;

	void Start() {
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (health <= 0) {
			Instantiate(corpse, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		invTimer -= Time.deltaTime;
	}

	/**
	 *  Reduce health depending on player's damage, play animation & sound and push back enemy
	 **/
	public void TakeDamage(float damage) {
		if (invTimer <= 0) {
			health -= damage;
			invTimer = invCd;
			if (health > 0) {
				animator.SetTrigger("hurt");
				audioSource.PlayOneShot(hit);
				bool pos = GameObject.Find("Player").transform.position.x > rb.transform.position.x;
				rb.AddForce(new Vector2(pos ? -pushX : pushX, pushY), ForceMode2D.Impulse);
			}
		}
	}

	/**
	 *  Play attack animation when touching the player's collider
	 **/
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") animator.SetTrigger("atk");
	}
}