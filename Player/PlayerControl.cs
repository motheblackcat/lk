﻿using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject npc;
    GameObject ghost;
    GameObject dialogBox;
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

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        dialogBox = GameObject.Find("DialogBox");
        ghost = GameObject.Find("Ghost");
        introSceneManager = FindObjectsOfType<IntroSceneManager>().Length > 0 ? FindObjectsOfType<IntroSceneManager>()[0] : null;
        sceneLoader = GameObject.Find("SceneTransition").GetComponent<SceneLoader>();
        pauseMenuManager = GameObject.Find("PauseUI").GetComponent<PauseMenuManager>();
    }

    void Update() {
        bool introDone = introSceneManager ? introSceneManager.introDone : true;
        bool loadScene = sceneLoader.loadScene;
        bool inDialog = dialogBox.GetComponent<DialogManager>().inDialog;
        bool tookDamage = playerHealth ? playerHealth.isInv : false;
        bool isDead = playerHealth ? playerHealth.isDead : false;
        bool isPaussed = pauseMenuManager.paused;

        canMove = introDone && !isPaussed && !inDialog && !loadScene && !tookDamage && !isDead;

        // TODO: Move to FixedUpdate()
        if (canMove)PlayerMove();
        if (inDialog)GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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

    private void OnTriggerExit2D(Collider2D other) {
        npc = null;
        if (other.name == "Environment") {
            sceneLoader.loadScene = true;
        }
    }
}