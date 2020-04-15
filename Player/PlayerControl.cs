using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject npc;
    GameObject ghost;
    GameObject playerUI;
    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    SceneTransition SceneTransition;
    PauseMenuManager pauseMenuManager;
    PlayerHealth playerHealth;
    PlayerSound playerSound;
    public float runSpeed = 40;
    public float jumpSpeed = 600;
    public bool canMove;
    public bool isGrounded;
    float colliderOffsetX;
    float colliderOffsetY;

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

        canMove = !isPaused && !inDialog && !isLoading && !tookDamage && !isDead;

        /** TODO: Moving to FixedUpdate() messes pushback */
        if (canMove) PlayerMove();
        if (inDialog || isLoading) GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        animator.SetBool("run", GetComponent<Rigidbody2D>().velocity.x != 0);
        animator.SetBool("air", !isGrounded);
    }

    void PlayerMove() {
        GetComponent<CapsuleCollider2D>().offset = new Vector2(sprite.flipX ? -colliderOffsetX : colliderOffsetX, colliderOffsetY);
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded && playerSound) {
            audioSource.PlayOneShot(playerSound.jumpSound);
            rb.velocity = Vector2.up * jumpSpeed;
        }
        if (rb.velocity.x > 0) sprite.flipX = false;
        if (rb.velocity.x < 0) sprite.flipX = true;
        if (ghost) ghost.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "StaticNPC") npc = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other) {
        npc = null;
        /** TODO: Use a more safe method to target the PolygonCollider2D component that acts as level boundaries */
        if (other.name == "Grid") StartCoroutine(SceneTransition.LoadScene(false));
    }
}