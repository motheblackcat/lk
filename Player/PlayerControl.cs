using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject npc;
    GameObject ghost;
    GameObject playerUI;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    SceneLoader sceneLoader;
    IntroSceneManager introSceneManager;
    PauseMenuManager pauseMenuManager;
    PlayerHealth playerHealth;
    public float runSpeed = 40;
    public float jumpSpeed = 600;
    public bool canMove;
    public bool isGrounded;
    float colliderOffsetX;
    float colliderOffsetY;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        playerUI = GameObject.Find("PlayerUI");
        ghost = GameObject.Find("Ghost");
        introSceneManager = GameObject.Find("GameManager") ? GameObject.Find("GameManager").GetComponent<IntroSceneManager>() : null;
        sceneLoader = GameObject.Find("SceneTransition") ? GameObject.Find("SceneTransition").GetComponent<SceneLoader>() : null;
        pauseMenuManager = GameObject.Find("PlayerUI") ? GameObject.Find("PlayerUI").GetComponent<PauseMenuManager>() : null;

        colliderOffsetX = GetComponent<CapsuleCollider2D>().offset.x;
        colliderOffsetY = GetComponent<CapsuleCollider2D>().offset.y;
    }

    void Update() {
        bool introDone = introSceneManager ? introSceneManager.introDone : true;
        bool isLoading = sceneLoader ? sceneLoader.isLoading : false;
        bool inDialog = playerUI ? playerUI.GetComponent<DialogManager>().inDialog : false;
        bool tookDamage = playerHealth ? playerHealth.isInv : false;
        bool isDead = playerHealth ? playerHealth.isDead : false;
        bool isPaused = pauseMenuManager? pauseMenuManager.paused : false;

        canMove = introDone && !isPaused && !inDialog && !isLoading && !tookDamage && !isDead;

        // TODO: Moving to FixedUpdate() messes pushback
        if (canMove)PlayerMove();
        if (inDialog)GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void PlayerMove() {
        GetComponent<CapsuleCollider2D>().offset = new Vector2(sprite.flipX ? -colliderOffsetX : colliderOffsetX, colliderOffsetY);
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded)rb.velocity = Vector2.up * jumpSpeed;
        if (rb.velocity.x > 0)sprite.flipX = false;
        if (rb.velocity.x < 0)sprite.flipX = true;
        if (ghost)ghost.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "StaticNPC")npc = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other) {
        npc = null;
        if (other.name == "Environment")sceneLoader.StartLoadScene(false);
    }
}