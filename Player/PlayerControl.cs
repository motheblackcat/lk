using UnityEngine;

public class PlayerControl : MonoBehaviour {
	[SerializeField] float runSpeed = 18;
	[SerializeField] float jumpSpeed = 18;

	public GameObject npc;
	public bool canMove = false;
	public bool isGrounded = false;

	GameObject ghost;
	GameObject playerUI;
	Animator animator;
	AudioSource audioSource;
	Rigidbody2D rb;
	SpriteRenderer sprite;
	PlayerInputActions playerInputs;
	SceneTransition SceneTransition;
	PauseMenuManager pauseMenuManager;
	PlayerHealth playerHealth;
	PlayerSound playerSound;

	bool jump = false;
	float direction = 0;
	float colliderOffsetX = 0;
	float colliderOffsetY = 0;

	void Awake() {
		playerInputs = new PlayerInputActions();
		playerInputs.Player.Move.performed += ctx => direction = ctx.ReadValue<float>();
		playerInputs.Player.Jump.performed += ctx => jump = true;
		playerInputs.Player.Jump.canceled += ctx => jump = false;
	}

	void Start() {
		playerUI = GameObject.Find("PlayerUI");
		ghost = GameObject.Find("Ghost");
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		playerHealth = GetComponent<PlayerHealth>();
		playerSound = GetComponent<PlayerSound>();
		colliderOffsetX = GetComponent<CapsuleCollider2D>().offset.x;
		colliderOffsetY = GetComponent<CapsuleCollider2D>().offset.y;
		SceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
		pauseMenuManager = GameObject.Find("PlayerUI").GetComponent<PauseMenuManager>();
	}

	void Update() {
		bool isLoading = SceneTransition ? SceneTransition.isLoading : false;
		bool inDialog = playerUI ? playerUI.GetComponent<DialogManager>().inDialog : false;
		bool tookDamage = playerHealth ? playerHealth.isInv : false;
		bool isDead = playerHealth ? playerHealth.isDead : false;
		bool isPaused = pauseMenuManager? pauseMenuManager.paused : false;
		bool introDone = PlayerState.Instance.introDone;

		canMove = !isPaused && !inDialog && !isLoading && !tookDamage && !isDead && introDone;

		if (inDialog || isLoading) GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		animator.SetBool("run", GetComponent<Rigidbody2D>().velocity.x != 0);
		animator.SetBool("air", !isGrounded);

		GetComponent<CapsuleCollider2D>().offset = new Vector2(sprite.flipX ? -colliderOffsetX : colliderOffsetX, colliderOffsetY);
		if (ghost) ghost.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
	}

	void FixedUpdate() {
		if (canMove) {
			rb.velocity = new Vector2(direction * runSpeed, rb.velocity.y);
			if (rb.velocity.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
			if (rb.velocity.x < 0) transform.eulerAngles = new Vector3(0, 180, 0);
			if (jump && isGrounded && playerSound && npc == null) {
				rb.velocity = (Vector2.up * jumpSpeed);
				audioSource.PlayOneShot(playerSound.jumpSound);
				jump = false;
			}
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") isGrounded = true;
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") isGrounded = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "NPC" || other.gameObject.tag == "StaticNPC") npc = other.gameObject;
	}

	void OnTriggerExit2D(Collider2D other) {
		npc = null;
		if (other.tag == "Level") StartCoroutine(SceneTransition.LoadScene(false));
	}

	void OnEnable() {
		playerInputs.Enable();
	}

	void OnDisable() {
		playerInputs.Disable();
	}
}