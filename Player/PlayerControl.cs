using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public GameObject npc;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    GameObject ghost;
    public float runSpeed = 40;
    public float jumpSpeed = 600;
    public bool canMove;
    public bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ghost = GameObject.Find("Ghost");
    }

    void Update() {
        // TOFIX: Dialog with Bob set canMove to false forever
        if (GameObject.Find("PlayerUI") && GameObject.Find("DialogBox").GetComponent<DialogManager>().inDialog) {
            canMove = false;
        }
        if (canMove) {
            PlayerMove();
            Flip();
        }
    }
    // Should move this to FixedUpdated()
    void PlayerMove() {
        if (Input.GetAxis("Horizontal") != 0) { rb.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rb.velocity.y); }
        if (Input.GetButtonDown("Jump") && isGrounded) { rb.velocity = Vector2.up * jumpSpeed; }
    }

    void Flip() {
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

    private void OnTriggerExit2D(Collider2D other) {
        npc = null;
        if (other.name == "Environment") {
            // TODO: Should make a safer reference or make the scene transition object stay at all time
            GameObject.Find("SceneTransition").GetComponent<SceneLoader>().loadScene = true;
        }
    }
}