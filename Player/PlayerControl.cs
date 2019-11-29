using UnityEngine;

public class PlayerControl : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sprite;
    GameObject ghost;
    PlayerHealth playerHealth;
    SceneLoader sceneLoader;
    IntroSceneManager introSceneManager;
    public GameObject npc;
    public float runSpeed = 40;
    public float jumpSpeed = 600;
    public bool canMove;
    public bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ghost = GameObject.Find("Ghost");
        playerHealth = GetComponent<PlayerHealth>();
        introSceneManager = GameObject.Find("SceneTransition").GetComponent<IntroSceneManager>();
        sceneLoader = GameObject.Find("SceneTransition").GetComponent<SceneLoader>();
    }

    void Update() {
        bool introDone = introSceneManager ? introSceneManager.introDone : true;
        bool loadScene = sceneLoader.loadScene;
        // TODO: Check if the dialogBox ref can be simplified overall
        bool inDialog = GameObject.FindWithTag("DialogBox") ? GameObject.FindWithTag("DialogBox").GetComponent<DialogManager>().inDialog : false;
        bool tookDamage = playerHealth ? playerHealth.isInv : false;
        bool isDead = playerHealth ? playerHealth.isDead : false;

        canMove = introDone && !inDialog && !loadScene && !tookDamage && !isDead;

        // TODO: Move to FixedUpdate()
        if (canMove) {
            PlayerMove();
        }
    }

    void PlayerMove() {
        if (Input.GetAxis("Horizontal") != 0) { rb.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rb.velocity.y); }
        if (Input.GetButtonDown("Jump") && isGrounded) { rb.velocity = Vector2.up * jumpSpeed; }
        if (Input.GetAxis("Horizontal") > 0) { sprite.flipX = false; }
        if (Input.GetAxis("Horizontal") < 0) { sprite.flipX = true; }
        GetComponent<CapsuleCollider2D>().offset = sprite.flipX ? new Vector2(-0.06f, -0.04f) : new Vector2(0.06f, -0.04f);
        if (ghost) { ghost.GetComponent<SpriteRenderer>().flipX = sprite.flipX; }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "StaticNPC") {
            npc = other.gameObject;
        }
    }

    // TODO: Add logic to switch scenes on directions and special cases
    private void OnTriggerExit2D(Collider2D other) {
        npc = null;
        if (other.name == "Environment") {
            sceneLoader.loadScene = true;
        }
    }
}